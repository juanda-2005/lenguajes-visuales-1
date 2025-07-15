using System;
using System.Linq;
using System.Windows;
using TallerApp.Models;
using TallerApp.Services;

namespace TallerApp.Views
{
    public partial class RegistroWindow : Window
    {
        private TallerContext _context;

        public RegistroWindow()
        {
            InitializeComponent();
            _context = new TallerContext();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    var usuario = new Usuario
                    {
                        NombreUsuario = txtUsuario.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Contraseña = txtPassword.Password,
                        FechaCreacion = DateTime.Now
                    };

                    _context.Usuarios.Add(usuario);
                    _context.SaveChanges();

                    MessageBox.Show("Usuario registrado exitosamente");
                    this.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al registrar: " + ex.Message;
                }
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lblMensaje.Text = "El nombre de usuario es requerido";
                txtUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMensaje.Text = "El email es requerido";
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                lblMensaje.Text = "La contraseña es requerida";
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Password.Length < 4)
            {
                lblMensaje.Text = "La contraseña debe tener al menos 4 caracteres";
                txtPassword.Focus();
                return false;
            }

            // Verificar usuario único
            var usuarioExistente = _context.Usuarios
                .Any(u => u.NombreUsuario == txtUsuario.Text.Trim());
            if (usuarioExistente)
            {
                lblMensaje.Text = "Ya existe un usuario con ese nombre";
                txtUsuario.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}