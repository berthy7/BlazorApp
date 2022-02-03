
using BlazorApp.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantillaUI.Services
{
    public class ExampleService
    {
        Example[] allExamples = new[] {
            new Example()
            {
                Name = "Solicitud",
                Expanded = false,
                Icon = "far fa-list-alt",
                Children = new [] {
                    new Example() {
                        Name = "Compra",
                        Path = "compra",
                        Icon = "fas fa-edit"
                    },
                    new Example() {
                        Name = "Recepcion",
                        Path = "recepcion",
                        Icon = "fas fa-edit"
                    },
                    new Example() {
                        Name = "Despacho",
                        Path = "despacho",
                        Icon = "fas fa-edit"
                    },
                    new Example() {
                        Name = "Traspaso",
                        Path = "traspaso",
                        Icon = "fas fa-edit"
                    }   ,
                    
                }
            },
            new Example()
            {
                Name = "Ajuste",
                Expanded = false,
                Icon = "far fa-list-alt",
                Children = new [] {
                    new Example() {
                        Name = "Tecnico",
                        Path = "tecnico",
                        Icon = "fas fa-edit"
                    },
                    new Example() {
                        Name = "Variación",
                        Path = "variacion",
                        Icon = "fas fa-edit"
                    }
                }
            },
            new Example()
            {
                Name = "Cierre",
                Expanded = false,
                Icon = "far fa-list-alt",
                Children = new [] {
                    new Example() {
                        Name = "Turno",
                        Path = "turno",
                        Icon = "fas fa-edit"
                    },
                    new Example() {
                        Name = "Inventario",
                        Path = "inventario",
                        Icon = "fas fa-edit"
                    }
                }
            }

        };

        public IEnumerable<Example> Examples
        {
            get
            {
                return allExamples;
            }
        }

        public IEnumerable<Example> Filter(string term)
        {
            Func<string, bool> contains = value => value.Contains(term, StringComparison.OrdinalIgnoreCase);

            Func<Example, bool> filter = (example) => contains(example.Name) || (example.Tags != null && example.Tags.Any(contains));

            return Examples.Where(category => category.Children != null && category.Children.Any(filter))
                           .Select(category => new Example()
                           {
                               Name = category.Name,
                               Expanded = true,
                               Children = category.Children.Where(filter).ToArray()
                           }).ToList();
        }

        public Example FindCurrent(Uri uri)
        {
            return Examples.SelectMany(example => example.Children ?? new[] { example })
                           .FirstOrDefault(example => example.Path == uri.AbsolutePath || $"/{example.Path}" == uri.AbsolutePath);
        }

        public string TitleFor(Example example)
        {
            if (example != null && example.Name != "First Look")
            {
                return example.Title ?? $"Blazor {example.Name} | a free UI component by Radzen";
            }

            return "Free Blazor Components | 50+ controls by Radzen";
        }

        public string DescriptionFor(Example example)
        {
            return example?.Description ?? "The Radzen Blazor component library provides more than 50 UI controls for building rich ASP.NET Core web applications.";
        }
    }
}
