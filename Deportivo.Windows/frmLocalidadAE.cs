using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
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
    public partial class frmLocalidadAE : Form
    {

        private IServicioLocalidades _servicio;

        public frmLocalidadAE(IServicioLocalidades servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }

        private Localidad localidad;
        private bool esEdicion = false;
    
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (localidad != null)
            {
                esEdicion = true;
                txtLocalidad.Text = localidad.NombreLocalidad;
            }
        }
        public Localidad GetLocalidad()
        {
            return localidad;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (localidad == null)
                {
                    localidad = new Localidad();

                }
               localidad.NombreLocalidad = txtLocalidad.Text;

                try
                {

                    if (!_servicio.Existe(localidad))
                    {
                        _servicio.Guardar(localidad);

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
                            localidad = null;
                            InicializarControles();

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
                        localidad = null;
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
            txtLocalidad.Clear();
            txtLocalidad.Focus();
        }

        private bool ValidarDatos()
        {

            bool valido = true;
            if (string.IsNullOrEmpty(txtLocalidad.Text))
            {
                valido = false;
                errorProvider1.SetError(txtLocalidad, "Debe ingresar el nombre de una localidad");

            }
            return valido;
        }

        public void SetLocalidad(Localidad localidad)
        {
            this.localidad = localidad;
        }

        private void frmLocalidadAE_Load(object sender, EventArgs e)
        {

        }
    }
}
