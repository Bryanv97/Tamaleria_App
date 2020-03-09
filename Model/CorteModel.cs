using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tamaleria_Miguelena.Model
{
    [Table("tbl_corte")]

    public class CorteModel
    {
        [PrimaryKey]
        public int id_corte { get; set; }
        public int turno { get; set; }
        public DateTime fecha_hora { get; set; }
        public int usuario { get; set; }
        public int sucursal { get; set; }
        public double efectivo_inicial { get; set; }
        public double efectivo_final { get; set; }
        public int estado { get; set; }
    }
}
