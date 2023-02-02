using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FGCIJOROSystem.Common
{
    public static class ObjectExtensions
    {
        public static string GetStringProperty(this object obj, string propertyName)
        {
            string value = "";
            try
            {
                value = (string)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
            }
            catch { }
            return value.Trim();
        }

        public static decimal? GetDecimalProperty(this object obj, string propertyName)
        {
            decimal? value = null;
            try
            {
                value = (decimal)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
            }
            catch { }
            return value;
        }

        public static void SetStringProperty(this object obj, string propName, string value)
        {
            Type t = obj.GetType();

            t.InvokeMember(propName, BindingFlags.SetProperty,
                                      null, obj, new object[] { value });
        }

        public static void SetValueProperty(this object obj, string propName, object value)
        {
            Type t = obj.GetType();

            t.InvokeMember(propName, BindingFlags.SetProperty,
                                      null, obj, new object[] { value });
        }

    }
}
