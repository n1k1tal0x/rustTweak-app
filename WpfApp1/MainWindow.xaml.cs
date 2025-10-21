using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using Microsoft.Web.WebView2.Core;
using System.IO;

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

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //private bool IsWindows11()
        //{
        //    Version v = Environment.OSVersion.Version;
        //    return v.Major == 10 && v.Build >= 22000;
        //}

        private void Close_Click(object sender, RoutedEventArgs e) {this.Close();}

        private void Minimize_Click(object sender, RoutedEventArgs e) { this.WindowState = WindowState.Minimized; }
    }
}
