using System;
using Movies.Models;

namespace Movies.Wpf
{
    public class MovieViewModel : ViewModel
    {
        private string _title;
        private DateTime _releaseDate;
        private string _genre;
        private decimal _price;
        private readonly Guid _id;

        public MovieViewModel(Guid id)
        {
            _id = id;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (Title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string ReleaseDate
        {
            get { return _releaseDate.ToString("yyyy"); }
            set
            {
                if (ReleaseDate != value)
                {
                    _releaseDate = new DateTime(Int32.Parse(value), 1, 1);
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                if (Genre != value)
                {
                    _genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (Price != value)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public Guid Id
        {
            get { return _id; }
        }
    }
}
