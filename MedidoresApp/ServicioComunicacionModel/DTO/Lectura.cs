using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{

    public class Lectura 
    {
        private DateTime fecha;
        private string valor;
        private int  tipo;
        
        private string estado;
        private string nroSerie;
        private string unidadMedida;


        public string NroSerie { get => nroSerie; set => nroSerie = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Estado { get => estado; set => estado = value; }
        
       

    }
}
