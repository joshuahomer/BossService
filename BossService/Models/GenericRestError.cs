using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossService.Models
{
    public class GenericRestError
    {
        public string HumanMessage { get; set; }
        public Exception Exception { get; set; }
    }
}