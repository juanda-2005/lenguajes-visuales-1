using System;
using System.Linq;
using System.Windows;
using TallerApp.Models;
using TallerApp.Services;

namespace TallerApp.Views
{
    public partial class CamionWindow : Window
    {
        private TallerContext _context;
        private Camion _camion;
        private bool _esEdicion = false;

        // Constructor para nuevo camión
        public CamionWindow()
        {
            InitializeComponent();
            _context = new TallerContext();
            _camion = new Camion();
            lblTitulo.Text = "NUEVO CAMIÓN";
        }

        // Constructor para editar camión
        public CamionWindow(Camion camion)
        {
            InitializeComponent();
            _context = new TallerContext();
            _camion = camion;
            _esEdicion = true;
            lblTitulo.Text = "EDITAR CAMIÓN";
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtMarca.Text = _camion.Marca;
            txtModelo.Text = _camion.Modelo;
            txtPatente.Text = _camion.Patente;
            txtAño.Text = _camion.Año.ToString();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    _camion.Marca = txtMarca.Text.Trim();
                    _camion.Modelo = txtModelo.Text.Trim();
                    _camion.Patente = txtPatente.Text.Trim().ToUpper();
                    _camion.Año = int.Parse(txtAño.Text);

                    if (!_esEdicion)
                    {
                        _context.Camiones.Add(_camion);
                    }

                    _context.SaveChanges();
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al guardar: " + ex.Message;
                }
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                lblMensaje.Text = "La marca es requerida";
                txtMarca.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                lblMensaje.Text = "El modelo es requerido";
                txtModelo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPatente.Text))
            {
                lblMensaje.Text = "La patente es requerida";
                txtPatente.Focus();
                return false;
            }

            if (!int.TryParse(txtAño.Text, out int año) || año < 1900 || año > DateTime.Now.Year + 1)
            {
                lblMensaje.Text = "Ingrese un año válido";
                txtAño.Focus();
                return false;
            }

            // Verificar patente única
            var patenteExistente = _context.Camiones
                .Any(c => c.Patente == txtPatente.Text.Trim().ToUpper() && c.Id != _camion.Id);
            if (patenteExistente)
            {
                lblMensaje.Text = "Ya existe un camión con esa patente";
                txtPatente.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void txtMarca_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}