using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using BlazorApp.Client.Helpers;
using System.Threading.Tasks;

namespace BlazorApp.Base
{
    public abstract class FOABMComponentBase : ComponentBase
    {
        /// <summary>
        /// Variable de evento para ejecutar la acción de volver atras (Pagina o panel anterior).
        /// </summary>
        [Parameter] public EventCallback VolverAtras { get; set; }
        /// <summary>
        /// Variable de evento para ejecutar la acción de buscar.
        /// </summary>
        [Parameter] public EventCallback Buscar { get; set; }
        /// <summary>
        /// Variable base para verificar los atributos de válidación que tiene el modelo antes de realizar alguna acción.
        /// <br>Nota: Debe ser inicializado con el modelo (no nulo) antes que cualquier llamada Http o método asíncrono al inicializar el formulario.</br>
        /// </summary>
        protected EditContext editContext { get; set; }
        protected bool visibleModal { get; set; }
        protected string mensajeDescripcion { get; set; }
        protected string mensajeAlerta { get; set; }
        protected Color color { get; set; }
        protected IconName iconName { get; set; }
        protected string iconNameString { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        #region Métodos Basicos para las funcionalidad del formulario
        /// <summary>
        /// Método Limpiar para el panel de datos. El cual se ejecuta al presionar el boton "Limpiar" ubicado en la barra de botones.
        /// <br>Este método setea el modelo como nuevo.</br>
        /// <br>Nota: Requiere el método CargarEditContext(), para que surta efecto en la parte visual.</br>
        /// <example> Por Ejemplo:
        /// <code>
        ///     <br>{</br>
        ///     <br>modelo = new modelo();</br>
        ///     <br>base.Limpiar(); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// </example>
        /// </summary>
        protected virtual void Limpiar()
        {
            CargarEditContext();
        }
        /// <summary>
        /// Método Guardar para el panel de datos. El cual se ejecuta al presionar el boton "Guardar" ubicado en la barra de botones.
        /// <br>Este método guarda o modifica el modelo con la información ingresada en el panel de datos.</br>
        /// <br>Se recomienda usar los métodos de ShowNotification(Correcto, Error, Alerta, etc.) y EventoBuscar() según se requiera.</br>
        /// <br>Ejemplo del uso correcto (el orden de ejecución es según su lógica):</br>
        /// <code>
        ///     <br>{</br>
        ///     <br>ShowNotificationCorrecto("su mensaje");</br>
        ///     <br>await EventoBuscar();</br>
        ///     <br>}</br>
        /// </code>
        /// <br>Nota: En caso de usar ventanas emergentes del ejemplo anterior no es necesario implementar el EventoBuscar();.</br>
        /// </summary>
        protected abstract void Guardar();
        /// <summary>
        /// Método Editar para el panel de datos.
        /// <br>Este método recupera la informacion del registro seleccionado desde la grilla de búsqueda del panel de búsqueda. Dicha 
        /// información se carga al modelo.</br>
        /// <br>Nota: Luego de cargar el modelo, es requerido llamar al método de CargarEditContext().</br>
        /// </summary>
        /// <param name="id">Valor de la llave primaria del registro.</param>
        public virtual void Editar(int? id)
        {
        }

        public virtual void EditarFlujo(int? id)
        {
        }

        public virtual void CambiarVisibilidadBotones()
        {
        }

        //public virtual void EditarFlujo(int? id,string flujoId,string pasoId)
        //{
        //}
        /// <summary>
        /// Método Eliminar para el panel de datos. El cual se ejecuta al presionar el boton "Eliminar" ubicado en la barra de botones 
        /// o desde la grilla de búsqueda del panel de búsqueda.
        /// <br>Este método elimina el registro seleccionado, ya sea que este en el panel de datos o búsqueda.</br>
        /// <br>Nota: Luego de eliminar el registro, es requerido llamar a los métodos Limpiar(), ShowNotification(Correcto, Error, Alerta, etc.) 
        /// y EventoBuscar() según corresponda.</br>
        /// <br>Ejemplo del uso correcto (el orden de ejecución es según su lógica):</br>
        /// <code>
        ///     <br>{</br>
        ///     <br>Limpiar();</br>
        ///     <br>ShowNotificationCorrecto("su mensaje");</br>
        ///     <br>await EventoBuscar();</br>
        ///     <br>}</br>
        /// </code>
        /// </summary>
        public abstract void Eliminar();
        /// <summary>
        /// Método Cancelar para el panel de datos. El cual se ejecuta al presionar el boton "Cancelar" ubicado en la barra de botones.
        /// <br>Este método vuelve al estado inicial (la informacion) con el que se visualizó por primera vez el panel de datos.</br>
        /// <br>Nota: Según la logica aplicada, vea si es requerido llamar al método de CargarEditContext() o Limpiar().</br>
        /// </summary>
        protected abstract void Cancelar();
        /// <summary>
        /// Método Volver Atras para el panel de datos.
        /// <br>Este llama a los métodos Limpiar() y MostrarPaneles().</br>
        /// <br>Nota: La variable VolverAtras(parametro evento) no debe estar nulo.</br>
        /// </summary>
        /// <returns></returns>
        public virtual void Imprimir(int? id)
        {
        }

        /// <summary>
        /// Método Volver Atras para el panel de datos.
        /// <br>Este llama a los métodos Limpiar() y MostrarPaneles().</br>
        /// <br>Nota: La variable VolverAtras(parametro evento) no debe estar nulo.</br>
        /// </summary>
        /// <returns></returns>
        private async Task EventoVolverAtrasPrivado()
        {
            Limpiar();
            LimpiarMensajesNotificaciones();
            await VolverAtras.InvokeAsync("");
        }
        /// <summary>
        /// Método Volver Atras para el panel de datos.
        /// <br>Este llama a los métodos Limpiar() y MostrarPaneles().</br>
        /// <br>Nota: La variable VolverAtras(parametro evento) no debe estar nulo y debe estar inicializado el método Limpiar(), 
        /// ya que son requeridos para un buen funcionamiento.</br>
        /// <br>En caso de necesitar editar seguir el siguiente ejemplo:</br>
        /// <code>
        ///     <br>{</br>
        ///     <br>//Codigo a implemetar</br>
        ///     <br>base.EventoVolverAtras(); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// </summary>
        /// <returns></returns>
        protected virtual async Task EventoVolverAtras()
        {
            await EventoVolverAtrasPrivado();
        }
        /// <summary>
        /// Método Buscar para el panel de datos.
        /// <br>Este llama al método Buscar() del panel de búsqueda.</br>
        /// <br>Nota: La variable Buscar(parametro evento) no debe estar nulo, ya que es requerido para un buen funcionamiento.</br>
        /// </summary>
        /// <returns></returns>
        private async Task EventoBuscarPrivado()
        {
            await Buscar.InvokeAsync("");
        }
        /// <summary>
        /// Método Buscar para el panel de datos.
        /// <br>Este llama al método Buscar() del panel de búsqueda.</br>
        /// <br>Nota: La variable Buscar(parametro evento) no debe estar nulo, ya que es requerido para un buen funcionamiento.</br>
        /// <br>En caso de necesitar editar seguir el siguiente ejemplo:</br>
        /// <code>
        ///     <br>{</br>
        ///     <br>//Codigo a implemetar</br>
        ///     <br>base.EventoBuscar(); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// </summary>
        /// <returns></returns>
        protected virtual async Task EventoBuscar()
        {
            await EventoBuscarPrivado();
        }
        /// <summary>
        /// Método de inicialización para la variable editContext.
        /// <br>Ejemplo de uso:</br>
        /// <code>
        ///     <br>{</br>
        ///     <br>editContext = new EditContext("variable modelo");</br>
        ///     <br>base.CargarEditContext(); //Esta linea es neceseria y no se debe remover o quitar.</br>
        ///     <br>}</br>
        /// </code>
        /// </summary>
        protected virtual void CargarEditContext()
        {
            StateHasChanged();
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
            visibleModal = true;
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
            visibleModal = true;
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
            visibleModal = true;
            this.mensajeDescripcion = mensajeDescripcion;
            this.mensajeAlerta = mensajeAlerta;
            color = Color.Danger;
            iconName = IconName.TimesCircle;
            this.iconNameString = iconNameString;

            StateHasChanged();
        }

        private void LimpiarMensajesNotificaciones()
        {
            this.mensajeDescripcion = string.Empty;
            this.mensajeAlerta = string.Empty;
        }
        #endregion

        #region Metodos de Ayuda
        /// <summary>
        /// Método que recuperar el tamaño maximo permitido que se puede ingresar al campo, definido en el DataAnnotations del modelo.
        /// </summary>
        /// <param name="obj">Variable del modelo definido.</param>
        /// <param name="property">Nombre del campo, propiedad o atributo del modelo que se necesita recuperar el tamaño.</param>
        /// <returns></returns>
        public int ObtenerMaxLength(object obj, string property)
        {
            return obj.ObtenerMaxlength(property);
        }
        #endregion
    }
}
