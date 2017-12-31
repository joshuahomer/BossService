using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossService.Models
{
    public class GenericRestResponse
    {
        public bool Success { get; set; }
        public object Response { get; set; }
    }
}