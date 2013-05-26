using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Generic
{
    public class Archivo : DBObject
    {
        public byte[] Data { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Mime { get; set; }
    }
}