using System;
using System.Windows.Data;

namespace EW30SX {


    /// <summary>
    /// CONVERT STRING TO STRING -------------------------//
    /// </summary>
    public class StringToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch (value.ToString().ToLower()) {
                case "verifying...":
                case "upgrading...":
                    return "STOP";
                default:
                    return "START";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }


    /// <summary>
    /// CONVERT STRING TO BOOL -----------------------------//
    /// </summary>
    public class StringToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch (value.ToString().ToLower()) {
                case "verifying...":
                case "upgrading...":
                    return false;
                default:
                    return true;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }


}
