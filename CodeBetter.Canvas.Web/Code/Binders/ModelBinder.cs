namespace CodeBetter.Canvas.Web.Binders
{
    using System;
    using System.Collections.Specialized;
    using System.Web.Mvc;
    using Canvas;
    using Canvas.Helpers;
    using Validation;
    using Web;

    public abstract class ModelBinder<T> : IModelBinder where T : class
    {
        private readonly IRepository _repository;
        private readonly IValidator _validator;
        protected IRepository Repository
        {
            get { return _repository; }
        }

        private NameValueCollection _parameters;
        private string _modelName;

        protected ModelBinder(IValidator validator) : this(null, validator){}
        protected ModelBinder(IRepository repository, IValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        protected virtual bool ShouldValidate
        {
            get { return true; }
        }
        protected virtual bool FromPost 
        { 
            get { return true; }
        }
        protected virtual ValidationError[] Validate(T entity)
        {
            return null;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var parameters = FromPost ? controllerContext.HttpContext.Request.Form : controllerContext.HttpContext.Request.QueryString;
            _modelName = string.Empty;
            if (!bindingContext.FallbackToEmptyPrefix)
            {
                _modelName = bindingContext.ModelName + '.';
            }            
            return BindModel(parameters, e => ((ApplicationController)controllerContext.Controller).AddError(e));
        }

        public object BindModel(NameValueCollection parameters, Action<ValidationError[]> addErrors)
        {
            _parameters = parameters;
            T entity;
            var id = Get("id").ToInt(0);
            if (id == 0)
            {
                entity = BindNew();
            }
            else
            {
                var original = _repository.Find<T>(id);
                entity = original == null ? null : BindExisting(original);
            }
            if (!DoValidation(entity, addErrors))
            {
                MassageInvalidEntity(entity);
            }
            return entity;            
        }

        public string Get(string name)
        {
            return _parameters[string.Concat(_modelName, name)];
        }
        public string Parameters(string name)
        {
            return Parameters(name);
        }

        private bool DoValidation(T entity, Action<ValidationError[]> addErrors)
        {
            if (entity == null)
            {
                addErrors(new[] {new ValidationError(null, "Failed to bind")});
                return false;
            }
            
            if (!ShouldValidate) { return true; }

            ValidationError[] errors;
            var isValid = true;
            if (!_validator.Validate(entity, out errors))
            {
                addErrors(errors);
                isValid = false;
            }
            var customErrors = Validate(entity);
            if (customErrors != null)
            {
                addErrors(customErrors);
                isValid = false;
            }
            return isValid;
        }

        public virtual void MassageInvalidEntity(T entity){}
        public abstract T BindNew();
        public abstract T BindExisting(T original);        
    }
}