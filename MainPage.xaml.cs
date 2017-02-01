using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string presentTemp = " ";
            string  presentDesc= " ";
            TextBlock myLocation = new TextBlock();
            try
            {
                var position = await LocationManager.GetLocationPosition();
                RootObject myWeather = await OpenWaetherAPI.GetWeather(position.Coordinate.Latitude,
                    position.Coordinate.Longitude);

                myLocation.FontSize = 56;
                myLocation.VerticalAlignment = VerticalAlignment.Center;
                myLocation.HorizontalAlignment = HorizontalAlignment.Center;
                myLocation.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                myLocation.Text = myWeather.city.name;

                BackgroundStack.Children.Add(myLocation);



                for (int i = 0; i < myWeather.list.Count; i++)
                {

                    if (i == 0)
                    {

                        StackPanel myStackPresent = new StackPanel();
                        myStackPresent.Margin = new Thickness(10, 10, 0, 10);

                        TextBlock myTextPresent = new TextBlock();
                        myTextPresent.Text = myWeather.list[i].main.temp.ToString();
                        myTextPresent.FontSize = 56;
                        presentTemp = myTextPresent.Text;
                        myTextPresent.Margin = new Thickness(0, 0, 10, 0);
                        myTextPresent.VerticalAlignment = VerticalAlignment.Center;
                        myTextPresent.Foreground = new SolidColorBrush(Windows.UI.Colors.White);


                        string iconPresent = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[i].weather[0].icon);
                        Image ResultImagePresent = new Image();
                        ResultImagePresent.Margin = new Thickness(10, 0, 10, 0);
                        ResultImagePresent.Height = 100;
                        ResultImagePresent.Width = 100;
                        ResultImagePresent.Source = new BitmapImage(new Uri(iconPresent, UriKind.Absolute));


                        TextBlock myTextDescPresent = new TextBlock();
                        myTextDescPresent.Text = myWeather.list[i].weather[0].description;
                        myTextDescPresent.FontSize = 20;
                        presentDesc = myTextDescPresent.Text;
                        myTextDescPresent.Margin = new Thickness(10, 0, 10, 0);
                        myTextDescPresent.VerticalAlignment = VerticalAlignment.Center;
                        myTextDescPresent.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                        myStackPresent.Children.Add(ResultImagePresent);
                        myStackPresent.Children.Add(myTextPresent);
                        myStackPresent.Children.Add(myTextDescPresent);

                        myStackPresent.Orientation = Orientation.Horizontal;
                        myStackPresent.VerticalAlignment = VerticalAlignment.Center;
                        myStackPresent.HorizontalAlignment = HorizontalAlignment.Center;

                        BackgroundStack.Children.Add(myStackPresent);
                        BackgroundStack.Orientation = Orientation.Vertical;
                    }
                    StackPanel myStack = new StackPanel();
                    myStack.Margin = new Thickness(10, 10, 0, 10);

                    TextBlock myText = new TextBlock();
                    myText.Text = myWeather.list[i].main.temp.ToString();
                    myText.FontSize = 56;
                    myText.Margin = new Thickness(0, 0, 10, 0);
                    myText.VerticalAlignment = VerticalAlignment.Center;
                    myText.Foreground = new SolidColorBrush(Windows.UI.Colors.White);


                    string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[i].weather[0].icon);
                    Image ResultImage = new Image();
                    ResultImage.Margin = new Thickness(10, 0, 10, 0);
                    ResultImage.Height = 100;
                    ResultImage.Width = 100;
                    ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));


                    TextBlock myTextDesc = new TextBlock();
                    myTextDesc.Text = myWeather.list[i].weather[0].description;
                    myTextDesc.FontSize = 20;
                    myTextDesc.Margin = new Thickness(10, 0, 10, 0);
                    myTextDesc.VerticalAlignment = VerticalAlignment.Center;
                    myTextDesc.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

                    myStack.Children.Add(ResultImage);
                    myStack.Children.Add(myText);
                    myStack.Children.Add(myTextDesc);

                    myStack.Orientation = Orientation.Horizontal;
                    myStack.VerticalAlignment = VerticalAlignment.Center;
                    myStack.HorizontalAlignment = HorizontalAlignment.Center;

                    BackgroundStack.Children.Add(myStack);
                    BackgroundStack.Orientation = Orientation.Vertical;
                    //TextBlock DescText = new TextBlock();
                    //DescText.Text = myWeather.list[i].weather[i].description.ToString();
                }

                //TemperatureTextBlock.Text = ((int)myWeather).ToString();
                //DescriptionTextBlock.Text = myWeather.weather[0].description;

                //Tile Updates
                var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);

                var tileAttributes = tileXml.GetElementsByTagName("text");
                tileAttributes[0].AppendChild(tileXml.CreateTextNode(myLocation.Text + "\n " + presentTemp + "\n" + presentDesc));
                var tileNotification = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);

            }
            catch
            {
                myLocation.Text = "Unable to loacte your location";
            }

        }
    }
}
