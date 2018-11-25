using System;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;

namespace ViewT.Crediario.Infra.Data.RepositoryFake
{
    public class TokenFakeRepository : ITokenRepository
    {
        public Token Add(Token newToken)
        {
            return new TokenBuilder()
                .WithTokenId(Guid.NewGuid())
                .WithUserToken(Guid.NewGuid());
        }

        public Token Update(Token token)
        {
            return new TokenBuilder().WithTokenId(Guid.NewGuid());
        }
    }
}