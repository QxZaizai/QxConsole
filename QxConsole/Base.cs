using System.Collections.Generic;

namespace QxConsole
{
    /// <summary>
    /// 定义命令结构体
    /// </summary>
    public struct CommandStruct
    {
        public string MainCommand;
        public List<string> Args;
    }

    /// <summary>
    /// 定义控制台中输出的消息类型的枚举
    /// </summary>
    public enum MessageType { Command, Info, Warning, Error, Other };
}
