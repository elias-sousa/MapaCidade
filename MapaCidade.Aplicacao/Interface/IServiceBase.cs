using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Aplicacao.Interface
{
   public interface IServiceBase<T> where T : class
    {
        T Adicionar(T obj);

        T Atualizar(T obj);

        T EncontrarPorId(Guid id);

        T Deletar(T obj);

        IReadOnlyList<T> ListarTodos();

    }
}
