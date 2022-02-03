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

namespace BlazorApp.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] MenuDataServices menuDataServices { get; set; }



        public IEnumerable<Example> examples;
        public SidebarInfo sidebarInfo { get; set; }

        protected override void OnInitialized()
        {

            examples = menuDataServices.Menu;

            CargarMenu();

            menuDataServices.OnChange += CambiosMenu;

        }

        private void CargarMenu()
        {
            sidebarInfo = new SidebarInfo();

            sidebarInfo.Brand = new SidebarBrandInfo
            {
                Text = "Ferroviaria Oriental"
            };

            List<SidebarItemInfo> sidebarItemInfos = new List<SidebarItemInfo>();

            CargarSidebarItemInfos(sidebarItemInfos);

            sidebarInfo.Items = sidebarItemInfos;
        }

        private void CargarSidebarItemInfos(List<SidebarItemInfo> sidebarItemInfos)
        {
            foreach (Example example in examples)
            {
                SidebarItemInfo padre = new SidebarItemInfo
                {
                    To = example.Path,
                    Text = example.Name,
                    Icon = example.Icon
                };

                if (example.Children != null)
                {
                    padre.SubItems = new List<SidebarItemInfo>();

                    CargarMenuSubMenu(example, padre);
                }

                sidebarItemInfos.Add(padre);
            }
        }

        private void CargarMenuSubMenu(Example example, SidebarItemInfo padre, SidebarItemInfo hijo = null)
        {
            foreach (Example exampleHijo in example.Children)
            {
                if (hijo == null)
                {
                    hijo = new SidebarItemInfo
                    {
                        To = exampleHijo.Path,
                        Text = exampleHijo.Name,
                        Icon = exampleHijo.Icon ?? null
                    };

                    padre.SubItems.Add(hijo);
                }
                else
                {

                    SidebarItemInfo SubHijo = new SidebarItemInfo
                    {
                        To = exampleHijo.Path,
                        Text = exampleHijo.Name,
                        Icon = exampleHijo.Icon
                    };

                    hijo.SubItems.Add(SubHijo);
                }

                if (exampleHijo.Children != null)
                {
                    hijo.SubItems = new List<SidebarItemInfo>();

                    CargarMenuSubMenu(exampleHijo, padre, hijo);
                }
                else
                {
                    if (hijo.SubItems == null)
                    {
                        hijo = null;
                    }
                }
            }
        }

        public void CargarHome()
        {
            menuDataServices.ResetMenu();

            ResetNavMenu();
        }

        private void CambiosMenu()
        {
            sidebarInfo.Items.ForEach(p =>
            {
                if (p.Text == menuDataServices.Padre)
                {
                    p.Visible = true;
                    return;
                }
                else
                {
                    if (p.SubItems != null)
                    {
                        p.SubItems.ForEach(h =>
                        {
                            if (h.Text == menuDataServices.Padre)
                            {
                                h.Visible = true;
                                return;
                            }
                            else
                            {
                                if (h.SubItems != null)
                                {
                                    h.SubItems.ForEach(sh =>
                                    {
                                        if (sh.Text == menuDataServices.Padre)
                                        {
                                            sh.Visible = true;
                                            return;
                                        }
                                    });
                                }
                            }
                        });
                    }
                }
            });

            StateHasChanged();
        }

        private void ResetNavMenu()
        {
            List<SidebarItemInfo> sidebarItemInfos = new List<SidebarItemInfo>();

            CargarSidebarItemInfos(sidebarItemInfos);

            sidebarInfo.Items = sidebarItemInfos;
        }

        public void Dispose()
        {
            menuDataServices.OnChange -= CambiosMenu;
        }

    }
}
