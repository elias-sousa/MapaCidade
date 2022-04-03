using MapaCidade.Dominio.Core.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapaCidade.Apresentacao.MVC.Models
{
    public class EstacaoRecargaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public TipoRecarga Tipo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<SelectListItem> Tipos { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = TipoRecarga.Movel.ToString() },
            new SelectListItem { Value = "0", Text = TipoRecarga.Veicular.ToString() },
        };


    }
}
