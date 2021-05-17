using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class Medidor
    {
        protected int id;
        protected DateTime dechaInstalacion;

        protected int Id { get => id; set => id = value; }

        public Medidor(int id)
        {
            this.Id = id;
        }


    }


}
