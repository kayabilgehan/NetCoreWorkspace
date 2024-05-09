using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions;
using Core.DependencyResolvers;

namespace WebAPI
{
    public class Program {
		public static void Main(string[] args) {

			var builder = WebApplication.CreateBuilder(args);

			// Autofac
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

			// Add services to the container.

			builder.Services.AddControllers();
			//Cors Step 1
			builder.Services.AddCors();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => {
					options.TokenValidationParameters = new TokenValidationParameters {
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
					};
				});

			builder.Services.AddDependencyResolvers(new ICoreModule[] {
				new CoreModule()
			});

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

			app.ConfigureCustomExceptionMiddleware();

			//Cors Step 2
			app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}