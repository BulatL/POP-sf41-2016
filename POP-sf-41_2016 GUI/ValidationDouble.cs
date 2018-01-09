using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_sf_41_2016_GUI
{
    class ValidationDouble : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string doubleValue = value as string;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (string.IsNullOrEmpty(doubleValue))
            {
                return new ValidationResult(false, "Polje mora biti popunjeno");
            }
            try
            {
                double.Parse(doubleValue);
                return new ValidationResult(true, null);
            }
            catch (Exception)
            {

                return new ValidationResult(false, "Vrednost mora biti broj");
            }
        }
    }
}
