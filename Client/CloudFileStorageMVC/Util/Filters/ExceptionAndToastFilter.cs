using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using CloudFileStorageMVC.Util.ExceptionHandling;

namespace CloudFileStorageMVC.Util.Filters;

public class ExceptionAndToastFilter(IToastNotification toastNotification
) : IActionFilter, //  action çalşırken veya çalıştıkran sonra neler yapılacağını belirtiyoruz
    IExceptionFilter //  action, exception throw ettiğinde neler yapılacağını belirtiyoruz

{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Controller is Controller controller)
        {

            if (controller.TempData["ErrorMessage"] != null)
            {
                var errorMessage = controller.TempData["ErrorMessage"]!.ToString();
                toastNotification.AddErrorToastMessage(errorMessage, new ToastrOptions
                {
                    Title = "Hata"
                });
                controller.TempData.Remove("ErrorMessage");
            }

            if (controller.TempData["SuccessMessage"] != null)
            {
                var errorMessage = controller.TempData["SuccessMessage"]!.ToString();
                toastNotification.AddSuccessToastMessage(errorMessage, new ToastrOptions
                {
                    Title = "Başarılı"
                });
                controller.TempData.Remove("SuccessMessage");
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnException(ExceptionContext context)
    {

        if (context.Exception is ApiException apiEx)
        {
            var toastNotification = context.HttpContext.RequestServices.GetService<IToastNotification>();
            toastNotification?.AddErrorToastMessage($"{apiEx.ApiError.Title}: {apiEx.ApiError.Detail}");

            context.HttpContext.Items["ErrorTitle"] = apiEx.ApiError.Title;
            context.HttpContext.Items["ErrorMessage"] = apiEx.ApiError.Detail;
            context.HttpContext.Items["ErrorCode"] = apiEx.ApiError.Status;
        }


        if (context.HttpContext.Request.Method == "GET")
        {
            context.Result = new RedirectToActionResult("Error", "Home", null);
        }
        else if (context.HttpContext.Request.Method == "POST")
        {
            var routeValues = context.RouteData.Values;
            var controller = routeValues["controller"];
            var action = routeValues["action"];

            context.Result = new RedirectToActionResult(action.ToString(), controller.ToString(), null);
        }

        context.ExceptionHandled = true;
    }
}