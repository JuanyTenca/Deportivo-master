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

        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles frm = new frmRoles();
            frm.ShowDialog();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmEmpleados frm = new frmEmpleados();
            frm.ShowDialog();
        }

        private void btnCanchas_Click(object sender, EventArgs e)
        {
            frmCanchas frm = new frmCanchas();
            frm.ShowDialog();
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            frmServicios frm = new frmServicios();
            frm.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {

        }

        private void btnCuotas_Click(object sender, EventArgs e)
        {
            frmCuotas frm = new frmCuotas();
            frm.ShowDialog();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            frmReservas frm = new frmReservas();
            frm.ShowDialog();
        }
    }
}
