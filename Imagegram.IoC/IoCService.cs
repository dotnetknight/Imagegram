using Autofac;
using Autofac.Extras.DynamicProxy;
using Imagegram.Infrastructure.Bus.Command;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Infrastructure.Command;
using Imagegram.Repository.Repository;
using Imagegram.Services;
using Imagegram.Services.CommandHandlers;
using Imagegram.Services.QueryHandlers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Imagegram.IoC
{
    public class IoCService
    {
        public static void RegisterServices(ContainerBuilder builder, IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(typeof(FileService), typeof(FileService));

            services.AddTransient<ICommandBusAsync, CommandBusAsync>();
            services.AddTransient<IQueryBusAsync, QueryBusAsync>();

            services.AddTransient(typeof(HateoasLinksService), typeof(HateoasLinksService));
            services.AddTransient(typeof(MediaTypeCheckService), typeof(MediaTypeCheckService));
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));


            builder.RegisterAssemblyTypes(typeof(CreateAccountCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandlerAsync<,>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(CreatePostCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandlerAsync<,>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(CreateCommentOnPostCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandlerAsync<>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(DeleteAccountCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandlerAsync<>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(PostQueryHandler).Assembly)
              .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
              .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(PostsQueryHandler).Assembly)
              .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
              .EnableClassInterceptors();
        }
    }
}
