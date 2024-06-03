using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore2.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Plz insert the name of Category")]
        [MaxLength(30)]
        //[Remote("checkName", ErrorMessage = "White space is not allowed.")]
       // [Remote(null, "checkName", ErrorMessage = "White space is not allowed.")]
        public string Name { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
