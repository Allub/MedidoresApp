using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioComunicacionModel.DTO;

namespace ServicioComunicacionModel.DAL
{
    public class MedidorConsumoDALArchivos : IMedidorConsumoDAL
    {
        private MedidorConsumoDALArchivos()
        {

        }
        private static IMedidorConsumoDAL instancia;

        public static IMedidorConsumoDAL GetInstancia()
        {
            if (instancia == null)
                instancia = new MedidorConsumoDALArchivos();
            return instancia;         
        }





        public List<MedidorConsumo> ObtenerMedidores()
        {
            List<MedidorConsumo> medidoresConsumo = new List<MedidorConsumo>();
            MedidorConsumo m1 = new MedidorConsumo(5, 5555);
            MedidorConsumo m2 = new MedidorConsumo(6, 6666);
            MedidorConsumo m3 = new MedidorConsumo(7, 7777);
            MedidorConsumo m4 = new MedidorConsumo(8, 8888);
            medidoresConsumo.Add(m1);
            medidoresConsumo.Add(m2);
            medidoresConsumo.Add(m3);
            medidoresConsumo.Add(m4);
            return medidoresConsumo;
        }
    }
}
