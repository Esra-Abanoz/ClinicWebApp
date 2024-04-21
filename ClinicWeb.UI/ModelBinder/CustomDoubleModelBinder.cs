using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicWeb.UI.ModelBinder
{
    public class CustomDoubleModelBinder : IModelBinder
    {
        public static readonly Type[] SUPPORTED_TYPES = new Type[] { typeof(double), typeof(double?) };
        private string GetModelName(ModelBindingContext bindingContext)
        {
            if (!string.IsNullOrEmpty(bindingContext.BinderModelName))
            {
                return bindingContext.BinderModelName;
            }
            return bindingContext.ModelName;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = this.GetModelName(bindingContext);

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrWhiteSpace(value) || value.ToLowerInvariant() == "null")
            {
                if (bindingContext.ModelMetadata.ModelType == typeof(double?))
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                }
                return Task.CompletedTask;
            }

            double realValue = 0;
            if (!double.TryParse(value.Replace(".", ","), out realValue))
            {
                // Non-integer arguments result in model state errors

                //bindingContext.ModelState.TryAddModelError(modelName, "Author Id must be an integer.");

                return Task.CompletedTask;
            }

            // Model will be null if not found, including for
            // out of range id values (0, -3, etc.)
            bindingContext.Result = ModelBindingResult.Success(realValue);
            return Task.CompletedTask;
        }
    }
}
