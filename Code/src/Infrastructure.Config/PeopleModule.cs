using Application;
using Autofac;
using Infrastructure.Persistence.Ef;
using Infrastructure.Query.Ef;
using Microsoft.EntityFrameworkCore;
using Framework.Command;
using Framework.Query;

namespace Infrastructure.Config;

public class PeopleModule: Module
{
    private readonly string _commandConnectionString;
    private readonly string _queryConnectionString;

    public PeopleModule(string commandConnectionString, string queryConnectionString)
    {
        _commandConnectionString = commandConnectionString;
        _queryConnectionString = queryConnectionString;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AutofacCommandHandlerResolver>().As<ICommandHandlerResolver>().InstancePerLifetimeScope();
        builder.RegisterType<AutofacQueryHandlerResolver>().As<IQueryHandlerResolver>().InstancePerLifetimeScope();
        builder.RegisterType<AutofacRequestHandlerResolver>().As<IRequestHandlerResolver>().InstancePerLifetimeScope();
        builder.RegisterType<QueryBus>().As<IQueryBus>().InstancePerLifetimeScope();
        builder.RegisterType<CommandBus>().As<ICommandBus>().InstancePerLifetimeScope();
        builder.RegisterType<RequestBus>().As<IRequestBus>().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(PersonCommandHandlers).Assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
            .InstancePerLifetimeScope();


        builder
            .RegisterAssemblyTypes(typeof(PersonQueryHandlers).Assembly)
            .AsClosedTypesOf(typeof(IQueryHandler<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();  
        
        builder
            .RegisterAssemblyTypes(typeof(PersonRequestHandlers).Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(PersonRepository).Assembly)
            .Where(type => typeof(IRepository).IsAssignableFrom(type))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.Register(c => CommandDbContext(_commandConnectionString))
            .As<PeopleDbContext>()
            .InstancePerLifetimeScope();

        builder.Register(c => QueryDbContext(_queryConnectionString))
            .As<PeopleQueryDbContext>()
            .InstancePerLifetimeScope();
    }
    private PeopleDbContext CommandDbContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder().UseSqlServer(connectionString);

        var context = new PeopleDbContext(options.Options);

        if (!context.Database.CanConnect())
        {
            context.Database.Migrate();

            context.Database.EnsureCreated();
        }
        return context;
    }
    private PeopleQueryDbContext QueryDbContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder().UseSqlServer(connectionString);

        var context = new PeopleQueryDbContext(options.Options);

        return context;

    }
}