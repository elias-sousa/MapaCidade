using MapaCidade.Dominio.Core.DTO;
using MapaCidade.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Dominio.Core.Interfaces
{
   public interface IEstacaoRecarga : IBase<EstacaoRecarga>
    {
        IReadOnlyList<EstacaoRecargaDistanciaDTO> ProcurarEstacoesProximas(double latitude, double longitude);
    }
}
