using Autenticacion.BLL.Servicios;
using Autenticacion.BLL.Servicios.Contrato;
using Autenticacion.DAL.Repositorio;
using Autenticacion.DAL.Repositorio.Contrato;
using Autenticacion.Model;
using Autenticacion.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbautenticacionjwtContext>(option=>
            option.UseSqlServer(configuration.GetConnectionString("CadenaSQL")));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IProductoService,ProductoService>();
            services.AddScoped<IUsuarioService,UsuarioService>();
        }
    }
}
