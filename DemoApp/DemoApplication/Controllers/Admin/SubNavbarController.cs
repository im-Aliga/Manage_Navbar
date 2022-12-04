using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.ViewModels.Admin.Subnavbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddViewModel = DemoApplication.ViewModels.Admin.Subnavbar.AddViewModel;

namespace DemoApplication.Controllers.Admin
{
    [Route("admin/subnavbar")]
    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;
        public SubNavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet("list", Name = "admin-subnavbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.SubNavbars
                .Select(sn => new ListViewModel(sn.Id,sn.Title, sn.Order,sn.Url,$"{sn.Navbar.Title}"))
                .ToListAsync();

            return View("~/Views/Admin/Subnavbar/ListAsync.cshtml", model);
        }

        [HttpGet("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Navbar = await _dataContext.Navbars
                    .Select(a => new NavbarListItemViewModel(a.Id, $" {a.Title}"))
                    .ToListAsync()
                
            };

            return View("~/Views/Admin/Subnavbar/AddAsync.cshtml", model);
        }

        [HttpPost("add", Name = "admin-subnavbar-add")]
        public async Task< IActionResult> AddAsync(AddViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var subnav = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars
                   .Select(a => new NavbarListItemViewModel(a.Id, $" {a.Title}"))
                   .ToListAsync()

                };
                return View("~/Views/Admin/Subnavbar/AddAsync.cshtml", subnav);
            }

            var subNavbar = new SubNavbar()
            {
                Title = model.Title,
                Url = model.Url,
                Order = model.Order,
                NavbarId = model.NavbarId,
            };
            await _dataContext.SubNavbars.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }

        [HttpGet("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null)
            {
                return NotFound();
            }
            

            var model = new UpdateViewModel
            {
               
                Title = subnavbar.Title,
                Url = subnavbar.Url,
                Order = subnavbar.Order,
                Navbars= _dataContext.Navbars.Select(n=>new NavbarListItemViewModel(n.Id,n.Title)).ToList()
               
            };

            return View("~/Views/Admin/SubNavbar/UpdateAsync.cshtml", model);
        }

        [HttpPost("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var subnavbar =  await _dataContext.SubNavbars.Include(n => n.Navbar).FirstOrDefaultAsync (n => n.Id == model.Id);
            if (subnavbar is null)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                var subnav = new UpdateViewModel
                {

                    Title = subnavbar.Title,
                    Url = subnavbar.Url,
                    Order = subnavbar.Order,
                    Navbars = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Title)).ToList()

                };
                return View("~/Views/Admin/SubNavbar/UpdateAsync.cshtml", subnav);
            }

            subnavbar.Title = model.Title;
            subnavbar.Url = model.Url;
            subnavbar.Order = model.Order;
            subnavbar.NavbarId = model.NavbarId;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnavbar-list");
        }

        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var model = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }















    }
}
