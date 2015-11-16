using System.Collections.Generic;
using System.Web.Mvc;

namespace Saturn72.Web.Framework.Mvc
{
    [ModelBinder(typeof (Saturn72ModelBinder))]
    public class BaseSaturn72Model
    {
        public BaseSaturn72Model()
        {
            CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        /// <summary>
        ///     Use this property to store any custom value for your models.
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        ///     Developers can override this method in custom partial classes
        ///     in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
        }
    }
}