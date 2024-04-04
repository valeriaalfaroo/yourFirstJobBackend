using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.Entidades.entities;
using yourFirstJobBackend.Entidades.Request;

namespace yourFirstJobBackend.Entidades.Response
{
    public class ResUpdateUsuario :ResBase
    {
        public Usuario usuario { get; set; }
        public List<Idiomas> idiomas { get; set; }
        public List<Habilidades> habilidades { get; set; }
        public List<Estudios> estudios { get; set; }
        public List<ArchivosUsuario> archivosUsuarios { get; set; }
        public List<ExperienciaLaboral> experienciaLaboral { get; set; }

      


    }
}
