using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();

            //var chrome = WindowChrome.GetWindowChrome(this);
            //if (IsWindows11())
            //    chrome.CornerRadius = new CornerRadius(28);
            //else
            //    chrome.CornerRadius = new CornerRadius(0);

        }
        private async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.AddHostObjectToScript("csAPI", new JsBridge());
            webView.CoreWebView2.WebMessageReceived += (sender, e) =>
            {
                string msg = e.TryGetWebMessageAsString();
                MessageBox.Show(msg);
            };
            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "content", "index.html");
            webView.Source = new Uri(htmlPath);
        }

        [System.Runtime.InteropServices.ComVisible(true)]
        public class JsBridge
        {
            public void ShowMessage(string msg)
            {
                MessageBox.Show(msg);
            }
        }
        private void InDev(object sender, RoutedEventArgs e) { MessageBox.Show("InDev!"); }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Игнорируем клики по кнопкам внутри
            if (e.Source is Button) return;

            try
            {
                // Одинарный клик — перемещение окна
                this.DragMove();
            }
            catch (InvalidOperationException)
            {
                // Иногда DragMove выбрасывает исключение, если мышь не над окном — просто игнорируем
            }
        }

        private void Fullsize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
        }

        private void Close_Click(object sender, RoutedEventArgs e) {this.Close();}

        private void Minimize_Click(object sender, RoutedEventArgs e) { this.WindowState = WindowState.Minimized; }
    }
}
