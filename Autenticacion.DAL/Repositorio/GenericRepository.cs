using Autenticacion.DAL.Repositorio.Contrato;
using Autenticacion.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.DAL.Repositorio
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbautenticacionjwtContext _dbautenticacionjwtContext;

        public GenericRepository(DbautenticacionjwtContext dbautenticacionjwtContext)
        {
            _dbautenticacionjwtContext = dbautenticacionjwtContext;
        }

        public  Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null!)
        {
            try
            {
                var query = filtro==null? _dbautenticacionjwtContext.Set<T>() : _dbautenticacionjwtContext.Set<T>().Where(filtro);

                return Task.FromResult(query);
            }
            catch
            {
                throw;
            }
        }

        public Task<T> Crear(T modelo)
        {
            try
            {
                _dbautenticacionjwtContext.Set<T>().Add(modelo);
                _dbautenticacionjwtContext.SaveChanges();
                return Task.FromResult(modelo);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Editar(T modelo)
        {
            try
            {
                _dbautenticacionjwtContext.Set<T>().Update(modelo);
                _dbautenticacionjwtContext.SaveChanges();
                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Eliminar(T modelo)
        {
            try
            {
                _dbautenticacionjwtContext.Set<T>().Remove(modelo);
                _dbautenticacionjwtContext.SaveChanges();
                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public Task<T> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                T modeloencontrado = _dbautenticacionjwtContext.Set<T>().Where(filtro).FirstOrDefault()!;
                return Task.FromResult(modeloencontrado);
            }
            catch
            {
                throw;
            }
            
        }
    }
}
