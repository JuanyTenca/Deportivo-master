using Deportivo.Entidades.Entidades;
using Deportivo.Servicios.Interfaces;
using Deportivo.Servicios.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deportivo.Windows.Helpers
{
    public class CombosHelper
    {

        public static void CargarComboLocalidades(ref ComboBox combo)
        {
            IServicioLocalidades servicioLocalidades = new ServicioLocalidades();
            var lista = servicioLocalidades.GetLocalidades(null);
            var defaultLocalidad = new Localidad()
            {
                LocalidadId = 0,
                NombreLocalidad = "Seleccione Localidad"
            };
            lista.Insert(0, defaultLocalidad);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreLocalidad";
            combo.ValueMember = "LocalidadId";
            combo.SelectedIndex = 0;
        }

        public static void CargarComboRoles(ref ComboBox combo)
        {
            IServicioRoles servicioRoles = new ServicioRoles();
            var lista = servicioRoles.GetRoles(null);
            var defaultRol = new Rol()
            {
                RolId = 0,
                Descripcion = "Seleccione Rol"
            };
            lista.Insert(0, defaultRol);
            combo.DataSource = lista;
            combo.DisplayMember = "Descripcion";
            combo.ValueMember = "RolId";
            combo.SelectedIndex = 0;
        }

    }
}
