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
    public partial class frmCanchaAE : Form
    {

        private IServicioCanchas _servicio;

        public frmCanchaAE(IServicioCanchas servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }

        private Cancha cancha;
        private bool esEdicion = false;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (cancha != null)
            {
                esEdicion = true;
                txtCancha.Text = cancha.NombreCancha;
            }
        }

        public Cancha GetCancha()
        {
            return cancha;
        }

        public void SetCancha(Cancha cancha)
        {
            this.cancha = cancha;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (cancha == null)
                {
                    cancha = new Cancha();

                }
                cancha.NombreCancha = txtCancha.Text;

                try
                {

                    if (!_servicio.Existe(cancha))
                    {
                        _servicio.Guardar(cancha);

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
                            cancha = null;
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
                        cancha = null;
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
            txtCancha.Clear();
            txtCancha.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            if (string.IsNullOrEmpty(txtCancha.Text))
            {
                valido = false;
                errorProvider1.SetError(txtCancha, "Debe ingresar el nombre de una cancha");

            }
            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
