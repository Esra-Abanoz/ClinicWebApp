namespace ClinicWeb.UI.Validators
{
    public abstract class BaseValidator
    {
        public static List<ValidatorMethod> ValidatorMaker;

        protected BaseValidator()
        {
            if (ValidatorMaker == null)
            {
                ValidatorMaker = new List<ValidatorMethod>();
            }
            else
            {
                ValidatorMaker.Clear();
            }
        }

        public ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (ValidatorMaker.FirstOrDefault()?.Message==null)
            {
                result.IsValid = true;
            }
            else
            {
                result.IsValid = false;
                result.ErrorMessage = ValidatorMaker;
            }

            return result;
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidatorMethod> ErrorMessage { get; set; }
    }

    public class ValidatorMethod
    {
        public string Property { get; set; }
        public string Message { get; set; }
    }

}
