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
        public IActionResult Edit ( int id  )
        {

            //  return Content($"{id}");
            //ما حددنا شو اسم الاكشن الي يروح عليه لما نعمل كبسة سب مت والتنين بودو عا كريت فيو فا باخد اسم ال اكشن الي وداه عليها هو بكتبه
            //
            // بدي اجيب الداتا من الدالتا بيس عشان اخد الاسم واحطه جوا الانبت لما يعمل ابديت
            var category = context.Categories.Find(id);
            if(category is null)
            {
                return NotFound();
            }
            var ViewModel = new CategoryVM
            {
                Id = id,
                Name = category.Name
            };
            //هون بعتنا ال رفيومودل عشان يحط القيمو جاو الانبت
            return View("create",ViewModel);

        }
        [HttpPost]
        public IActionResult Edit ( CategoryVM categoryVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryVm);
                
            }
            var category = context.Categories.Find(categoryVm.Id);
            if (category == null)
            {
                return NotFound();

            }
            category.Name = categoryVm.Name;
            context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        public IActionResult Details(int id)
        {
            var category = context.Categories.Find(id);
            if(category is null)
            {
                return NotFound();
            }
            var viewModel = new CategoryVM
            {
                Id = id,
                Name = category.Name,
                CreatedOn=category.CreatedOn,
                UpdatedOn=category.UpdatedOn
            };

            return View(viewModel);
        }
        public IActionResult Delete(int Id)
        {
            var category = context.Categories.Find(Id);
            if(category is null)
            {
                return NotFound();
            }
            context.Remove(category);
            context.SaveChanges();
            // return RedirectToAction("Index");
            return Ok();
        }

    }
}
