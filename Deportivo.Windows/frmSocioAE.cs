using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
using Deportivo.Windows.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deportivo.Windows
{
    public partial class frmSocioAE : Form
    {

        private readonly IServicioSocios _servicios;
        private bool esEdicion = false;

        public frmSocioAE(IServicioSocios servicios)
        {
            InitializeComponent();
            _servicios = servicios;
        }

        private Socio socio;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboLocalidades(ref cbLocalidad);
            if (socio != null)
            {
                esEdicion = true;
                txtNombre.Text = socio.Nombre;
                txtApellido.Text = socio.Apellido;
                txtDocumento.Text = socio.NroDocumento.ToString();
                cbLocalidad.SelectedValue = socio.Localidad;
                txtTelefono.Text = socio.NroTelefono;
                txtCorreo.Text = socio.Email;
            }
        }


        public Socio GetSocio()
        {
            return socio;
        }

        public void SetSocio(Socio socio)
        {
            this.socio = socio;
        }

        private void frmSocioAE_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (socio == null)
                {
                    socio = new Socio();
                }
                socio.SocioId = socio.SocioId;
                socio.Nombre = txtNombre.Text;
                socio.Apellido = txtApellido.Text;
                socio.NroDocumento = txtDocumento.Text;
                socio.NroTelefono = txtTelefono.Text;
                socio.Email = txtCorreo.Text;
                socio.Localidad = (Localidad)cbLocalidad.SelectedItem;
                socio.LocalidadId = (int)cbLocalidad.SelectedValue;


                try
                {

                    if (!_servicios.Existe(socio))
                    {
                        _servicios.Guardar(socio);

                        if (!esEdicion)
                        {
                            MessageBox.Show("Registro agregado",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult dr = MessageBox.Show("¿Desea agregar otro registro?",
                                "Pregunta",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.No)
                            {
                                DialogResult = DialogResult.OK;

                            }
                            else
                            {
                                socio = null;
                                InicializarControles();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Registro editado", "Mensaje",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Registro duplicado",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        socio = null;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void InicializarControles()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDocumento.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            //cboCiudades.Items.Clear();
            cbLocalidad.SelectedIndex = 0;
            txtNombre.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                valido = false;
                errorProvider1.SetError(txtNombre, "Los nombres son requeridos");
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                valido = false;
                errorProvider1.SetError(txtApellido, "El apellido es requerido");
            }
            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                valido = false;
                errorProvider1.SetError(txtDocumento, "El documento es requerido");
            }
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                valido = false;
                errorProvider1.SetError(txtTelefono, "El telefono es requerido");
            }
            if (cbLocalidad.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cbLocalidad, "Debe seleccionar una localidad");
            }
         
            return valido;
        }
    }
}
