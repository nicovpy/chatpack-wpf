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
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for demoExample.xaml
    /// </summary>
    public partial class demoExample : Window
    {
        public demoExample()
        {
            InitializeComponent();

            ////http://www.wpf-tutorial.com/listview-control/listview-with-gridview/
            //Bild.ItemsSource = list;
            //Name.ItemsSource = listn;
            //Kreis.ItemsSource = liste;

            //< ListBoxItem >

            //    < TextBlock >
            //        < Image Source = "monkey.png" Height = "15" Width = "50" />

            //             < Run > Text </ Run >

            //         </ TextBlock >

            //     </ ListBoxItem >


            //List<TextBlock> list = new List<TextBlock>();
            //TextBlock a = new TextBlock();
            //a.Text = "asdada";

            //list.Add(a);

            //TextAndImage.ItemsSource = list;

            List <User> items= new List<User>();

            items.Add(new User("John"));
            items.Add(new User("Symmy"));// { Name = "Jane Doe", Age = 39 });
            items.Add(new User("kasdk"));// { Name = "Sammy Doe", Age = 13 });
            TextAndImage.ItemsSource = items;




        }
    }
}
