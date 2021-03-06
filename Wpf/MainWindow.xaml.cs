﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using LiveCharts;
using LiveCharts.Wpf;
using System.Media;

namespace Wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SoundPlayer plr = new SoundPlayer("oof.wav");

        private const int INFO_COLUMN = 6;

        private bool isInfoOn = false;
        private TextBlock[] infoUserBlock = new TextBlock[INFO_COLUMN];
        //später wichtig
        private List<User> friendlist = new List<User>();
        //
        private Color colorBlue = (Color)ColorConverter.ConvertFromString("#1d55af");
        private Color colorViolet = (Color)ColorConverter.ConvertFromString("#63085d");
        private SolidColorBrush bgColor = new SolidColorBrush();

        private StackPanel sp = new StackPanel();
        private Button profBtn = new Button();
                 

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
        public object SeriersCollection { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            
            this.FontFamily = new FontFamily("Comic Sans MS");

            bgColor.Color = colorBlue;
            center_Grid.Background = bgColor;

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("smittyWerbenJaggerManJensen.jpg",UriKind.Relative));
            myBrush.Stretch = Stretch.UniformToFill;        //@"C:\Users\Stephan\Desktop\lsad\Wpf\ProfilePicture\smittyWerbenJaggerManJensen.jpg"
            profPic.Fill = myBrush;
            profPic.Height = 60;
            profPic.Width = 60;

            List<StackPanel> spList = new List<StackPanel>();

            popUpSetting.VerticalOffset = -btnSetting.ActualHeight;
            popUpSetting.HorizontalOffset = -btnSetting.ActualWidth;

            SetTextTitles();
            //FileRead("friends.txt");

            CreateSPItem("friends.txt", spList);
            friendsView.ItemsSource = spList;

            addBtn.Click += TypeTagNumber;

            btnBlue.IsEnabled = false;

            //
            sp = CreateUserInformation();
            //
        }
        private Border CreateCenterButton(string text)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(12);
            Button btn = new Button();
            btn.Content = text;
            border.Child = btn;
            return border;
        }
        private void CreateSPItem(string path, List<StackPanel> spList)
        {
            StackPanel sp;
            TextBlock tb;

            string[] friendrow = File.ReadAllLines(path);
            for (int i = 0; i < friendrow.Length; i++)
            {
                string[] elem = friendrow[i].Split(';');
                User friend = new User(elem[0],elem[1]);

                sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                tb = new TextBlock();
                tb.FontSize = 16;
                tb.Text = friend.Name;
                tb.VerticalAlignment = VerticalAlignment.Center;

                Ellipse ellImg = new Ellipse();
                ImageBrush imgBrush= new ImageBrush();
                imgBrush.ImageSource = friend.Img;
                imgBrush.Stretch = Stretch.UniformToFill;
                ellImg.Fill = imgBrush;
                ellImg.Height = 56;
                ellImg.Width = 56;

                
                sp.Children.Add(ellImg);
                sp.Children.Add(tb);
                
                spList.Add(sp);

            }
        }
        private void TypeTagNumber(object sender, RoutedEventArgs e)
        {
            popUpTag.IsOpen = !popUpTag.IsOpen;
        }
        //löschen
        public void FileRead(string filepath)
        {
            string[] friendRow = File.ReadAllLines(filepath);
            for (int i = 0; i < friendRow.Length; i++)
            {
                string[] elem = friendRow[i].Split(';');
                User friend = new User(elem[0],elem[1]);
                friendlist.Add(friend);
               
            }
            friendlist.Sort();
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
        public StackPanel CreateUserInformation()
        {
            TextBox tb1 = new TextBox();
            tb1.Text = "Smitty";
            tb1.IsEnabled = false;
            //tb1.IsReadOnly = true;
            StackPanel sp1 = new StackPanel();
            sp1.Orientation = Orientation.Horizontal;
            sp1.Children.Add(infoUserBlock[0]);
            sp1.Children.Add(tb1);

            TextBlock tb2 = new TextBlock();
            tb2.Text = "#1841";
            StackPanel sp2 = new StackPanel();
            sp2.Orientation = Orientation.Horizontal;
            sp2.Children.Add(infoUserBlock[1]);
            sp2.Children.Add(tb2);

            TextBlock tb3 = new TextBlock();
            tb3.Text = "01.06.2017";
            StackPanel sp3 = new StackPanel();
            sp3.Orientation = Orientation.Horizontal;
            sp3.Children.Add(infoUserBlock[2]);
            sp3.Children.Add(tb3);

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Margin = new Thickness(0, 100, 0, 0);
            sp.Children.Add(sp1);
            sp.Children.Add(sp2);
            sp.Children.Add(sp3);

            return sp;
        }
        private void ShowUserInfo(object sender, RoutedEventArgs e)
        {
            if (!IsInfoOn)
            {
                
                //Info.Children.Clear();
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
        }
        public void SetTextTitles()
        {
            for (int i = 0; i < INFO_COLUMN; i++)
            {
                infoUserBlock[i] = new TextBlock();
            }
            infoUserBlock[0].Text = "Username: ";
            infoUserBlock[1].Text = "Tag-Number: ";
            infoUserBlock[2].Text = "Created since: ";
            infoUserBlock[3].Text = "Friends amount: ";
            infoUserBlock[4].Text = "Total messages sent: ";
            infoUserBlock[5].Text = "Total messages received:";
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
        public void SelectFriend(object sender, SelectionChangedEventArgs e)
        {
            selFriendGrid.Children.Clear();
            StackPanel tempSP;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;

            TextBlock tb = new TextBlock();
            tb.FontSize = 16;
            Ellipse el = new Ellipse();
            el.Width = 56;
            el.Height = 56;

            Border remBtnBdr = new Border();
            remBtnBdr = CreateCenterButton("Remove");
            
            Border statsBtnBdr = new Border();
            statsBtnBdr = CreateCenterButton("Stats");
            Grid.SetColumn(statsBtnBdr, 1);

            tempSP = (StackPanel)friendsView.SelectedItem;
            if (tempSP == null)
            {    
                remStatGrid.Children.Clear();
                return;
            }
            tb.Text = (tempSP.Children[1] as TextBlock).Text;
            el.Fill = (tempSP.Children[0] as Ellipse).Fill;

            sp.Children.Add(el);
            sp.Children.Add(tb);
            
            selFriendGrid.Children.Add(sp);

            remStatGrid.Children.Add(remBtnBdr);
            remStatGrid.Children.Add(statsBtnBdr);
        }
        
        private void Settings(object sender, RoutedEventArgs e)
        {
          
            popUpSetting.IsOpen = !popUpSetting.IsOpen;

            plr.Load();
            plr.Play();

        }
        private void ShowStats(object sender, RoutedEventArgs e)
        {
            plr.Load();
            plr.Play();
        }
        private void ChangeInformation(object sender, RoutedEventArgs e)
        {
            plr.Load();
            plr.Play();

            popUpSetting.IsOpen = !popUpSetting.IsOpen;
            Info.Children.Clear();
            Info.Background = new SolidColorBrush(Colors.White);
            IsInfoOn = true; ;

            
            profBtn.Width = 100;
            profBtn.Height = 70;
            profBtn.VerticalAlignment = VerticalAlignment.Center;
            profBtn.Content = "Change Image";
            profBtn.Click += OpenFileDiaForImg;
            
            Info.Children.Add(sp);
            Info.Children.Add(profBtn);



        }

        private void OpenFileDiaForImg(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDia = new OpenFileDialog();

            

            fileDia.Filter = "Images (*.png, *.jpg)|*.png; *jpg";
            if (fileDia.ShowDialog() == true)
            {
                ImageBrush temp = new ImageBrush();
                temp.ImageSource = new BitmapImage(new Uri(fileDia.FileName));
                profPic.Fill = temp;
            }
        }

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btnBlue))
                bgColor.Color = colorBlue;
            else
                bgColor.Color = colorViolet;


            btnBlue.IsEnabled = !btnBlue.IsEnabled;
            btnVio.IsEnabled = !btnVio.IsEnabled;

            center_Grid.Background = bgColor;
        }

        private void friendsView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //plr.Load();
            //plr.Play();

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
    }
}
