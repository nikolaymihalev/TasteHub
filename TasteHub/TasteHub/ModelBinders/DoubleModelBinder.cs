using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace TasteHub.ModelBinders
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                double result = 0;
                bool success = false;

                try
                {
                    string value = valueResult.FirstValue.Trim();
                    value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    result = Convert.ToDouble(value, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (Exception fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }
            }

            return Task.CompletedTask;
        }
    }
}
