using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Tamaleria_Miguelena.Helpers;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;
using Xamarin.Forms;

namespace Tamaleria_Miguelena.ViewModel
{
    class Agregar_Producto_Detalle: PropertyChange
    {
        readonly Repository_Api bdapi;
        readonly Get_ID get;


        private ObservableCollection<Pedido_Detalle_Model> _pedido;
        public ObservableCollection<Pedido_Detalle_Model> Pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Inventario_Model> _inventario;
        public ObservableCollection<Inventario_Model> inventario
        {
            get { return _inventario; }
            set { _inventario = value; OnPropertyChanged(); }
        }

        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }


        private int _cantidad;
        public int cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; OnPropertyChanged(); }
        }
        public Command add_product_list { get; set; }
        public Command Guardar_producto { get; set; }

        
        public Inventario_Model ItemSelected { get; set; }


        public Agregar_Producto_Detalle()
        {
            bdapi = new Repository_Api();
            Pedido = new ObservableCollection<Pedido_Detalle_Model>();
            add_product_list = new Command(add_to_list);
            Guardar_producto = new Command(guardar_orden);
            getInventario();
        }

        public void add_to_list()
        {
            if( ItemSelected != null) { 
            Pedido_Detalle_Model pedido_add = new Pedido_Detalle_Model();
            pedido_add.nombre_producto = ItemSelected.nombre_producto;
            pedido_add.precio = ItemSelected.precio;
            pedido_add.inventario = ItemSelected.id_inventario;
            pedido_add.cantidad = cantidad;
            pedido_add.pedido_orden = id;
            pedido_add.correlativo = "000";
            Pedido.Add(pedido_add);
            }
            else {
                App.Current.MainPage.DisplayAlert("Advertencia", "Selecione Un Producto", "Ok");
            }

        }
        
        public async void guardar_orden()
        {
            if(Pedido != null) { 
            Pedido_Detalle_Model pedido_guardar = new Pedido_Detalle_Model();
                double total = 0;
                double efectivo = 0;

            foreach (Pedido_Detalle_Model orden in Pedido)
            {

                pedido_guardar.nombre_producto = orden.nombre_producto;
                pedido_guardar.inventario = orden.inventario;
                pedido_guardar.precio = orden.precio;             
                pedido_guardar.pedido_orden = orden.pedido_orden;
                pedido_guardar.cantidad = orden.cantidad;
                total = pedido_guardar.cantidad * pedido_guardar.precio;
                efectivo = efectivo + total;     
                pedido_guardar.correlativo = orden.correlativo;
                await bdapi.PedidoOrdenGuardar(pedido_guardar);

            }

                PedidoModel update = new PedidoModel();
                update.efectivo_total = efectivo;
                update.id_pedido_orden = id;
                await bdapi.Actualizar_efectivo_pedido(update);

            }
            else
            {
              await App.Current.MainPage.DisplayAlert("Advertencia", "Debe Agregar Producto", "OK");
            }


        }
        
        public async void getInventario()
        {
            var datos = await bdapi.getInventario();
            inventario = new ObservableCollection<Inventario_Model>(datos);
        }


    }
}
