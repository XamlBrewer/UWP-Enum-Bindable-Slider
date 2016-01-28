using System;
using Windows.UI.Xaml.Data;

namespace XamlBrewer.Uwp.Controls
{
    public class EnumConverter : IValueConverter
    {
        private Type _enum;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            _enum = value.GetType();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (_enum == null)
            {
                return null;
            }

            return Enum.ToObject(_enum, (int)value);
        }
    }
}
