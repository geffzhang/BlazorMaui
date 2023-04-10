﻿// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf7WithWebview2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //使用本机功能将消息从 Web 内容传递到主机
            InitializeAsync();
        }

        //初始化 CoreWebView2 后，注册要响应WebMessageReceived的事件处理程序。 在 MainWindow.xaml.cs中，使用以下代码更新 InitializeAsync 和添加 UpdateAddressBar
        async void InitializeAsync()
        {
            //设置web用户文件夹 
            var browserExecutableFolder =   "c:\\wb2";
            webView.CreationProperties = new Microsoft.Web.WebView2.Wpf.CoreWebView2CreationProperties()
            {
                BrowserExecutableFolder = browserExecutableFolder 
            };

            webView.NavigationStarting += EnsureHttps;
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += UpdateAddressBar;

            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.postMessage(window.document.URL);");
            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");

            webView.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting; 

            //指定下载保存位置
            webView.CoreWebView2.Profile.DefaultDownloadFolderPath = @"C:\mytargetdowloadpath";
        }

        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var uri = args.TryGetWebMessageAsString();
            addressBar.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }

        void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            var uri = args.Uri;
            if (!uri.StartsWith("https://"))
            {
                args.Cancel = true;
            }
        }

        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            var downloadOperation = e.DownloadOperation;

            //指定下载后保存位置
            //e.ResultFilePath = @"C:\mytargetdowloadpath\mydownloadedfile.zip";
        }


        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate(addressBar.Text);
            }
        }
    }
}
