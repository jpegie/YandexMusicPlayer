using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaPlayer.Model.Classes.Commands;
internal class StartPlayerCommand : YaPlayerBaseCommand
{
    public override string MessageStringEquality => "/start";

    public override string UIButtonID => "";
    public override bool Invoke(YaPlayerAutomationProvider automationProvider)
    {
        var process = Process.Start(ConfigurationManager.AppSettings["PlayerPath"]!);
        if (process != null)
        {
            automationProvider.ForceUpdateAppId();
            automationProvider.ForceUpdateWindow();
            return true;
        }
        return false;
    }
}
