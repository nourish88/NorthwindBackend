using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
namespace Business.DependencyResolvers.AutoFac
{
   public class AutoFacBusinessModule:Module
    {// dependency Injection da karşşılık Interfaceere karşılık gelen sınıfları yazdığımız modul.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();

            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        }
    }
}
