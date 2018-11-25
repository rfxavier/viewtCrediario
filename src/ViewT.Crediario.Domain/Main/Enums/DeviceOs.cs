using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewT.Crediario.Domain.Main.Enums
{
    public class DeviceOs
    {
        public static DeviceOs iOS { get; } = new DeviceOs(1, "iOS");
        public static DeviceOs Android { get; } = new DeviceOs(2, "Android");

        private DeviceOs(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private DeviceOs() { } //required for EF

        public int Value { get; private set; }
        public string Name { get; private set; }

        public static IEnumerable<DeviceOs> List()
        {
            return new[] { iOS, Android };
        }

        public static DeviceOs FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static DeviceOs FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }
    }
}