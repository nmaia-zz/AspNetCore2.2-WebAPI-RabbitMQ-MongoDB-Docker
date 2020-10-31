using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Demo.API.ViewModels.Extensions
{
    public class FiliationArrayLenghtValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var filiationArray = value as string[];

            if (filiationArray != null)
            {
                return filiationArray.Count() == 2;
            }

            return false;
        }
    }
}
