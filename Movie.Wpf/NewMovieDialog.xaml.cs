using System;
using System.Windows;
using Movies.Models;

namespace Movies.Wpf
{
    /// <summary>
    /// Interaction logic for NewAlbumDialog.xaml
    /// </summary>
    public partial class NewAlbumDialog : Window
    {
        public NewAlbumDialog()
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

        public Movie Movie
        {
            get
            {
                return new Movie(TxtTitle.Text, new DateTime(Int32.Parse(TxtYear.Text), 1, 1),
                    TxtGenre.Text, decimal.Parse(TxtPrice.Text));
            }
        }
    }
}
