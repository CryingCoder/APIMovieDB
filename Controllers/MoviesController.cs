using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIMovieDB.Models;

namespace APIMovieDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDBContext _context;

        public MoviesController(MoviesDBContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        //GET: api/Movies/SearchByCategory
        [HttpGet("SearchByCategory/{category}")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchByCategory(string category)
        {
            return await _context.Movies.Where(m => m.Category.Contains(category)).ToListAsync();
        }


        //GET: api/Movies/GetRandomMovie
        [HttpGet("GetRandomMovie")]
        public async Task<ActionResult<Movie>> RandomMovie()
        {

            return RandomMovies();

        }

        //GET: api/Movies/GetRandomMovieByCategory
        [HttpGet("GetRandomMovieByCategory/{category}")]
        public async Task<ActionResult<Movie>> RandomCategory(string category)
        {
            var rnd = new Random();
            List<Movie> movies = _context.Movies.ToList();
            var moviecat = movies.Where(m => m.Category.Contains(category)).ToList();
            var id = rnd.Next(0, moviecat.Count);
            return moviecat[id];

        }



        /*[HttpGet("GetRandomMovieByQuantity/{quantity}")]
        public async Task<ActionResult<Movie>> RandomQuantity(int quantity)
        {
            var rnd = new Random();            
            List<Movie> movies = _context.Movies.ToList();
            int mcount = movies.Count;
            int id = rnd.Next(0, movies.Count());

            for (quantity = 0; quantity > 0; quantity++)
            {
                return movies[id];
            }
            
        }*/



        private Movie RandomMovies()
        {
            var rnd = new Random();
            List<Movie> movies = _context.Movies.ToList();
            var id = rnd.Next(0, movies.Count);
            
            return movies[id];
        }



        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
