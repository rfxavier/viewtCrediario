namespace ViewT.Crediario.Domain.Core.Commands
{
    public interface IAppImageCommandHandler<T> where T : ICommand
    {
        byte[] Handle(T command);
    }
}