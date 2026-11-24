using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV_Wpf_Command_1.ViewModel
{

    /// <summary>
    /// Alaposutály, a viewmodell számára, a Viewmodell osztályok ebből szátmaznak
    /// Más programok is felhasználhatják
    /// </summary>

    internal class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelBase() { }

        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
