using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore2.ViewModel
{
    public class ApplicationUserFormVM
    {
         public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        //   اهاي رح ابعتها لل داتا بيس عشان يعرف  اسم الرول الي يخزنه
        public List<string> SelectedRols { get; set; } = new List<string>();
        //the same!! ??
        public List<SelectListItem>? Rols { get; set; }


    }
}
