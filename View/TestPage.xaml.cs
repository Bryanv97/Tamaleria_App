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
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Seleccionar_Sucursal_Page());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Login_Page());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Ver_ordenes_page());

        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Ver_Envios_Page());
        }
    }
}