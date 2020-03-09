using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;
using Tamaleria_Miguelena.Helpers;
using Tamaleria_Miguelena.View;
using Tamaleria_Miguelena.ViewModel;
using Xamarin.Forms;
using System.Linq;
//using Android.OS;

namespace Tamaleria_Miguelena.ViewModel
{
    class Pedidos_ViewModel : PropertyChange
    {
        readonly Repository_Api bdapi;
        readonly Get_ID get;


        private ObservableCollection<SucursalModel> _sucursal;
        public ObservableCollection<SucursalModel> Sucursales
        {
            get { return _sucursal; }
            set { _sucursal = value; OnPropertyChanged(); }
        }

        private DateTime _fecha_entrega;
        public DateTime fecha_entrega
        {
            get { return _fecha_entrega; }
            set { _fecha_entrega = value; OnPropertyChanged(); }
        }



        private string _Nombrecliente;
        public string nombrecliente
        {
            get { return _Nombrecliente; }
            set { _Nombrecliente = value; OnPropertyChanged(); }
        }



        private bool _EnvioP;
        public bool EnvioP
        {
            get { return _EnvioP; }
            set { _EnvioP = value; OnPropertyChanged(); }
        }


        private string _telefono;
        public string telefono
        {
            get { return _telefono; }
            set { _telefono = value; OnPropertyChanged(); }
        }

        private string _lentrega;
        public string lentrega
        {
            get { return _lentrega; }
            set { _lentrega = value; OnPropertyChanged(); }
        }

        private string _correlativo;
        public string correlativo
        {
            get { return _correlativo; }
            set { _correlativo = value; OnPropertyChanged(); }
        }


        private ObservableCollection<PedidoModel> _pedido;
        public ObservableCollection<PedidoModel> pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(); }
        }
        public Command add_order { get; set; }
        public Command test { get; set; }

        public SucursalModel ItemSelected { get; set; }



        public Pedidos_ViewModel()
        {
            bdapi = new Repository_Api();
            get = new Get_ID();
            fecha_entrega = DateTime.Now;
            add_order = new Command(guardar_orden);
            test = new Command(test1);
            getSucursal();
        }
        public void test1()
        {
            App.Current.MainPage.DisplayAlert("FYH", fecha_entrega.ToString(), "ok");
        }
        public async void guardar_orden() 
        {
           if(EnvioP == !false) {

                //string date = fecha_entrega.Date.ToString();
              var contenido =  await bdapi.Agregar_pedido(new PedidoModel
                {

                    usuario = get.get_usuario(),
                    nombre_cliente = nombrecliente,
                    correlativo = correlativo,
                    lugar_entrega = lentrega,
                    fecha_hora_entrega = fecha_entrega.Date,
                    telefono = telefono,
                    lugar_pedido = get.get_nombresucursal()

                });
                List<PedidoModel> id_pedido = contenido.ToList();
                int id = 0;
                foreach (PedidoModel info in id_pedido)
                {
                    id = info.id_pedido_orden;
                    //datos.id_corte = info.id_corte;
                }
          
                //await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "ORDEN GENERADA", "OK");
                await App.Current.MainPage.Navigation.PushAsync(
                    new Add_Producto_Pedido(){ 
                        BindingContext = new Agregar_Producto_Detalle()
                        { id = id } 
                    });
            }
            else {
                //string date = fecha_entrega.Date.ToString();
               var contenido = await bdapi.Agregar_pedido(new PedidoModel {
               usuario = get.get_usuario(),
               nombre_cliente = nombrecliente,
               correlativo = correlativo,
               lugar_entrega = ItemSelected.nombre_sucursal,
               fecha_hora_entrega = fecha_entrega.Date,
               telefono = telefono,
               lugar_pedido = get.get_nombresucursal()
               
           });
               // await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "ORDEN GENERADA", "OK");
                List<PedidoModel> id_pedido = contenido.ToList();
                int id = 0;
                foreach (PedidoModel info in id_pedido)
                {
                    id = info.id_pedido_orden;
                    //datos.id_corte = info.id_corte;
                }
                await App.Current.MainPage.Navigation.PushAsync(
                    new Add_Producto_Pedido()
                    {
                        BindingContext = new Agregar_Producto_Detalle()
                        { id = id }
                    });
            }
        }

        public async void getSucursal()
        {
            var datos = await bdapi.getSucursal_pedido();
            Sucursales = new ObservableCollection<SucursalModel>(datos);
        }


    }
}
