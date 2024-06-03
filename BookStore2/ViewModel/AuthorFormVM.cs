using System.ComponentModel.DataAnnotations;

namespace BookStore2.ViewModel
{
    public class AuthorFormVM
    {
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage ="the name field can't exceed 50 characters")]
        public string Name { get; set; } = null!;
     
    }
}
