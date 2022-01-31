using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Components
{
    public class FOLoadingBase : ComponentBase
    {
        [Parameter] public bool visible { get; set; }
    }
}
