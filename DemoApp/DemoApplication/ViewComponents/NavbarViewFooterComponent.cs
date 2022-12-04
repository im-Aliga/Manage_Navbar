using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.ViewComponents
{
    [ViewComponent(Name = "NavbarFooter")]
    public class NavbarViewFooterComponent : ViewComponent
    {
        private readonly DataContext _datacontext;
        public NavbarViewFooterComponent(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _datacontext.Navbars.Include(n => n.SubNavbars.OrderBy(sn=>sn.Order)).Where(n => n.IsShowFooter).OrderBy(n => n.Order).ToList();

            return View("~/Views/Shared/Components/Navbar/IndexFooter.cshtml",model);
        }
    }
}
