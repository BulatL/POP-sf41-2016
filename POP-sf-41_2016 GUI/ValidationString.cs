using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_sf_41_2016_GUI
{
    class ValidationString : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value as string;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, "Polje mora biti popunjeno");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
