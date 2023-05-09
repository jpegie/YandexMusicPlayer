using YaPlayer.Model.Classes;

namespace YaPlayer.Model.Interfaces;
internal interface IYaPlayerCommand
{
    bool Invoke(YaPlayerAutomationProvider automationProvider);
    
}
