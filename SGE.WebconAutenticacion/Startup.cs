using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Sesion;
using SGE.Entidades.Usuarios;

[assembly: OwinStartupAttribute(typeof(SGE.WebconAutenticacion.Startup))]
namespace SGE.WebconAutenticacion
{
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
            //se crean roles
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //se inicializa super admin,		

                var user = new ApplicationUser();
                user.Nombre = "SuperAdministrador";
                user.Apellido = "SuperAdministrador";
                user.UserName = "master";
                user.Email = "master@gmail.com";

                string userPWD = "A@z200711";
                var chkUser = UserManager.Create(user, userPWD);

                //se le vincula el Rol admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
                //se sincroniza con nuestro SGEContext
                BaseRepositorio<Administrador> repoAdmin = new BaseRepositorio<Administrador>();

                Administrador admin = null;

                admin = new Administrador()
                {
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    NombreUsuario = user.UserName,
                    Password = userPWD,

                    Nui = "SuperAdmin",

                };
                repoAdmin.Create(admin);
            }
            //se crea un rol cliente que sera para todos los que se registren desde la pagina
                if (!roleManager.RoleExists("Cliente"))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = "Cliente";
                    roleManager.Create(role);

                }
                
            }
        }
    }

