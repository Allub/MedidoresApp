using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioComunicacionModel.DTO;

namespace ServicioComunicacionModel.DAL
{
    public class MedidorTraficoDALArchivos : IMedidorTraficoDAL
    {
        private MedidorTraficoDALArchivos()
        {

        }
        private static IMedidorTraficoDAL instancia;

        public static IMedidorTraficoDAL GetInstancia()
        {
            if (instancia == null)
                instancia = new MedidorTraficoDALArchivos();
            return instancia;         
        }
      

       

        public List<MedidorTrafico> ObtenerMedidores()
        {
            List<MedidorTrafico> medidores = new List<MedidorTrafico>();
            MedidorTrafico m1 = new MedidorTrafico(1, 1111);
            MedidorTrafico m2 = new MedidorTrafico(2, 2222);
            MedidorTrafico m3 = new MedidorTrafico(3, 3333);
            MedidorTrafico m4 = new MedidorTrafico(4, 4444);
            medidores.Add(m1);
            medidores.Add(m2);
            medidores.Add(m3);
            medidores.Add(m4);
            return medidores;
        }

    }
}
