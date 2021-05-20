using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class MedidorConsumo
    {
        private int nro_medidor;
        private int id;

        public MedidorConsumo(int nro_medidor, int id)
        {
            this.nro_medidor = nro_medidor;
            this.id = id;
        }

        public int Nro_medidor { get => nro_medidor; set => nro_medidor = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return id + ";" + Nro_medidor;
        }

    }
}
