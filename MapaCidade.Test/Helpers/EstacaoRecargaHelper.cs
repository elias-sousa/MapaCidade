using MapaCidade.Dominio.Core.Entities;
using MapaCidade.Dominio.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Test.Helpers
{
    internal class EstacaoRecargaHelper
    {
        internal static EstacaoRecarga MonteEstacaoRecarga(string nome, TipoRecarga tipo,  Double latitude, Double longitude)
        {
            return new EstacaoRecarga
            {
                Id = Guid.Parse(RandomHelper.GetString()),
                Nome = nome,
                Tipo = tipo,
                Latitude = latitude,
                Longitude = longitude
            };
        }

        internal static EstacaoRecarga MonteEstacaoRecarga()
        {
            return new EstacaoRecarga
            {
                Id = Guid.Parse(RandomHelper.GetString()),
                Nome = "Flamengo",
                Tipo = TipoRecarga.Movel,
                Latitude = -29895,
                Longitude = -2068652
            };
        }
    }
}
