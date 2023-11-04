using AutoMapper;
using Entities.Models;
using Shared.Dtos.MovieDtos;

namespace MovieApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(c => c.Genres,
                opt => opt.MapFrom(x => x.Genres.Split(' ', StringSplitOptions.RemoveEmptyEntries)));

            CreateMap<MovieForCreation, Movie>()
                .ForMember(m => m.Genres,
                opt => opt.MapFrom(x => string.Join(' ', x.Genres)));

            CreateMap<MovieForUpdate, Movie>()
                .ForMember(m => m.Genres,
                opt => opt.MapFrom(x => string.Join(' ', x.Genres)));

        }
    }
}
