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
            //se obtiene el puerto desde App.Config
            int puerto = Int32.Parse(ConfigurationManager.AppSettings["puerto"]);
            //se escribe en la consola que esta iniciando en ese puerto
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);
            //se crea un nuevo Serversocket pasando el parametro del puerto
            ServerSocket servidor = new ServerSocket(puerto);
            //creacion de nuevo hilo
            Thread t = new Thread(new ThreadStart(servidor.Iniciar));
            //para que funcione en segundo plano
            t.IsBackground = true;
            //se inicia el hilo
            t.Start();
            {
                //mientras espera el cliente
                while (true)
                {
                    //cuando obtenga un cliente
                    if (servidor.ObtenerCliente())
                    {
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
                            //parsea el tipo
                            int tipoInt = int.Parse(tipo);
                            //si tipo es 1, trafico
                            if (tipoInt == 1)
                            {
                                //si fecha es menor a 30 min y nro medidor de trafico se encuentra
                                if (validadorFecha(fecha) && validarNroMTrafico(nro_medidor))
                                {
                                    //servidor manda mensaje donde va la fecha actual + |WAIT
                                    servidor.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "|WAIT");
                                    //servidor recibe segundo mensaje
                                    string actualizacion = servidor.Leer();
                                    Console.WriteLine(actualizacion);
                                    //Split al segundo mensaje
                                    string[] formatActua = actualizacion.Split('|');
                                    string nro_Serie, fechaL,tipoL, valor, estado;
                                    nro_Serie = formatActua[0];
                                    fechaL = formatActua[1];
                                    DateTime fechatra = Convert.ToDateTime(fechaL);
                                    tipoL = formatActua[2];
                                    valor = formatActua[3];
                                    estado = formatActua[4];
                                    //se valida si el nro de serie de trafico se encuentra
                                    if (ValidarNroSerieTrafico(nro_Serie))
                                    {
                                        //se crea nuevo objeto lectura
                                        //con los atributos recibidos en el mensaje
                                        Lectura l = new Lectura()
                                        {
                                            NroSerie = nro_Serie,
                                            Fecha = fechatra,
                                            Tipo = tipoInt,
                                            Valor =  valor+" vehículos",
                                            Estado = EstadoToString(estado)

                                        };
                                        //se guardan en archivo de texto
                                        lock (dalLe)
                                        {
                                            dalLe.RegistrarLectura(l, "traficos.txt");
                                        }
                                        //servidor envia mensaje ""
                                        //eso demuestra que esta correcto(esto lo valida el cliente)
                                        servidor.Escribir("");                           
                                    }
                                    else
                                    {
                                        //en caso de que exista error en el mensaje de actualizacion
                                        //que no se encuentra el nro de serie del medidor
                                        //se envia mensaje al cliente con fecha actual, nro de serie y mensaje ERROR
                                        servidor.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "|" + nro_Serie + "|ERROR");       
                                    }
                                }
                                else
                                {
                                    //si la fecha recibida en el primer mensaje
                                    //es false
                                    //servidor manda mensaje con el error(cliente muestra este mensaje)
                                    if (validadorFecha(fecha) == false)
                                    {
                                        servidor.Escribir("Error en la fecha");
                                    }
                                    //sino si el nro de medidor no se encuentra
                                    //es false
                                    //servidor manda mensaje con el error(Cliente muestra este error)
                                    else if (validarNroMConsumo(nro_medidor) == false)
                                    {
                                        servidor.Escribir("No se encuentra el medidor");
                                    }
                                }
                            }
                            //si el tipo es 2, consumo
                            else if (tipoInt == 2)
                            {
                                if (validadorFecha(fecha) && validarNroMConsumo(nro_medidor))
                                {
                                    //servidor manda mensaje donde va la fecha actual + |WAIT
                                    servidor.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "|WAIT");
                                    //servidor recibe segundo mensaje
                                    string actualizacion = servidor.Leer();
                                    Console.WriteLine(actualizacion);
                                    //Split al segundo mensaje
                                    string[] formatActua = actualizacion.Split('|');
                                    string nro_Serie, fechaL, tipoL, valor, estado;
                                    nro_Serie = formatActua[0];
                                    fechaL = formatActua[1];
                                    DateTime fechaCon = Convert.ToDateTime(fechaL);
                                    tipoL = formatActua[2];
                                    valor = formatActua[3];
                                    estado = formatActua[4];
                                    //se valida si el nro de serie de consumo se encuentra
                                    if (ValidarNroSerieConsumo(nro_Serie))
                                    {
                                        //se crea nuevo objeto lectura
                                        //con los atributos recibidos en el mensaje
                                        Lectura l = new Lectura()
                                        {
                                            NroSerie = nro_Serie,
                                            Fecha = fechaCon,
                                            Tipo = tipoInt,
                                            Valor = valor + " Kwh consumidos",
                                            Estado = EstadoToString(estado)

                                        };
                                        //se guardan en archivo de texto
                                        lock (dalLe)
                                        {
                                            dalLe.RegistrarLectura(l, "consumos.txt");
                                        }
                                        //servidor envia mensaje ""
                                        //eso demuestra que esta correcto(esto lo valida el cliente)
                                        servidor.Escribir("");
                                    }
                                    else
                                    {
                                        //en caso de que exista error en el mensaje de actualizacion
                                        //que no se encuentra el nro de serie del medidor
                                        //se envia mensaje al cliente con fecha actual, nro de serie y mensaje ERROR
                                        servidor.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "|" + nro_Serie + "|ERROR");
                                    }

                                }
                                else
                                {
                                    //si la fecha recibida en el primer mensaje
                                    //es false
                                    //servidor manda mensaje con el error(cliente muestra este mensaje)
                                    if (validadorFecha(fecha) == false  )
                                    {
                                        servidor.Escribir("Error en la fecha");
                                    }
                                    //sino si el nro de medidor no se encuentra
                                    //es false
                                    //servidor manda mensaje con el error(Cliente muestra este error)
                                    else if (validarNroMConsumo(nro_medidor) == false)
                                    {
                                        servidor.Escribir("No se encuentra el medidor");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

