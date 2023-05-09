using Interop.UIAutomationClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaPlayer.Model.Classes;
internal class YaPlayerAutomationProvider
{
    CUIAutomation? _uiAutomation;
    IUIAutomationElement? _yaWindow;
    int _appId = -1;
    public YaPlayerAutomationProvider()
    {
        _uiAutomation = new CUIAutomation();
    }
    public int YaMusicAppId 
    { 
        get 
        { 
            if (_appId == -1 || _appId == 0)
            {
                _appId = GetYaProcessId();
            }
            return _appId; 
        }
    }
    public CUIAutomation? UiAutomation 
    {
        get
        {
            if (_uiAutomation == null)
            {
                _uiAutomation = new CUIAutomation();
            }
            return _uiAutomation;
        }
    }
    public IUIAutomationElement? YaMusicWindow 
    { 
        get
        {
            if (_yaWindow == null)
            {
                _yaWindow = GetYaWindow();
            }
            return _yaWindow;
        }
    }
    public void ForceUpdateAppId()
    {
        _appId = GetYaProcessId();
    }
    public void ForceUpdateWindow()
    {
        _yaWindow = GetYaWindow(); 
    }
    public IUIAutomationElement? GetYaWindow()
    {
        var appId = YaMusicAppId;
        var yaMusicWindow = UiAutomation.GetRootElement().FindFirst
        (
            TreeScope.TreeScope_Subtree,
            UiAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_ProcessIdPropertyId, appId)
        ) ;

        if (yaMusicWindow == null)
        {
            var handle = Process.GetProcessById(appId).MainWindowHandle;
            if (handle != IntPtr.Zero)
            {
                yaMusicWindow = UiAutomation.ElementFromHandle(handle);
            }
            else
            {
                return null;
            }

        }
        return yaMusicWindow;
    }
    static public int GetYaProcessId()
    {
        // Ищем процесс Яндекс.Музыка
        Process[] processes = Process.GetProcessesByName("Y.Music");
        if (processes.Length > 0)
        {
            // Возвращаем идентификатор первого найденного процесса
            return processes[0].Id;
        }
        else
        {
            return 0;
        }
    }

}
