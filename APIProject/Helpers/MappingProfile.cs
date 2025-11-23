using APIProject.Dtos;
using APIProject.Models;
using AutoMapper;

namespace APIProject.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie , MovieDetailsDto>();  // To Get all details in movie details Dto class
            CreateMap<CreateMovie, Movie>().ForMember(Src => Src.Poster, opt => opt.Ignore()); // to ignore the fileform of poster 
        }
    }
}
