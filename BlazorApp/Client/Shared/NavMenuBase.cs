using Blazorise;
using Blazorise.Sidebar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using BlazorApp.Client.Interfaces;
using BlazorApp.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Client.Helpers;
using BlazorApp.Shared.Models;

namespace BlazorApp.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] IAuthDataServices authDataServices { get; set; }

        public Menu[] menuArray;

        public MenuResult menu;


        protected override async Task OnInitializedAsync()
        {
            await CargarMenu();
        }

        private async Task CargarMenu()
        {
            menu = await authDataServices.ObtenerMenu();
            menuArray = menu.Menu;
        }

    }
}