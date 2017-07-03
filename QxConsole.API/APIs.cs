using QxConsole.Plugin;
using QxConsole.Library;

namespace QxConsole.API
{
    public static class APIs
    {
        /// <summary>
        /// 添加插件命令
        /// </summary>
        /// <param name="cmd">命令信息，详情参阅 QxConsole SDK</param>
        public static void AddPluginCommand(string cmd)
        {
            // 检查插件添加的命令是否正确
            string[] t = cmd.Split(':');
            if (t.Length != 3)
            {
                System.Console.WriteLine("[Plugin API] Error at: AddPluginCommand");
                return;
            }

            try
            {
                System.Convert.ToString(t[0]);
                System.Convert.ToString(t[1]);
                System.Convert.ToInt32(t[2]);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Plugin API] Error at: AddPluginCommand");
            }

            PluginManager.pluginCmdList.Add(cmd);
        }
        
        /// <summary>
        /// 添加插件命令帮助，详情参阅 QxConsole SDK
        /// </summary>
        /// <param name="Plugin"></param>
        /// <param name="Commands"></param>
        /// <param name="Do"></param>
        /// <param name="Args"></param>
        public static void AddPluginCommandHelp(string Plugin, string Commands, string Do, string Args = "Null")
        {
            Functions.helpList.Add(Plugin + "\t" + Commands + "\t" + Args + "\t" + Do);
        }

        /// <summary>
        /// 添加插件命令帮助，详情参阅 QxConsole SDK
        /// </summary>
        /// <param name="Plugin"></param>
        /// <param name="Commands"></param>
        /// <param name="Do"></param>
        /// <param name="Args"></param>
        public static void AddPluginCommandHelpEx(string helpText)
        {
            Functions.helpList.Add(helpText);
        }
        
        public static void SetCustomColor(MessageType messageType, System.ConsoleColor color)
        {
            
        }
    }
}
