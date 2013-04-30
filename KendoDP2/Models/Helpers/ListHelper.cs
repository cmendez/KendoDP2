using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Helpers
{
    public class ListHelper
    {
        public static int GetIndexOfElement<T>(IList<T> lista, int ID)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].GetType().GetMember("ID").GetValue(lista[i]).Equals(ID))
                    return i;
            }
            return 0;
        }
    }
}