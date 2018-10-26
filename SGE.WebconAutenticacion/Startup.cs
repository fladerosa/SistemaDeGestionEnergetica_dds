using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SGE.Entidades.Sesion;

[assembly: OwinStartupAttribute(typeof(SGE.WebconAutenticacion.Startup))]
namespace SGE.WebconAutenticacion {
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // creating Creating Manager role 
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        role.Name = "Admin";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Cliente"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
    role.Name = "Cliente";
                roleManager.Create(role);

            }
          
        }
    }
}
