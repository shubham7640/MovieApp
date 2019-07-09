using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MovieApplication.Models
{
    public class ShowData
    {
        [Key]
        public int Id { get; set; }
        public int MoviesId { get; set; }
        public string MovName { get; set; }
        
        public string Plot { get; set; }
        public string Poster { get; set; }
        public Nullable<System.DateTime> Realease { get; set; }
        public string ActName { get; set; }

    }
}