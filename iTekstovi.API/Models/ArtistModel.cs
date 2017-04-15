using System;
using System.ComponentModel.DataAnnotations;
using iTekstovi.API.App_Classes;

namespace iTekstovi.API.Models
{
    public class ArtistModel
    {
        [ColumnName("id")]
        public int Id { get; set; }

        [Required]
        [ColumnName("first_name")]
        public string FirstName { get; set; }

        [Required]
        [ColumnName("last_name")]
        public string LastName { get; set; }

        [ColumnName("about")]
        public string About { get; set; }

        [ColumnName("created")]
        public DateTime Created { get; set; }

        [ColumnName("updated")]
        public DateTime? Updated { get; set; }
    }
}