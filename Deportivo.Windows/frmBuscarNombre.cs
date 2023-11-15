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
    public partial class frmBuscarNombre : Form
    {
        public frmBuscarNombre()
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
            textoFiltro = txtBLocalidad.Text;
            DialogResult = DialogResult.OK;
           
        }
    }
}
