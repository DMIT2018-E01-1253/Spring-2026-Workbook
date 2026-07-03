using HogWildSystemV2.BLL;
using HogWildSystemV2.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogWildSystemV2
{
	public static class HogWildEntensionV2
	{
		public static void AddBackendDependenciesV2(this IServiceCollection services,
			Action<DbContextOptionsBuilder> options)
		{

			services.AddDbContext<OLTPDMIT2018Context>(options);

			services.AddTransient<CustomerService>((ServiceProvider) =>
			{
				//  Retrieve an instance of OLTPDMIT2018Context from the service provider.
				var context = ServiceProvider.GetService<OLTPDMIT2018Context>();

				// Create a new instance of CustomerService,
				//   passing the OLTPDMIT2018Context instance as a parameter.
				return new CustomerService(context);
			});
		}
	}
}
