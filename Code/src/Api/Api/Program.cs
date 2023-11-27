
using Api.Configurations;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var sqlConfiguration = configuration.GetSection(nameof(SqlServerConfiguration)).Get<SqlServerConfiguration>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new PeopleModule(sqlConfiguration.CommandConnectionString,
        sqlConfiguration.QueryConnectionString));
});

builder.Services.AddControllers(config =>
{
    config.Conventions.Add(new CqrsConvention());
});

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();