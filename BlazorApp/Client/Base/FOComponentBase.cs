using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace PlantillaUI.Base
{
    public abstract class FOComponentBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// Injección de servicio para mostrar los tooltips (etiquetas textos).
        /// </summary>
        //[Inject] TooltipService tooltipService { get; set; }
        /// <summary>
        /// Injección de servicio para navegación entre las paginas.
        /// </summary>
        [Inject] NavigationManager navigationManager { get; set; }
        /// <summary>
        /// Injección de servicio para mostrar las notificaciones para las acciones y de todo tipo.
        /// </summary>
        //[Inject] NotificationService NotificationService { get; set; }
        /// <summary>
        /// Injección de servicio para las ventanas emergentes.
        /// </summary>
        //[Inject] protected DialogService DialogService { get; set; }

        #region Variables para las funciones del formulario
        /// <summary>
        /// Variable para indicar si se muestran los filtros de búsqueda.
        /// </summary>
        protected bool mostrarFiltrosBusqueda { get; set; }
        /// <summary>
        /// Variable para indicar si se muestra el panel de Búsqueda.
        /// </summary>
        protected bool mostrarBusqueda { get; set; }
        /// <summary>
        /// Variable para indicar si se muestra el panel de Datos.
        /// </summary>
        protected bool mostrarCrearEditar { get; set; }
        /// <summary>
        /// Variable bandera para verificar si muestra el panel de Reporte
        /// </summary>
        ///         /// <summary>
        /// Variable para indicar si se muestra el panel de Datos.
        /// </summary>
        protected bool mostrarReporte { get; set; }
        protected bool volverAtras { get; set; }
        #endregion

        #region Variables para las notificaciones de alertas
        protected bool visibleAlert { get; set; }
        protected string mensajeDescripcion { get; set; }
        protected string mensajeAlerta { get; set; }
        protected Color color { get; set; }
        protected IconName iconName { get; set; }
        protected string iconNameString { get; set; }
        #endregion

        /// <summary>
        /// Método para la visualización de los tooltips (etiquetas texto).
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="options"></param>
        #region Tooltip en caso de que se quiera
        //public void ShowTooltip(ElementReference elementReference, TooltipOptions options = null) => tooltipService.Open(elementReference, options.Text, options);
        #endregion

        protected override Task OnInitializedAsync()
        {
            //DialogService.OnClose += OnCloseDialog;
            mostrarFiltrosBusqueda = true;
            mostrarBusqueda = false;
            mostrarCrearEditar = true;
            mostrarReporte = false;
            volverAtras = true;

            return base.OnInitializedAsync();
        }

        #region Métodos Basicos para las funcionalidad del formulario
        /// <summary>
        /// Método de búsqueda para los registros.
        /// </summary>
        /// <returns></returns>
        protected abstract Task Buscar();
        /// <summary>
        /// Método editar para el panel de búsqueda. El cual se ejecuta al presionar el boton "Editar" ubicado en la grilla de búsqueda
        /// en la columna de Acciones.
        /// <br>Este llama al método editar del panel de datos y mostrar paneles.</br>
        /// <example> Por Ejemplo:
        /// <code>
        ///     <br>{</br>
        ///     <br>formEdit.Editar(id);</br>
        ///     <br>base.Editar(id); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// <br>Nota: En caso de usar ventanas emergentes el ejemplo anterior no es necesario.</br>
        /// </example>
        /// </summary>
        /// <param name="id">Valor de la llave primaria del registro.</param>
        protected virtual void Editar(int id)
        {
            MostrarPanelesPrivado();
        }

        protected virtual void EditarFlujo(int id)
        {
            MostrarPanelesPrivado();
        }

        protected virtual void CambiarVisibilidadBotones()
        {
        
        }

        //protected virtual void EditarFlujo(int id,string flujoId,string pasoId)
        //{
        //    MostrarPanelesPrivado();
        //}
        /// <summary>
        /// Método eliminar para el panel de búsqueda. El cual se ejecuta al presionar el boton "Eliminar" ubicado en la grilla de búsqueda
        /// en la columna de Acciones.
        /// <br>Este setea el id en el objeto y llama al metodo eliminar del panel datos.</br>
        /// <example> Por Ejemplo:
        /// <code>
        ///     <br>{</br>
        ///         <br>formEdit.objeto.idPrimario = id;</br>
        ///         <br>formEdit.Eliminar();</br>
        ///     <br>}</br>
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="id">Valor de la llave primaria del registro.</param>
        protected virtual void Eliminar(int id)
        {
        }

        protected virtual void EliminarIntermedia(int id, int id2)
        {
        }

        protected virtual void Imprimir(int id)
        {
            MostrarPanelesPrivado();
        }
        #endregion

        #region Métodos para las funciones del formulario
        /// <summary>
        /// Método de la visualización para los paneles de búsqueda o datos.
        /// </summary>
        private void MostrarPanelesPrivado()
        {
            mostrarBusqueda = !mostrarBusqueda;
            mostrarCrearEditar = !mostrarCrearEditar;
            mostrarReporte = !mostrarReporte;
        }
        /// <summary>
        /// Método de la visualización para los paneles de búsqueda o datos.
        /// <example> En caso de necesitar editar seguir el siguiente ejemplo:
        /// <code>
        ///     <br>{</br>
        ///     <br>//Codigo a implemetar</br>
        ///     <br>base.MostrarPaneles(); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// </example>
        /// </summary>
        protected virtual void MostrarPaneles()
        {
            MostrarPanelesPrivado();
        }
        /// <summary>
        /// Método de navegación entre los paneles o pagina principal.
        /// </summary>
        private void VolverAtrasPrivado()
        {
            if (volverAtras)
            {
                navigationManager.NavigateTo("");
            }
            else
            {
                MostrarPanelesPrivado();
            }
        }
        /// <summary>
        /// Método de navegación entre los paneles o pagina principal.
        /// <example> En caso de necesitar editar seguir el siguiente ejemplo:
        /// <code>
        ///     <br>{</br>
        ///     <br>//Codigo a implemetar</br>
        ///     <br>base.MostrarPaneles(); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// </example>
        /// </summary>
        protected virtual void VolverAtras()
        {
            VolverAtrasPrivado();
        }
        #endregion

        #region Métodos para la ventana emergente
        protected virtual void OnCloseDialog(dynamic result)
        {
            Buscar();
        }
        #endregion

        #region Metodos para las notificaciones
        /// <summary>
        /// Método para visualizar mensaje de exito.
        /// <br>Este método levanta una notificación emergente de color verde en caso de exito sobre alguna acción realizada.</br>
        /// </summary>
        /// <param name="mensaje">Mensaje ha visualizar o mostrar en la notificación emergente.</param>
        protected void ShowNotificationCorrecto(string mensajeDescripcion, string mensajeAlerta = "", string iconNameString = "")
        {
            visibleAlert = true;
            this.mensajeDescripcion = mensajeDescripcion;
            this.mensajeAlerta = mensajeAlerta;
            color = Color.Success;
            iconName = IconName.CheckCircle;
            this.iconNameString = iconNameString;

            StateHasChanged();
        }
        /// <summary>
        /// Método para visualizar mensaje de alerta.
        /// <br>Este método levanta una notificación emergente de color naranja en caso de validación u información sobre alguna acción realizada.</br>
        /// </summary>
        /// <param name="mensaje">Mensaje ha visualizar o mostrar en la notificación emergente.</param>
        protected void ShowNotificationAlerta(string mensajeDescripcion, string mensajeAlerta = "", string iconNameString = "")
        {
            visibleAlert = true;
            this.mensajeDescripcion = mensajeDescripcion;
            this.mensajeAlerta = mensajeAlerta;
            color = Color.Warning;
            iconName = IconName.ExclamationTriangle;
            this.iconNameString = iconNameString;

            StateHasChanged();
        }
        /// <summary>
        /// Método para visualizar mensaje de error.
        /// <br>Este método levanta una notificación emergente de color rojo en caso de error sobre alguna acción realizada.</br>
        /// </summary>
        /// <param name="mensaje">Mensaje ha visualizar o mostrar en la notificación emergente.</param>
        protected void ShowNotificationError(string mensajeDescripcion, string mensajeAlerta = "", string iconNameString = "")
        {
            visibleAlert = true;
            this.mensajeDescripcion = mensajeDescripcion;
            this.mensajeAlerta = mensajeAlerta;
            color = Color.Danger;
            iconName = IconName.TimesCircle;
            this.iconNameString = iconNameString;

            StateHasChanged();
        }
        #endregion

        public void Dispose()
        {
            //DialogService.OnClose -= OnCloseDialog;
        }
    }
}
