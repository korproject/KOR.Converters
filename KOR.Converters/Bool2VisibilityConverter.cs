using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KOR.Converters
{
    /// <summary>
    /// Multiway with boolean to Visibility conveter
    /// </summary>
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class Bool2VisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Bool to Visibility converter
        /// </summary>
        /// <param name="value">bolean value</param>
        /// <param name="parameter">toogle or default (0) and (-) hidden or collapsed (1)</param>
        /// <param name="culture">any</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is bool)
            {
                var val = (bool)value;

                if (parameter == null)
                {
                    // convert directly
                    return DirectVisibility(val);
                }

                // split separated => (0)-(1) values
                var parameters = ((string)parameter).Split(Custom.Separator);

                if (parameters.Length == 2)
                {
                    switch (parameters[0])
                    {
                        case "default":
                            return DirectVisibility(val);
                        case "toggle": // optinal parameters: collapsed or hidden
                            return Reversevisibility(DirectVisibility(val), parameters[1]);
                    }
                } else {
                    switch ((string)parameter)
                    {
                        case "default":
                            return DirectVisibility(val);
                        case "toggle": // default value is hidden
                            return Reversevisibility(DirectVisibility(val), "hidden");
                    }
                }
            }

            // default value
            return Visibility.Hidden;
        }

        /// <summary>
        /// Convert directly bool to Visibility Hidden or Visible
        /// </summary>
        /// <param name="value">boolean value</param>
        public Visibility DirectVisibility(bool value)
        {
            return value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Check parameter is collapsed or hidden
        /// </summary>
        /// <param name="parameter">string parameter value "hidden" or collapsed</param>
        public Visibility IsCollapsedVisibility(string parameter)
        {
            return parameter == "collapsed" ? Visibility.Collapsed : Visibility.Hidden;
        }

        /// <summary>
        /// Reverse Visibility for Toggle Visibility
        /// </summary>
        /// <param name="visibility">DirectVisibility value</param>
        /// <param name="parameter">convert parameter part of 2</param>
        public Visibility Reversevisibility(Visibility visibility, string parameter)
        {
            return visibility == Visibility.Visible ? IsCollapsedVisibility(parameter) : Visibility.Visible;
        }

        /// <summary>
        /// Convertback for bindings, mode: TwoWay
        /// </summary>
        /// <param name="value">Visibility*value</param>
        /// <param name="parameter">none</param>
        /// <param name="culture">any</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Visibility)
            {
                var vis = (Visibility)value;

                if (vis == Visibility.Collapsed || vis == Visibility.Hidden)
                {
                    return false;
                }
                else if (vis == Visibility.Visible)
                {
                    return true;
                }
            }

            throw new Exception(string.Format($"Can not convert back, unsupported value: \"{value}\""));
        }
    }
}
