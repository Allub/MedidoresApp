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
            //se obtienen ip y puerto desde App.Config
            String ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            //se le pasa como parametro a un nuevo clienteSocket
            ClienteSocket clienteSocket = new ClienteSocket(ip, puerto);
            //Se escribe en consola que esta conectando a ese puerto
            Console.WriteLine("Conectandose al servidor {0} en el puerto {1}", ip, puerto);
            //si se logra comunicar:
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Cliente Conectado");   
                //se pide ingresar tipo, fecha y nroMedidor al cliente
                //se hacen las validaciones
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
                //si el mensaje recibido es un error
                //la consola del cliente se cierra luego de presionar Enter
                if (mensajeRecibido.Equals("Error en la fecha")  || mensajeRecibido.Equals("No se encuentra el medidor"))
                {
                    Console.WriteLine("Presione Enter para salir . . .");
                    Console.ReadLine();
                }
                //si es correcto
                else
                {
                    //split al mensaje buscando WAIT
                    string[] formatos2 = mensajeRecibido.Split('|');
                    string confPeticion;
                    confPeticion = formatos2[1];
                    if (confPeticion.Equals("WAIT"))
                    {
                        //si se encuentra WAIT en el mensaje recibido:
                        Console.WriteLine("Actualizacion de estado");
                        //se piden los nuevos ingresos por parte del cliente
                        int nro_serie = GetNroSerie();
                        string fechaAct = GetFecha();
                        int valor = GetValor();
                        int estado = GetEstado();
                        //seleccion parte como 3, ya que cambiará el valor mas adelante
                        int seleccion = 3;
                        //si estado es 1(que si tiene estado)
                        if (estado == 1)
                        {
                            seleccion = GetSeleccion();
                            if (seleccion == -1)
                            {
                                //si seleccion por el cliente es -1, seleccion pasa a ser -1
                                seleccion = -1;
                            }
                            else if (seleccion == 0)
                            {
                                //si seleccion por el cliente es 0, seleccion pasa a ser 0
                                seleccion = 0;
                            }
                            else if (seleccion == 1)
                            {
                                //si seleccion por el cliente es 1, seleccion pasa a ser 1
                                seleccion = 1;
                            }
                            else if (seleccion == 2)
                            {
                                //si seleccion por el cliente es 2, seleccion pasa a ser 2
                                seleccion = 2;
                            }
                        }
                        //si es 2(no tiene estado)
                        //estado pasa a ser 11
                        else if (estado == 2)
                        {
                            estado = 11;
                        }
                        //Cliente manda mensaje con las variables concatenadas mas el simbolo | y UPDATE
                        clienteSocket.Escribir(nro_serie + "|" + fecha + "|" + tipo + "|" + valor + "|" + seleccion + "|UPDATE");
                        //Se recibe ultimo mensaje
                        string confirmacion = clienteSocket.Leer();
                        //si el mensaje es distinto a "", es que hay un error en el ingreso
                        //se muestra el mensaje recibido con el error
                        //y se le pide al cliente presionar enter para cerrar la consola
                        if (confirmacion != "")
                        {                           
                            Console.WriteLine(confirmacion);
                            Console.WriteLine("Presione Enter para salir. . .");
                            Console.ReadLine();
                        }
                        //si es correcto
                        //cliente se desconecta
                        else
                        {
                            clienteSocket.Desconectar();
                        }                        
                    }                  
                }
            }
            //si cliente no se puede conectar:
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error de conexion");
            }
        }
    }
}
