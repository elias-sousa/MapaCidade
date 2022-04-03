using MapaCidade.Aplicacao.Interface;
using MapaCidade.Dominio.Core.Entities;
using MapaCidade.Dominio.Core.Enum;
using MapaCidade.Dominio.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Aplicacao.Service
{
    public class EstacaoRecargaService : IEstacaoRecargaService
    {
        private IEstacaoRecarga _repository;
      
        public EstacaoRecargaService(IEstacaoRecarga repository)
        {
            _repository = repository;
        }

        public EstacaoRecarga Adicionar(EstacaoRecarga obj)
        {
           return _repository.Adicionar(obj);
        }

        public EstacaoRecarga Atualizar(EstacaoRecarga obj)
        {
            return _repository.Atualizar(obj);
        }

        public EstacaoRecarga Deletar(EstacaoRecarga obj)
        {
            return _repository.Deletar(obj);
        }

        public EstacaoRecarga EncontrarPorId(Guid id)
        {
            return _repository.EncontrarPorId(id);
        }

        public IReadOnlyList<EstacaoRecarga> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        public IReadOnlyList<EstacaoRecarga> ProcurarEstacoesProximas(double latitude, double longitude)
        {          
            var lista = _repository.ProcurarEstacoesProximas(latitude, longitude).
                OrderBy(a => a.Distancia);

            List<EstacaoRecarga> listaEstacaoRecarga = new List<EstacaoRecarga>();

            EstacaoRecarga estacaoRecarga;

            foreach (var item in lista)
            {
                estacaoRecarga = new EstacaoRecarga();
                estacaoRecarga.Id = item.Id;
                estacaoRecarga.Nome = item.Nome;
                estacaoRecarga.Latitude = item.Latitude;
                estacaoRecarga.Longitude = item.Longitude;
                estacaoRecarga.Tipo = item.Tipo;

                listaEstacaoRecarga.Add(estacaoRecarga);
            }
            return listaEstacaoRecarga;

        }

        /// <summary>
        /// Este metodo gera um arquivo a partir da chave primaria de uma estacao de recarga
        /// </summary>
        /// <param name="id">Id da estação a ser baixado</param>
        /// <param name="directory">Diretório onde o arquivo gerado deve ser salvo</param>
        /// <returns>Retorna o caminho onde o arquivo foi gerado</returns>
        public string GerarArquivo(Guid id, string directory)
        {
            var estacaoRecarga = _repository.EncontrarPorId(id);

            if (estacaoRecarga == null) return "Esta Estação de Recarga não existe";

            string texto = $@"

                     Estação Recarga: {estacaoRecarga.Nome}
                     Latitude: {estacaoRecarga.Latitude}
                     Longitude: {estacaoRecarga.Longitude}

            ";
            string path = directory + estacaoRecarga.Id.ToString() + ".txt";
            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(texto);
            sw.Close();
            return path;
        }
    }
}
