using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Wpf
{
    class User : IComparable<User>
    {
        private string _name;
        public User(string name)
        {
            _name = name;
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
