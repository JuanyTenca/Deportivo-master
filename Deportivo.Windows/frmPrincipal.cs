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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnLocalidades_Click(object sender, EventArgs e)
        {
            frmLocalidades frm = new frmLocalidades();
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSocios_Click(object sender, EventArgs e)
        {
            frmSocios frm = new frmSocios();
            frm.ShowDialog();
        }
    }
}
