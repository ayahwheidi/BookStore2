using AutoMapper;
using BookStore2.Data;
using BookStore2.Models;
using BookStore2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriesController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
       
        public IActionResult Index()

        {
            var categories = context.Categories.ToList();

            // auto mapper method
            var viewModel = mapper.Map<List<CategoryVM>>(categories);


            /*var categoriesVm = categories.Select(category => new CategoryVM
            {
                Id=category.Id,
                Name=category.Name,
                CreatedOn=category.CreatedOn,
                UpdatedOn=category.UpdatedOn

            }).ToList();*/
            return View(viewModel);
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
            {//باخد معه الايرور الي عمله  ال بموجودة في categoryVM الي انا كتبتها اصلا غلط
                return View("Create", categoryVM);
            }

            //by Auto mapper

            var category = mapper.Map<Category>(categoryVM);
            /*var category = new Category()
            {
                Name = categoryVM.Name
            };*/
            try
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                // الهون ال "" خلتني لما يصير في ايرور يضيف عليه هاد validation error
                ModelState.AddModelError("Name", " the Category name is already exsista");
                return View("Create", categoryVM);
            }
            
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
        public IActionResult checkName(CategoryVM categoryVm)
        {
            var isExsist = context.Categories.Any(e => e.Name == categoryVm.Name);
            return Json(isExsist);
        }

    }
}
