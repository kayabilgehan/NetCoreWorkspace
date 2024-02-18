using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;

namespace WebAPI {
	public class Program {
		public static void Main(string[] args) {

			var builder = WebApplication.CreateBuilder(args);

			// Autofac
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject -> IoC Container
			// AOP
			// builder.Services.AddSingleton<IProductService, ProductManager>();
			// builder.Services.AddSingleton<IProductDal, EfProductDal>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}