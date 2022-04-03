using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapaCidade.Aplicacao.Interface;
using MapaCidade.Aplicacao.Service;
using MapaCidade.Dominio.Core.Entities;
using MapaCidade.Dominio.Core.Interfaces;
using Xunit;
using MapaCidade.Test.Helpers;
using FluentAssertions;
using System.IO;

namespace MapaCidade.Test.Aplicacao.Service
{
   public class EstacaoRecargaServiceTest
    {
        private Mock<IEstacaoRecarga> _IestacaoRecarga;
        private Mock<IEstacaoRecargaService> _IestacaoRecargaService;
        private EstacaoRecargaService _estacaoRecargaService;
        public EstacaoRecargaServiceTest()
        {
            _IestacaoRecarga = new Mock<IEstacaoRecarga>();
            _IestacaoRecargaService = new Mock<IEstacaoRecargaService>();
            _IestacaoRecargaService.Setup(fes => fes.Deletar(It.IsAny<EstacaoRecarga>()));
            _estacaoRecargaService = new EstacaoRecargaService(_IestacaoRecarga.Object);
        }

        [Fact]
        public void ListarTodos_deve_chamar_estacoes_de_recargas()
        {
            _estacaoRecargaService.ListarTodos();

            _IestacaoRecarga.Verify(fes => fes.ListarTodos(), Times.Once);
        }
                
        [Fact]
        public void ListarPorId_deve_chamar_servico()
        {
            var id = Guid.Parse(RandomHelper.GetString());
            _estacaoRecargaService.EncontrarPorId(id);

            _IestacaoRecarga.Verify(fes => fes.EncontrarPorId(id), Times.Once);
        }

        [Fact]
        public void Salvar_EstadoRecarga_deve_funcionar()
        {
            var estacaoRecarga = EstacaoRecargaHelper.MonteEstacaoRecarga();

            _IestacaoRecarga.Setup(s => s.Adicionar(estacaoRecarga)).Returns(estacaoRecarga);

            var actual = _estacaoRecargaService.Adicionar(estacaoRecarga);

            _IestacaoRecarga.Verify(s => s.Adicionar(estacaoRecarga), Times.Once);

            actual.Should().NotBeNull();

        }

        [Fact]
        public void Atualizar_EstadoRecarga_deve_funcionar()
        {
            var estacaoRecarga = EstacaoRecargaHelper.MonteEstacaoRecarga();
            estacaoRecarga.Tipo = Dominio.Core.Enum.TipoRecarga.Veicular;

            _IestacaoRecarga.Setup(s => s.Atualizar(estacaoRecarga)).Returns(estacaoRecarga);

            var actual = _estacaoRecargaService.Atualizar(estacaoRecarga);

            _IestacaoRecarga.Verify(s => s.Atualizar(estacaoRecarga), Times.Once);

            actual.Should().NotBeNull();

        }

        [Fact]
        public void Deletar_estacao_recarga_nao_deve_funcionar()
        {
            EstacaoRecarga estacaoRecarga = new EstacaoRecarga();
            
            var actual = _estacaoRecargaService.Deletar(estacaoRecarga);

            _IestacaoRecarga.Verify(tr => tr.Deletar(It.IsAny<EstacaoRecarga>()), Times.Once);

            actual.Should().BeNull();
        }

        [Fact]
        public void Deletar_estacao_recarga_deve_funcionar()
        {
            var estacaoRecarga = EstacaoRecargaHelper.MonteEstacaoRecarga();

            var actual = _estacaoRecargaService.Deletar(estacaoRecarga);

            _IestacaoRecarga.Verify(tr => tr.Deletar(It.IsAny<EstacaoRecarga>()), Times.Once);

            actual.Should().BeNull();
        }               

    }
}
