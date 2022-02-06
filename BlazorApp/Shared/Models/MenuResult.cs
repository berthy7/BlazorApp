using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class MenuResult
    {
        public bool SuccessFul { get; set; }
        public string Error { get; set; }
        public string IdUsuarioApp { get; set; }
        public string RolUsuarioApp { get; set; }
        public Menu[] Menu { get; set; }
    }
}
