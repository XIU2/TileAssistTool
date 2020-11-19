using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace 磁贴辅助小工具
{
    public partial class Form : System.Windows.Forms.Form
    {
        readonly string[] args; // 启动参数（右键菜单启动）
        readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\";
        public Form(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (args.Length > 0 && args[0] != "")
            {
                File_Judgment(args); // 判断文件
            }
            else
            {
                Read_Config(); // 读入配置文件
            }
            this.Close(); // 关闭程序
        }

        /// <summary>
        /// 文件判断
        /// </summary>
        /// <param name="args">拖入的文件数组</param>
        private void File_Judgment(string[] args)
        {
            string Temp_args_2;
            string Temp_command;
            foreach (string Temp_args in args)
            {
                Temp_command = ""; 
                Temp_args_2 = Temp_args;
                //Debug.Print(Temp_args_2);
                string Extension = Path.GetExtension(Temp_args_2).ToLower(); // 获取文件后缀
                //Debug.Print(Extension);
                if (Extension == ".lnk") // 如果是快捷方式，就判断一下
                {
                    string[] Shortcut_Info = File_cs.Shortcut.Get_Shortcut_Info(Temp_args_2); // 获取快捷方式目标路径
                    //Debug.Print(TargetPath);
                    if (Shortcut_Info[0] == "") // 如果快捷方式目标路径为空，代表快捷方式为 UWP快捷方式
                    {
                        Write_Config("lnk.uwp", Path.GetFileName(Temp_args_2), Path.GetFileNameWithoutExtension(Temp_args_2), "", Temp_args_2);
                        continue;
                    }
                    if (!File.Exists(Shortcut_Info[0])) // 如果不是 UWP快捷方式，就判断快捷方式目标路径是否存在
                    {
                        if (Directory.Exists(Shortcut_Info[0]))
                        {
                            Write_Config("directory", Temp_args_2, Path.GetFileNameWithoutExtension(Temp_args_2));
                        }
                        continue; // 如果不存在，就跳出当前循环
                    }
                    Extension = Path.GetExtension(Shortcut_Info[0]).ToLower(); // 如果存在，获取目标路径文件后缀继续下面的判断
                    Temp_args_2 = Shortcut_Info[0]; // 将路径改为快捷方式目标路径
                    Temp_command = Shortcut_Info[1];
                }
                switch (Extension)
                {
                    case ".exe":
                    case ".bat":
                        if (File.Exists(Temp_args_2))
                        {
                            Write_Config("exe", Temp_args_2, Path.GetFileNameWithoutExtension(Temp_args_2), Temp_command);
                        }
                        break;
                    case ".url":
                        string URL = IniHelper_cs.IniHelper.GetValue("InternetShortcut", "URL", "", Temp_args_2);
                        if (URL != "")
                        {
                            Write_Config("url", URL, Path.GetFileNameWithoutExtension(Temp_args_2), Temp_command);
                        }
                        break;
                    default:
                        if (File.Exists(Temp_args_2))
                        {
                            Write_Config("file", Temp_args_2, Path.GetFileNameWithoutExtension(Temp_args_2), Temp_command);
                        }
                        else if (Directory.Exists(Temp_args_2))
                        {
                            Write_Config("directory", Temp_args_2, Path.GetFileNameWithoutExtension(Temp_args_2), Temp_command);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 写出配置文件
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="Path">路径</param>
        /// <param name="FileName">文件名（不带后缀）</param>
        /// <param name="Command">命令行，可空</param>
        /// <param name="UWPLnkPath">UWP快捷方式路径，UWP快捷方式专用，可空</param>
        private void Write_Config(string Type, string Path, string FileName, string Command = "", string UWPLnkPath = "")
        {
            string TempFileName = FileName;
            // 判断桌面是否已存在同名文件
            if (File.Exists(DesktopPath + FileName + ".exe") || File.Exists(DesktopPath + FileName + ".ini"))
            {
                // 如果存在同名文件，则随机文件名
                FileName += "_" + GetRandom();
            }
            if (Type == "lnk.uwp") // 判断类型是不是 UWP 快捷方式
            {
                //Debug.Print(UWPLnkPath + " " + DesktopPath + TempFileName + ".lnk");
                if (UWPLnkPath == DesktopPath + TempFileName + ".lnk") // 判断是不是拖入 磁贴辅助小工具 的快捷方式
                {
                    if (FileName != TempFileName) // 如果有同名的 .exe .ini 文件，才需要重命名该 UWP快捷方式
                    {
                        File.Move(UWPLnkPath, DesktopPath + FileName + ".lnk"); // 如果是一个文件，则重命名
                    }
                }
                else
                {
                    File.Copy(UWPLnkPath, DesktopPath + FileName + ".lnk"); // 如果不是，则复制一份到桌面
                }
                _ = MessageBox.Show("因为 UWP 等特殊快捷方式无法获取目标路径，所以必须保留该快捷方式文件" + Environment.NewLine + "快捷方式文件可以移动，但必须和对应的 [.exe .ini] 文件放在一起！", "注意：", MessageBoxButtons.OK);
            }
            // 移动自身到桌面并重命名
            File.Copy(Application.ExecutablePath, DesktopPath + FileName + ".exe");
            // 写出配置文件
            IniHelper_cs.IniHelper.SetValue("Config", "Type", Type, DesktopPath + FileName + ".ini");
            IniHelper_cs.IniHelper.SetValue("Config", "Path", Path, DesktopPath + FileName + ".ini");
            IniHelper_cs.IniHelper.SetValue("Config", "Command", Command, DesktopPath + FileName + ".ini");
        }

        /// <summary>
        /// 读入配置文件
        /// </summary>
        private void Read_Config()
        {
            string ConfigPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ini";
            if (File.Exists(ConfigPath))
            {
                Run(IniHelper_cs.IniHelper.GetValue("Config", "Type", "", ConfigPath), IniHelper_cs.IniHelper.GetValue("Config", "Path", "", ConfigPath), IniHelper_cs.IniHelper.GetValue("Config", "Command", "", ConfigPath));
            }
            else
            {
                _ = MessageBox.Show("找不到对应的配置文件： " + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ini ！" + Environment.NewLine + Environment.NewLine + "如果你是首次使用「磁贴辅助小工具」，可以简单了解下使用方法：" + Environment.NewLine + "· 拖拽单个或多个 [任意文件/文件夹] 到 [磁贴辅助小工具.exe] 上面，软件会自动生成 [启动程序(.exe)]、[配置文件(.ini)] 文件到桌面，双击 xxx.exe 文件即可打开对应的文件/文件夹。" + Environment.NewLine + "支持类型：" + Environment.NewLine + "· 应用程序/文件（.exe、.bat 等任意可以双击打开的文件）；" + Environment.NewLine + "· 快捷方式（UWP快捷方式 [从开始菜单拖拽 UWP应用 到桌面来生成快捷方式【只有UWP快捷方式才需要保留，其他快捷方式都可以删除】] 、URL快捷方式 [Steam、Epic等游戏平台创建的游戏快捷方式就是URL快捷方式]）；" + Environment.NewLine + "· 文件夹（拖拽文件夹只会把该文件夹生成启动器和配置文件，并不会遍历文件夹内的文件）。" + Environment.NewLine + Environment.NewLine + "如果需要更详细的使用说明或 GIF 动图演示，请打开 [使用说明+更新日志.txt] 文件找到使用说明网页地址。", "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        private void Run(string Type, string Path, string Command = "")
        {
            if (Type != "" && Path != "")
            {
                if (Type == "lnk.uwp")
                {
                    Path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + Path;
                }
                if (Type != "url")
                {
                    if (!File.Exists(Path))
                    {
                        if (!Directory.Exists(Path))
                        {
                            _ = MessageBox.Show("配置文件内容错误，请检查！ ", "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }
                    }
                }
                switch (Type)
                {
                    case "exe":
                        ProcessStartInfo exeRun = new ProcessStartInfo();
                        exeRun.FileName = Path;
                        exeRun.Arguments = Command;
                        if (System.IO.Path.GetFileNameWithoutExtension(Path) == "schtasks")
                        {
                            exeRun.CreateNoWindow = true;
                            exeRun.UseShellExecute = false;
                        }
                        _ = Process.Start(exeRun);
                        break;
                    case "lnk.uwp":
                    case "url":
                    case "directory":
                        _ = Process.Start(Path);
                        break;
                    default:
                        _ = Process.Start("explorer", Path);
                        break;
                }
            }
            else
            {
                _ = MessageBox.Show("配置文件内容错误，请检查！ ", "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 取随机数
        /// </summary>
        /// <returns>返回随机数字 0-999</returns>
        private string GetRandom()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(0, 999).ToString();
        }
    }
}
