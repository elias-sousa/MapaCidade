using MapaCidade.Dominio.Core.DTO;
using MapaCidade.Dominio.Core.Entities;
using MapaCidade.Dominio.Core.Enum;
using MapaCidade.Dominio.Core.Interfaces;
using MapaCidade.Infra.Bd.Config;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Infra.Bd.Repository
{
    public class EstacaoRecargaRepository : RepositoryBase<EstacaoRecarga>, IEstacaoRecarga
    {
        private readonly DbContextOptions<MapaContext> _optionsBuilder;
        private string GetConnectString { get; }
        public EstacaoRecargaRepository(DbContextOptions<MapaContext> optionsBilder, IConfiguration config) : base(optionsBilder)
        {
            _optionsBuilder = optionsBilder;
            GetConnectString = config.GetConnectionString("MapaEstacao");
        }

        public IReadOnlyList<EstacaoRecargaDistanciaDTO> ProcurarEstacoesProximas(double latitude, double longitude)
        {
            string squery = @"
                SELECT Id, (6371 * acos(
                 cos( radians("+ latitude + @") )
                 * cos( radians( Latitude ) )
                 * cos( radians( Longitude ) - radians("+ longitude + @") )
                 + sin( radians("+ latitude + @") )
                 * sin( radians( Latitude ) ) 
                 )
                ) AS Distancia, Nome, Latitude, Longitude, Tipo
                FROM EstacaoRecargas 
                order by Distancia
            ";
            SqlConnection connection = new SqlConnection(GetConnectString);

            SqlCommand cmd = new SqlCommand(squery, connection)
            {
                CommandType = CommandType.Text
            };

            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = cmd
            };

            DataTable dt = new DataTable();

            ad.Fill(dt);

            List<EstacaoRecargaDistanciaDTO> listaDTO = new List<EstacaoRecargaDistanciaDTO>();

            EstacaoRecargaDistanciaDTO dto;

            foreach (DataRow row in dt.Rows)
            {
                dto = new EstacaoRecargaDistanciaDTO();
                dto.Id = Guid.Parse(row["Id"].ToString());
                dto.Nome = row["Nome"].ToString();
                dto.Latitude = double.Parse(row["Latitude"].ToString());
                dto.Longitude = double.Parse(row["Longitude"].ToString());
                dto.Tipo = (TipoRecarga)Enum.Parse(typeof(TipoRecarga), row["Tipo"].ToString());
                dto.Distancia = double.Parse(row["Distancia"].ToString());
                
                listaDTO.Add(dto);
            }
            return listaDTO;
        }
    }
}
