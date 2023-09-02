using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/movies
        [HttpGet]
        public IActionResult GetCollection()
        {
            var movies = _context.Movies.ToArray();

            return Ok(movies);
        }

        // GET api/movies/<id>
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null) return NotFound();

            return Ok(movie);
        }

        // POST api/movies
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Created($"/api/movies/{movie.Id}", movie);
        }

        // PUT api/movies/<id>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie updatedMovie)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null) return NotFound();

            movie.Title = updatedMovie.Title;
            movie.RunningTime = updatedMovie.RunningTime;
            movie.Genre = updatedMovie.Genre;
            _context.SaveChanges();

            return Ok(movie);
        }

        // DELETE api/movies/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null) return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges(true);

            return NoContent();
        }
    }
}