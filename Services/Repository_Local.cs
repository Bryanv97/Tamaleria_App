using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using Tamaleria_Miguelena.Model;

namespace Tamaleria_Miguelena.Services
{
    public class Repository_Local
    {
        SQLiteConnection BD_local;
        public static string StatusMessage { get; set; }
        public Repository_Local()
        {
            var ubicacionBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"bd_local2");
            BD_local = new SQLiteConnection(ubicacionBD);
            BD_local.CreateTable<SucursalModel>();
            BD_local.CreateTable<UsuariosModel>();
            BD_local.CreateTable<CorteModel>();
        }

        // Ingresar Usuario En BD Local
        public void CreateUser(UsuariosModel newUser)
        {
            try
            {
                BD_local.Insert(newUser);
                StatusMessage = $"Registro Ingresado exitosamente ID:{newUser.username}";

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al insertar{ex.Message}";
            }
        }
        
        // Ingresar Sucursal En BD Local

        public void CreateSucursal(SucursalModel newSucursal)
        {
            try
            {
                BD_local.Insert(newSucursal);
                StatusMessage = $"Registro Ingresado exitosamente ID:{newSucursal.nombre_sucursal}";

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al insertar{ex.Message}";
            }
        }

        // Ingresar Sucursal En BD Local

        public void CreateCorte(CorteModel newCorte)
        {
            try
            {
                BD_local.Insert(newCorte);
                StatusMessage = $"Registro Ingresado exitosamente ID:{newCorte.id_corte}";

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al insertar{ex.Message}";
            }
        }

        // Obtener Sucursal
        public List<SucursalModel> GetSucursal()
        {
            try 
            {
                return BD_local.Table<SucursalModel>().ToList();
            }
            catch(Exception ex) 
            {
                StatusMessage = $"Error al insertar{ex.Message}";
                return null;
            }
        }
        // Obtener Usuario
        public List<UsuariosModel> GetUsuario()
        {
            try
            {
                return BD_local.Table<UsuariosModel>().ToList();
            }
            catch(Exception ex) 
            {
                StatusMessage = $"Error al insertar{ex.Message}";
                return null;
            }
        }
        // Obtener Corte
        public List<CorteModel> GetCorte()
        {
            try
            {
                return BD_local.Table<CorteModel>().ToList();
            }
            catch( Exception ex) 
            {
                StatusMessage = $"Error al insertar{ex.Message}";
                return null;
            }
        }

        // Borrar Sucursal
        public void DeleteSucursal(SucursalModel sucursal)
        {
            try 
            {
                BD_local.Delete(sucursal);
            }
            catch(Exception ex) 
            {
                StatusMessage = $"Error al insertar{ex.Message}";
            }
        }

        // Borrar Usuario
        public void DeleteUser(UsuariosModel user)
        {
            try
            {
                BD_local.Delete(user);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al insertar{ex.Message}";
            }
        }

        // Borrar Corte
        public void CorteModel(CorteModel corte)
        {
            try
            {
                BD_local.Delete(corte);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al insertar{ex.Message}";
            }
        }

    }
}
