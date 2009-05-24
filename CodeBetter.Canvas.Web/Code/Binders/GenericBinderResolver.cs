namespace CodeBetter.Canvas.Web.Binders
{
    using System;
    using System.Web.Mvc;

    public class GenericBinderResolver : DefaultModelBinder
    {
        private readonly Func<Type, IModelBinder> _resolver;
        private static readonly Type _binderType = typeof(ModelBinder<>);        
        public GenericBinderResolver(Func<Type, IModelBinder> resolver)
        {

            _resolver = resolver;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (typeof(ValueType).IsAssignableFrom(bindingContext.ModelType))
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            var genericBinderType = _binderType.MakeGenericType(bindingContext.ModelType);
            var binder = _resolver(genericBinderType);
            if (binder == null)
            {
                return base.BindModel(controllerContext, bindingContext);                
            }
            return binder.BindModel(controllerContext, bindingContext);
        }
    }

}