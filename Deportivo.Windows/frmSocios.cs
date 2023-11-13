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
    public partial class frmSocios : Form
    {
        private readonly IServicioSocios _servicio;

        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 12;

        int? localidadFiltro = null;
        List<SocioListDto> lista;
        bool filterOn = false;

        public frmSocios()
        {
            InitializeComponent();
            _servicio = new ServicioSocios();
        }

        private void frmSocios_Load(object sender, EventArgs e)
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
            lista = _servicio.GetSociosPorPagina(registrosPorPagina, paginaActual, localidadFiltro);
            MostrarDatosEnGrilla();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var pais in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, pais);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmSocioAE frm = new frmSocioAE(_servicio) { Text = "Agregar Socio" };
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
            SocioListDto socio = (SocioListDto)r.Tag;
            try
            {
                //TODO: Se debe controlar que no este relacionado
                DialogResult dr = MessageBox.Show("¿Desea borrar el registro seleccionado?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicio.Borrar(socio.SocioId);
                GridHelper.QuitarFila(dgvDatos, r);
                registros = _servicio.GetCantidad(localidadFiltro);
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                lblRegistros.Text = registros.ToString();
                lblPaginas.Text = paginas.ToString();
                //lblCantidad.Text = _servicio.GetCantidad().ToString();
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
            SocioListDto socioDto = (SocioListDto)r.Tag;
            Socio socio = _servicio.GetSocioPorId(socioDto.SocioId);
            Socio socioCopia = (Socio)socio.Clone();

            try
            {
                frmSocioAE frm = new frmSocioAE(_servicio) { Text = "Editar Socio" };
                frm.SetSocio(socio);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelper.SetearFila(r, socioCopia);

                    return;
                }
                socio = frm.GetSocio();
                if (socio != null)
                {
                    GridHelper.SetearFila(r, socio);

                }
                else
                {
                    GridHelper.SetearFila(r, socioCopia);

                }
            }
            catch (Exception ex)
            {
                GridHelper.SetearFila(r, socioCopia);
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
