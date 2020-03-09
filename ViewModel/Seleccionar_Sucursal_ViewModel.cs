using System;
using System.Collections.Generic;
using System.Text;
using Tamaleria_Miguelena.Services;
using Tamaleria_Miguelena.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;


namespace Tamaleria_Miguelena.ViewModel
{
    public class Seleccionar_Sucursal_ViewModel : PropertyChange
    {
        Repository_Local bdlocal;
        Repository_Api bdapi;

        private ObservableCollection<SucursalModel> _sucursal;
        public ObservableCollection<SucursalModel> Sucursales
        {
            get { return _sucursal; }
            set { _sucursal = value; OnPropertyChanged(); } 
        }

        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; OnPropertyChanged(); }
        }
        public SucursalModel ItemSelected { get; set; }
        public Command asignarsucursal { get; set; }
        public Command versucursal { get; set; }
        public Command eliminarsucursal { get; set; }

        public Seleccionar_Sucursal_ViewModel()
        {
            bdlocal = new Repository_Local();
            bdapi = new Repository_Api();
            getSucursal();
            asignarsucursal = new Command(asignar_sucursal);
            eliminarsucursal = new Command(eliminar_sucursal);
            Nombre = ver_sucursal();          

        }
        public string ver_sucursal() 
        {
            var nombre = "";
            var eliminar = bdlocal.GetSucursal();
            foreach (SucursalModel element in eliminar)
            {
                nombre = element.nombre_sucursal;
            }
            //App.Current.MainPage.DisplayAlert("ADVERTENCIA", nombre + direccion + id.ToString() , "CONTINUAR");
            return nombre;

        }
        public async void eliminar_sucursal() 
        {
            var nombre = "";
            var id = 0;
            var direccion = "";
            var eliminar = bdlocal.GetSucursal();
            foreach (SucursalModel element in eliminar)
            {
                nombre = element.nombre_sucursal;
                id = element.id_sucursal;
                direccion = element.direccion;
            }

            if (eliminar.Count == 0) 
            {
                await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "NO EXISTE UNA SUCURSAL ASIGNADA A ESTE DISPOSITIVO", "CONTINUAR");

            }
            else 
            {
                bdlocal.DeleteSucursal(new SucursalModel { id_sucursal = id, direccion = direccion, nombre_sucursal = nombre});
                await bdapi.Actualizar_Sucursal(new SucursalModel { nombre_sucursal = nombre, id_sucursal = id, direccion = direccion, asignado = 0 });
                await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "BORRADO CON EXITO", "CONTINUAR");
                getSucursal();
                Nombre = ver_sucursal();

            }
        }

        public async void asignar_sucursal() 
        {
            var exitente = bdlocal.GetSucursal();

            if(exitente.Count != 0) 
            {
                await App.Current.MainPage.DisplayAlert("ADVERTENCIA","YA EXISTE UNA SUCURSAL ASIGNADA A ESTE DISPOSITIVO","CONTINUAR");
            }
            else
            {
                if (ItemSelected != null)
                {
                    var nombre = ItemSelected.nombre_sucursal;
                    var direccion = ItemSelected.direccion;
                    var id = ItemSelected.id_sucursal;
                    bdlocal.CreateSucursal(new SucursalModel { nombre_sucursal = nombre, id_sucursal = id, direccion = direccion });
                    await bdapi.Actualizar_Sucursal(new SucursalModel { nombre_sucursal = nombre, id_sucursal = id, direccion = direccion, asignado = 1 });
                    await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "SE HA ASIGNADO " + nombre + " COMO SUCURSAL", "CONTINUAR");
                    getSucursal();
                    Nombre = ver_sucursal();
                }
                else 
                {
                    await App.Current.MainPage.DisplayAlert("ADVERTENCIA", "NO SE HA SELECCIONADO NINGUNA SUCURSAL", "CONTINUAR") ;
                }
            }


        }
        public async void getSucursal() 
        {
            var datos = await bdapi.getSucursal();
            Sucursales = new ObservableCollection<SucursalModel>(datos);
        }

    }
}
