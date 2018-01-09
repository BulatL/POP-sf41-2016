using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_sf_41_2016_GUI
{
    public class ValidationPopust : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string intValue = value as string;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (string.IsNullOrEmpty(intValue))
            {
                return new ValidationResult(false, "Polje mora biti popunjeno");
            }
            try
            {
                var intv = int.Parse(intValue);
                if (intv > 99 || intv <= 0)
                {
                    return new ValidationResult(false, "Popust mora biti izmedju 0-99");
                }
                else return new ValidationResult(true, null);
            }
            catch (Exception)
            {

                return new ValidationResult(false, "Vrednost mora biti ceo broj");
            }
        }
    }
}
