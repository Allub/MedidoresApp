using ClienteMedidor.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMedidor
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            String ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            ClienteSocket clienteSocket = new ClienteSocket(ip, puerto);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectandose al servidor {0} en el puerto {1}", ip, puerto);
            if (clienteSocket.Conectar())
            {
                //comunicarme
                Console.WriteLine("Cliente Conectado");
                
                int tipo = GetTipo();
                string fecha = GetFecha();
                int nro_medidor = GetNroMedidor();
                // se concatena fecha, nro_medidor y tipom agregando simbolo '|'
                string mensaje = fecha + "|" + nro_medidor + "|" + tipo;
                // cliente envia primer mensaje
                clienteSocket.Escribir(mensaje);

                // se recibe el mensaje de confirmacion
                string mensajeRecibido = clienteSocket.Leer();
                Console.WriteLine(mensajeRecibido);
                string[] formatos2 = mensajeRecibido.Split('|');
                string confPeticion;
                confPeticion = formatos2[1];
                if (confPeticion.Equals("WAIT"))
                {
                    Console.WriteLine("Actualizacion de estado");
                    int nro_serie = GetNroSerie();
                    string fechaAct = GetFecha();
                    int TipoN = GetTipo();
                    int valor = GetValor();
                    int estado = GetEstado();
                    if (estado == 1)
                    {
                        int seleccion = GetSeleccion();
                        if (seleccion == -1)
                        {
                            seleccion = -1;
                        }
                        else if (seleccion == 0)
                        {
                            seleccion = 0;
                        }
                        else if (seleccion == 1)
                        {
                            seleccion = 1;
                        }
                        else if (seleccion == 2)
                        {
                            seleccion = 2;
                        }
                    }
                    else if (estado == 2)
                    {
                        estado = 11;
                    }
                    clienteSocket.Escribir(nro_serie + "|" + fecha + "|" + tipo + "|" + valor + "|" + estado + "|UPDATE");

                }
                else
                {
                    Console.WriteLine("error en la actualizacion");
                    clienteSocket.Desconectar();
                }
                



            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error de conexion");
            }
        }
    }
}
