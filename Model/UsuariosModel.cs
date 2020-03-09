using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tamaleria_Miguelena.Model
{
    [Table("tbl_usuario")]
    public class UsuariosModel
    {
        [PrimaryKey]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int rol { get; set; }

    }
}
