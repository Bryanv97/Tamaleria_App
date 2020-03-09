using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tamaleria_Miguelena.Model
{
    [Table ("tbl_sucursal")]
    public class SucursalModel
    {
        [PrimaryKey]
        public int id_sucursal { get; set; }
        public string nombre_sucursal { get; set; }
        public string direccion { get; set; }
        public int asignado { get; set; }
    }
}
