using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class Menu
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public bool Expanded { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Menu> Children { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
