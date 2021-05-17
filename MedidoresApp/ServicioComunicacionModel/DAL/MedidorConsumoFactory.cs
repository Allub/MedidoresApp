using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public class MedidorConsumoFactory
    {
        public static IMedidorConsumoDAL CreateDal()
        {
            return MedidorConsumoDALArchivos.GetInstancia();
        }
    }
}
