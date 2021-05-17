using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class MedidorTrafico : Medidor
    {
        private int nro_medidor;

        public MedidorTrafico(int nro_medidor, int pId)
            : base(pId)
        {
            this.id = pId;
            this.Nro_medidor = nro_medidor;
        }

        public int Nro_medidor { get => nro_medidor; set => nro_medidor = value; }

        public override string ToString()
        {
            return id + ";" + Nro_medidor;
        }



    }
}
