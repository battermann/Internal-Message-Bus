﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FunctionalExtensions;
using Movies.Commands;
using Movies.Contracts;

namespace Movies.Wpf
{
    public class MovieListViewModel : ViewModel
    {
        private readonly ICommandSender _commandSender;
        private readonly IMovieQueryFacade _movieRepository;
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

        public MovieListViewModel(ICommandSender commandSender, IMovieQueryFacade movieRepository)
        {
            _commandSender = commandSender;
            _movieRepository = movieRepository;
            _movies = new ObservableCollection<MovieViewModel>();
            Refresh();
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

        private ICommand _refresh;

        public ICommand RefreshCommand
        {
            get { return _refresh ?? (_refresh = new RelayCommand(p => Refresh())); }
        }

        private void Refresh()
        {
            _movies.Clear();
            _movieRepository.GetAll().ToList().ForEach(x => _movies.Add(new MovieViewModel(x.Id)
            {
                Title = x.Title,
                Genre = x.Genre,
                ReleaseDate = x.ReleaseDate.ToString("yyyy", CultureInfo.InvariantCulture),
                Price = x.Price
            }));
            _movies.CollectionChanged += MoviesOnCollectionChanged;
        }

        private void MovieViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var vm = sender as MovieViewModel;
            if (vm != null)
            {
                switch (args.PropertyName)
                {
                    case "Title":
                        var command = new ChangeMovieTitle(vm.Id, vm.Title);
                        _commandSender.Send(command);
                        break;
                }
            }
        }

        private void ExecuteAddMovieCommand()
        {
            var result = GetMovieFromInputDialog();

            result.Match(
                onChoice1Of2: x =>
                {
                    var createMovie = new CreateMovie(x.Id, x.Title, new DateTime(Int32.Parse(x.ReleaseDate), 1, 1), x.Genre, x.Price);
                    _commandSender.Send(createMovie);
                    _movies.Add(x);
                },
                onChoice2Of2: err =>
                {
                    if (err == MovieInputError.ParsingError)
                        MessageBox.Show("An error occurred. Wrong input.");
                });
        }

        private static Choice<MovieViewModel, MovieInputError> GetMovieFromInputDialog()
        {
            var input = new NewMovieDialog();
            input.ShowDialog();
            if (input.DialogResult == true)
                return input.Movie.Select(x =>
                    new MovieViewModel(Guid.NewGuid())
                    {
                        Title = x.Title,
                        Genre = x.Genre,
                        ReleaseDate = x.ReleaseDate.ToString("yyyy", CultureInfo.InvariantCulture),
                        Price = x.Price
                    });

            return Choice.NewChoice2Of2<MovieViewModel, MovieInputError>(MovieInputError.Canceled);
        }
    }

    public enum MovieInputError { Canceled, ParsingError }
}
