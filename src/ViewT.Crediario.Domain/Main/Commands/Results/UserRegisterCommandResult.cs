using System;
using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Results
{
    public class UserRegisterCommandResult : ICommandResult
    {
        public string SerialKey { get; set; }
    }
}