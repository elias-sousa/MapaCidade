using MapaCidade.Aplicacao.Interface;
using MapaCidade.Aplicacao.Service;
using MapaCidade.Dominio.Core.Interfaces;
using MapaCidade.Infra.Bd.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapaCidade.Apresentacao.MVC.DI
{
    public static partial class EstacaoRecargaModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void ResolveEstacaoRecarga(this IServiceCollection services)
        {

            services.AddTransient(typeof(IBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IEstacaoRecarga, EstacaoRecargaRepository>();
            services.AddTransient<IEstacaoRecargaService, EstacaoRecargaService>();
           
        }
    }
}
