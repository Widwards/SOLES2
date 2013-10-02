using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOLES2.Models
{
    public class LinEqInfo
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Solution { get; set; }
        public bool IsXForm { get; set; }

    }
}