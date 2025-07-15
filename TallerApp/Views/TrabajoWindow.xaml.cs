using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TallerApp.Models;
using TallerApp.Services;

namespace TallerApp.Views
{
    public partial class TrabajoWindow : Window
    {
        private TallerContext _context;
        private Trabajo _trabajo;
        private bool _esEdicion = false;

        // Constructor para nuevo trabajo
        public TrabajoWindow()
        {
            InitializeComponent();
            _context = new TallerContext();
            _trabajo = new Trabajo();
            lblTitulo.Text = "NUEVO TRABAJO";
            CargarCamiones();
            dpFechaInicio.SelectedDate = DateTime.Now.Date;
        }

        // Constructor para editar trabajo
        public TrabajoWindow(Trabajo trabajo)
        {
            InitializeComponent();
            _context = new TallerContext();
            _trabajo = trabajo;
            _esEdicion = true;
            lblTitulo.Text = "EDITAR TRABAJO";
            CargarCamiones();
            CargarDatos();
        }

        private void CargarCamiones()
        {
            var camiones = _context.Camiones.ToList();
            var camionesDisplay = camiones.Select(c => new
            {
                Id = c.Id,
                DisplayText = $"{c.Marca} {c.Modelo} - {c.Patente}"
            }).ToList();

            cbCamion.ItemsSource = camionesDisplay;
        }

        private void CargarDatos()
        {
            txtDescripcion.Text = _trabajo.Descripcion;
            txtPrecio.Text = _trabajo.Precio.ToString();
            dpFechaInicio.SelectedDate = _trabajo.FechaInicio;
            dpFechaFin.SelectedDate = _trabajo.FechaFin;
            cbCamion.SelectedValue = _trabajo.CamionId;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    _trabajo.Descripcion = txtDescripcion.Text.Trim();
                    _trabajo.Precio = decimal.Parse(txtPrecio.Text);
                    _trabajo.FechaInicio = dpFechaInicio.SelectedDate.Value;
                    _trabajo.FechaFin = dpFechaFin.SelectedDate;
                    _trabajo.CamionId = (int)cbCamion.SelectedValue;

                    if (!_esEdicion)
                    {
                        _context.Trabajos.Add(_trabajo);
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
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblMensaje.Text = "La descripción es requerida";
                txtDescripcion.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                lblMensaje.Text = "Ingrese un precio válido";
                txtPrecio.Focus();
                return false;
            }

            if (dpFechaInicio.SelectedDate == null)
            {
                lblMensaje.Text = "La fecha de inicio es requerida";
                dpFechaInicio.Focus();
                return false;
            }

            if (cbCamion.SelectedValue == null)
            {
                lblMensaje.Text = "Seleccione un camión";
                cbCamion.Focus();
                return false;
            }

            if (dpFechaFin.SelectedDate != null && dpFechaFin.SelectedDate < dpFechaInicio.SelectedDate)
            {
                lblMensaje.Text = "La fecha fin no puede ser anterior a la fecha de inicio";
                dpFechaFin.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}