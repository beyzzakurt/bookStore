using AutoMapper;
using PatikaProject.BookOperations.CreateBook;
using PatikaProject.BookOperations.GetBookById;
using PatikaProject.BookOperations.GetBooks;
using PatikaProject.Entity;

namespace PatikaProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, GetByIdViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}