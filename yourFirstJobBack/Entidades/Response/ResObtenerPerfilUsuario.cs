using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobBack.Entidades.entities;

namespace yourFirstJobBack.Entidades.Request;

public class ResObtenerPerfilUsuario:ResBase
{
    public List<usuario> usuarios {  get; set; }

}