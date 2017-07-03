namespace QxConsole.Frame.IO
{
    public static class MyConsole
    {
        public static System.ConsoleColor Color_Command = System.ConsoleColor.Yellow;
        public static System.ConsoleColor Color_Info = System.ConsoleColor.Green;
        public static System.ConsoleColor Color_Warning = System.ConsoleColor.Magenta;
        public static System.ConsoleColor Color_Error = System.ConsoleColor.Red;
        public static System.ConsoleColor Color_Other = System.ConsoleColor.White;

        public static void Highlighting(QxConsole.MessageType messageType)
        {
            switch (messageType)
            {
                case QxConsole.MessageType.Command:
                    System.Console.ForegroundColor = Color_Command;
                    break;

                case QxConsole.MessageType.Info:
                    System.Console.ForegroundColor = Color_Info;
                    break;

                case QxConsole.MessageType.Warning:
                    System.Console.ForegroundColor = Color_Warning;
                    break;

                case QxConsole.MessageType.Error:
                    System.Console.ForegroundColor = Color_Error;
                    break;

                case QxConsole.MessageType.Other:
                    System.Console.ForegroundColor = Color_Other;
                    break;
            }
        }

        public static void Info(string str)
        {
            Highlighting(QxConsole.MessageType.Info);
            System.Console.WriteLine("[INFO] {0}", str);
        }

        public static void Warning(string str)
        {
            Highlighting(QxConsole.MessageType.Warning);
            System.Console.WriteLine("[WARN] {0}", str);
        }

        public static void Error(string str)
        {
            Highlighting(QxConsole.MessageType.Error);
            System.Console.WriteLine("[ERROR] {0}", str);
        }

        public static void Other(string str)
        {
            Highlighting(QxConsole.MessageType.Other);
            System.Console.WriteLine(str);
        }

        public static void Clear()
        {
            System.Console.Clear();
        }

        public static void SetTitle(string title)
        {
            System.Console.Title = title;
        }
    }
}