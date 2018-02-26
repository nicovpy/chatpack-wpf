using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Wpf
{
    class User : IComparable<User>
    {
        private string _name;
        private BitmapImage _img;
        public User(string name, string img)
        {
            _name = name;
            _img = new BitmapImage(new Uri(@"C:\Users\Stephan\Desktop\lsad\Wpf\ProfilePicture\"+ img));
        }

        #region Prop
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public BitmapImage Img
        {
            get
            {
                return _img;
            }

            set
            {
                _img = value;
            }
        }
        #endregion

        public int CompareTo(User other)
        {
            return this.Name.CompareTo(other.Name);
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
