using Interop.UIAutomationClient;

namespace YaPlayer.Model.Classes.Commands;
internal class NextTrackCommand : YaPlayerBaseCommand
{
    public override string MessageStringEquality => "/next";

    public override string UIButtonID => YaButtonsIDs.NextButton;
}
