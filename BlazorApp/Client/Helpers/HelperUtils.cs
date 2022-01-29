using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorApp.Client.Helpers
{
    public static class HelperUtils
    {
        public static int ObtenerMaxlength(this object instance, string propertyName)
        {
            var property = instance.GetType().GetProperty(propertyName);

            foreach (CustomAttributeData customAttributeData in property.CustomAttributes)
            {
                switch (customAttributeData.AttributeType.Name)
                {
                    case "StringLengthAttribute":
                        return ((StringLengthAttribute)property.GetCustomAttributes(typeof(StringLengthAttribute), false).First()).MaximumLength;
                    case "MaxLength":
                        return ((MaxLengthAttribute)property.GetCustomAttributes(typeof(StringLengthAttribute), false).First()).Length;
                    case "Range":
                        return ((Range)property.GetCustomAttributes(typeof(StringLengthAttribute), false).First()).Start.Value;
                }
            }

            return 0;
        }

        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
    }
}
