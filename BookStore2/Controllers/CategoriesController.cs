using BookStore2.Data;
using BookStore2.Models;
using BookStore2.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
       
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryVM);
            }
            var category = new Category()
            {
                Name = categoryVM.Name
            };

            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public IActionResult Edit (  )
        {
            //ما حددنا شو اسم الاكشن الي يروح عليه لما نعمل كبسة سب مت والتنين بودو عا كريت فيو فا باخد اسم ال اكشن الي وداه عليها هو بكتبه   
            return View("create");

        }
        [HttpPost]
        public IActionResult Edit ( CategoryVM categoryVm)
        {
            var category = context.Categories.Find(categoryVm.Id);
            if (category == null)
            {

            }
            category.Name = categoryVm.Name;
            context.SaveChanges();

            return RedirectToAction("Index");
            
        }
    }
}
