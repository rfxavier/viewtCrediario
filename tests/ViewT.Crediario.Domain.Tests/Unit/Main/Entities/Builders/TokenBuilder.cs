using System;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders
{
    public class TokenBuilder
    {
        private Guid _tokenId = Guid.Empty;
        private Guid _userToken = Guid.Empty;
        private DeviceOs _deviceOs = DeviceOs.iOS;
        private bool _active = false;

        public Token Build()
        {
            var token = new Token(_tokenId, _userToken, _deviceOs);

            if (_active)
            {
                token.Activate();
            }
            else
            {
                token.Deactivate();
            }

            return token;
        }

        public TokenBuilder WithTokenId(Guid tokenId)
        {
            this._tokenId = tokenId;
            return this;
        }

        public TokenBuilder WithUserToken(Guid userToken)
        {
            this._userToken = userToken;
            return this;
        }

        public TokenBuilder WithActive(bool active)
        {
            this._active = active;
            return this;
        }

        public static implicit operator Token(TokenBuilder instance)
        {
            return instance.Build();
        }

    }
}