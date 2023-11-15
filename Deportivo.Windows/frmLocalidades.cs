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
    public partial class frmLocalidades : Form
    {
        public frmLocalidades()
        {
            InitializeComponent();
            _servicio = new ServicioLocalidades();
        }
        private readonly ServicioLocalidades _servicio;
        private List<Localidad> lista;

        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;

        bool filtroOn = false;
        string textoFiltro = null;

        private void frmLocalidades_Load(object sender, EventArgs e)
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
            lista = _servicio.GetLocalidadesPorPagina(registrosPorPagina, paginaActual, textoFiltro);
            MostrarDatosEnGrilla();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var localidad in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, localidad);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmLocalidadAE frm = new frmLocalidadAE(_servicio) { Text = "Agregar Localidad" };
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
            Localidad localidad = (Localidad)r.Tag;
            try
            {
               
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }

                if (!_servicio.EstaRelacionado(localidad))
                {
                    _servicio.Borrar(localidad.LocalidadId);
                    GridHelper.QuitarFila(dgvDatos, r);
                    
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
            Localidad localidad = (Localidad)r.Tag;
            Localidad localidadCopia = (Localidad)localidad.Clone();
            try
            {
                frmLocalidadAE frm = new frmLocalidadAE(_servicio) { Text = "Editar Localidad" };
                frm.SetLocalidad(localidad);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, localidadCopia);

                    return;
                }
                localidad = frm.GetLocalidad();
                if (localidad != null)
                {
                    GridHelper.SetearFila(r, localidad);
                }
                else
                {
                    GridHelper.SetearFila(r, localidadCopia);
                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, localidadCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!filtroOn)
            {
                frmBuscarLocalidad frm = new frmBuscarLocalidad() { Text = "Buscar por Nombre de la Localidad" };
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
                    lista = _servicio.GetLocalidades(textoFiltro);
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
