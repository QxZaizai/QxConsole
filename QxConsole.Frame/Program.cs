using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using QxConsole;
using QxConsole.Plugin;
using QxConsole.Library;
using QxConsole.Frame.IO;

namespace QxConsole.Frame
{
    class Program
    {
        static void Main(string[] args)
        {
            // 运行参数处理
            bool quickMode = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "-q")
                {
                    quickMode = true;
                }
            }

            // 设置控制台标题
            Console.Title = "QxConsole";

            // 设置控制台字体颜色
            Console.ForegroundColor = ConsoleColor.Yellow;

            // 输出欢迎信息
            MyConsole.Other("Welcome to QxConsole!");
            Thread.Sleep(1000);

            MyConsole.Other("Have fun!");
            Thread.Sleep(500);

            // 判断 QuickMode
            if (quickMode == true)
            {
                MyConsole.Other("<QuickMode enable!>");
                goto e;
            }

            /*** 开始寻找插件 ***/
            MyConsole.Other("");
            MyConsole.Other("Loading plugins...");
            MyConsole.Info("*** QxConsole Plugin Loader ***");

            // 判断插件目录是否存在
            if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\plugins"))
            {
                // 定义存放插件集合变量
                List<string> pluginsFileName = new List<string>();

                // 获取插件目录 (plugins) 下所有文件
                string[] files = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\plugins");

                // 开始循环寻找适用于程序的插件
                foreach (string fileName in files)
                {
                    // 判断当前文件是否为 Dll 文件
                    if (fileName.ToLower().EndsWith(".dll"))
                    {
                        try
                        {
                            // 载入 Dll
                            Assembly asm = Assembly.LoadFrom(fileName);
                            Type[] types = asm.GetTypes();

                            foreach (Type t in types)
                            {
                                // 如果该插件中某些类实现了 QxConsole.API.IPlugin, 则把它加入插件列表
                                if (t.GetInterface("IPlugin") != null)
                                {
                                    MyConsole.Info("Loading plugin: " + fileName + " (Class: " + t.FullName + ")...");
                                    PluginManager.plugins.Add(asm.CreateInstance(t.FullName));
                                    
                                    // 调用插件方法
                                    object obj = PluginManager.plugins[PluginManager.plugins.Count - 1];
                                    Type type = obj.GetType();

                                    // 获取插件名
                                    MethodInfo func_GetPluginName = type.GetMethod("GetPluginName");
                                    string retn = func_GetPluginName.Invoke(obj, null).ToString();
                                    PluginManager.plugsName.Add(retn);

                                    // 调用初始化插件方法
                                    MethodInfo func_OnPluginInit = type.GetMethod("OnPluginInit");
                                    func_OnPluginInit.Invoke(obj, null);

                                    pluginsFileName.Add(fileName);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            MyConsole.Error("Can't load plugin: " + fileName);
                        }
                    }
                }
            }
            
            if (PluginManager.plugins.Count == 0)
            {
                MyConsole.Info("No plugin found");
            }

            /*** 插件寻找结束 ***/

            e: MyConsole.Other("");
            MyConsole.Other("Enter \"help\" to get commands' help");
            Thread.Sleep(1000);

            MyConsole.Other("Enter commands...");
            MyConsole.Other("");
            Thread.Sleep(500);

            // 开始接收用户命令
            while (true)
            {
                // 设置高亮
                MyConsole.Highlighting(MessageType.Command);

                Console.Write("Command>> ");
                string input = Console.ReadLine();

                // 判断用户输入的命令是否是空文本
                if (input.Trim() == "")
                {
                    // 结束单次循环
                    continue;
                }

                // 解析命令
                CommandStruct cs = Parser.ParseCommand(input);

                // 判断用户输入指令是否有错
                CommandInfo ci = Parser.GetCommandInfo(cs);
                if (ci.MainCommand == null)
                {
                    // 如果在系统指令列表中找不到用户输入的指令，则从插件指令列表中寻找
                    PluginCmdInfo plugCmdInfo = PluginManager.GetCommandInfo(cs);
                    
                    if (plugCmdInfo.PluginName == null)
                    {
                        MyConsole.Error("System>> " + Parser.GetLastError());
                        MyConsole.Error("Plugin>> " + PluginManager.GetLastError());
                        continue;
                    }
                    else
                    {
                        // 插件指令正确，执行
                        object plug = PluginManager.GetPluginObj(plugCmdInfo.PluginName);
                        if (plug != null)
                        {
                            PluginManager.Execute(cs, plug);
                        }
                        else
                        {
                            MyConsole.Other("[Plugin API] Error at: GetPluginObj");
                        }
                    }
                }
                else
                {
                    // 系统指令正确，执行
                    Functions.Execute(cs);
                }
            }
        }
    }
}
