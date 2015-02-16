using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Movies.Commands;
using Movies.Data;
using Movies.Infrastructure;
using Movies.Models;

namespace Movies.Wpf
{
    public class MovieListViewModel : ViewModel
    {
        private readonly ICommandSender _commandSender;
        private readonly IRepository _repository;
        private ObservableCollection<MovieViewModel> _movies;

        public ObservableCollection<MovieViewModel> Movies
        {
            get { return _movies; }
            set
            {
                if (Movies != value)
                {
                    _movies = value;
                    OnPropertyChanged("Movies");
                }
            }
        }

        public MovieListViewModel(ICommandSender commandSender, IRepository repository)
        {
            _commandSender = commandSender;
            _repository = repository;
            _movies = new ObservableCollection<MovieViewModel>();
            _repository.GetAll().ToList().ForEach(x => _movies.Add(new MovieViewModel(x)));
            _movies.CollectionChanged += MoviesOnCollectionChanged;
        }

        private void MoviesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MovieViewModel vm in e.NewItems)
                {
                    vm.PropertyChanged += MovieViewModelOnPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MovieViewModel vm in e.OldItems)
                {
                    // remove
                }
            }
        }

        private ICommand _addMovieCommand;

        public ICommand AddMovieCommand
        {
            get { return _addMovieCommand ?? (_addMovieCommand = new RelayCommand(p => ExecuteAddMovieCommand())); }
        }

        private void MovieViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var vm = sender as MovieViewModel;
            if (vm != null)
            {
                // create command
                // send command
            }
        }

        private void ExecuteAddMovieCommand()
        {
            var result = GetMovieFromInputDialog();

            if (result.Item1 == false)
                return;

            var movie = result.Item2;

            _movies.Add(new MovieViewModel(movie));

            _commandSender.Send(new CreateMovie
            {
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price
            });
        }

        private static Tuple<bool, Movie> GetMovieFromInputDialog()
        {
            var input = new NewAlbumDialog();
            input.ShowDialog();
            if(input.DialogResult == true)  
                return Tuple.Create(true, input.Movie);
            return Tuple.Create(false, (Movie)null);
        }
    }
}
