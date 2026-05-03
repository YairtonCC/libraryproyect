using AutoMapper;
using Library.Domain.DTOs;
using Library.Domain.Enities;
using Library.Domain.Enities.Library.Domain.Entities;
using Library.Domain.Entities;

namespace LibraryProyect.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Authors
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();

            // Books
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();

            // Categories
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            // Members
            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberDto, Member>();

            // Loans
            CreateMap<Loan, LoanDto>();
            CreateMap<CreateLoanDto, Loan>();

            // BookCategories
            CreateMap<BookCategory, BookCategoryDto>();
            CreateMap<CreateBookCategoryDto, BookCategory>();
        }
    }
}
