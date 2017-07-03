using System;
using System.Collections.Generic;

using QxConsole;

namespace QxConsole.Library
{
    /// <summary>
    /// 定义命令信息结构体
    /// 
    /// 命令格式: [主命令(string)]:[参数个数(int)]
    /// 
    /// 当 [参数个数(int)] = -1 表示参数个数不定
    /// </summary>
    public struct CommandInfo
    {
        public string MainCommand;
        public int ArgsCount;
    }

    /// <summary>
    /// 命令解析器类
    /// 
    /// 用于解析用户输入的指令
    /// </summary>
    public static class Parser
    {
        // 最后错误信息文本变量        
        private static string lastError = "";

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
        public static CommandInfo GetCommandInfo(CommandStruct cs)
        {
            // 加载命令列表
            Functions.LoadCommandList();

            // 定义结果变量
            CommandInfo ci = new CommandInfo();

            for (int i = 0; i < Functions.CommandList.Count; i++)
            {
                // 定义 info 文本型数组，长度为2
                string[] info = new string[2];

                // 对当前行命令信息进行分割
                // info[0] - 主命令
                // info[1] - 参数个数
                info = Functions.CommandList[i].Split(':');

                // 判断用户输入的命令是否等于列表中当前的命令
                if (cs.MainCommand == info[0])
                {
                    // 如果是，继续判断输入的命令参数个数是否正确。注意参数个数是"-1"的命令
                    if (cs.Args.Count == Convert.ToInt32(info[1]) || (Convert.ToInt32(info[1]) == -1 && cs.Args.Count > 0))
                    {
                        // 如果是，将命令信息存入 ci 变量中
                        ci.MainCommand = info[0];
                        ci.ArgsCount = Convert.ToInt32(info[1]);

                        // 跳出循环
                        break;
                    }
                    // 如果上条判断不成立，提示参数过多或过少。注意参数个数是"-1"的命令
                    else
                    {
                        if (cs.Args.Count > Convert.ToInt32(info[1]))
                        {
                            if (Convert.ToInt32(info[1]) == -1)
                            {
                                lastError = "Too few arguments! Command \"" + cs.MainCommand
                                    + "\" takes >0 arguments";
                                break;
                            }
                            else
                            {
                                lastError = "Too many arguments! Command \"" + cs.MainCommand
                                    + "\" only takes " + Convert.ToString(info[1]) + " arguments";
                                break;
                            }
                        }
                        else
                        {
                            lastError = "Too few arguments! Command \"" + cs.MainCommand
                                + "\" takes " + Convert.ToString(info[1]) + " arguments";
                            break;
                        }
                    }
                }
                // 如果不是，提示"未知命令"
                else
                {
                    lastError = "Unknown command: " + cs.MainCommand;
                }
            }
            return ci;
        }

        /// <summary>
        /// 对用户输入的命令进行解析
        /// </summary>
        /// <param name="cmd">用户输入的命令</param>
        /// <returns>命令结构</returns>
        public static CommandStruct ParseCommand(string cmd)
        {
            // 定义结果变量 cs
            CommandStruct cs;

            // 删命令首尾空
            cmd = cmd.Trim();
            string[] str = cmd.Split(' ');

            // 取出用户输入的命令的"主命令"
            cs.MainCommand = str[0];

            // 初始化 cs.Args
            cs.Args = new List<string>();

            // 开始循环，将用户输入的命令中的参数加入 cs.Args
            for (int i = 1; i < str.Length; i++)
            {
                cs.Args.Add(str[i]);
            }

            return cs;
        }
    }
}
