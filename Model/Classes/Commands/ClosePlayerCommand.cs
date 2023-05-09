using System.Diagnostics;

namespace YaPlayer.Model.Classes.Commands;
internal class ClosePlayerCommand : YaPlayerBaseCommand
{
    public override string MessageStringEquality => "/close";
    public override string UIButtonID => "";
    public override bool Invoke(YaPlayerAutomationProvider automationProvider)
    { 
        var process = Process.GetProcessById(automationProvider.YaMusicAppId);
       
        if (process != null && process.Id != 0)
        {
            process.Kill();
            return true;
        }
        return false;
    }
}
