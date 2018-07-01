﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movies
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public MovieGenre MovieGenre { get; set; }

        [Display(Name = "Number in Stock")]
        public int Stock { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte MovieGenreId { get; set; } //MUST BE TYPE BYTE ! ! !! !
    }

    //movies/random
}