using MapaCidade.Infra.Bd.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapaCidade.Apresentacao.MVC.DI
{
    public static partial class ContextModule
    {
        public static void ResolveGestQualContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("MapaEstacao");

            services.AddDbContext<MapaContext>(options =>
            {
                options.UseSqlServer(connection);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
