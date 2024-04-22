using Autofac;
using System.Reflection;


namespace WebApplication1.Config
{
    public class AutofacModuleRegister:Autofac.Module
    {
        //注册接口
        protected override void Load(ContainerBuilder builder)
        {
            Assembly InterfaceServiceAssembly = Assembly.Load("IService");
            Assembly ServiceAssembly = Assembly.Load("Service");
            builder.RegisterAssemblyTypes(InterfaceServiceAssembly, ServiceAssembly).AsImplementedInterfaces();

            Assembly InterfaceRepoAssembly = Assembly.Load("IRepo");
            Assembly ReopAssembly = Assembly.Load("Repo");
            builder.RegisterAssemblyTypes(InterfaceRepoAssembly, ReopAssembly).AsImplementedInterfaces();
        }
    }
}
