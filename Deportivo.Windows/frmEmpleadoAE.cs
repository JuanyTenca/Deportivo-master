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
    public partial class frmEmpleadoAE : Form
    {

        private readonly IServicioEmpleados _servicios;
        private bool esEdicion = false;

        public frmEmpleadoAE(IServicioEmpleados servicios)
        {
            InitializeComponent();
            _servicios = servicios;
        }

        private Empleado empleado;

        public Empleado GetEmpleado()
        {
            return empleado;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboLocalidades(ref cbLocalidad);
            CombosHelper.CargarComboRoles(ref cbRol);
            if (empleado != null)
            {
                esEdicion = true;
                txtNombre.Text = empleado.Nombre;
                txtApellido.Text = empleado.Apellido;
                txtDocumento.Text = empleado.NroDocumento.ToString();
                //cbLocalidad.SelectedValue = socio.Localidad;
                cbLocalidad.SelectedValue = empleado.LocalidadId;
                txtTelefono.Text = empleado.NroTelefono;
                txtCorreo.Text = empleado.Email;
                cbRol.SelectedValue = empleado.RolId;
            }
        }

        public void SetEmpleado(Empleado empleado)
        {
            this.empleado = empleado;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (empleado == null)
                {
                    empleado = new Empleado();
                }
                empleado.EmpleadoId = empleado.EmpleadoId;
                empleado.Nombre = txtNombre.Text;
                empleado.Apellido = txtApellido.Text;
                empleado.NroDocumento = txtDocumento.Text;
                empleado.NroTelefono = txtTelefono.Text;
                empleado.Email = txtCorreo.Text;
                //socio.Localidad = (Localidad)cbLocalidad.SelectedItem;
                empleado.LocalidadId = (int)cbLocalidad.SelectedValue;
                empleado.RolId = (int)cbRol.SelectedValue;


                try
                {

                    if (!_servicios.Existe(empleado))
                    {
                        _servicios.Guardar(empleado);

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
                                empleado = null;
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
                        empleado = null;
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
            txtCorreo.Clear(); ;
            cbLocalidad.SelectedIndex = 0;
            txtNombre.Focus();
            cbRol.SelectedIndex = 0;
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
            if (cbRol.SelectedIndex == 0) 
            { 
                valido = false;
                errorProvider1.SetError(cbRol, "Debe seleccionar un rol");
            }

            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
