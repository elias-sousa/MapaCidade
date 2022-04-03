using MapaCidade.Dominio.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Dominio.Core.Entities
{
    public class EstacaoRecarga : EntityBase
    {
        public string Nome { get; set; }
        public TipoRecarga Tipo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool EhValido()
            => string.IsNullOrWhiteSpace(Nome) == false;
    }
}
