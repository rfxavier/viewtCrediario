namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserUpdatePushTokenCommand: ICommand
    {
        public UserUpdatePushTokenCommand(string serialKey, string pushToken)
        {
            PushToken = pushToken;
            SerialKey = serialKey;
        }
        public string PushToken { get; private set; }
        public string SerialKey { get; private set; }
    }
}
