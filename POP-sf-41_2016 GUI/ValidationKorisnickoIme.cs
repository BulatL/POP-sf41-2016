using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_sf_41_2016_GUI
{
    class ValidationKorisnickoIme : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value as string;
            bool provera = false;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, "Polje mora biti popunjeno");
            }
            else
            {
                foreach (var item in Projekat.Instance.Korisnici)
                {
                    if (strValue == item.KorisnickoIme)
                    {
                        provera = true;
                        return new ValidationResult(false, "Korisnicko ime je vec zauzeto");
                    }
                }
                if(provera == false)
                {
                    return new ValidationResult(true, null);
                }

                return null;
            }
        }
    }
}
