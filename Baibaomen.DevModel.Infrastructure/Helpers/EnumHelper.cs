using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Baibaomen.DevModel.Infrastructure
{
    public static class EnumHelper
    {
        public static IDictionary<string, string> GetDisplayNames<T>() where T : struct
        {
            var type = typeof (T);
            if (!type.IsEnum) throw new InvalidOperationException();
            var names = Enum.GetNames(type);
            IDictionary<string, string> displayNames = new Dictionary<string, string>();
            foreach (var name in names)
            {
                var displayAttribute = type.GetField(name)
                    .GetCustomAttributes(typeof (DisplayAttribute), false)
                    .SingleOrDefault() as DisplayAttribute;
                if (displayAttribute != null)
                {
                    displayNames.Add(name, displayAttribute.Name);
                }
            }
            return displayNames;
        }

        public static T? GetNullableValue<T>(string displayName) where T : struct
        {
            if (string.IsNullOrEmpty(displayName))
            {
                return null;
            }
            return GetValue<T>(displayName);
        }

        public static T GetValue<T>(string displayName) where T:struct 
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var displayAttribute = field.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .SingleOrDefault() as DisplayAttribute;
                if (displayAttribute != null && displayAttribute.Name == displayName)
                {
                    return (T)field.GetValue(null);
                }
            }
            return default(T);
        }

        public static bool IsValid<T>(string displayName)
        {
            T value=default(T);
            return TryGetValue(displayName,ref value);
        }

        public static bool TryGetValue<T>(string displayName, ref T value)
        {
            var type = typeof (T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var displayAttribute = field.GetCustomAttributes(typeof (DisplayAttribute), false)
                    .SingleOrDefault() as DisplayAttribute;
                if (displayAttribute != null && displayAttribute.Name == displayName)
                {
                    value = (T) field.GetValue(null);
                    return true;
                }
            }
            return false;
        }
    }
}