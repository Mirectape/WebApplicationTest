using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Component
{
    public class LogoutViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
