using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var chrome = WindowChrome.GetWindowChrome(this);
            if (IsWindows11())
                chrome.CornerRadius = new CornerRadius(28);
            else
                chrome.CornerRadius = new CornerRadius(0);
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private bool IsWindows11()
        {
            Version v = Environment.OSVersion.Version;
            return v.Major == 10 && v.Build >= 22000;
        }

        private void Close_Click(object sender, RoutedEventArgs e) {this.Close();}

        private void Minimize_Click(object sender, RoutedEventArgs e) { this.WindowState = WindowState.Minimized; }
    }
}
