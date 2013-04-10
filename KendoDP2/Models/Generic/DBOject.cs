using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Models.Generic
{
    public abstract class DBOject
    {
        [Key]
        public int ID { get; set; }
        public bool IsEliminado { get; set; }
    }
}