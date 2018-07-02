using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<MovieGenre> MovieGenres { get; set; }

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        public int? Stock { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? MovieGenreId { get; set; } //MUST BE TYPE BYTE ! ! !! !

        public MovieFormViewModel()
        {
            Id = 0; //default Id to 0 instead of null
            //ReleaseDate = DateTime.Now; // We can set defaults here.
        }

        public MovieFormViewModel(Movies movie) //when we have movie, we set it
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            Stock = movie.Stock;
            MovieGenreId = movie.MovieGenreId;
        }


    }
}