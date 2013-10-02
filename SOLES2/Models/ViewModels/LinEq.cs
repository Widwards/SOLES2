using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOLES2.Models.ViewModels
{
    public class LinEq
    {
        [Required]
        public string Expression { get; set; }

        public int Size { get; set; }
    }
}