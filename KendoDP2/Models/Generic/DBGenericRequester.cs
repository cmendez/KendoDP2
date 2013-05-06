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
                ModifyElement(elemento_a_eliminar);
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
        public List<T> All(bool incluyeEliminadoLogico = false)
        {
            var res = Dbset.ToList();
            if (res == null) return new List<T>();
            List<T> ans = new List<T>();
            foreach( var item in res )
                if (incluyeEliminadoLogico || !incluyeEliminadoLogico && !item.IsEliminado) ans.Add(item);
            return ans;
        }

        /*
         * Ejecuta Where. Si se desea obtener algun eliminado logico, se debe especificar aparte.
         */
        public List<T> Where(Func<T, bool> predicate, bool incluyeEliminadoLogico = false)
        {
            try
            {
                return Dbset.Where(predicate).Where(p => !p.IsEliminado || incluyeEliminadoLogico).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /*
         * Retorna un elemento que cumpla cierta condicion
         */
        public T One(Func<T, bool> predicate, bool incluyeEliminadoLogico = false)
        {
            try
            {
                return Dbset.Where(predicate).Where(p => !p.IsEliminado || incluyeEliminadoLogico).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Any(Func<T, bool> predicate, bool incluyeEliminadoLogico = false)
        {
            try
            {
                return Dbset.Where(predicate).Where(p => !p.IsEliminado || incluyeEliminadoLogico).Count() > 0;
            }
            catch (Exception)
            {
                return false;
            }
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
         * Modifica en la base de datos el elemento dado por ID. El objeto camposCambiados debe tener null en todo campo que no cambia. El resto de campos
         * debe tener su nuevo valor. No se puede modificar el ID.
         */
        public void ModifyElement(T camposCambiados)
        {
            if (!(camposCambiados.ID >= 1))
                throw new Exception("El campo ID no puede ser null");
        
            var elemento_db = Dbset.Find(camposCambiados.ID);
            if (elemento_db == null) throw new Exception("No existe el ID en la BD");
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(camposCambiados))
            {
                if (!property.Name.Equals("ID") && property.GetValue(camposCambiados) != null)
                {
                    property.SetValue(elemento_db, property.GetValue(camposCambiados))  ;
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

    }
}