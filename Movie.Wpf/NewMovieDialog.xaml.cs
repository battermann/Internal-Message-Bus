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

        public Option<Movie> Movie
        {
            get
            {
                try
                {
                    return new Movie(TxtTitle.Text, new DateTime(Int32.Parse(TxtYear.Text), 1, 1),
                        TxtGenre.Text, decimal.Parse(TxtPrice.Text));
                }
                catch (Exception)
                {
                    return Option.None<Movie>();
                }
            }
        }
    }
}
