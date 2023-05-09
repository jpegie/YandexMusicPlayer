using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaPlayer.Model.Classes.Commands;
using YaPlayer.Model.Interfaces;

namespace YaPlayer.Model.Classes;
internal class DefaultCommandsFactory : ICommandsFactory
{
    public IEnumerable<YaPlayerBaseCommand> CreateCommands()
    {
        return new List<YaPlayerBaseCommand>
        {
            new NextTrackCommand(),
            new PlayStopCommand(),
            new StartPlayerCommand(),
            new ClosePlayerCommand()
        };
    }

}
