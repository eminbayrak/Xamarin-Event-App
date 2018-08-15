using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ParPorApp.Helpers
{
    public class PropertyChangeNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void HasChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                //PropertyChanged.Raise(this, property);
            }
        }

        public void HasChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                HasChanged(property);
            }
        }
    }
}
