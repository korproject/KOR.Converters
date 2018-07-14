using KOR.Converters.CustomParsers;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace KOR.Converters
{
    /// <summary>
    /// String limit converter
    /// </summary>
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class StringLimitConverter : IValueConverter
    {
        /// <summary>
        /// String to limited string converter
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="parameter">word or character (0) and (-) limit (1) and (-) suffix (2)</param>
        /// <param name="culture">any</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string || parameter == null)
            {
                var val = (string)value;

                // split separated => (0)-(1)-(2) values
                var parameters = ((string)parameter).Split(Custom.Separator);

                var limit = 0;

                if (parameters != null && parameters.Length > 2 && !parameters[1].IntParse().Int2Bool())
                {
                    return val;
                }

                if (parameters.Length == 3)
                {
                    limit = parameters[1].IntParse();

                    switch (parameters[0])
                    {
                        case "char":
                            return LimitCharacters(val, limit, parameters[2]);
                        case "word":
                            return LimitWords(val, limit, parameters[2]);
                    }
                }
                else if(parameters.Length == 2)
                {
                    limit = parameters[1].IntParse();

                    switch (parameters[0])
                    {
                        case "char":
                            return LimitCharacters(val, limit, null);
                        case "word":
                            return LimitWords(val, limit, null);
                    }
                } 
                // default option limit characters
                else
                {
                    if (((string)parameter).IntParse().Int2Bool())
                    {
                        return LimitCharacters(val, ((string)parameter).IntParse(), null);
                    }
                }
            }

            // default value is itself
            return value;
        }

        /// <summary>
        /// Set limit words
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="limit">limit of word count</param>
        /// <param name="suffix">optional suffix end of text</param>
        public string LimitWords(string value, int limit, string suffix = null)
        {
            var words = Regex.Matches(value, @"\b[\w']*\b");

            if (words.Count > limit)
            {
                var newValue = string.Empty;

                var i = 1;

                foreach (var word in words)
                {
                    if (i <= limit && !string.IsNullOrEmpty(word.ToString()))
                    {
                        newValue += $" {word}";
                        i++;
                    } else if (i > limit)
                    {
                        break;
                    }
                }

                if (string.IsNullOrEmpty(newValue))
                {
                    newValue += (suffix ?? "");
                }

                return newValue.Trim();
            }

            return value; 
        }

        /// <summary>
        /// Set limit characters
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="limit">limit of character count</param>
        /// <param name="suffix">optional suffix end of text</param>
        public string LimitCharacters(string value, int limit, string suffix = null)
        {
            if (value.Length > limit)
            {
                return value.Substring(0, limit) + (suffix ?? "");
            }

            return value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Convert back not supported");
        }
    }
}
