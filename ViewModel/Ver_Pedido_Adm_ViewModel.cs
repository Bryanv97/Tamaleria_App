using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Tamaleria_Miguelena.Helpers;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;
using Tamaleria_Miguelena.View;
using Xamarin.Forms;

namespace Tamaleria_Miguelena.ViewModel
{
    class Ver_Pedido_Adm_ViewModel : PropertyChange
    {
        readonly Repository_Api Db_api;
        //readonly Repository_Local Db_local;
        readonly Get_ID get_ID;
        public Command add_pedido { get; set; }
        public Command pull { get; set; }

        public PedidoModel orden_selecionada { get; set; }

        private ObservableCollection<PedidoModel> _pedido;
        public ObservableCollection<PedidoModel> pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(); }
        }

        private bool _estado;
        public bool estado
        {
            get { return _estado; }
            set { _estado = value; OnPropertyChanged(); }
        }

        public Ver_Pedido_Adm_ViewModel()
        {
            Db_api = new Repository_Api();
            get_ID = new Get_ID();
            get_ordenes();
            add_pedido = new Command(go_order);
            pull = new Command(pull_get_ordenes);
        }

        public async void get_ordenes()
        {
            var ordenes = await Db_api.getPedido_Adm();
            pedido = new ObservableCollection<PedidoModel>(ordenes);
        }

        public async void pull_get_ordenes()
        {
            estado = true;
            var ordenes = await Db_api.getPedido_Adm();
            pedido = new ObservableCollection<PedidoModel>(ordenes);
            estado = false;
        }

        public void go_order()
        {
            App.Current.MainPage.Navigation.PushAsync(new Pedido_Page());
        }
    }
}
