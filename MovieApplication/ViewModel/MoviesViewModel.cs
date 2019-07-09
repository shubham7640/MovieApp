using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApplication.Models;

namespace MovieApplication.ViewModel
{
    public class MoviesViewModel
    {
        public Movies Movies { get; set; }
        public IEnumerable<SelectListItem> AllActors { get; set; }

        private List<int> _selectedActors;
        public List<int> SelectedActors
        {
            get
            {
                if (_selectedActors == null)
                {
                    //_selectedActors = Movies.Actors.Select(m => m.Id).ToList();
                }
                return _selectedActors;
            }
            set { _selectedActors = value; }
        }
    }
}