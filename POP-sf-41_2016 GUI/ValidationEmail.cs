using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace POP_sf_41_2016_GUI
{
    class ValidationEmail : ValidationRule
    {
        Regex regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            String v = value as string;
            if (v != null && regex.Match(v).Success)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Neispravan format e-mail adrese");
        }
    }
}
