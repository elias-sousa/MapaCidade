using FluentAssertions;
using MapaCidade.Dominio.Core.Entities;
using MapaCidade.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MapaCidade.Test.Domain.Entities
{
    public class EstacaoRecargaTest
    {
        [Fact]
        public void EhValida_deve_retornar_true_quando_id_nome_estiverem_preenchidos()
        {
            var estacaoRecarga = new EstacaoRecarga
            {
                Id = Guid.Parse(RandomHelper.GetString()),
                Nome = RandomHelper.GetString(),
                Latitude = RandomHelper.GetDouble(),
                Longitude = RandomHelper.GetDouble(),
                Tipo = Dominio.Core.Enum.TipoRecarga.Movel
            };
            var actual = estacaoRecarga.EhValido();
            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        public void EhValida_deve_retornar_false_quando__id_nome_nao_estiverem_preenchidos(string nome)
        {
            var estacao = new EstacaoRecarga
            {
                Id = Guid.Parse(RandomHelper.GetString()),
                Nome = nome,
                Tipo = Dominio.Core.Enum.TipoRecarga.Movel,
                Latitude = RandomHelper.GetDouble(),
                Longitude = RandomHelper.GetDouble(),
            };
            var actual = estacao.EhValido();
            actual.Should().BeFalse();
        }
    }
}
