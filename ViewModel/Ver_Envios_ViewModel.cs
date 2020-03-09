using System;
using System.Collections.Generic;
using System.Text;
using Tamaleria_Miguelena.Services;
using Xamarin.Forms;
using Tamaleria_Miguelena.View;
using Tamaleria_Miguelena.Helpers;
using Tamaleria_Miguelena.Model;
using System.Collections.ObjectModel;

namespace Tamaleria_Miguelena.ViewModel
{
    class Ver_Envios_ViewModel : PropertyChange
    {
        readonly Repository_Api Db_api;
        //readonly Repository_Local Db_local;
        readonly Get_ID get_ID;
        public Command add_pedido { get; set; }
        public Command pull { get; set; }

        public EnviosModel orden_selecionada { get; set; }

        private ObservableCollection<EnviosModel> _pedido;
        public ObservableCollection<EnviosModel> pedido
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
        public Command add_envio { get; set; }
        public Ver_Envios_ViewModel()
        {
            add_envio = new Command(go_envio);
            Db_api = new Repository_Api();
            get_ID = new Get_ID();
            get_ordenes();
            add_envio = new Command(go_envio);
            pull = new Command(pull_get_ordenes);
        }

        public async void get_ordenes()
        {

            var ordenes = await Db_api.Get_envio(new EnviosModel { origen = get_ID.get_nombresucursal(), destino = get_ID.get_nombresucursal() });
            pedido = new ObservableCollection<EnviosModel>(ordenes);
        }

        public void go_envio()
        {
            App.Current.MainPage.Navigation.PushAsync(new Envios_Page());
        }
        public async void pull_get_ordenes()
        {
            estado = true;
            var ordenes = await Db_api.Get_envio(new EnviosModel { origen = get_ID.get_nombresucursal(), destino = get_ID.get_nombresucursal() });
            pedido = new ObservableCollection<EnviosModel>(ordenes);
            estado = false;
        }

    }
}
