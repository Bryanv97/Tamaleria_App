using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamaleria_Miguelena.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reportes_Page : ContentPage
    {
        public Reportes_Page()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Ver_PedidoAdm());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Ver_EnvioAdm());

        }
    }
}