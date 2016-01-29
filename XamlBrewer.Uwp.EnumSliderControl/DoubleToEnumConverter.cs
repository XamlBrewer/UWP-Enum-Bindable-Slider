using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Data;

namespace XamlBrewer.Uwp.Controls
{
    /// <summary>
    /// Internal use only.
    /// </summary>
    class DoubleToEnumConverter : IValueConverter
    {
        private Type _enum;

        public DoubleToEnumConverter(Type type)
        {
            _enum = type;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var _name = Enum.ToObject(_enum, (int)(double)value);

            // Look for a 'Display' attribute.
            var _member = _enum
                .GetRuntimeFields()
                .FirstOrDefault(x => x.Name == _name.ToString());
            if (_member == null)
            {
                return _name;
            }

            var _attr = (DisplayAttribute)_member.GetCustomAttribute(typeof(DisplayAttribute));
            if (_attr == null)
            {
                return _name;
            }

            return _attr.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value; // Never called
        }
    }
}
