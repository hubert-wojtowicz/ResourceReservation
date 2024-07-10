using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Validations
{
    public class NotDefaultGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is Guid guidValue)
            {
                return guidValue != Guid.Empty;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field must not be the default GUID value.";
        }
    }
}
