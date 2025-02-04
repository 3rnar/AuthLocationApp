using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AuthLocationApp.Application.Behaviours;

namespace AuthLocationApp.Application.DependencyInjection
{
   public static class ApplicationServiceRegistration
   {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services)
      {
         services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

         services.AddMediatR(cfg =>
         {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
         });

         return services;
      }
   }
}
