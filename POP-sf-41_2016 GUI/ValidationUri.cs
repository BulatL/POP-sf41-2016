using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace POP_sf_41_2016_GUI
{
    class ValidationUri : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Uri uriResult;
            string uriName = value as string;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (string.IsNullOrEmpty(uriName))
            {
                return new ValidationResult(false, "Polje mora biti popunjeno");
            }
            else
            {
                bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if(result == true) return new ValidationResult(true, null);

                else return new ValidationResult(false, "Pogresan format");

            }
        }
    }
}
