using System;
using System.Collections.Generic;
using System.Text;

namespace Tamaleria_Miguelena.Model
{
    public class Pedido_Detalle_Model
    {
        public int id_pedido_orden_detalle { get; set; }
        public int pedido_orden { get; set; }
        public string nombre_producto { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string correlativo { get; set; }
        public int inventario { get; set; }
    }
}
