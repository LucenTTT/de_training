using AutoMapper;
using de_training.DTOs;

namespace de_training.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Book, GetBookDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(t => t.BookId));
            CreateMap<PostBookDto, Book>();
        }
    }
}
