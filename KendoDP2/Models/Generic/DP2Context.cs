using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Generic
{
    public partial class DP2Context : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DBGenericRequester<Rol> TablaRoles { get; set; }
        public DBGenericRequester<Usuario> TablaUsuarios { get; set; }
        
        public DP2Context()
            //: base("Data Source=inti.lab.inf.pucp.edu.pe;Initial Catalog=inf2450881h4;Persist Security Info=True;User ID=inf2450881h4dba;Password=zapatilla")
        {
            TablaRoles = new DBGenericRequester<Rol>(this, Roles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, Usuarios);
        }

        private void SeedRol()
        {
            TablaRoles.AddElement(new Rol("Administrador"));
            TablaRoles.AddElement(new Rol("Invitado"));
        }

        private void SeedUsuario()
        {
            var administrador = TablaRoles.FindByAttributeStringAsSingle("Nombre", "Administrador");
            var invitado = TablaRoles.FindByAttributeStringAsSingle("Nombre", "Invitado");
            TablaUsuarios.AddElement(new Usuario("a", "b", TablaRoles.FindByID(1)));
            TablaUsuarios.AddElement(new Usuario("c", "d", TablaRoles.FindByID(2)));
        }

        // Colocar aqui todos los seeds.
        public void Seed(){
            SeedRol();
            SeedUsuario();
        }

        // Colocar aqui el codigo referente al modelado 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

    /*
     * Elegir una de las dos siguientes lineas. La primera es para limpiar la BD cada vez que el modelo cambia. La segunda es para limpiar la BD
     * siempre que se corra la aplicacion. En ambos casos, al limpiar la BD se realiza el seed.
     */
   public class DP2ContextInitializer : DropCreateDatabaseIfModelChanges<DP2Context>{
   //public class DP2ContextInitializer : DropCreateDatabaseAlways<DP2Context>{
        protected override void Seed(DP2Context context){
            context.Seed();
        }
    }
}