using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Data
{
    public static class TipoMensaje
    {
        //Guardar
        public static string GuardarFaltaCamposObligatorios = "Por favor verifique los campos obligatorios.";
        public static string GuardarExitoso = "Registro guardado correctamente.";
        public static string GuardarError = "No se pudo guardar el registro.";

        //Modificar
        public static string ModificarExitoso = "Registro modificado correctamente.";
        public static string ModificarError = "No se pudo modificar el registro.";
        //Eliminar
        public static string EliminarExitoso = "Registro eliminado correctamente.";
        public static string EliminarError = "No se pudo eliminar el registro.";
        public static string EliminarFaltaSeleccion = "Debe seleccionar un registro para eliminar.";
    }
}
