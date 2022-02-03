using BlazorApp.Client.Helpers;
using PlantillaUI.Services;
using System;
using System.Collections.Generic;

namespace BlazorApp.Client.Services
{
    public class MenuDataServices
    {
        ExampleService ExampleService = new ExampleService();

        private IEnumerable<Example> menu;
        public IEnumerable<Example> Menu
        {
            get
            {
                return menu;
            }
            set
            {
                menu = value;
            }
        }

        private string padre { get; set; }

        public string Padre
        {
            set
            {
                padre = value;
                NotifyDataChanged();
            }
            get
            {
                return padre;
            }
        }

        public MenuDataServices()
        {
            menu = ExampleService.Examples;
        }

        public void ResetMenu()
        {
            Menu = ExampleService.Examples;
            Padre = "";
        }

        public void FilterPanelMenu(string filtro)
        {
            if (string.IsNullOrEmpty(filtro))
            {
                Menu = ExampleService.Examples;
            }
            else
            {
                Menu = ExampleService.Filter(filtro);
            }
        }

        public event Action OnChange;

        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}
