using Deportivo.Entidades.Entidades;
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
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
            _servicio = new ServicioRoles();
        }
        private readonly ServicioRoles _servicio;
        private List<Rol> lista;

        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;

        bool filtroOn = false;
        string textoFiltro = null;


        private void frmRoles_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                registros = _servicio.GetCantidad(null);
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
            lista = _servicio.GetRolesPorPagina(registrosPorPagina, paginaActual, textoFiltro);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var rol in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, rol);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRolAE frm = new frmRolAE(_servicio) { Text = "Agregar Rol" };
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
            Rol rol = (Rol)r.Tag;
            try
            {
                //TODO: Se debe controlar que no este relacionado
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }

                if (!_servicio.EstaRelacionado(rol))
                {
                    _servicio.Borrar(rol.RolId);
                    GridHelper.QuitarFila(dgvDatos, r);
                    //lblCantidad.Text = _servicio.GetCantidad().ToString();
                    MessageBox.Show("Registro borrado", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Localidad relacionada", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            RecargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Rol rol = (Rol)r.Tag;
            Rol rolCopia = (Rol)rol.Clone();
            try
            {
                frmRolAE frm = new frmRolAE(_servicio) { Text = "Editar Rol" };
                frm.SetRol(rol);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, rolCopia);

                    return;
                }
                rol = frm.GetRol();
                if (rol != null)
                {
                    GridHelper.SetearFila(r, rol);
                }
                else
                {
                    GridHelper.SetearFila(r, rolCopia);
                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, rolCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!filtroOn)
            {
                frmBuscarRol frm = new frmBuscarRol() { Text = "Buscar por Nombre del Rol" };
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    textoFiltro = frm.GetTexto();
                    btnBuscar.BackColor = Color.Aquamarine;
                    filtroOn = true;
                    lista = _servicio.GetRoles(textoFiltro);
                    registros = _servicio.GetCantidad(textoFiltro);
                    paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                    MostrarDatosEnGrilla();

                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                MessageBox.Show("Quite el filtro activo!!!", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            filtroOn = false;
            btnBuscar.BackColor = Color.White;
            textoFiltro = null;
            RecargarGrilla();
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
