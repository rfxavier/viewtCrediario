using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserAuthenticateCommand : ICommand
    {
        public UserAuthenticateCommand(string user, string password, string identification, int deviceOs, string deviceModel, string versionOs)
        {
            User = user;
            Password = password;
            Identification = identification;
            DeviceOs = deviceOs;
            DeviceModel = deviceModel;
            VersionOs = versionOs;
        }

        public string User { get; private set; }
        public string Password { get; private set; }
        public string Identification { get; private set; }
        public int DeviceOs { get; private set; }
        public string DeviceModel { get; private set; }
        public string VersionOs { get; private set; }
    }
}