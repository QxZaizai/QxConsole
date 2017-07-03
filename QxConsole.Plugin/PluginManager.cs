using System;
using System.Collections.Generic;
using System.Reflection;

using QxConsole;

namespace QxConsole.Plugin
{
    /// <summary>
    /// 定义插件命令信息结构体
    /// 
    /// 命令格式: [插件(string)]:[主命令(string)]:[参数个数(int)]
    /// </summary>
    public struct PluginCmdInfo
    {
        public string PluginName;
        public string MainCommand;
        public int ArgsCount;
    }

    /// <summary>
    /// 插件命令管理类
    /// </summary>
    public static class PluginManager
    {
        // 插件命令存放变量
        public static List<string> pluginCmdList = new List<string>();

        // 最后错误信息文本变量        
        private static string lastError = "";

        // 存放插件集合变量
        public static List<object> plugins = new List<object>();

        // 存放插件名的变量
        public static List<string> plugsName = new List<string>();

        /// <summary>
        /// 获取最后错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        public static string GetLastError()
        {
            string result = lastError;
            lastError = "";
            return result;
        }

        /// <summary>
        /// 获取命令信息
        /// </summary>
        /// <param name="cs">命令结构体</param>
        /// <returns>命令信息</returns>
        public static PluginCmdInfo GetCommandInfo(CommandStruct cs)
        {
            // 定义结果变量
            PluginCmdInfo plugCmdInfo = new PluginCmdInfo();
            
            for (int i = 0; i < pluginCmdList.Count; i++)
            {
                // 定义 info 文本型数组，长度为3
                string[] info = new string[3];

                // 对当前行命令信息进行分割
                // info[0] - 插件
                // info[1] - 主命令
                // info[2] - 参数个数
                info = pluginCmdList[i].Split(':');

                // 判断用户输入的命令是否等于列表中当前的命令
                if (cs.MainCommand == info[1])
                {
                    // 如果是，继续判断输入的命令参数个数是否正确。注意参数个数是"-1"的命令
                    if (cs.Args.Count == Convert.ToInt32(info[2]) || (Convert.ToInt32(info[2]) == -1 && cs.Args.Count > 0))
                    {
                        // 如果是，将命令信息存入 plugCmdInfo 变量中
                        plugCmdInfo.PluginName = info[0];
                        plugCmdInfo.MainCommand = info[1];
                        plugCmdInfo.ArgsCount = Convert.ToInt32(info[2]);

                        // 跳出循环
                        break;
                    }
                    // 如果上条判断不成立，提示参数过多或过少。注意参数个数是"-1"的命令
                    else
                    {
                        if (cs.Args.Count > Convert.ToInt32(info[2]))
                        {
                            if (Convert.ToInt32(info[2]) == -1)
                            {
                                lastError = "Too few arguments! Command \"" + cs.MainCommand
                                    + "\" takes >0 arguments";
                                break;
                            }
                            else
                            {
                                lastError = "Too many arguments! Command \"" + cs.MainCommand
                                    + "\" only takes " + Convert.ToString(info[2]) + " arguments";
                                break;
                            }
                        }
                        else
                        {
                            lastError = "Too few arguments! Command \"" + cs.MainCommand
                                + "\" takes " + Convert.ToString(info[2]) + " arguments";
                            break;
                        }
                    }
                }
                // 如果不是，提示"未知命令"
                else
                {
                    lastError = "Unknown plugin command: " + cs.MainCommand;
                }
            }
            if (pluginCmdList.Count == 0)
            {
                lastError = "There's no plugin command";
            }
            return plugCmdInfo;
        }

        /// <summary>
        /// 获取插件的 Object
        /// </summary>
        /// <param name="plugName">插件名</param>
        /// <returns>插件的 Object</returns>
        public static object GetPluginObj(string plugName)
        {
            for (int i = 0; i < plugsName.Count; i++)
            {
                if (plugsName[i] == plugName)
                {
                    return plugins[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 执行插件指令
        /// </summary>
        /// <param name="cs">命令结构体</param>
        /// <param name="pluginObj">插件 obj</param>
        public static void Execute(CommandStruct cs, object pluginObj)
        {
            try
            {
                Type t = pluginObj.GetType();
                MethodInfo func = t.GetMethod("OnPluginCommand");

                func.Invoke(pluginObj, new object[] { cs });
            }
            catch (Exception)
            {
                Console.WriteLine("[Plugin API] Error at: Excute");
            }
        }
    }
}
