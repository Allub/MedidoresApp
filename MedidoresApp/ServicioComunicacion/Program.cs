using ServicioComunicacion.Comunicacion;
using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicacion
{
    public partial class Program
    {

        

        static void Main(string[] args)
        {
            
            

            int puerto = Int32.Parse(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Iniciando Servidor en puerto {0}",puerto);


            ServerSocket servidor = new ServerSocket(puerto);
            Thread t = new Thread(new ThreadStart(servidor.Iniciar));
            t.IsBackground = true;
            t.Start();
            {
                //mientras espera el cliente
                while (true)
                {
       
                    //cuando obtenga un cliente
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Solicitud recibida!");
                        //Protocolo de comunicacion
                        string mensaje = "";
                        {
                            //se lee el primer mensaje del cliente
                            mensaje = servidor.Leer();

                            //split del primer mensaje
                            string[] formatMensaje = mensaje.Split('|');
                            string fecha, nro_medidor, tipo;
                            fecha = formatMensaje[0];
                            nro_medidor = formatMensaje[1];
                            tipo = formatMensaje[2];
                            Console.WriteLine(fecha + "|" + nro_medidor + "|" + tipo);
                            //parsea el tipo
                            int tipoInt = int.Parse(tipo);
                            //si tipo es 1
                            if (tipoInt == 1)
                            {
                                if (validadorFecha(fecha) && validarNroMTrafico(nro_medidor))
                                {
                                    servidor.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+"|WAIT");
                                    string actualizacion = servidor.Leer();
                                    Console.WriteLine(actualizacion);
                                    string[] formatActua = actualizacion.Split('|');
                                    string nro_Serie, fechaL, tipoL, valor, estado;
                                    nro_Serie = formatActua[0];
                                    fechaL = formatActua[1];
                                    tipoL = formatActua[2];
                                    valor = formatActua[3];
                                    estado = formatActua[4];
                                    Lectura l = new Lectura()
                                    {
                                        NroSerie = nro_Serie,
                                        Fecha = fechaL,
                                        Tipo = int.Parse(tipoL),
                                        Valor = valor,
                                        Estado = estado

                                    };
                                    lock (dalLe)
                                    {
                                        dalLe.RegistrarLectura(l,"traficos.txt");
                                    }
                                    servidor.CerrarConexion();


                                }
                                else 
                                {
                                    Console.WriteLine("Medidor no encontrado");
                                }
                               
                                
                            }
                            else if (tipoInt == 2)
                            {
                                if (validadorFecha(fecha) && validarNroMConsumo(nro_medidor))
                                {
                                    servidor.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "|WAIT");
                                    string actualizacion = servidor.Leer();
                                    Console.WriteLine(actualizacion);
                                    string[] formatActua = actualizacion.Split('|');
                                    string nro_Serie, fechaL, tipoL, valor, estado;
                                    nro_Serie = formatActua[0];
                                    fechaL = formatActua[1];
                                    tipoL = formatActua[2];
                                    valor = formatActua[3];
                                    estado = formatActua[4];
                                    Lectura l = new Lectura()
                                    {
                                        NroSerie = nro_Serie,
                                        Fecha = fechaL,
                                        Tipo = int.Parse(tipoL),
                                        Valor = valor,
                                        Estado = estado

                                    };
                                    lock (dalLe)
                                    {
                                        dalLe.RegistrarLectura(l,"consumos.txt");
                                    }
                                    servidor.CerrarConexion();
                                }
                                else
                                {
                                    Console.WriteLine("Medidor no encontrado");
                                }
                            }
                            else
                            {
                                Console.WriteLine("error en el tipo");
                            }
                            


                       



                            
                        }
                        
                    }
                }
                
            }
            /*si no es capaz de tomar el control del puerto*/
           
        }
        }
    }

