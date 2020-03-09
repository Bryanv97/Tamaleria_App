using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamaleria_Miguelena.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        Repository_Local bdlocal = new Repository_Local();

        private ObservableCollection<UsuariosModel> _datos;
        public ObservableCollection<UsuariosModel> datos
        {
            get { return _datos; }
            set { _datos = value; OnPropertyChanged(); }
        }
        public Inicio()
        {
            InitializeComponent();
            lblnombre.Text = ver_usuario();
        }
        public string ver_usuario()
        {
            UsuariosModel data = new UsuariosModel();
            var usuario = bdlocal.GetUsuario();
            datos = new ObservableCollection<UsuariosModel>(usuario);
            foreach (UsuariosModel user in datos)
            {
                data.nombre = user.nombre;
            }
            return data.nombre;
            //App.Current.MainPage.DisplayAlert("Advertencia", "Usuario Actual: " + data.nombre , "CONTINUAR") ;
            //ver_nCorte();

        }
    }
}