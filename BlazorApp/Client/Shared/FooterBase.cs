using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Shared
{
    public class FooterBase: ComponentBase
    {
        public string texto { get; set; }

        protected override Task OnInitializedAsync()
        {
            texto = "FO, Copyright &copy; " + DateTime.Now.Year.ToString();

            return base.OnInitializedAsync();
        }
    }
}
