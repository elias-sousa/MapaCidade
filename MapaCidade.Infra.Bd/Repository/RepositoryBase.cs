using MapaCidade.Dominio.Core.Interfaces;
using MapaCidade.Infra.Bd.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Infra.Bd.Repository
{
    public class RepositoryBase<T> : IBase<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<MapaContext> _optionsBuilder;

        public RepositoryBase(DbContextOptions<MapaContext> optionsBilder)
        {
            _optionsBuilder = optionsBilder;
        }

        public T Adicionar(T obj)
        {
            using(var bd = new MapaContext(_optionsBuilder))
            {
                bd.Set<T>().Add(obj);
                bd.SaveChanges();
            }
            return obj;
        }

        public T Atualizar(T obj)
        {
            using (var bd = new MapaContext(_optionsBuilder))
            {
                bd.Set<T>().Update(obj);
                bd.SaveChanges();
            }
            return obj;
        }

        public T Deletar(T obj)
        {
            using (var bd = new MapaContext(_optionsBuilder))
            {
                bd.Set<T>().Remove(obj);
                bd.SaveChanges();
            }
            return obj;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public T EncontrarPorId(Guid id)
        {
            using (var bd = new MapaContext(_optionsBuilder))
            {
               return bd.Set<T>().Find(id);               
            }
        }

        public IReadOnlyList<T> ListarTodos()
        {
            using (var bd = new MapaContext(_optionsBuilder))
            {
                return bd.Set<T>().AsNoTracking().ToList();
            }
        }
    }
}
