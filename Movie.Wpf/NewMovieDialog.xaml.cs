using System;
using System.Windows;
using FunctionalExtensions;
using Movies.Models;

namespace Movies.Wpf
{
    /// <summary>
    /// Interaction logic for NewAlbumDialog.xaml
    /// </summary>
    public partial class NewMovieDialog : Window
    {
        public NewMovieDialog()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            TxtTitle.Focus();
        }

        public Choice<Movie, MovieInputError> Movie
        {
            get
            {
                try
                {
                    var movie = new Movie(TxtTitle.Text, new DateTime(Int32.Parse(TxtYear.Text), 1, 1), TxtGenre.Text, decimal.Parse(TxtPrice.Text));
                    return Choice.NewChoice1Of2<Movie, MovieInputError>(movie);
                }
                catch (Exception)
                {
                    return Choice.NewChoice2Of2<Movie, MovieInputError>(MovieInputError.ParsingError);
                }
            }
        }
    }
}
