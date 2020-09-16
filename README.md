# XIU2/TileAssistTool

[![Release Version](https://img.shields.io/github/v/release/XIU2/TileAssistTool.svg?style=flat-square&label=Release&color=1784ff)](https://github.com/XIU2/TileAssistTool/releases/latest)
[![GitHub license](https://img.shields.io/github/license/XIU2/TileAssistTool.svg?style=flat-square&label=License&color=3cb371)](https://github.com/XIU2/TileAssistTool/blob/master/LICENSE)
[![GitHub Star](https://img.shields.io/github/stars/XIU2/TileAssistTool.svg?style=flat-square&label=Star&color=3cb371)](https://github.com/XIU2/TileAssistTool/stargazers)
[![GitHub Fork](https://img.shields.io/github/forks/XIU2/TileAssistTool.svg?style=flat-square&label=Fork&color=3cb371)](https://github.com/XIU2/TileAssistTool/network/members)

**Windows10 磁贴辅助小工具**  

用于固定包括但不限于UWP、Steam等平台游戏、任意文件、文件夹等为磁贴！  
请搭配我的另一个项目使用：https://github.com/XIU2/TileTool

****

## 下载地址

* 蓝奏云 ：[https://www.lanzoux.com/b0sp46eh](https://www.lanzoux.com/b0sp46eh)
* Github：[https://github.com/XIU2/TileAssistTool/releases](https://github.com/XIU2/TileAssistTool/releases)

****

## 使用说明

### 游戏平台(Steam等)的游戏 如何固定为磁贴？

![](https://raw.githubusercontent.com/XIU2/TileAssistTool/master/img/01.gif)  

1. 在 Steam 等游戏平台，找到 **[创建游戏快捷方式]** 之类的选项；
2. 把 **快捷方式.url** 拖拽到 **[磁贴辅助小工具.exe]** 文件上，软件会生成相应的配置文件；
3. 双击 **[CSGO.exe]** 后就会发现开始启动游戏平台+游戏了；
4. 这时候你就可以用我的 [磁贴美化小工具](https://github.com/XIU2/TileTool) 把该软件固定为磁贴并自定义样式了！  
5. 只需要保留 **启动器(CSOG.exe) 配置文件(CSGO.ini)** ，图里的其他文件都可以删除。  

****

### UWP 应用 如何自定义磁贴样式？

![](https://raw.githubusercontent.com/XIU2/TileAssistTool/master/img/02.gif)  

1. 拖拽 UWP 应用到任意位置（例如桌面），就会得到一个 **快捷方式.lnk** ；
2. 把 **快捷方式.lnk** 拖拽到 **[磁贴辅助小工具.exe]** 文件上，然后软件会生成相应的配置文件；
3. 双击 **[便笺.exe]** 后就会发现可以打开 UWP 应用了；
4. 这时候你就可以用我的 [磁贴美化小工具](https://github.com/XIU2/TileTool) 把该软件固定为磁贴并自定义样式了！  
5. 只需要保留 **启动器(便笺.exe) 配置文件(便笺.ini) 快捷方式(便笺.lnk)** ，图里的其他文件都可以删除。  

> UWP 快捷方式需要和启动器与配置文件放在一起，可以一起移动。  
> 只有 UWP快捷方式需要保留，其他快捷方式拖入后都可以删除。  

****

> · 一次可以拖入 **多个且类型都不一样的文件。**  
> ——  
> · 每一个启动器都可以拖入文件来制作新的启动器和配置文件的！
> ——  
> · 因为一个程序(.exe)只能对应一个磁贴，所以如果要 **制作第二个** ，可以拖拽其他文件到任意启动器上，会自动生成一份新的启动器和配置文件。  
> ——  
> · 磁贴辅助小工具会把启动器和配置文件都生成到你的桌面，可以移动但是这两个文件需要放在一起且文件名一致！  

****

## 其他说明

### 运行提示 .NET 错误？

本软件最低依赖是 .NET Framework 4.6，报错说明你系统的该依赖版本低于 4.6（Win10 默认满足该依赖），请安装更高版本的 [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) ！

****

## 许可证

The GPL-3.0 License.

本软件仅供学习交流，请勿用于商用。  

软件所有权归 X.I.U(XIU2) 所有。  

> 该项目只在 [吾爱破解论坛](https://www.52pojie.cn/thread-1266756-1-1.html)、[知乎文章](https://zhuanlan.zhihu.com/p/79630122) 发布过，其他网站均为转载。当然，**欢迎转载！** 