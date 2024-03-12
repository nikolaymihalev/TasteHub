using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TasteHub.ModelBinders
{
    public class DoubleModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(double) ||
                context.Metadata.ModelType == typeof(double?))
            {
                return new DoubleModelBinder();
            }

            return null;
        }
    }
}
