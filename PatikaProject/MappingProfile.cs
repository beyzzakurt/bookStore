using AutoMapper;
using PatikaProject.Application.AuthorOperations.Commands.CreateAuthor;
using PatikaProject.Application.AuthorOperations.Commands.UpdateAuthor;
using PatikaProject.Application.AuthorOperations.Queries.GetAuthorById;
using PatikaProject.Application.AuthorOperations.Queries.GetAuthors;
using PatikaProject.Application.BookOperations.Commands.CreateBook;
using PatikaProject.Application.BookOperations.Queries.GetBookById;
using PatikaProject.Application.BookOperations.Queries.GetBooks;
using PatikaProject.Application.GenreOperations.Queries.GetGenreDetail;
using PatikaProject.Application.GenreOperations.Queries.GetGenres;
using PatikaProject.Application.UserOperations.Commands.CreateUser;
using PatikaProject.Entity;

namespace PatikaProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Book - DTO eşlemeleri
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, GetByIdViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name)); 

            // Genre - DTO eşlemeleri
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            // Author - DTO eşlemeleri
            CreateMap<CreateAuthorViewModel, Author>();
            CreateMap<UpdateAuthorViewModel, Author>();

            CreateMap<Author, AuthorViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title)));
            CreateMap<Author, AuthorByIdViewModel>()
                 .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title)));

            // User - DTO eşlemeleri
            CreateMap<CreateUserModel, User>();
        }
    }
}