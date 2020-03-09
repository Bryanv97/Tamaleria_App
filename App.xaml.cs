using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tamaleria_Miguelena.Helpers;
namespace Tamaleria_Miguelena
{
    public partial class App : Application
    {
        
        public static MasterDetailPage MasterDetail { get; set; }
        Get_ID ids = new Get_ID();
        public App()
        {
            
            InitializeComponent();
            int Id = ids.get_usuario();
            if (Id == 0) { 
            MainPage = new NavigationPage(new View.Login_Page());
            }
            else{
            MainPage = new NavigationPage(new View.MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

        }
    }
}
