using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string AuthorName { get; set; }

        public GenreDto Genre { get; set; }

        public int GenreId { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int NumberInStock { get; set; }
    }
}
