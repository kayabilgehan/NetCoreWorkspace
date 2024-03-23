﻿using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers {
	public class CoreModule : ICoreModule {
		public void Load(IServiceCollection serviceCollection) {
			serviceCollection.AddMemoryCache(); // Injects IMemoryCache
			serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
		}
	}
}
