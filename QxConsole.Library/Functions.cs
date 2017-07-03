using System;
using System.Collections.Generic;

using QxConsole;
using QxConsole.Frame.IO;

namespace QxConsole.Library
{
    public static class Functions
    {
        public static List<string> CommandList = new List<string>();

        #region Library Functions

        private static CommandStruct cs;

        private static void Library_Exit()
        {
            Environment.Exit(0);
        }

        private static void Library_Print()
        {
            string str = "";
            for (int i = 0; i < cs.Args.Count; i++)
            {
                str += cs.Args[i] + " ";
            }
            MyConsole.Info(str);
        }

        private static void Library_Cmd()
        {
            System.Diagnostics.Process.Start("cmd");
        }

        private static void Library_Shell()
        {
            System.Diagnostics.Process.Start(cs.Args[0]);
        }

        private static void Library_Clear()
        {
            MyConsole.Clear();
        }

        public static List<string> helpList = new List<string>();
        private static void Library_Help()
        {
            MyConsole.Info("*** QxConsole Help System ***");
            MyConsole.Info("[System Commands]");
            MyConsole.Info("Command\tArgs\tDo");
            MyConsole.Info("----------------------------------------------------------");
            MyConsole.Info("exit\tNull\tExit QxConsole");
            MyConsole.Info("help\tNull\tGet QxConsole commands' help");
            MyConsole.Info("clear\tNull\tClear console");
            MyConsole.Info("print\t>0\tPrint a sentence. For example, \"print Hello!\"");
            MyConsole.Info("cmd\tNull\tOpen Microsoft Windows cmd");
            MyConsole.Info("shell\t[EXE]\tOpen a program");
            if (Plugin.PluginManager.pluginCmdList.Count > 0)
            {
                MyConsole.Info("");
                MyConsole.Info("[Plugin Commands]");
                MyConsole.Info("Plugin\t\tCommand\t\tArgs\tDo");
                MyConsole.Info("----------------------------------------------------------");
                foreach (string item in helpList)
                {
                    MyConsole.Info(item);
                }
            }
        }

        #endregion

        public static void LoadCommandList()
        {
            CommandList.Clear();

            CommandList.Add("exit:0");
            CommandList.Add("print:-1");
            CommandList.Add("help:0");
            CommandList.Add("cmd:0");
            CommandList.Add("shell:1");
            CommandList.Add("clear:0");
        }

        public static void Execute(CommandStruct _cs)
        {
            cs = _cs;
            switch (_cs.MainCommand)
            {
                case "exit":
                    Library_Exit();
                    break;

                case "print":
                    Library_Print();
                    break;

                case "help":
                    Library_Help();
                    break;

                case "cmd":
                    Library_Cmd();
                    break;

                case "shell":
                    Library_Shell();
                    break;

                case "clear":
                    Library_Clear();
                    break;

                default:
                    break;
            }
        }
    }
}
