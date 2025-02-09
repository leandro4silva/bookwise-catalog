using AutoMapper;
using BookWise.Catalog.Application.Handlers.v1.CreateBook;
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
    }
}