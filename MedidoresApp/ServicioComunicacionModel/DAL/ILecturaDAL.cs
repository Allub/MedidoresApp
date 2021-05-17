using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public interface ILecturaDAL
    {
        void RegistrarLectura(Lectura l, string archivo);
        List<Lectura> ObtenerLecturasTrafico();
        List<Lectura> ObtenerLecturasConsumo();

    }
}
