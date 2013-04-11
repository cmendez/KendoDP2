using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;
using System.Data.Entity;
using System.ComponentModel;
using System.Data;

namespace KendoDP2.Models.Generic
{
    public class DBGenericRequester<T> where T : DBObject
    {
        public DbSet<T> Dbset { get; set; }
        private DbContext context;
        private Type classType;
       
        public DBGenericRequester(DbContext context, DbSet<T> dbset)
        {
            this.context = context;
            Dbset = dbset;
            classType = typeof(T);
        }

        /*
         * Remueve un elemento dado su id. Se debe especificar cuando se quiera un eliminado fisico.
         */
        public void RemoveElementByID(int ID, bool isEliminadoFisico = false)
        {
            var elemento_a_eliminar = Dbset.Find(ID);
            if (elemento_a_eliminar == null) throw new Exception("No existe el ID en la BD");
            if (!isEliminadoFisico)
            {
                elemento_a_eliminar.IsEliminado = true;
                ModifyElement(elemento_a_eliminar, ID);
            }
            else
            {
                Dbset.Remove(elemento_a_eliminar);
                context.SaveChanges();
            }
        }

        /* 
         * Retorna todos los elementos en la tabla.
         */
        public List<T> GetAll(bool incluyeEliminadoLogico = false)
        {
            var res = Dbset.ToList();
            if (res == null) return new List<T>();
            List<T> ans = new List<T>();
            foreach( var item in res )
                if (incluyeEliminadoLogico || !incluyeEliminadoLogico && !item.IsEliminado) ans.Add(item);
            return ans;
        }

        /*
         * Busca un elemento por su ID.
         */
        public T FindByID(int ID, bool incluyeEliminadoLogico = true)
        {
            try
            {
                var a = Dbset.Find(ID);
                if (a.IsEliminado && !incluyeEliminadoLogico) return null;
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /*
         * Busca un elemento usando un atributo de tipo string como busqueda. 
         * name: Nombre del atributo en la clase misma. Por ejemplo, Username de la clase Usuario
         * value: Valor a buscar.
         */
        public T FindByAttributeStringAsSingle(string name, string value, bool incluyeEliminadoLogico = false)
        {
            var query = Dbset.SqlQuery("select * from " + classType.Name + " where " + name + " = '" + value + "'");
            var elementos = query.ToList().Where(x => !x.IsEliminado || incluyeEliminadoLogico).ToList();
            return elementos.Count > 0 ? elementos[0] : null;
        }

        /*
         * Modifica en la base de datos el elemento dado por ID. El objeto camposCambiados debe tener null en todo campo que no cambia. El resto de campos
         * debe tener su nuevo valor.
         */
        public void ModifyElement(T camposCambiados, int ID)
        {
            var elemento_db = Dbset.Find(ID);
            if (elemento_db == null) throw new Exception("No existe el ID en la BD");
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(camposCambiados))
            {
                if (!property.Name.Equals("ID") && property.GetValue(camposCambiados) != null)
                {
                    property.SetValue(elemento_db, property.GetValue(camposCambiados));
                }
            }
            context.Entry(elemento_db).State = EntityState.Modified;
            context.SaveChanges();
        }

        /*
         * Agrega el elemento dado a la base de datos. Esta funcion retorna el ID del elemento agregado.
         */
        public int AddElement(T element)
        {
            Dbset.Add(element);
            context.SaveChanges();
            return element.ID;
        }

        /*
         * Busca todos los elementos cuyos campos sean iguales a los especificados. Se especifican los campos enviando un objeto
         * del mismo tipo que se busca y se ponen en null todos los campos que no seran utilizados.
         */
        public List<T> BuscarElementosPorCampos(T campos)
        {
            string where = string.Empty;
            bool first_line = true;
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(campos))
            {
                if (property.GetValue(campos) != null)
                {
                    var value = property.GetValue(campos);
                    if (first_line)
                        first_line = false;
                    else where += " And ";
                    if (property.GetType() == where.GetType()) //query de un string
                        where += property.Name + ".StartsWith(\"" + (string)value + "\")";
                    else //query de cualquier otro tipo de dato
                        where += property.Name + " = " + value;
                }
            }
            var elementos = Dbset.SqlQuery("select * from " + campos.GetType().Name + " where " + where);
            return elementos.ToList();
        }
    }
}