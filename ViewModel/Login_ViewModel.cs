using System;
using System.Collections.Generic;
using System.Text;
using Tamaleria_Miguelena.Services;
using Tamaleria_Miguelena.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Tamaleria_Miguelena.Helpers;
using System.Linq;

namespace Tamaleria_Miguelena.ViewModel
{
    public class Login_ViewModel : PropertyChange
    {
        #region Propiedaes

        readonly Repository_Local bdlocal;
        readonly Repository_Api bdapi;


        private string _usuario;
        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; OnPropertyChanged(); }
        }


        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; OnPropertyChanged(); }
        }
        private string _pass;
        public string pass
        {
            get { return _pass; }
            set { _pass = value; OnPropertyChanged(); }
        }

        private ObservableCollection<UsuariosModel> _datos;
        public ObservableCollection<UsuariosModel> datos
        {
            get { return _datos; }
            set { _datos = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CorteModel> _corte;
        public ObservableCollection<CorteModel> corte
        {
            get { return _corte; }
            set { _corte = value; OnPropertyChanged(); }
        }
        public Command Efectuar_Login { get; set; }
        public Command VerUsuarioActivo { get; set; }
        public Command CerrarCorte { get; set; }
        public Command CerrarSesion { get; set; }

        #endregion
        public Login_ViewModel()
        {
            bdlocal = new Repository_Local();
            bdapi = new Repository_Api();
            Efectuar_Login = new Command(Login);
            //VerUsuarioActivo = new Command(ver_usuario);
            CerrarCorte = new Command(cerrar_corte);
            CerrarSesion = new Command(cerrar_sesion);

        }

        // Login
        public async void Login() 
        {
            Get_ID ids = new Get_ID();

            int activo = ids.get_usuario();
            if(activo == 0) 
            { 
                // Buscar Usuarios En la API
                var usuarios = await bdapi.getUsuarios(new UsuariosModel { username = usuario , password = pass });
                datos = new ObservableCollection<UsuariosModel>(usuarios);
                if (datos.Count == 0) 
                {
                    await App.Current.MainPage.DisplayAlert("Usuario","Incorrecto","OK");
                
                }
                // Guardar Usuario Localmente
                else 
                {
                UsuariosModel data = new UsuariosModel();                
                //await App.Current.MainPage.DisplayAlert("Usuario", "correcto", "OK");
                foreach(UsuariosModel user in datos) 
                {
                    data.id_usuario = user.id_usuario;
                    data.rol = user.rol;
                    data.username = user.username;
                    data.password = user.password;
                    data.nombre = user.nombre;
                    data.apellido = user.apellido;
                }
                bdlocal.CreateUser(data);
                //nombre = ver_usuario();

                    // Registrar Corte
                    if (data.rol == 2)
                    {
                        Iniciar_Corte();
                    }
                await App.Current.MainPage.Navigation.PushAsync(new View.MainPage());
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("ADVERTENCIA","YA EXISTE UN USUARIO lOGEADO","OK");
            }
        }

        // Ingresar Corte Al Sistema
        public async void Iniciar_Corte() 
        {
            CorteModel datos = new CorteModel(); 
            Get_ID ids = new Get_ID();
            Get_NCorte num = new Get_NCorte();
            int n_corte = await num.get_ncorte();
            int id_usuario = ids.get_usuario();
            int id_sucursal = ids.get_sucursal();
            string nombre = ids.get_nombreusuario();
            //await App.Current.MainPage.DisplayAlert("Corte","ID Usuario: " + id_usuario.ToString()+ "\n"+ "ID Sucursal: " + id_sucursal.ToString() + "\n" + "Nombre: " + nombre , "OK");
            var corte_save = await bdapi.Iniciar_Corte(new CorteModel { 
                                                            usuario = id_usuario,
                                                            sucursal = id_sucursal,
                                                            efectivo_final=0.00,
                                                            efectivo_inicial = 0.00,
                                                            // Estado 1 Para indicar que se inicio 
                                                            estado = 1, 
                                                            turno = n_corte + 1 });
            corte = new ObservableCollection<CorteModel>(corte_save);
            foreach (CorteModel info in corte)
            {
                datos.id_corte = info.id_corte;
                datos.fecha_hora = info.fecha_hora;
                datos.efectivo_final = info.efectivo_final;
                datos.efectivo_inicial = info.efectivo_inicial;
                datos.estado = info.estado;
                datos.sucursal = info.sucursal;
                datos.usuario = info.usuario;
                datos.turno = info.turno;
            }
            bdlocal.CreateCorte(datos);
            //int num_corte = await num.get_ncorte();
            //await App.Current.MainPage.DisplayAlert("Corte","Numero De Turno: " + num_corte, "OK");
        }

       
        // Obtener Usuario Registrado
  
        // Cerrar Corte
        public async void cerrar_corte()
        {
            Get_ID ids = new Get_ID();
            await bdapi.Finalizar_Corte(new CorteModel { estado = 0, id_corte = ids.get_corte()});
            UsuariosModel data = new UsuariosModel();
            var usuario = bdlocal.GetUsuario();
            datos = new ObservableCollection<UsuariosModel>(usuario);
            foreach (UsuariosModel user in datos)
            {
                data.id_usuario = user.id_usuario;
                data.rol = user.rol;
                data.username = user.username;
                data.password = user.password;
                data.nombre = user.nombre;
                data.apellido = user.apellido;
            }
            bdlocal.DeleteUser(data);
            //await App.Current.MainPage.DisplayAlert("ADVERTENCIA","SE HA CERRADO EL CORTE", "CONTINUAR");
            App.Current.MainPage = new NavigationPage(new View.Login_Page());

        }
        public void cerrar_sesion()
        {
            UsuariosModel data = new UsuariosModel();
            var usuario = bdlocal.GetUsuario();
            datos = new ObservableCollection<UsuariosModel>(usuario);
            foreach (UsuariosModel user in datos)
            {
                data.id_usuario = user.id_usuario;
                data.rol = user.rol;
                data.username = user.username;
                data.password = user.password;
                data.nombre = user.nombre;
                data.apellido = user.apellido;
            }
            bdlocal.DeleteUser(data);
            App.Current.MainPage = new NavigationPage(new View.Login_Page());
        }

        public async void ver_nCorte() 
        {
            Get_ID ids = new Get_ID();
            int g = 0;
            int id_corte = ids.get_corte();
            Get_NCorte n = new Get_NCorte();
            g = await n.get_ncorte();
            await App.Current.MainPage.DisplayAlert("Advertencia", "Numero De Turno Actual: " + g.ToString(), "ok");
            await App.Current.MainPage.DisplayAlert("Advertencia", "ID del Corte Actual: " + id_corte.ToString(), "ok");
        }
        

    }
}
