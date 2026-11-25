using EÁ_rock_zenekarok_70s.Commands;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EÁ_rock_zenekarok_70s.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        public ICommand LoadBandDataCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        private ObservableCollection<Band> _bands;
        public ObservableCollection<Band> Bands
        {
            get => _bands;
            set
            {
                _bands = value;
                OnPropertyChanged(nameof(Bands));
            }
        }
        private string _selectedBandName;
        public string SelectedBandName
        {
            get => _selectedBandName;
            set
            {
                _selectedBandName = value;
                OnPropertyChanged(nameof(SelectedBandName));

                if (SelectionChangedCommand.CanExecute(value))
                    SelectionChangedCommand.Execute(value);
            }
        }
        private string _selectedBandActive;
        public string SelectedBandActive
        {
            get => _selectedBandActive;
            set
            {
                _selectedBandActive = value;
                OnPropertyChanged(nameof(SelectedBandActive));
            }
        }
        private string _selectedBandUrl;
        public string SelectedBandUrl
        {
            get => _selectedBandUrl;
            set
            {
                _selectedBandUrl = value;
                OnPropertyChanged(nameof(SelectedBandUrl));
            }
        }
        private string _host;
        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                OnPropertyChanged(nameof(Host));
            }
        }
        private string _port;
        public string Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }
        private string _databaseName;
        public string DatabaseName
        {
            get => _databaseName;
            set
            {
                _databaseName = value;
                OnPropertyChanged(nameof(DatabaseName));
            }
        }
        private string _user;
        public string User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private bool _buttonDisabled;
        public bool ButtonDisabled
        {
            get => _buttonDisabled;
            set
            {
                _buttonDisabled = value;
                OnPropertyChanged(nameof(ButtonDisabled));
            }
        }
        public ViewModel()
        {
            LoadBandDataCommand = new RelayCommand(_ => LoadBandData());
            SelectionChangedCommand = new RelayCommand(_ => UpdateData());
        }
        private void LoadBandData()
        {
            ObservableCollection<Band> bands = new ObservableCollection<Band>();
            string connectionString =
                $"Server={_host};Port={_port};Database={_databaseName};User ID={_user};Password={_password}";

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM zenekarok", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bands.Add(new Band(
                            reader.GetInt32(reader.GetOrdinal("id")), 
                            reader.GetString(reader.GetOrdinal("nev")), 
                            reader.GetString(reader.GetOrdinal("tagok")), 
                            reader.GetInt32(reader.GetOrdinal("aktiv_evek")), 
                            reader.GetString(reader.GetOrdinal("kep_url"))));
                    }
                }
            }
            Bands = bands;
            ButtonDisabled = true;
        }
        private void UpdateData()
        {
            foreach (var band in Bands)
            {
                if (band.Name == SelectedBandName)
                {
                    SelectedBandActive = band.Url;
                    SelectedBandUrl = band.Url;
                }
            }
        }
    }
}
