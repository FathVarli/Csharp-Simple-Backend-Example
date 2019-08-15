using DevFramework.Core.DataAccess;
using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Core.DataAccess.NHihabernate;
using DevFramework.DataAccess.Concrete.NHibernate.Helpers;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    public class BusinessModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();

            Bind<IUserService>().To<UserManager>();
            Bind<IUserDal>().To<EfUserDal>();


            //standart
            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>)).InSingletonScope();
            Bind<DbContext>().To<NorthwindContext>().InSingletonScope();
            Bind<NHibernateHelper>().To<SqlServerHelper>().InSingletonScope();
        }
    }
}
