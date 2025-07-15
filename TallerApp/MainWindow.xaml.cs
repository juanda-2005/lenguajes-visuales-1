using System;
using System.Linq;
using System.Windows;
using TallerApp.Models;
using TallerApp.Services;
using TallerApp.Views;

namespace TallerApp
{
    public partial class MainWindow : Window
    {
        private TallerContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new TallerContext();
            CargarDatos();
        }

        private void CargarDatos()
        {
            CargarCamiones();
            CargarTrabajos();
        }

        private void CargarCamiones()
        {
            dgCamiones.ItemsSource = _context.Camiones.ToList();
        }

        private void CargarTrabajos()
        {
            dgTrabajos.ItemsSource = _context.Trabajos.Include("Camion").ToList();
        }

        // EVENTOS CAMIONES
        private void BuscarCamion_Click(object sender, RoutedEventArgs e)
        {
            string filtro = txtFiltroCamion.Text.ToLower();
            var camiones = _context.Camiones
                .Where(c => c.Marca.ToLower().Contains(filtro) ||
                           c.Modelo.ToLower().Contains(filtro) ||
                           c.Patente.ToLower().Contains(filtro))
                .ToList();
            dgCamiones.ItemsSource = camiones;
        }

        private void LimpiarFiltroCamion_Click(object sender, RoutedEventArgs e)
        {
            txtFiltroCamion.Clear();
            CargarCamiones();
        }

        private void NuevoCamion_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new CamionWindow();
            if (ventana.ShowDialog() == true)
            {
                CargarCamiones();
            }
        }

        private void EditarCamion_Click(object sender, RoutedEventArgs e)
        {
            if (dgCamiones.SelectedItem is Camion camion)
            {
                var ventana = new CamionWindow(camion);
                if (ventana.ShowDialog() == true)
                {
                    CargarCamiones();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un camión para editar");
            }
        }

        private void EliminarCamion_Click(object sender, RoutedEventArgs e)
        {
            if (dgCamiones.SelectedItem is Camion camion)
            {
                var resultado = MessageBox.Show($"¿Eliminar el camión {camion.Marca} {camion.Modelo}?",
                    "Confirmar", MessageBoxButton.YesNo);
                if (resultado == MessageBoxResult.Yes)
                {
                    _context.Camiones.Remove(camion);
                    _context.SaveChanges();
                    CargarCamiones();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un camión para eliminar");
            }
        }

        // EVENTOS TRABAJOS
        private void BuscarTrabajo_Click(object sender, RoutedEventArgs e)
        {
            string filtro = txtFiltroTrabajo.Text.ToLower();
            var trabajos = _context.Trabajos.Include("Camion")
                .Where(t => t.Descripcion.ToLower().Contains(filtro))
                .ToList();
            dgTrabajos.ItemsSource = trabajos;
        }

        private void LimpiarFiltroTrabajo_Click(object sender, RoutedEventArgs e)
        {
            txtFiltroTrabajo.Clear();
            CargarTrabajos();
        }

        private void NuevoTrabajo_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new TrabajoWindow();
            if (ventana.ShowDialog() == true)
            {
                CargarTrabajos();
            }
        }

        private void EditarTrabajo_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrabajos.SelectedItem is Trabajo trabajo)
            {
                var ventana = new TrabajoWindow(trabajo);
                if (ventana.ShowDialog() == true)
                {
                    CargarTrabajos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un trabajo para editar");
            }
        }

        private void EliminarTrabajo_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrabajos.SelectedItem is Trabajo trabajo)
            {
                var resultado = MessageBox.Show($"¿Eliminar el trabajo: {trabajo.Descripcion}?",
                    "Confirmar", MessageBoxButton.YesNo);
                if (resultado == MessageBoxResult.Yes)
                {
                    _context.Trabajos.Remove(trabajo);
                    _context.SaveChanges();
                    CargarTrabajos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un trabajo para eliminar");
            }
        }

        // EVENTOS MENU
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnClosed(EventArgs e)
        {
            _context?.Dispose();
            base.OnClosed(e);
        }
    }
}