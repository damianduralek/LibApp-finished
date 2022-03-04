using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres() 
        {
            return _context.Genre;
         }

        public void AddGenre(Genre genre)
        {
            _context.Genre.Add(genre);
        }

        public void DeleteGenre(int genreId)
        {
            _context.Genre.Remove(GetGenreById(genreId));
        }

        public Genre GetGenreById(int genreId)
        {
            return _context.Genre.Find(genreId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            _context.Genre.Update(genre);
        }

    }
}