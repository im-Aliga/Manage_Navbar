using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.ViewModels.Admin.Navbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AddViewModel = DemoApplication.ViewModels.Admin.Navbar.AddViewModel;
using Navbar = DemoApplication.Database.Models.NavbarM;

namespace DemoApplication.Controllers.Admin
{
    [Route("admin/navbar")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List
        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select(n => new ListViewModel(n.Id, n.Title, n.Order, n.IsBold, n.IsShowHeader, n.IsShowFooter))
                .ToListAsync();

            return View("~/Views/Admin/Navbar/ListAsync.cshtml", model);
        }
        #endregion



        #region Add
        [HttpGet("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View("~/Views/Admin/Navbar/AddAsync.cshtml");
        }

        [HttpPost("add", Name = "admin-navbar-add")]

        
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/AddAsync.cshtml",model);
            }

            var navbar = new NavbarM
            {
                Title = model.Title,
                Url = model.Url,
                Order = model.Order,
                IsBold = model.IsBold,
                IsShowHeader = model.IsShowHeader,
                IsShowFooter = model.IsShowFooter

            };
            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");
        }
        #endregion


        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = navbar.Id,
                Title = navbar.Title,
                Order = navbar.Order,
                Url = navbar.Url,
                IsBold = navbar.IsBold,
                IsShowFooter = navbar.IsShowFooter,
                IsShowHeader = navbar.IsShowHeader,

            };

            return View("~/Views/Admin/Navbar/UpdateAsync.cshtml", model);
        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbar is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/UpdateAsync.cshtml", model);
            }

            navbar.Title = model.Title;
            navbar.Order = model.Order;
            navbar.Url = model.Url;
            navbar.IsBold = model.IsBold;
            navbar.IsShowHeader = model.IsShowHeader;
            navbar.IsShowFooter = model.IsShowFooter;

            await _dataContext.SaveChangesAsync();




            return RedirectToRoute("admin-navbar-list");





        }
        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }
    }
}
