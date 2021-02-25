using System.Reflection;
using Autofac;
using FigoparaCaseStudyApi.Request;
using FigoparaCaseStudyApi.Validations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Module = Autofac.Module;

namespace FigoparaCaseStudyApi.Modules
{
    public class ApiServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = Assembly.GetExecutingAssembly();

            builder.RegisterGeneric(typeof(Logger<>))
                   .As(typeof(ILogger<>))
                   .SingleInstance();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(what => what.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(what => what.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();


            builder.RegisterType<UserAddValidation>().As<IValidator<UserAddRequest>>();
        }
    }
}