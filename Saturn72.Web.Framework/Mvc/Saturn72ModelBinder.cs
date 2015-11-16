using System.Web.Mvc;

namespace Saturn72.Web.Framework.Mvc
{
    public class Saturn72ModelBinder: DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseSaturn72Model)
            {
                ((BaseSaturn72Model)model).BindModel(controllerContext, bindingContext);
            }
            return model;
        }
    }
}