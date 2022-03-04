using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "AuthorName is required")]
		public string AuthorName { get; set; }
		[Required(ErrorMessage = "Genre is required")]
		public Genre Genre { get; set; }
		[Display(Name="Genre")]
		[Required(ErrorMessage = "GenreId is required")]
		public byte GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		[Display(Name="Release Date")]
		public DateTime ReleaseDate { get; set; }
		[Display(Name = "Number in Stock")]
		[Range(1, 20, ErrorMessage = "Number in stock must be between 1 and 20.")]
		public int NumberInStock { get; set; }
		[Required(ErrorMessage = "NumberAvailable is required")]
		public int NumberAvailable { get; set; }
	}
      
}
