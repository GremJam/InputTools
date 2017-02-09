using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CursorWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point MausePosition => PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));
        readonly DispatcherTimer updater = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            InitializeCursorMonitoring();
        }

        private void InitializeCursorMonitoring()
        {
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            updater.Tick += delegate
            {
                Application.Current.MainWindow.CaptureMouse();
                if (point != Mouse.GetPosition(Application.Current.MainWindow))
                {
                    point = PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));
                    positionView.Content = $"{point.X}x{point.Y}";
                }
                Application.Current.MainWindow.ReleaseMouseCapture();
            };

            updater.Interval = new TimeSpan(0, 0, 0, 0, 100);
            updater.Start();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            updater.Stop();
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
            updater.Start();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            updater.Stop();
        }
    }
}