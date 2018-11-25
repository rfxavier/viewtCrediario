using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Interfaces
{
    public interface ITokenRepository
    {
        Token Add(Token newToken);
        Token Update(Token token);
    }
}