using System;
using System.Collections.Generic;

namespace APIMovieDB.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? Runtime { get; set; }
    }
}
