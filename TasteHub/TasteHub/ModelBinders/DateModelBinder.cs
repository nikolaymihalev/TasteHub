using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace TasteHub.ModelBinders
{
    public class DateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                DateTime result;

                try
                {
                    result = DateTime.ParseExact(valueResult.FirstValue.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception ex)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
                }
            }

            return Task.CompletedTask;
        }
    }
}
