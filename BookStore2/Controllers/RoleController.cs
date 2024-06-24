using BookStore2.Data;
using BookStore2.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore2.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManeger;
        //عملنا object بدون استخدما ال new
        public RoleController (ApplicationDbContext _context,RoleManager<IdentityRole> _RoleManeger )
        {
            context = _context;
            roleManeger = _RoleManeger;
        }

        public async Task <IActionResult> Index()
        {
            // var role = context.Roles.ToList();
            var rols = await roleManeger.Roles.ToListAsync();
            var rolevm = rols.Select(role => new RoleVM
            {
                Name = role.Name
            }).ToList();
            return View(rolevm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(RoleVM rolevm)
        {
            if (!ModelState.IsValid)
            {
                return View(rolevm);
            }
            var result = await roleManeger.CreateAsync(new IdentityRole { Name = rolevm.Name });
            return RedirectToAction("Index");
        }
    }
}
