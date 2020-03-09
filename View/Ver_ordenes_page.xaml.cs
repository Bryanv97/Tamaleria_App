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
    public partial class Ver_ordenes_page : ContentPage
    {
        public Ver_ordenes_page()
        {
            InitializeComponent();
            //ItemsSource="{Binding ordenes}" ItemSelected="{Binding orden_selecionada}"
        }
    }
}