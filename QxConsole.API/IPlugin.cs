using QxConsole;

namespace QxConsole.API
{
    public interface IPlugin
    {
        string GetPluginName();
        void OnPluginInit();
        void OnPluginCommand(CommandStruct cs);
    }
}
