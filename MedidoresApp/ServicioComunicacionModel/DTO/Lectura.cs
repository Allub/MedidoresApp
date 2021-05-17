using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{

    public class Lectura 
    {
        private String fecha;
        private string valor;
        private int tipo;
        private string unidadMedida;
        private string estado;
        private string nroSerie;

        
        
      


        public string Valor { get => valor; set => valor = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
        public string Estado { get => estado; set => estado = value; }
        public string NroSerie { get => nroSerie; set => nroSerie = value; }
        public string Fecha { get => fecha; set => fecha = value; }

        public override string ToString()
        {
            return nroSerie + "|" + Fecha + "|" + Tipo + "|" + Valor + "|" + Estado +"|"+ "UPDATE";
        }
    }
}
