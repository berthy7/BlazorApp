using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Data
{
    public class AppSettings
    {
        public string UrlSeguridad { get; set; }
        public string UrlNegocio { get; set; }

        public string UrlMaestros { get; set; }

        public string UrlBpm { get; set; }
        public string CodeApp { get; set; }
    }
}
