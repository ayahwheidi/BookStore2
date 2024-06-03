using BookStore2.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore2.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; }=null!;
        public string publisher { get; set; } = null!;
        public DateTime publishDate { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Categories { get; set; }

    }
}
