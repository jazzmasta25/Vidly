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
        public Movies Movies { get; set; }
        public IEnumerable<MovieGenre> MovieGenres { get; set; }
        }
}