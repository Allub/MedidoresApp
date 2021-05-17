using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public class MedidorTraficoFactory
    {
        public static IMedidorTraficoDAL CreateDal()
        {
            return MedidorTraficoDALArchivos.GetInstancia();
        }
    }
}
