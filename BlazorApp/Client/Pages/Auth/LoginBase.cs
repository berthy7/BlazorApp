using BlazorApp.Client.Auth;
using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Shared;
using BlazorApp.Shared.Models;
using Blazored.Modal.Services;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages.Auth
{
    public class LoginBase : ComponentBase
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        [Inject] IAuthDataServices authDataServices { get; set; }
        [Inject] ILoginServices loginService { get; set; }

        public Validations annotationsValidations { get; set; }


        public UserLogin userInfo = new UserLogin();

        public bool modalVerificacionLogin { get; set; }


        public async Task Loger()
        {
            modalVerificacionLogin = true;
            //var modal = Modal.Show<ModalWait>("", SharedModalOptions.modalOptionsWait);

            //userInfo.CodigoApp = "CIS";

            LoginResult loginResult = await authDataServices.LoginAsync(userInfo);

            if (loginResult.Successful)
            {
                await loginService.Login(loginResult.user.accessToken);
                navigationManager.NavigateTo("");
            }
            else
            {
                //modal.Close();
                //Modal.Show<ModalInfo>("Error", SharedModalOptions.SetParameterModalInfo(loginResult.Error, "alert alert-danger"), SharedModalOptions.modalOptionsInfo);

            }

            modalVerificacionLogin = false;

        }

        public async Task Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await Loger();
            }
        }

    }
}
