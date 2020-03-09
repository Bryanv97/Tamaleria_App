using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Tamaleria_Miguelena.Helpers;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;
using Xamarin.Forms;
using System.Linq;


namespace Tamaleria_Miguelena.ViewModel
{
    class Envios_ViewModel : PropertyChange
    {
        readonly Repository_Api bdapi;
        readonly Get_ID get;

        private ObservableCollection<SucursalModel> _sucursal;
        public ObservableCollection<SucursalModel> Sucursales
        {
            get { return _sucursal; }
            set { _sucursal = value; OnPropertyChanged(); }
        }

        private ObservableCollection<EnviosModel> _envios;
        public ObservableCollection<EnviosModel> Envios
        {
            get { return _envios; }
            set { _envios = value; OnPropertyChanged(); }
        }
        public Command add_envio { get; set; }

        public SucursalModel ItemSelected { get; set; }

        public Envios_ViewModel()
        {
            bdapi = new Repository_Api();
            get = new Get_ID();
            getSucursal();
            add_envio = new Command(guardar_envio);
            

        }

        public async void guardar_envio()
        {
            string local = get.get_nombresucursal();
            if (local != ItemSelected.nombre_sucursal ) { 
            await bdapi.Agregar_envio(new EnviosModel
            {
                usuario = get.get_usuario(),
                destino = ItemSelected.nombre_sucursal,
                origen = get.get_nombre_sucursal()
            });
            await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "ENVIO GENERADA", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "NO PUEDE ENVIAR PRODUCTO A SU MISMA SUCURSAL", "OK");
            }
        }

        public async void getSucursal()
        {
            var datos = await bdapi.getSucursal_pedido();
            var lista = datos.ToList();
            var itemToRemove = datos.Single(r => r.nombre_sucursal == get.get_nombresucursal());
            lista.Remove(itemToRemove);
            Sucursales = new ObservableCollection<SucursalModel>(lista);
        }

    }
}
