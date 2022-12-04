using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.ViewComponents
{
    [ViewComponent(Name = "Navbar")]
    public class NavbarViewHeaderComponent : ViewComponent
    {
        private readonly DataContext _datacontext;
        public NavbarViewHeaderComponent(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _datacontext.Navbars.Include(n => n.SubNavbars.OrderBy(sn=>sn.Order)).Where(n => n.IsShowHeader).OrderBy(n => n.Order).ToList();

            return View("~/Views/Shared/Components/Navbar/Index.cshtml",model);
        }
    }
}
