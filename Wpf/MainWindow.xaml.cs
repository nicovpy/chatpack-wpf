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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int INFO_COLUMN = 5;

        private bool isInfoOn = false;
        private bool isSettingOn = false;
        private TextBlock[] infoUserBlock = new TextBlock[INFO_COLUMN];
        private List<User> friendsList = new List<User>();
        private SolidColorBrush BGcolor = new SolidColorBrush(Colors.BlueViolet);


        public bool IsInfoOn
        {
            get
            {
                return isInfoOn;
            }
            set
            {
                isInfoOn = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            this.FontFamily = new FontFamily("Comic Sans MS");

            curF_TextF_BG.Background = BGcolor;

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"C:\Schule\3Klasse\syp\project\guiDemo\Wpf\ProfilePicture\smittyWerbenJaggerManJensen.jpg"));
            myBrush.Stretch = Stretch.UniformToFill;
            ProfPic.Fill = myBrush;
            ProfPic.Height = 60;
            ProfPic.Width = 60;

            List<StackPanel> spList = new List<StackPanel>();
            popUpSetting.VerticalOffset = -btnSetting.ActualHeight;
            popUpSetting.HorizontalOffset = -btnSetting.ActualWidth;

            SetTBlockTitle();
            FileRead("friends.txt");
            CreateStackPFriend(spList);
            friendsView.ItemsSource = spList;
        }
        public void FileRead(string filepath)
        {
            string[] friendRow = File.ReadAllLines(filepath);
            for (int i = 0; i < friendRow.Length; i++)
            {
                string[] elem = friendRow[i].Split(';');
                User friend = new User(elem[0]);
                friendsList.Add(friend);


                //list.Add(friend.Name);

                //imgList.Add(CreateEllipse(elem[1]));
            }
            friendsList.Sort();
        }
        public void CreateStackPFriend(List<StackPanel> list)
        {
            StackPanel sp;
            TextBlock tb;

            for (int i = 0; i < friendsList.Count; i++)
            {
                sp = new StackPanel();
                tb = new TextBlock();
                tb.FontSize = 18;
                tb.Text = friendsList[i].Name;
                sp.Children.Add(tb);
                list.Add(sp);
            }
        }
        public Ellipse CreateEllipse(string imageName)
        {
            Ellipse pic = new Ellipse();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"C:\Schule\3Klasse\syp\project\guiDemo\Wpf\ProfilePicture\" + imageName));
            myBrush.Stretch = Stretch.UniformToFill;
            pic.Height = 60;
            pic.Width = 60;

            pic.Fill = myBrush;

            return pic;
        }
        public void CreateUserInformation()
        {
            //FileRead
        }
        public void SetTBlockTitle()
        {
            for (int i = 0; i < INFO_COLUMN; i++)
            {
                infoUserBlock[i] = new TextBlock();
            }
            infoUserBlock[0].Text = "Username: ";
            infoUserBlock[1].Text = "Created since: ";
            infoUserBlock[2].Text = "Friends amount: ";
            infoUserBlock[3].Text = "Total messages sent: ";
            infoUserBlock[4].Text = "Total messages received: ";
        }
        private void SendBtn(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text == "")
                return;

            DateTime dateTime = DateTime.Now;
            //DateShow.Text += dateTime.ToString("hh:mm")+ "\n";
            ShowInputBlock.Text += dateTime.ToString("hh:mm    ") + InputBox.Text + "\n";
            InputBox.Text = String.Empty;

        }
        private void OnKeyEnterHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (InputBox.Text == "")
                    return;

                DateTime dateTime = DateTime.Now;
                ShowInputBlock.Text += dateTime.ToString("hh:mm    ") + InputBox.Text + "\n";
                InputBox.Text = String.Empty;
            }
        }
        public void AddFriend(object sender, RoutedEventArgs e)
        {

        }
        public void SelectFriend(object sender, SelectionChangedEventArgs e)
        {
            selectedFriend = friendsView.SelectedItem as StackPanel;
        }
        private void ShowUserInfo(object sender, RoutedEventArgs e)
        {
            //Grid newGrid = new Grid();
            //newGrid.Width = FriendBox.ActualWidth;
            //newGrid.HorizontalAlignment = HorizontalAlignment.Left;
            //newGrid.VerticalAlignment = VerticalAlignment.Top;
            //newGrid.ShowGridLines = true;
            //newGrid.Background = new SolidColorBrush(Colors.Black);

            //StackPanel[] stackpanels = new StackPanel[5];



            //infoBlock.HorizontalAlignment = HorizontalAlignment.Center;
            SetTBlockTitle();
            TextBox tb1 = new TextBox();
            tb1.Text = "Smitty";
            //tb1.IsEnabled = false;
            tb1.IsReadOnly = true;

            StackPanel sp1 = new StackPanel();
            sp1.Orientation = Orientation.Horizontal;
            sp1.Children.Add(infoUserBlock[0]);
            sp1.Children.Add(tb1);

            //StackPanel bsp = new StackPanel();
   
            TextBlock tb2 = new TextBlock();
            tb2.Text = "01.06.2017";

            StackPanel sp2 = new StackPanel();
            sp2.Orientation = Orientation.Horizontal;
            sp2.Children.Add(infoUserBlock[1]);
            sp2.Children.Add(tb2);

            StackPanel sp = new StackPanel();

            sp.Orientation = Orientation.Vertical;
            sp.Margin = new Thickness(0, 100, 0, 0);
            sp.Children.Add(sp1);
            sp.Children.Add(sp2);
            
            
            if (!IsInfoOn)
            {
                Info.Children.Add(sp);
                Info.Background = new SolidColorBrush(Colors.White);
                IsInfoOn = true;
            }
            else
            {
                Info.Children.Clear();
                Info.Background = new SolidColorBrush();
                isInfoOn = false;
            }
            //Info.Background = new SolidColorBrush(Colors.Black);

            //RowDefinition gridRow1 = new RowDefinition();

            //gridRow1.Height = new GridLength(45);
            //Info.RowDefinitions.Add(gridRow1);
            //TextBlock txtBlock1 = new TextBlock();

            //txtBlock1.Text = "Author Name";

            //txtBlock1.FontSize = 14;

            //txtBlock1.FontWeight = FontWeights.Bold;
            //txtBlock1.VerticalAlignment = VerticalAlignment.Top;

            //Grid.SetRow(txtBlock1, 0);

            //Grid.SetColumn(txtBlock1, 0);
            //Info.Children.Add(txtBlock1);
            
        }
        private void Settings(object sender, RoutedEventArgs e)
        {
            isSettingOn = !isSettingOn;
            popUpSetting.IsOpen = isSettingOn;

        }
        private void ShowStats(object sender, RoutedEventArgs e)
        {

        }
        private void ChangeInformation(object sender, RoutedEventArgs e)
        {
            isSettingOn = !isSettingOn;
            popUpSetting.IsOpen = isSettingOn;
            

        }
        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            BGcolor = new SolidColorBrush(Colors.Crimson);
            //curF_TextF_BG.Background = BGcolor;
        }

        private void friendsView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
                friendsView.UnselectAll();
        }

        //private void Image_Loaded(object sender, RoutedEventArgs e)
        //{
        //    BitmapImage b = new BitmapImage();
        //    b.BeginInit();
        //    b.UriSource = new Uri(@"C:\Users\Stephan\Desktop\guiDemo\smittyWerbenJaggerManJensen.jpg");
        //    b.EndInit();

        //    Image image = sender as Image;
        //    image.Source = b;
        //}
        //private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
