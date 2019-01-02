namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public static class UserUpdatePushTokenCommandScopes
    {
        #region .:: SerialKey validations ::.
        public static bool HasSerialKeyNotNull(this UserUpdatePushTokenCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(command.SerialKey, Resources.Messages.SerialKeyRequired));
        }

        public static bool HasSerialKeyNotEmpty(this UserUpdatePushTokenCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotEmpty(command.SerialKey, Resources.Messages.SerialKeyRequired));
        }

        public static bool HasPushTokenNotNull(this UserUpdatePushTokenCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(command.PushToken, Resources.Messages.PushTokenRequired));
        }

        public static bool HasPushTokenNotEmpty(this UserUpdatePushTokenCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotEmpty(command.PushToken, Resources.Messages.PushTokenRequired));
        }

        public static bool HasSerialKey(this UserUpdatePushTokenCommand command)
        {
            return HasSerialKeyNotNull(command) && HasSerialKeyNotEmpty(command);
        }

        public static bool HasPushToken(this UserUpdatePushTokenCommand command)
        {
            return HasPushTokenNotNull(command) && HasPushTokenNotEmpty(command);
        }
        #endregion

        public static bool HasFoundPerson(this UserUpdatePushTokenCommand command, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(person, Resources.Messages.UserNotFound));
        }
    }
}
