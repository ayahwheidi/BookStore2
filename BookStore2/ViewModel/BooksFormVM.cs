using BookStore2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore2.ViewModel
{
    public class BooksFormVM
    {
         public int Id { get; set; }
        [MaxLength(50,ErrorMessage ="max length 50 charactor")]
        public string Title { get; set; }
        [Display(Name ="Author")]
        public int AuthorId { get; set; }
        //حطينا علامة تعجب لانه هو بكون يستنى اوثر اي دي فا رح يعطيني ايرور
        public List<SelectListItem>? Authors { get; set; }
        public string publisher { get; set; } = null!;
        [Display(Name = "publish Date")]
        public DateTime publishDate { get; set; } = DateTime.Now;

        [Display(Name = "plz choose Your Image")]
       public IFormFile? ImageUrl { get; set; }
        public string Description { get; set; } = null!;

        [Display(Name = "Select Categories")]
        //هاي رح ابعتها لل داتا بيس عشان يعرف ال اي دي للكاتيجوري
        public List<int> SelectedCategories { get; set; }
        //the same!! ??
        public List<SelectListItem>? Categories { get; set; }
        

    }
}
