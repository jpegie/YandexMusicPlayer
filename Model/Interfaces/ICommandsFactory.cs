using YaPlayer.Model.Classes.Commands;

namespace YaPlayer.Model.Interfaces;
internal interface ICommandsFactory
{
    IEnumerable<YaPlayerBaseCommand> CreateCommands();
}
