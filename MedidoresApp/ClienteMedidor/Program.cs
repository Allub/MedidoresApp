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

                string mensajeRecibido = clienteSocket.Leer();
                Console.WriteLine(mensajeRecibido);
                string[] formatos2 = mensajeRecibido.Split('|');
                string confPeticion;
                confPeticion = formatos2[1];
                if (confPeticion.Equals("WAIT"))
                {
                    Console.WriteLine("Actualizacion de estado");
                    Console.WriteLine("Ingresar Numero de Serie");
                    string nro_serie = Console.ReadLine().Trim();
                    Console.WriteLine("Ingresar fecha");
                    string fechaAct = Console.ReadLine().Trim();
                    Console.WriteLine("Ingresar tipo");
                    Console.WriteLine("1. Trafico");
                    Console.WriteLine("2. Consumo");
                    string tipoN = Console.ReadLine().Trim();
                    Console.WriteLine("Ingresar valor");
                    string valor = Console.ReadLine().Trim();
                    Console.WriteLine("¿Ingresar Estado?");
                    Console.WriteLine("1. Si");
                    Console.WriteLine("2. No");
                    string estado = Console.ReadLine().Trim();
                    int estadoNum = int.Parse(estado);
                    if (estadoNum == 1)
                    {
                        Console.WriteLine("Seleccione estado");
                        Console.WriteLine("-1. Error de lectura");
                        Console.WriteLine("0. OK");
                        Console.WriteLine("1. Punto de carga lleno");
                        Console.WriteLine("2. Requiere mantencion preventiva");
                        string estadoSelec = Console.ReadLine().Trim();
                        int estadoSelecNum = int.Parse(estadoSelec);
                        if (estadoSelecNum == -1)
                        {
                            estadoSelecNum = -1;
                        }
                        else if (estadoSelecNum == 0)
                        {
                            estadoSelecNum = 0;
                        }
                        else if (estadoSelecNum == 1)
                        {
                            estadoSelecNum = 1;
                        }
                        else if (estadoSelecNum == 2)
                        {
                            estadoSelecNum = 2;
                        }
                    }
                    else if (estadoNum == 2)
                    {
                        estado = "sinEstado";
                    }
                    clienteSocket.Escribir(nro_serie + "|" + fecha + "|" + tipo + "|" + valor + "|" + estado + "|UPDATE");

                }
                else
                {
                    Console.WriteLine("error");
                }
                clienteSocket.Desconectar();



            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error de conexion");
            }
        }
    }
}
