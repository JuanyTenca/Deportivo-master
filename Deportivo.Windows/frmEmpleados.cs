using Deportivo.Entidades.Dtos.Empleado;
using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
using Deportivo.Servicios.Servicio;
using Deportivo.Windows.Helpers;
using Microsoft.Win32;
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
    public partial class frmEmpleados : Form
    {

        private readonly IServicioEmpleados _servicio;

        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;

        int? localidadFiltro = null;
        int? rolFiltro = null;
        List<EmpleadoListDto> lista;
        bool filtroOn = false;

        public frmEmpleados()
        {
            InitializeComponent();
            _servicio = new ServicioEmpleados();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                registros = _servicio.GetCantidad(null, null);
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MostrarPaginado()
        {
            lista = _servicio.GetEmpleadosPorPagina(registrosPorPagina, paginaActual, localidadFiltro, rolFiltro);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var empleado in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, empleado);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEmpleadoAE frm = new frmEmpleadoAE(_servicio) { Text = "Agregar Empleado" };
            DialogResult dr = frm.ShowDialog(this);
            RecargarGrilla();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            EmpleadoListDto empleado = (EmpleadoListDto)r.Tag;
            try
            {
               
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicio.Borrar(empleado.EmpleadoId);
                GridHelper.QuitarFila(dgvDatos, r);
                registros = _servicio.GetCantidad(localidadFiltro, rolFiltro);
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                lblRegistros.Text = registros.ToString();
                lblPaginas.Text = paginas.ToString();
                
                MessageBox.Show("Registro borrado", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            EmpleadoListDto empleadoDto = (EmpleadoListDto)r.Tag;
            Empleado empleado = _servicio.GetEmpleadoPorId(empleadoDto.EmpleadoId);
            Empleado empleadoCopia = (Empleado)empleado.Clone();

            try
            {
                frmEmpleadoAE frm = new frmEmpleadoAE(_servicio) { Text = "Editar Empleado" };
                frm.SetEmpleado(empleado);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, empleadoCopia);

                    return;
                }
                empleado = frm.GetEmpleado();
                if (empleado != null)
                {
                    GridHelper.SetearFila(r, empleado);

                }
                else
                {
                    GridHelper.SetearFila(r, empleadoCopia);

                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, empleadoCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            MostrarPaginado();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual == 1)
            {
                return;
            }
            paginaActual--;
            MostrarPaginado();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual == paginas)
            {
                return;
            }
            paginaActual++;
            MostrarPaginado();
        }

        private void btnUltima_Click(object sender, EventArgs e)
        {
            paginaActual = paginas;
            MostrarPaginado();
        }
    }
}
