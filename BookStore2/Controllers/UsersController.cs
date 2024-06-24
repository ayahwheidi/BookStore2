using BookStore2.Data;
using BookStore2.Models;
using BookStore2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Controllers
{
    [Authorize (Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManeger;

        public UsersController( ApplicationDbContext _context,RoleManager<IdentityRole> _RoleManager,UserManager<ApplicationUser> _UserManeger)
        {
            context = _context;
            roleManager = _RoleManager;
            userManeger = _UserManeger;
        }
        public async Task< IActionResult> Index()
        {
            var users = await userManeger.Users.ToListAsync();
            var userViewModels = new List<ApplicationUserVM>();
            foreach(var user in users)
            {
                var roles = await userManeger.GetRolesAsync(user);
                var userVM = new ApplicationUserVM()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Rols = roles.ToList()
                };
                userViewModels.Add(userVM);
            }
            return View(userViewModels);
           /* var userVM = users.Select(user => new ApplicationUserVM
            {
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
            }).ToList();*/
            //return View(userVM);
        }
        [HttpGet]
        public async Task <IActionResult> Create()
        {

            var rols = await roleManager.Roles.ToListAsync();
            var viewModel = new ApplicationUserFormVM
            {
                Rols = rols.Select(role => new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList(),
            };
              
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserFormVM uservm)
        {
            if (!ModelState.IsValid)
            {
                return View(uservm);
            }
            var user = new ApplicationUser
            {
                UserName=uservm.UserName,
                Email = uservm.Email,
                Address = uservm.Address,
                PhoneNumber = uservm.PhoneNumber
            };
            var result = await userManeger.CreateAsync(user, uservm.PasswordHash);
            if (!result.Succeeded)
            {
                return View(uservm);
            }
            await userManeger.AddToRolesAsync(user, uservm.SelectedRols);
               return RedirectToAction("Index");
            

        }
    }
}
