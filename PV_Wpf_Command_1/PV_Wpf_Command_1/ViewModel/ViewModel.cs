using PV_Wpf_Command_1.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PV_Wpf_Command_1.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        public ICommand ShowMessageBoxCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        private string _host;
        public string Host
        {
            get { return _host; }
            set
            {
                if (_host != value)
                {
                    _host = value;
                    OnPropertyChanged("Host");
                }
            }
        }
        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged("User");
                }
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _database;
        public string Database
        {
            get { return _database; }
            set
            {
                if (_database != value)
                {
                    _database = value;
                    OnPropertyChanged("Database");
                }
            }
        }
        private string _port;
        public string Port
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    OnPropertyChanged("Port");
                }
            }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        public ViewModel() 
        {
            ShowMessageBoxCommand = new RelayCommand(_ => ShowMessageBox());
            ExitCommand = new RelayCommand(_ => ExitApplication());
        }
        private void ShowMessageBox()
        {
            MessageBox.Show("Hello World!");
        }
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
        private void ConnectDatabase()
        {

        }
    }
}
