using BookStore2.Data;
using BookStore2.Models;
using BookStore2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore2.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorsController (ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var authors = context.Autors.ToList();
            // var authorsVm = new List<AuthorVM>();

            var authorsVm = authors.Select(author => new AuthorVM
            {
                Name = author.Name,
                Id = author.Id,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn
            }).ToList();
              
            /*foreach(var item in authors)
            {
                var authorVm = new AuthorVM()
                {
                    Name = item.Name,
                    Id = item.Id,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn
                };
                authorsVm.Add(authorVm);
            }*/


            return View(authorsVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }
        [HttpPost]
        public IActionResult Create(AuthorFormVM authorVm)
        {
            if (!ModelState.IsValid)
            {//باخد معه الايرور الي عمله  ال بموجودة في categoryVM الي انا كتبتها اصلا غلط
                return View("Form", authorVm);
            }

            var author = new Author()
            {
                Name =authorVm.Name,
            };
            try
            {
                context.Autors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                // الهون ال "" خلتني لما يصير في ايرور يضيف عليه هاد validation error
             //   ModelState.AddModelError("Name", " the Author name is already exsista");
                return View("Form", authorVm);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            //  return Content($"{id}");
            //ما حددنا شو اسم الاكشن الي يروح عليه لما نعمل كبسة سب مت والتنين بودو عا كريت فيو فا باخد اسم ال اكشن الي وداه عليها هو بكتبه
            //
            // بدي اجيب الداتا من الدالتا بيس عشان اخد الاسم واحطه جوا الانبت لما يعمل ابديت
            var author = context.Autors.Find(id);
            if (author is null)
            {
                return NotFound();
            }
            var ViewModel = new AuthorFormVM()
            {
                Id = id,
                Name = author.Name
            };
            //هون بعتنا ال رفيومودل عشان يحط القيمو جاو الانبت
            return View("Form", ViewModel);

        }
        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", authorVm);

            }
            var auther = context.Autors.Find(authorVm.Id);
            if (auther == null)
            {
                return NotFound();

            }
            auther.Name = authorVm.Name;
            context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            var auther = context.Autors.Find(id);
            if (auther is null)
            {
                return NotFound();
            }
            var viewModel = new AuthorVM()
            {
                Name = auther.Name,
                CreatedOn = auther.CreatedOn,
                UpdatedOn = auther.UpdatedOn
            };

            return View(viewModel);
        }


      
        public IActionResult Delete(int Id)
        {
            var author = context.Autors.Find(Id);
            if (author is null)
            {
                return NotFound();
            }
            context.Remove(author);
            context.SaveChanges();
             return RedirectToAction("Index");
           // return Ok();
        }
    }
}
