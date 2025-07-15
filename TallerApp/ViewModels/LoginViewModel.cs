using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TallerApp.Services;


namespace TallerApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _usuario;
        private string _contraseña;

        public string Usuario
        {
            get => _usuario;
            set { _usuario = value; OnPropertyChanged(nameof(Usuario)); }
        }

        public string Contraseña
        {
            get => _contraseña;
            set { _contraseña = value; OnPropertyChanged(nameof(Contraseña)); }
        }

        public Services.RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Services.RelayCommand(Login);
        }

        private void Login()
        {
            using (var context = new TallerContext())
            {
                var user = context.Usuarios.FirstOrDefault(u =>
                    u.NombreUsuario == Usuario && u.Contraseña == Contraseña);

                if (user != null)
                {
                    // Abrir ventana principal
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    App.Current.MainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
        }
    }
}
