using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        private readonly IMoviceService _moviceService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;
        private new List<string> _AllowedExtention=new List<string> {".png",".jpg"};
        private long _maxSize = 1024*1024;
        public MoviesController(IMoviceService moviceService, IGenresService genresService, IMapper mapper)
        {
            _moviceService = moviceService;
            _genresService = genresService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GitAllAsync()
        {
            var movies = await _moviceService.GetAll();
            var x = _mapper.Map<IEnumerable<ShowMovieDto>>(movies);
            return Ok(x);
             
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviceService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        [HttpGet("GetByIdGenre/{id}")]
        public async Task<IActionResult> GetByIdGenreAsync(int id)
        {
          var genre=await _moviceService.GetAll(id);
            
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateMovieDto dto)
        {
            if (!_AllowedExtention.Contains(Path.GetExtension(dto.poster.FileName.ToLower()))){
                return BadRequest(" allow extention only file .png or .jpg");
            }
            if (dto.poster.Length > _maxSize)
            {
                return BadRequest("the maximum size is 1M ");
            }
            var _isValid = await _genresService.Isvalid(dto.GenreId);

            if (!_isValid)
            {
                return BadRequest("the genre is not valid");
            }
            using var dataStream = new MemoryStream();
            await dto.poster.CopyToAsync(dataStream);
            var movie = new Movie
            {
                Title = dto.Title,
                GenreId = dto.GenreId,
                Year = dto.Year,
                rate=dto.rate,
                Storeline = dto.Storeline,
                poster = dataStream.ToArray()
            };
            await _moviceService.Create(movie);
            
            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateMovieDto dto)
        {
            var movie = await _moviceService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            if(dto.poster != null)
            {
                if (!_AllowedExtention.Contains(Path.GetExtension(dto.poster.FileName.ToLower())))
                {
                    return BadRequest(" allow extention only file .png or .jpg");
                }
                if (dto.poster.Length > _maxSize)
                {
                    return BadRequest("the maximum size is 1M ");
                }
                using var dataStream = new MemoryStream();
                await dto.poster.CopyToAsync(dataStream);
                movie.poster = dataStream.ToArray();
            }
            var _isValid = await _genresService.Isvalid(id);
            if (!_isValid)
            {
                return BadRequest("the genre is not valid");
            }
            
            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;
            movie.rate = dto.rate;
            movie.Storeline = dto.Storeline;
            _moviceService.Update(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviceService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            _moviceService.Delete(movie);
            
            return Ok(movie);
        }
    }
}
