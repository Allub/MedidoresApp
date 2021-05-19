using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacion
{
    public partial class Program
    {

        static IMedidorTraficoDAL dalMt = MedidorTraficoFactory.CreateDal();
        static IMedidorConsumoDAL dalMc = MedidorConsumoFactory.CreateDal();
        static ILecturaDAL dalLe = LecturaFactory.CreateDal();


        /// <summary>
        /// se parsea a DateTime
        /// se obtiene diferencia de minutos con TimeSpan
        /// </summary>
        /// <param name="fecha">obtenida por el cliente en String</param>
        /// <returns>si es menor a 30 minutos, retorna true,
        /// si es mayor retorna false</returns>
        static Boolean validadorFecha(string fecha)
        {
            DateTime fechaFormat = DateTime.Parse(fecha);
            TimeSpan diferencia = fechaFormat - DateTime.Now;
            double diferenciaMinutos = diferencia.TotalMinutes;
            double diferenciaMinutosPos = Math.Abs(diferenciaMinutos);
            if (diferenciaMinutosPos > 30)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
            

        static Boolean validarNroMTrafico(string nro_medidor)
        {
            int nro_medidorInt = int.Parse(nro_medidor);
            MedidorTrafico me = dalMt.ObtenerMedidores().Find(m => m.Nro_medidor == nro_medidorInt);
            if (me != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Boolean validarNroMConsumo(string nro_medidor)
        {
            int nro_medidorInt = int.Parse(nro_medidor);
            MedidorConsumo me = dalMc.ObtenerMedidores().Find(m => m.Nro_medidor == nro_medidorInt);
            if (me != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




    }
}
