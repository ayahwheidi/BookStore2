using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore2.ViewModel
{
    public class ApplicationUserVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        //   اهاي رح ابعتها لل داتا بيس عشان يعرف  اسم الرول الي يخزنه
        public List<string>? Rols { get; set; } 
       
      
    }
}
