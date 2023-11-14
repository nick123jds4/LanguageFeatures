using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ControllersAndActions.Controllers
{
    public class PocoController
    {
        //public string Index() => "This is string";

        public ViewResult Index()
        {
            var model = new EmptyModelMetadataProvider();
            var state = new ModelStateDictionary();

            var viewResult = new ViewResult()
            {
                ViewName = "Result",
                ViewData = new ViewDataDictionary(model, state)
                {
                    Model = $"this is POCO controller"
                }
            };

            return viewResult;
        }
    }
}
