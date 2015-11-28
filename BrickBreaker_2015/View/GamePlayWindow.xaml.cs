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
using BrickBreaker_2015.Model;
using System.Windows.Threading;

namespace BrickBreaker_2015.View
{
    /// <summary>
    /// Interaction logic for GamePlayWindow.xaml.
    /// </summary>
    public partial class GamePlayWindow : Window
    {
        #region Fields

        // The error log viewmodel.
        private ErrorLogViewModel errorLogViewModel;

        // The game ViewModel.
        private NewGameViewModel newGameViewModel;

        // The options ViewModel.
        private OptionsViewModel optionsViewModel;

        // The dispacher timer.
        private DispatcherTimer timer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePlayWindow"/> class.
        /// </summary>
        public GamePlayWindow()
        {
            try
            {
                InitializeComponent();

                errorLogViewModel = new ErrorLogViewModel();
                //canvas.Background = new SolidColorBrush(Colors.White);

                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                timer.Tick += Timer_Tick;
                //timer.Start();
            }
            catch
            { }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the KeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                newGameViewModel.KeyDown(e, ref timer);
            }
            catch (Exception error)
            {
                errorLogViewModel.LogError(error);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                newGameViewModel.KeyUp(e);
            }
            catch (Exception error)
            {
                errorLogViewModel.LogError(error);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                newGameViewModel.MouseMove(canvas, e);
            }
            catch (Exception error)
            {
                errorLogViewModel.LogError(error);
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                newGameViewModel.MouseLeftButtonDown(e, ref timer);
            }
            catch (Exception error)
            {
                errorLogViewModel.LogError(error);
            }
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                newGameViewModel.MoveObjects();
                newGameViewModel.BallAtContact();
                newGameViewModel.RacketAtContactWithBonus();
                newGameViewModel.CheckForGameOver(ref timer);
            }
            catch (Exception error)
            {
                errorLogViewModel.LogError(error);
            }
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            newGameViewModel = new NewGameViewModel(canvas.ActualWidth, canvas.ActualHeight);
            optionsViewModel = new OptionsViewModel();
            this.DataContext = newGameViewModel;
            newGameViewModel.PresetValues();

            foreach (var item in newGameViewModel.BallList)
            {
                Ball ujlabda = item;

                Ellipse ujLabdaEllipse = new Ellipse();
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri(item.ImagePath,UriKind.Relative));
                ujLabdaEllipse.Fill = myBrush;
                canvas.Children.Add(ujLabdaEllipse);

                Binding widthBinding = new Binding("Area.Width");
                widthBinding.Source = ujlabda;
                ujLabdaEllipse.SetBinding(Ellipse.WidthProperty, widthBinding);

                Binding heightBinding = new Binding("Area.Height");
                heightBinding.Source = ujlabda;
                ujLabdaEllipse.SetBinding(Ellipse.HeightProperty, heightBinding);

                Binding xBinding = new Binding("Area.X");
                xBinding.Source = ujlabda;
                ujLabdaEllipse.SetBinding(Canvas.LeftProperty, xBinding);

                Binding yBinding = new Binding("Area.Y");
                yBinding.Source = ujlabda;
                ujLabdaEllipse.SetBinding(Canvas.TopProperty, yBinding);
            }
            foreach (var item in newGameViewModel.BrickList)
            {
                Brick ujbrick = item;

                Rectangle ujBrickRect = new Rectangle();
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri(item.ImagePath, UriKind.Relative));
                ujBrickRect.Fill = myBrush;

                Binding widthBinding = new Binding("Area.Height");
                widthBinding.Source = ujbrick;
                ujBrickRect.SetBinding(Rectangle.WidthProperty, widthBinding);

                Binding heightBinding = new Binding("Area.Width");
                heightBinding.Source = ujbrick;
                ujBrickRect.SetBinding(Rectangle.HeightProperty, heightBinding);

                Binding xBinding = new Binding("Area.X");
                xBinding.Source = ujbrick;
                ujBrickRect.SetBinding(Canvas.LeftProperty, xBinding);

                Binding yBinding = new Binding("Area.Y");
                yBinding.Source = ujbrick;
                ujBrickRect.SetBinding(Canvas.TopProperty, yBinding);
                canvas.Children.Add(ujBrickRect);

            }

            foreach (var item in newGameViewModel.RacketList)
            {
                Racket racket = item;

                Rectangle ujracket = new Rectangle();
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri(item.ImagePath, UriKind.Relative));
                ujracket.Fill = myBrush;

                Binding widthBinding = new Binding("Area.Width");
                widthBinding.Source = racket;
                ujracket.SetBinding(Rectangle.WidthProperty, widthBinding);

                Binding heightBinding = new Binding("Area.Height");
                heightBinding.Source = racket;
                ujracket.SetBinding(Rectangle.HeightProperty, heightBinding);

                Binding xBinding = new Binding("Area.X");
                xBinding.Source = racket;
                ujracket.SetBinding(Canvas.LeftProperty, xBinding);

                Binding yBinding = new Binding("Area.Y");
                yBinding.Source = racket;
                ujracket.SetBinding(Canvas.TopProperty, yBinding);
                canvas.Children.Add(ujracket);
            }
        }

        #endregion Methods
    }
}
