using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMedidor
{
    public partial class Program
    {
        /// <summary>
        /// se obtiene en string lo que ingresa el usuario
        /// si no se puede hacer el parce, aux pasa a -1 y se repite el ciclo
        /// sino si aux es diferente a 1 y 2, aux pasa a -1 y se repite el ciclo
        /// </summary>
        /// <returns>aux correcto ingresado por usuario</returns>
        public static int GetTipo()
        {
            int aux = -1;
            do
            {
                Console.WriteLine("Ingrese tipo de medidor");
                Console.WriteLine("1. Trafico");
                Console.WriteLine("2. Consumo");
                if (!int.TryParse(Console.ReadLine().Trim(), out aux))
                {
                    Console.WriteLine("El tipo debe ser numerico");
                    aux = -1;
                }
                else if (aux != 1 && aux != 2)
                {
                    Console.WriteLine("El tipo deber ser 1 o 2");
                    aux = -1;
                }

            } while (aux != 1 && aux != 2);
            return aux;
        }
        /// <summary>
        /// se obtiene string ingresado por el usuario
        /// se convierte en un array de string quitando el simbolo -
        /// se hace trycatch, en caso de no poder dar un string a la variables de la fecha
        /// cae en excepcion y fecha pasa a null
        /// caso contrario se guarda el arreglo en formato fecha requerido
        /// ademas se repite el ciclo si fecha es null o elementos del arreglo supera la cantidad
        /// de caracteres requerida
        /// </summary>
        /// <returns>fecha en formato requerido</returns>
        public static string GetFecha()
        {
            string fecha;
            string años = "";
            string meses = "";
            string dias = "";
            string horas = "";
            string minutos = "";
            string segundos = "";
            do
            {
                Console.WriteLine("Ingrese Fecha Actual del servidor en formato yyyy-MM-dd-HH-mm-ss");
                string fechaFormat = Console.ReadLine().Trim();
                string[] formatos = fechaFormat.Split('-');
                try
                {
                    años = formatos[0];
                    meses = formatos[1];
                    dias = formatos[2];
                    horas = formatos[3];
                    minutos = formatos[4];
                    segundos = formatos[5];
                    fecha = años + "-" + meses + "-" + dias + " " + horas + ":" + minutos + ":" + segundos;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ingrese fecha en formato requerido");
                    fecha = null;
                }
              
            } while (fecha == null || años.Length != 4 || meses.Length != 2 || dias.Length != 2 
            || horas.Length != 2 || minutos.Length != 2 || segundos.Length != 2);
            return fecha;
        }

        /// <summary>
        /// se obtiene en string lo que ingresa el usuario
        /// si no se puede hacer el parce, aux pasa a -1 y se repite el ciclo
        /// sino si aux es menor a 0 o mayor a 9, aux pasa a -1 y se repite el ciclo
        /// </summary>
        /// <returns>aux coorecto ingresador por usuario</returns>
        public static int GetNroMedidor()
        {
            int aux = -1;
            string auxStr = "" + aux;
            do
            {
                Console.WriteLine("Ingrese numero de Medidor");
                if (!int.TryParse(Console.ReadLine().Trim(), out aux))
                {
                    Console.WriteLine("El numero del medidor debe ser numerico");
                    aux = -1;
                }
                else if (aux < 0 || aux > 9)
                {
                    Console.WriteLine("El numero del medidor es de largo 1");
                    aux = -1;
                }
            } while (aux == -1);
            return aux;       
        }
    }
}
