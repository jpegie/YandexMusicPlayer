using Interop.UIAutomationClient;
using YaPlayer.Model.Interfaces;

namespace YaPlayer.Model.Classes.Commands;
internal abstract class YaPlayerBaseCommand : IYaPlayerCommand
{
    public abstract string MessageStringEquality { get; }

    public abstract string UIButtonID { get; }

    public virtual bool Invoke(YaPlayerAutomationProvider automationProvider)
    {
        var window = automationProvider.YaMusicWindow;
        var ui = automationProvider.UiAutomation;

        if (window != null && ui != null)
        {
            IUIAutomationElement button = window.FindFirst
            (
                TreeScope.TreeScope_Children,
                ui.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, UIButtonID)
            );

            if (button != null)
            {
                var invokePattern = (IUIAutomationInvokePattern)button.GetCurrentPattern(UIA_PatternIds.UIA_InvokePatternId);
                invokePattern.Invoke();
                return true;
            }
        }
        return false;
    }
}
