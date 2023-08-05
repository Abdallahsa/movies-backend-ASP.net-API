using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
      
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        
        

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GitAll();
            return Ok(genres);
           
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };
             await _genresService.Create(genre);
            return Ok(genre);
            
            
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] CreateGenreDto dto)
        {
            var genre =await _genresService.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = dto.Name;
             _genresService.Update(genre);
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre =await _genresService.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            _genresService.Delete(genre);
            
            return Ok(genre);

        }   
    }
}
