using Interop.UIAutomationClient;
using YaPlayer.Model.Interfaces;

namespace YaPlayer.Model.Classes.Commands;
internal class PlayStopCommand : YaPlayerBaseCommand
{
    public override string MessageStringEquality => "/play";

    public override string UIButtonID => YaButtonsIDs.PlayStop;
}
