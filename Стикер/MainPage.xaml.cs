using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Tasks;



namespace Стикер
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IconicTileData _tileData;
        private string _firstString = "", _secondString = "", _thirdString = "", _fourthString = "";
        private Microsoft.Phone.Scheduler.Reminder _reminder;



        public MainPage()
        {
            InitializeComponent();

            if (!AppHelper.Storage.Contains("FirstString")) AppHelper.Storage.Add("FirstString", "");
            if (!AppHelper.Storage.Contains("SecondString")) AppHelper.Storage.Add("SecondString", "");
            if (!AppHelper.Storage.Contains("ThirdString")) AppHelper.Storage.Add("ThirdString", "");
            if (!AppHelper.Storage.Contains("FourthString")) AppHelper.Storage.Add("FourthString", "");


            if (!AppHelper.Storage.Contains("Reminder")) AppHelper.Storage.Add("Reminder", false);



            _tileData = new IconicTileData();

            UpdateMainTile();











        }
        public void UpdateMainTile()
        {
            //Get application's main tile 
            var mainTile = ShellTile.ActiveTiles.First();

            if (null != mainTile)
            {
                _tileData.BackgroundColor = Color.FromArgb(255, 195, 61, 39);
                _tileData.IconImage = new Uri("ApplicationIcon.png", UriKind.Relative);
                _tileData.SmallIconImage = new Uri("ApplicationIcon.png", UriKind.RelativeOrAbsolute);

                _tileData.Title = TBThird.Text;
                _tileData.WideContent1 = TBTitle.Text;
                _tileData.WideContent2 = TBFirst.Text;
                _tileData.WideContent3 = TBSecond.Text;

                mainTile.Update(_tileData);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            TBTitle.Text = TBExampleTitle.Text = _firstString = (string)AppHelper.Storage["FirstString"];
            TBFirst.Text = TBExampleFirst.Text = _secondString = (string)AppHelper.Storage["SecondString"];
            TBSecond.Text = TBExampleSecond.Text = _thirdString = (string)AppHelper.Storage["ThirdString"];
            TBThird.Text = TBExampleThird.Text = _fourthString = (string)AppHelper.Storage["FourthString"];

            TBlostCharsTitle.Text = "Доступно символов: " + (20 - TBTitle.Text.Length);
            TBlostCharsFirst.Text = "Доступно символов: " + (34 - TBFirst.Text.Length);
            TBlostCharsSecond.Text = "Доступно символов: " + (34 - TBSecond.Text.Length);
            TBlostCharsThird.Text = "Доступно символов: " + (20 - TBThird.Text.Length);

            if (ScheduledActionService.GetActions<ScheduledAction>().Count() > 0) ScheduledActionService.Remove("myreminder");
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.UpdateMainTile();

            if ((bool)AppHelper.Storage["Reminder"])
            {
                _reminder = new Microsoft.Phone.Scheduler.Reminder("myreminder")
                {
                    Title = _firstString,
                    Content = _secondString + "\n" + _thirdString,
                    RecurrenceType = RecurrenceInterval.Daily,
                    NavigationUri = new Uri("Page1.xaml", UriKind.Relative)

                };

                ScheduledActionService.Add(_reminder);
            }

            AppHelper.Storage["FirstString"] = _firstString;
            AppHelper.Storage["SecondString"] = _secondString;
            AppHelper.Storage["ThirdString"] = _thirdString;
            AppHelper.Storage["FourthString"] = _fourthString;

            AppHelper.Storage.Save();
        }

      

      

        // Изменяем текст заголовка
        private void TBTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBTitle.Text.Length <= 20) _firstString = TBTitle.Text;
            else TBTitle.Text = _firstString;

            TBExampleTitle.Text = _firstString;

            TBlostCharsTitle.Text = "Доступно символов: " + (20 - TBTitle.Text.Length);
        }

        // Изменяем текст первой строки
        private void TBFirst_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBFirst.Text.Length <= 34) _secondString = TBFirst.Text;
            else TBFirst.Text = _secondString;

            TBExampleFirst.Text = _secondString;

            TBlostCharsFirst.Text = "Доступно символов: " + (34 - TBFirst.Text.Length);
        }

        // Изменяем текст второй строки
        private void TBSecond_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBSecond.Text.Length <= 34) _thirdString = TBSecond.Text;
            else TBSecond.Text = _thirdString;

            TBExampleSecond.Text = _thirdString;

            TBlostCharsSecond.Text = "Доступно символов: " + (34 - TBSecond.Text.Length);
        }

        // Изменяем текст третий строки
        private void TBThird_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TBThird.Text.Length <= 20) _fourthString = TBThird.Text;
            else TBThird.Text = _fourthString;

            TBExampleThird.Text = _fourthString;

            TBlostCharsThird.Text = "Доступно символов: " + (20 - TBThird.Text.Length);
        }





        private void CBReminder_Checked(object sender, RoutedEventArgs e)
        {
            AppHelper.Storage["Reminder"] = true;
        }

        private void CBReminder_Unchecked(object sender, RoutedEventArgs e)
        {
            AppHelper.Storage["Reminder"] = false;
        }

        private void AppMarker_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void AppMyApps_Click(object sender, EventArgs e)
        {
            MarketplaceSearchTask showMyApps = new MarketplaceSearchTask { SearchTerms = "AMShishkin" };
            showMyApps.Show();
        }

        private void AppRate_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask rate = new MarketplaceReviewTask();
            rate.Show();
        }

      



    }
}