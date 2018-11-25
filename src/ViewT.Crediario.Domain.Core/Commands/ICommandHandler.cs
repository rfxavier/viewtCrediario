namespace ViewT.Crediario.Domain.Core.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
