using AutoMapper;
using BookWise.Catalog.Application.Handlers.v1.CreateBook;
using BookWise.Catalog.Application.Handlers.v1.DeleteBook;
using BookWise.Catalog.Application.Handlers.v1.GetBookById;
using BookWise.Catalog.Application.Handlers.v1.UpdateBookCover;
using BookWise.Catalog.Domain.Entities;

namespace BookWise.Catalog.Application.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        _ = CreateMap<CreateBookCommand, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Payload!.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Payload!.Description))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Payload!.Isbn))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Payload!.Publisher))
            .ForMember(dest => dest.GenreBook, opt => opt.MapFrom(src => src.Payload!.GenreBook))
            .ForMember(dest => dest.NumberOfPages, opt => opt.MapFrom(src => src.Payload!.NumberOfPages))
            .ForMember(dest => dest.YearOfPublish, opt => opt.MapFrom(src => src.Payload!.YearOfPublish))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Payload!.CreatedDate));

        _ = CreateMap<Book, CreateBookResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        _ = CreateMap<Book, UpdateBookCoverResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.GenreBook, opt => opt.MapFrom(src => src.GenreBook))
            .ForMember(dest => dest.NumberOfPages, opt => opt.MapFrom(src => src.NumberOfPages))
            .ForMember(dest => dest.YearOfPublish, opt => opt.MapFrom(src => src.YearOfPublish))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));
        
        _ = CreateMap<Book, DeleteBookResult>()
            .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => true));
        
        _ = CreateMap<Book, GetBookByIdResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.GenreBook, opt => opt.MapFrom(src => src.GenreBook))
            .ForMember(dest => dest.NumberOfPages, opt => opt.MapFrom(src => src.NumberOfPages))
            .ForMember(dest => dest.YearOfPublish, opt => opt.MapFrom(src => src.YearOfPublish))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));
            
    }
}