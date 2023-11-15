using Deportivo.Entidades.Dtos.Empleado;
using Deportivo.Entidades.Dtos.Socio;
using Deportivo.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deportivo.Windows.Helpers
{
    public static class GridHelper
    {
        public static void LimpiarGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }
        public static DataGridViewRow ConstruirFila(DataGridView dgv)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgv);
            return r;

        }

        public static void SetearFila(DataGridViewRow r, object obj)
        {
            switch (obj)
            {
                case Localidad localidad:
                    r.Cells[0].Value = localidad.NombreLocalidad;
                    break;
                case SocioListDto socio:
                    r.Cells[0].Value = $"{socio.Apellido}, {socio.Nombre}";
                    r.Cells[1].Value = socio.NroDocumento;
                    r.Cells[2].Value = socio.NombreLocalidad;
                    r.Cells[3].Value = socio.NroTelefono;
                    r.Cells[4].Value = socio.Email;
                    break;
                case Rol rol:
                    r.Cells[0].Value = rol.Descripcion;
                    break;
                case EmpleadoListDto empleado:
                    r.Cells[0].Value = $"{empleado.Apellido}, {empleado.Nombre}";
                    r.Cells[1].Value = empleado.NroDocumento;
                    r.Cells[2].Value = empleado.NombreLocalidad;
                    r.Cells[3].Value = empleado.NombreRol;
                    r.Cells[4].Value = empleado.NroTelefono;
                    r.Cells[5].Value = empleado.Email;
                    break;
                case Cancha cancha:
                    r.Cells[0].Value = cancha.NombreCancha;
                    break;

            }
            r.Tag = obj;

        }

        public static void AgregarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Add(r);
        }

        public static void QuitarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Remove(r);
        }

        public static void MostrarDatosEnGrilla<T>(DataGridView dgv, List<T> lista) where T : class
        {
            GridHelper.LimpiarGrilla(dgv);
            foreach (var obj in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgv);
                GridHelper.SetearFila(r, obj);
                GridHelper.AgregarFila(dgv, r);
            }

        }


    }
}
