using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaPlayer.Model.Classes.Commands;
using YaPlayer.Model.Interfaces;

namespace YaPlayer.Model.Classes;
internal class CommandsExecutor
{
    private IEnumerable<YaPlayerBaseCommand> _commands;
    private YaPlayerAutomationProvider _automationProvider;
    public CommandsExecutor(ICommandsFactory factory, YaPlayerAutomationProvider automationProvider)
    {
        _commands = factory.CreateCommands();
        _automationProvider = automationProvider;
    }

    public bool HandleCommand(string command)
    {
        return TryExecute(command);
    }
    private bool TryExecute(string command)
    {
        var cmd = _commands.FirstOrDefault(c => c.MessageStringEquality.Split(";").Contains(command), null);
        if (cmd != null)
        {
            return cmd.Invoke(_automationProvider);
        }
        return false;
    }
}
