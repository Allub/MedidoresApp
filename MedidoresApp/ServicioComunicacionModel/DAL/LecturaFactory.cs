using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public class LecturaFactory
    {
        public static ILecturaDAL CreateDal()
        {
            return LecturaDALArchivos.GetInstancia();
        }
    }
}
