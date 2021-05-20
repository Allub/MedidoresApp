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
        /// se obtiene fecha en formato string
        /// se parsea fecha a formato Datetime
        /// en una variable TomeSpan se guarda la diferencia entre la fecha parseada y la fecha actual
        /// en una variable double, se guarda la diferencia de minutos
        /// la variable double pasa a ser numero positivo
        /// </summary>
        /// <param name="fecha">Valor obtenido de mensaje de cliente</param>
        /// <returns>si es mayor a 30 retorna false
        /// en caso contrario retorna true</returns>
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

        /// <summary>
        /// Se valida si se encuentra numero de medidor en lista de Medidores de trafico
        /// se parsea valor obtenido por el cliente a int
        /// se busca en la lista de medidores de trafico si coincide la variable con una 
        /// ya guardada en la lista
        /// retorna null cuando no se encuentra
        /// retorna el objeto cuando se encuentra
        /// </summary>
        /// <param name="nro_medidor">valor obetindo desde el mensaje del cliente</param>
        /// <returns>si es distinto a null retorna true
        /// en caso contrario retorna falso</returns>
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
        /// <summary>
        /// Se valida si se encuentra numero de medidor en lista de Medidores de consumo
        /// se parsea valor obtenido por el cliente a int
        /// se busca en la lista de medidores de consumo si coincide la variable con una 
        /// ya guardada en la lista
        /// retorna null cuando no se encuentra
        /// retorna el objeto cuando se encuentra
        /// </summary>
        /// <param name="nro_medidor">valor obetindo desde el mensaje del cliente</param>
        /// <returns>si es distinto a null retorna true
        /// en caso contrario retorna falso</returns>
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

        /// <summary>
        /// Se Valida si se encuentra nroSerie en lista de medidores de consumo
        /// se parsea valor obtenido por el cliente a int
        /// se busca en la lista de medidores de consumo si coincide la variable con una 
        /// ya guardada en la lista
        /// retorna null cuando no se encuentra
        /// retorna el objeto cuando se encuentra
        /// </summary>
        /// <param name="nroSerie">valor obetindo desde el mensaje del cliente</param>
        /// <returns>si es distinto a null retorna true
        /// en caso contrario retorna falso</returns>
        static Boolean ValidarNroSerieConsumo(string nroSerie)
        {
            int nroSerieInt = int.Parse(nroSerie);
            MedidorConsumo me = dalMc.ObtenerMedidores().Find(m => m.Id == nroSerieInt);
            if (me != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Se Valida si se encuentra nroSerie en lista de medidores de trafico
        /// se parsea valor obtenido por el cliente a int
        /// se busca en la lista de medidores de consumo si coincide la variable con una 
        /// ya guardada en la lista
        /// retorna null cuando no se encuentra
        /// retorna el objeto cuando se encuentra
        /// </summary>
        /// <param name="nroSerie">valor obetindo desde el mensaje del cliente</param>
        /// <returns>si es distinto a null retorna true
        /// en caso contrario retorna falso</returns>
        static Boolean ValidarNroSerieTrafico(String nroSerie)
        {
            int nroSerieInt = int.Parse(nroSerie);
            MedidorTrafico me = dalMt.ObtenerMedidores().Find(m => m.Id == nroSerieInt);
            if (me != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// retorna en texto lo seleccionado por el cliente
        /// </summary>
        /// <param name="estado">Valor obtenido desde el mensaje del cliente</param>
        /// <returns>dependiendo de lo seleccionado, retorna un string</returns>
        static string EstadoToString(string estado)
        {
            int estadoInt = int.Parse(estado);
            if (estadoInt == -1 )
            {
                return "Error de lectura";
            }
            else if (estadoInt == 0)
            {
                return "OK";
            }
            else if (estadoInt == 1)
            {
                return "Punto de carga lleno";
            }
            else if (estadoInt == 2)
            {
                return "Requiere mantención preventiva";
            }
            else if (estadoInt == 11)
            {
                return "";
            }
            return null;
        }
    }
}
