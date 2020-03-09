using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tamaleria_Miguelena.Model;

namespace Tamaleria_Miguelena.Services
{
    class Repository_Api
    {

        const string URL = "http://192.168.1.11:80/tamaleria_miguelena_api/index.php";

        public HttpClient getCliente()
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            cliente.DefaultRequestHeaders.Add("Connection", "close");
            return cliente;
        }
        //Obtener Sucursales
        public async Task<IEnumerable<SucursalModel>> getSucursal()         
        {
                HttpClient cliente = getCliente();
                var resultado = await cliente.GetAsync($"{URL}/Sucursal/get_sucursal");
                if (resultado.IsSuccessStatusCode)
                {
                    string contenido = await resultado.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<SucursalModel>>(contenido);
                }
                else
                {
                    return Enumerable.Empty<SucursalModel>();
                }
        }
        // Obtener Inventario
        public async Task<IEnumerable<Inventario_Model>> getInventario()
        {
            HttpClient cliente = getCliente();
            var resultado = await cliente.GetAsync($"{URL}/Inventario/get_inventario");
            if (resultado.IsSuccessStatusCode)
            {
                string contenido = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Inventario_Model>>(contenido);
            }
            else
            {
                return Enumerable.Empty<Inventario_Model>();
            }
        }


        public async Task<IEnumerable<SucursalModel>> getSucursal_pedido()
        {
            HttpClient cliente = getCliente();
            var resultado = await cliente.GetAsync($"{URL}/Sucursal/get_sucursal_asignar");
            if (resultado.IsSuccessStatusCode)
            {
                string contenido = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<SucursalModel>>(contenido);
            }
            else
            {
                return Enumerable.Empty<SucursalModel>();
            }
        }
        //Obtener Usuarios
        public async Task<IEnumerable<UsuariosModel>> getUsuarios(UsuariosModel loginfo)
        {       
            HttpClient cliente = getCliente();
            string json = JsonConvert.SerializeObject(loginfo);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await cliente.PostAsync($"{URL}/Login/Login",contenido);
            if (resultado.IsSuccessStatusCode)
            {
                string log = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<UsuariosModel>>(log);
            }
            else
            {
                return Enumerable.Empty<UsuariosModel>();
            }
        }
        //Obtener Todos los Envios 
        public async Task<IEnumerable<EnviosModel>> getEnvio_Adm()
        {
            HttpClient cliente = getCliente();
            var resultado = await cliente.GetAsync($"{URL}/Envio/get_envio");
            if (resultado.IsSuccessStatusCode)
            {
                string contenido = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<EnviosModel>>(contenido);
            }
            else
            {
                return Enumerable.Empty<EnviosModel>();
            }
        }
        //Obtener Todos Los Envios
        public async Task<IEnumerable<PedidoModel>> getPedido_Adm()
        {
            HttpClient cliente = getCliente();
            var resultado = await cliente.GetAsync($"{URL}/Ordenes/get_pedido");
            if (resultado.IsSuccessStatusCode)
            {
                string contenido = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<PedidoModel>>(contenido);
            }
            else
            {
                return Enumerable.Empty<PedidoModel>();
            }
        }
        //Asignar Sucursales A Dispositivos
        public async Task<dynamic> Actualizar_Sucursal(SucursalModel sucursal)
        {
            HttpClient client = getCliente();
            string json = JsonConvert.SerializeObject(sucursal);
            try
            {
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await client.PostAsync($"{URL}/Sucursal/update_sucursal", contenido);
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", $"{ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return "";
        }

        //Actualizar Total Orden
        public async Task<dynamic> Actualizar_efectivo_pedido(PedidoModel pedido)
        {
            HttpClient client = getCliente();
            string json = JsonConvert.SerializeObject(pedido);
            try
            {
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await client.PostAsync($"{URL}/Ordenes/update_efectivo", contenido);
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", $"{ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return "";
        }

        //Guardar Productos De Ordenes
        public async Task<dynamic> PedidoOrdenGuardar(Pedido_Detalle_Model pedido)
        {
            HttpClient client = getCliente();
            string json = JsonConvert.SerializeObject(pedido);
            try
            {
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await client.PostAsync($"{URL}/Ordenes/guardar_orden", contenido);
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", $"{ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return "";
        }

        //Agregar Pedido
        public async Task<IEnumerable<PedidoModel>> Agregar_pedido(PedidoModel pedido)
        {
            HttpClient client = getCliente();
            string json = JsonConvert.SerializeObject(pedido);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await client.PostAsync($"{URL}/Ordenes/Crear_orden", contenido);
            if (resultado.IsSuccessStatusCode)
            {
                string log = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<PedidoModel>>(log);
            }
            else
            {
                return Enumerable.Empty<PedidoModel>();
                
            }

        }

        public async Task<dynamic> Agregar_envio(EnviosModel envio)
        {
            HttpClient client = getCliente();
            string json = JsonConvert.SerializeObject(envio);
            try
            {
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await client.PostAsync($"{URL}/Envio/Crear_envio", contenido);
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", $"{ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return "";



        }
        // Login E Iniciar El Corte
        public async Task<IEnumerable<CorteModel>> Iniciar_Corte(CorteModel cortinfo)
        {

            HttpClient cliente = getCliente();
            string json = JsonConvert.SerializeObject(cortinfo);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await cliente.PostAsync($"{URL}/Corte/Iniciar_corte", contenido);
            if (resultado.IsSuccessStatusCode)
            {
                string log = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CorteModel>>(log);
            }
            else
            {
                return Enumerable.Empty<CorteModel>();
            }
        }
        // Cerrar Session
        public async Task<dynamic> Finalizar_Corte(CorteModel fincorte)
        {
            HttpClient client = getCliente();
            string json = JsonConvert.SerializeObject(fincorte);
            try
            {
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await client.PostAsync($"{URL}/Corte/Finalizar_corte", contenido);
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", $"{ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return "";
        }
        //Prueba json list 
        public string TestJson(IEnumerable<UsuariosModel> sucursal)
        {
            string json = JsonConvert.SerializeObject(sucursal);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            return json;
        }

        //Obtener Numero De Corte
        public async Task<IEnumerable<CorteModel>> numero_corte(CorteModel id_sucursal)
        {

            HttpClient cliente = getCliente();
            string json = JsonConvert.SerializeObject(id_sucursal);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await cliente.PostAsync($"{URL}/Corte/get_ncorte", contenido);
            if (resultado.IsSuccessStatusCode)
            {
                string log = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CorteModel>>(log);
            }
            else
            {
                return Enumerable.Empty<CorteModel>();
            }
        }

        // Obtener Pedidos
        public async Task<IEnumerable<PedidoModel>> Get_pedido(PedidoModel pedido)
        {

            HttpClient cliente = getCliente();
            string json = JsonConvert.SerializeObject(pedido);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await cliente.PostAsync($"{URL}/Ordenes/get_pedido", contenido);
            if (resultado.IsSuccessStatusCode)
            {
                string log = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<PedidoModel>>(log);
            }
            else
            {
                return Enumerable.Empty<PedidoModel>();
            }
        }

        // Obtener Envio
        public async Task<IEnumerable<EnviosModel>> Get_envio(EnviosModel pedido)
        {

            HttpClient cliente = getCliente();
            string json = JsonConvert.SerializeObject(pedido);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var resultado = await cliente.PostAsync($"{URL}/Envio/get_envio", contenido);
            if (resultado.IsSuccessStatusCode)
            {
                string log = await resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<EnviosModel>>(log);
            }
            else
            {
                return Enumerable.Empty<EnviosModel>();
            }
        }

    }
}
