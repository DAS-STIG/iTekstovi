using System;
using System.ComponentModel.DataAnnotations;
using iTekstovi.API.AppClasses;

namespace iTekstovi.API.Models 
{
    public class SongModel 
    {    
        [ColumnName("id")]
        public Guid Id { get; set; }

        [Required]
        [ColumnName("name")]
        public string Name { get; set; }

        [Required]
        [ColumnName("lyrics")]
        public string Lyrics { get; set; }

        [ColumnName("description")]
        public string Description { get; set; }

        [ColumnName("created")]
        public DateTime Created { get; set; }

        [ColumnName("updated")]
        public DateTime? Updated { get; set; }

        [ColumnName("is_visible")]
        public bool IsVisible { get; set; }

        [Required]
        [ColumnName("artist_id")]
        public int ArtistId { get; set;}
    }
}
