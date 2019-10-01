using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> Invoke()
        {
            return View();
        }
    }
}
