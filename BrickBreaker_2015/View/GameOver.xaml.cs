using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BrickBreaker_2015.ViewModel;

namespace BrickBreaker_2015.View
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        OptionsViewModel optionsViewModel;

        public GameOver()
        {
            InitializeComponent();

            optionsViewModel = new OptionsViewModel();
            this.Width = optionsViewModel.HorizontalScaleNumber;
            this.Height = optionsViewModel.VerticalScaleNumber;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = new MainWindow();
            this.DialogResult = true;
            parent.ShowDialog();
        }
    }
}
