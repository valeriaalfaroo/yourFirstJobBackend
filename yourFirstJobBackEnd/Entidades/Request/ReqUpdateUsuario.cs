using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBackend.Entidades.entities;

namespace yourFirstJobBackend.Entidades.Request
{
    public class ReqUpdateUsuario : Usuario
    {
        public Usuario usuario { get; set; }
        public List<Idiomas> idiomas { get; set; }
        public List<Habilidades> habilidades { get; set; }
        public List<Estudios> estudios { get; set; }
        public List<ArchivosUsuario> archivosUsuarios { get; set; }
        public List<ExperienciaLaboral> experienciaLaboral { get; set; }

        public ReqUpdateUsuario(Usuario usuario, List<Idiomas> idiomas, List<Habilidades> habilidades, List<Estudios> estudios, List<ArchivosUsuario> archivosUsuarios, List<ExperienciaLaboral> experienciaLaboral)
        {
            this.usuario = usuario;
            this.idiomas = idiomas;
            this.habilidades = habilidades;
            this.estudios = estudios;
            this.archivosUsuarios = archivosUsuarios;
            this.experienciaLaboral = experienciaLaboral;

            AsignarListasAUsuario();
        }

        private void AsignarListasAUsuario()
        {
            usuario.listaIdiomas = idiomas;
            usuario.listaHabilidades = habilidades;
            usuario.listaEstudios = estudios;
            usuario.listaArchivosUsuarios = archivosUsuarios;
            usuario.listaExperienciaLaboral = experienciaLaboral;
        }

    }

}
