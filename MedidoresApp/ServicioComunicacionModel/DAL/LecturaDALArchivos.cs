using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioComunicacionModel.DTO;

namespace ServicioComunicacionModel.DAL
{
    public class LecturaDALArchivos : ILecturaDAL
    {
        private LecturaDALArchivos()
        {

        }

        private static ILecturaDAL instancia;

        public static ILecturaDAL GetInstancia()
        {
            if (instancia == null)
                instancia = new LecturaDALArchivos();
            return instancia;
        }

        List<Lectura> lecturas = new List<Lectura>();
       

        public List<Lectura> ObtenerLecturasConsumo()
        {
            
            try
            {
                using (StreamReader reader = new StreamReader(archivoConsumo))
                {
                    String texto = null;

                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            String[] textoArray = texto.Split('|');
                            Lectura l = new Lectura()
                            {
                                NroSerie = textoArray[0],
                                Fecha = textoArray[1],
                                Tipo = int.Parse(textoArray[2]),
                                Valor = textoArray[3],
                                Estado = textoArray[4]


                            };
                            lecturas.Add(l);
                            
                        }


                    } while (texto != null);
                }


            }
            catch (Exception)
            {

                lecturas = null;
            }
            return lecturas;
        }

        public List<Lectura> ObtenerLecturasTrafico()
        {

            try
            {
                using (StreamReader reader = new StreamReader(archivoConsumo))
                {
                    String texto = null;

                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            String[] textoArray = texto.Split('|');
                            Lectura l = new Lectura()
                            {
                                NroSerie = textoArray[0],
                                Fecha = textoArray[1],
                                Tipo = int.Parse(textoArray[2]),
                                Valor = textoArray[3],
                                Estado = textoArray[4]


                            };
                            lecturas.Add(l);

                        }


                    } while (texto != null);
                }


            }
            catch (Exception)
            {

                lecturas = null;
            }
            return lecturas;
        }

        public void RegistrarLectura(Lectura l, string archivo)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(l);
                    writer.Flush();
                }

            }
            catch (IOException ex)
            {

                
            }
        }

      
            
            private string archivoTrafico = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "traficos.txt";
            private string archivoConsumo = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "consumos.txt";
    
        
    }
}
