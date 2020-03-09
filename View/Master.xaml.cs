using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tamaleria_Miguelena.Helpers;

namespace Tamaleria_Miguelena.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        Get_ID ids = new Get_ID();
        public Master()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            int rol = ids.get_rol();
            var masterPageItems = new List<MasterPageItem>();
            if (rol == 2) { 
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "ORDEN DE PEDIDOS",
                    TargetType = typeof(Ver_ordenes_page),
                    Icon = "carrito"

                });
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "ENVIAR PRODUCTO",
                    TargetType = typeof(Ver_Envios_Page),
                    Icon = "camion"

                });
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "VER PRODUCTO DISPONILE",
                    TargetType = typeof(Page3),
                    Icon = "ventas"
                });

                masterPageItems.Add(new MasterPageItem
                {
                    Title = "FINALIZAR TURNO",
                    TargetType = typeof(Finalizar_Page),
                    Icon = "salida"
                });
            }
            if(rol == 1) { 
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Agregar Sucursal",
                TargetType = typeof(Seleccionar_Sucursal_Page),
                Icon = "homeicon"
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Ver Reportes",
                TargetType = typeof(Reportes_Page),
                Icon = "reporte"
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Salir",
                TargetType = typeof(Finalizar_Adm),
                Icon = "salida"
            });
            }

            ListView.ItemsSource = masterPageItems;

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                App.MasterDetail.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                ListView.SelectedItem = null;
                App.MasterDetail.IsPresented = false;
            }
        }

    }
    
}