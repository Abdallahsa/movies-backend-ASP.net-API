using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie,ShowMovieDto>();
            CreateMap<Genre, CreateGenreDto>();


        }
    }
}
