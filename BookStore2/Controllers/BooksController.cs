using BookStore2.Data;
using BookStore2.Models;
using BookStore2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore2.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController( ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        
        //[AllowAnonymous]
        public IActionResult Index()
        {
            var books = context.Books.
                Include(book=>book.Author).
                Include(book => book.Categories).
                ThenInclude(book => book.Category).
                ToList();
            var bookVms = books.Select(book => new BookVM
            {
Id=book.Id,
Title=book.Title,
Author=book.Author.Name,
publisher=book.publisher,
ImageUrl=book.ImageUrl,
Categories=book.Categories.Select(e=>e.Category.Name).ToList()
            }).ToList();

            return View(bookVms);

            /* foreach(var item in books)
             {

                 Console.WriteLine($"Title:{item.Title}...{item.Author.Name}...");
                 foreach (var item2 in item.Categories)
                 {
                     Console.WriteLine(item2.Category.Name);
             }
             }*/


        }
       
        public IActionResult Create()
        {
            var authors = context.Autors.ToList();
            var categories = context.Categories.ToList();
            var AuthorsList = new List<SelectListItem>();
            foreach(var author in authors)
            {
                AuthorsList.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = author.Name
                });
            }

            var categpryList = new List<SelectListItem>();
            foreach(var item in categories)
            {
                categpryList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }

            var viewModel = new BooksFormVM
            {
                Authors = AuthorsList,
                Categories= categpryList

            };
            return View(viewModel);
            
        }
       
        public IActionResult Store(BooksFormVM viewModel)
        {
            string ImageName = null;
            if (!ModelState.IsValid)
            {
                return View ("Create", viewModel);
            }
            if (viewModel.ImageUrl != null)
            {
                 ImageName = Path.GetFileName(viewModel.ImageUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/book_img",ImageName);
                var stream = System.IO.File.Create(path);
                viewModel.ImageUrl.CopyTo(stream);
            }
            var book = new Book
            {
                AuthorId = viewModel.AuthorId,
                Title = viewModel.Title,
                publisher = viewModel.publisher,
                Description = viewModel.Description,
                ImageUrl = ImageName,
                Categories = viewModel.SelectedCategories.Select(id => new BookCategory
                {
                    CategoryId = id
                }).ToList()
            };

            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
          
        }

        public IActionResult Delete(int id)
        {
            //return Content($"{id}");
            var book = context.Books.Find(id);
          /*  var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/book_img", book.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }*/

            context.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
