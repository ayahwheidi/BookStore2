using AutoMapper;
using BookStore2.Models;
using BookStore2.ViewModel;

namespace BookStore2.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
           // CreateMap<CategoryVM, Category>();
        }
    }
}
