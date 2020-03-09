using System;
using System.Collections.Generic;
using System.Text;

namespace Tamaleria_Miguelena.Model
{
    public class EnviosModel
    {
        public string origen { get; set; }
        public string destino { get; set; }
        public DateTime fecha_hora { get; set; }
        public int usuario { get; set; }
        public int estado { get; set; }
    }
}
