using MapaCidade.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Aplicacao.Interface
{
    public interface IEstacaoRecargaService : IServiceBase<EstacaoRecarga>
    {
        IReadOnlyList<EstacaoRecarga> ProcurarEstacoesProximas(double latitude, double longitude);

        string GerarArquivo(Guid id, string Directory);
    }
}
