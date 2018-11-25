using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;

namespace ViewT.Crediario.Domain.Core.DomainNotification
{
    public static class AssertionConcern
    {
        public static bool IsSatisfiedBy(params Events.DomainNotification[] validations)
        {
            var notificationsNotNull = validations.Where(validation => validation != null);
            NotifyAll(notificationsNotNull);

            return notificationsNotNull.Count().Equals(0);
        }

        private static void NotifyAll(IEnumerable<Events.DomainNotification> notifications)
        {
            notifications.ToList().ForEach(validation => { DomainEvent.Raise(validation); });
        }

        /// <summary>
        /// Assert if the string value respects the minimum and maximum limits size
        /// </summary>
        /// <param name="stringValue">String value</param>
        /// <param name="minimum">Minimun limit size</param>
        /// <param name="maximum">Maximun limit size</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertLength(string stringValue, int minimum, int maximum, string message)
        {
            var length = stringValue.Trim().Length;

            return (length < minimum || length > maximum)
                ? new Events.DomainNotification("AssertArgumentLength", message)
                : null;
        }

        /// <summary>
        /// Assert if the string value matches with the Regex Pattern
        /// </summary>
        /// <param name="pattern">Regex Pattern</param>
        /// <param name="stringValue">String value</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertMatches(string pattern, string stringValue, string message)
        {
            var regex = new Regex(pattern);

            return (!regex.IsMatch(stringValue))
                ? new Events.DomainNotification("AssertArgumentMatches", message)
                : null;
        }

        /// <summary>
        /// Assert if the string value is not null, empty or white spaces
        /// </summary>
        /// <param name="stringValue">String Value</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertNotEmpty(string stringValue, string message)
        {
            return (string.IsNullOrWhiteSpace(stringValue))
                ? new Events.DomainNotification("AssertArgumentNotEmpty", message)
                : null;
        }

        /// <summary>
        /// Assert if the object is not null
        /// </summary>
        /// <param name="object1">Object to check</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertNotNull(object object1, string message)
        {
            return (object1 == null)
                ? new Events.DomainNotification("AssertArgumentNotNull", message)
                : null;
        }

        /// <summary>
        /// Assert if the object is null
        /// </summary>
        /// <param name="object1">Object to check</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertIsNull(object object1, string message)
        {
            return (object1 != null)
                ? new Events.DomainNotification("AssertArgumentNull", message)
                : null;
        }

        /// <summary>
        /// Assert if the validation statement or boolean value is true
        /// </summary>
        /// <param name="boolValue">Validation statement or boolean value</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertTrue(bool boolValue, string message)
        {
            return (!boolValue)
                ? new Events.DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        /// <summary>
        /// Assert if the values are equals
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="match">String value to match</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertAreEquals(string value, string match, string message)
        {
            return (!(value == match))
                ? new Events.DomainNotification("AssertArgumentEquals", message)
                : null;
        }

        /// <summary>
        ///  Assert if the integer value 01 is bigger than integer value 02
        /// </summary>
        /// <param name="value1">Integer value 01</param>
        /// <param name="value2">Integer value 02</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertIsGreaterThan(int value1, int value2, string message)
        {
            return (!(value1 > value2))
                ? new Events.DomainNotification("AssertArgumentGreatherThan", message)
                : null;
        }

        /// <summary>
        /// Assert if the decimal value 01 is bigger than decimal value 02
        /// </summary>
        /// <param name="value1">Decimal value 01</param>
        /// <param name="value2">Decimal value 02</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertIsGreaterThan(decimal value1, decimal value2, string message)
        {
            return (!(value1 > value2))
                ? new Events.DomainNotification("AssertArgumentGreatherThan", message)
                : null;
        }

        /// <summary>
        /// Assert if the integer value 01 is bigger than or equals integer value 02
        /// </summary>
        /// <param name="value1">Integer value 01</param>
        /// <param name="value2">Integer value 02</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertIsGreaterOrEqualThan(int value1, int value2, string message)
        {
            return (!(value1 >= value2))
                ? new Events.DomainNotification("AssertArgumentGreatherOrEqualThan", message)
                : null;
        }

        /// <summary>
        /// Assert if the decimal value 01 is bigger than decimal value 02
        /// </summary>
        /// <param name="value1">Decimal value 01</param>
        /// <param name="value2">Decimal value 02</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertIsGreaterOrEqualThan(decimal value1, decimal value2, string message)
        {
            return (!(value1 >= value2))
                ? new Events.DomainNotification("AssertArgumentGreatherOrEqualThan", message)
                : null;
        }

        /// <summary>
        /// Assert if the string value matches with Regex Pattern
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="regex">Regex Pattern to match</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertRegexMatch(string value, string regex, string message)
        {
            return (!Regex.IsMatch(value, regex, RegexOptions.IgnoreCase))
                ? new Events.DomainNotification("AssertRegexNotMatch", message)
                : null;
        }

        /// <summary>
        /// Check if the Guid value is different of Guid.Empty
        /// </summary>
        /// <param name="value">Not nullable Guid</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertGuidIsNotEmpty(Guid value, string message)
        {
            return (value == Guid.Empty)
                ? new Events.DomainNotification("AssertGuidIsEmpty", message)
                : null;
        }

        /// <summary>
        /// Assert if the string value is an valid email
        /// </summary>
        /// <param name="email">String value</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertEmailIsValid(string email, string message)
        {
            var emailRegex =
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return (!Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase))
                ? new Events.DomainNotification("AssertEmailIsInvalid", message)
                : null;
        }

        /// <summary>
        /// Assert if the string value is an valid url
        /// </summary>
        /// <param name="url">String value</param>
        /// <param name="message">Message to return if Assert occurred</param>
        /// <returns>DomainNotification object</returns>
        public static Events.DomainNotification AssertUrlIsValid(string url, string message)
        {
            // Do not validate if no URL is provided
            // You can call AssertNotEmpty before this if you want
            if (String.IsNullOrEmpty(url))
                return null;

            var regex = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

            return (!Regex.IsMatch(url, regex, RegexOptions.IgnoreCase))
                ? new Events.DomainNotification("AssertUrlIsInvalid", message)
                : null;
        }
    }
}
