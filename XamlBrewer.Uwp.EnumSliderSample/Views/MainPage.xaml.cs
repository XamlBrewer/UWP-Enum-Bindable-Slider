using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.Uwp.Controls;
using XamlBrewer.Uwp.EnumSliderSample.ViewModels;

namespace XamlBrewer.Uwp.EnumSliderSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ViewModel = new MainPageViewModel();

            this.Loaded += MainPage_Loaded;
        }

        internal MainPageViewModel ViewModel { get; private set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.CodeBehindSlider.BindTo(ViewModel, "Importance");
        }
    }
}
