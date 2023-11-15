using Deportivo.Entidades.Entidades;
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
    public partial class frmBuscarPorLocalidad : Form
    {
        private Localidad localidadSeleccionada;

        public frmBuscarPorLocalidad()
        {
            InitializeComponent();
        }

        private string textoFiltro;

        public string GetTexto()
        {
            return textoFiltro;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }

        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (cboLocalidad.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboLocalidad, "Debe seleccionar una Localidad");
            }
            
            return valido;
        }

        public object GetLocalidad()
        {
            return localidadSeleccionada;
        }

        private void frmBuscarPorLocalidad_Load(object sender, EventArgs e)
        {
            CombosHelper.CargarComboLocalidades(ref cboLocalidad);
        }

        private void cboLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}
