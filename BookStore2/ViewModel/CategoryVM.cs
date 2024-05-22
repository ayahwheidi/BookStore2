using System.ComponentModel.DataAnnotations;

namespace BookStore2.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Plz insert the name of Category")]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
    }
}
