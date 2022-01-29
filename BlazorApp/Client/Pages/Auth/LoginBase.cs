using BlazorApp.Client.Auth;
using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Shared;
using BlazorApp.Shared.Models;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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


        public UserLogin userInfo = new UserLogin();

        public User user = new User();

        public async Task Loger()
        {
            var modal = Modal.Show<ModalWait>("", SharedModalOptions.modalOptionsWait);

            userInfo.CodigoApp = "CIS";

            LoginResult loginResult = await authDataServices.LoginAsync(userInfo);

            if (loginResult.Successful)
            {
                await loginService.Login(loginResult.user.accessToken);
                navigationManager.NavigateTo("");
            }
            else
            {
                modal.Close();
                Modal.Show<ModalInfo>("Error", SharedModalOptions.SetParameterModalInfo(loginResult.Error, "alert alert-danger"), SharedModalOptions.modalOptionsInfo);

            }

            //var result = await http.PostAsJsonAsync<UserLogin>("Login/Login", userInfo);
            //if (result.IsSuccessStatusCode)
            //{
            //    await loginService.Login(result.Content.ReadAsStringAsync().Result);
            //    uriHelper.NavigateTo("");
            //}
            //else
            //{
            //    modal.Close();
            //    Modal.Show<ModalInfo>("Error", SharedModalOptions.SetParameterModalInfo(result.Content.ReadAsStringAsync().Result, "alert alert-danger"), SharedModalOptions.modalOptionsInfo);
            //}

        }
    }
}
