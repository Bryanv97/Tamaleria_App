using System;
using System.Collections.Generic;
using System.Text;

namespace Tamaleria_Miguelena.Model
{
    public class PedidoModel
    {
        public int id_pedido_orden { get; set; }
        public int usuario { get; set; }
        public DateTime fecha_hora_entrega { get; set; }
        public string lugar_entrega { get; set; }
        public string lugar_pedido { get; set; }
        public double efectivo_total { get; set; }
        public string telefono { get; set; }
        public string nombre_cliente { get; set; }
        public string correlativo { get; set; }
    }
}
