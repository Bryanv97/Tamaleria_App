using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tamaleria_Miguelena.Model;
using Tamaleria_Miguelena.Services;

namespace Tamaleria_Miguelena.Helpers
{
    public class Get_ID
    {
        UsuariosModel usuarios;
        SucursalModel sucursal;
        CorteModel corte;
        Repository_Local local;
        public Get_ID()
        {
            usuarios = new UsuariosModel();
            sucursal = new SucursalModel();
            corte = new CorteModel();
            local = new Repository_Local();
        }


        public int get_usuario()
        {
            int id_usuario = 0;
            List<UsuariosModel> datos = local.GetUsuario();
            foreach (UsuariosModel id in datos)
            {
                id_usuario = id.id_usuario;
            }
            return id_usuario;
        }


        public int get_rol()
        {
            int id_rol = 0;
            List<UsuariosModel> datos = local.GetUsuario();
            foreach (UsuariosModel id in datos)
            {
                id_rol = id.rol;
            }
            return id_rol;
        }
        public string get_nombreusuario()
        {
            string id_usuario = "";
            List<UsuariosModel> datos = local.GetUsuario();
            foreach (UsuariosModel id in datos)
            {
                id_usuario = id.nombre;
            }
            return id_usuario;
        }


        public int get_sucursal()
        {
            int id_sucursal = 0;
            List<SucursalModel> datos = local.GetSucursal();
            foreach (SucursalModel id in datos)
            {
                id_sucursal = id.id_sucursal;
            }
            return id_sucursal;
        }

        public string get_nombresucursal()
        {
            string id_sucursal = "";
            List<SucursalModel> datos = local.GetSucursal();
            foreach (SucursalModel id in datos)
            {
                id_sucursal = id.nombre_sucursal;
            }
            return id_sucursal;
        }

        public string get_nombre_sucursal()
        {
            string id_sucursal = "";
            List<SucursalModel> datos = local.GetSucursal();
            foreach (SucursalModel id in datos)
            {
                id_sucursal = id.nombre_sucursal;
            }
            return id_sucursal;
        }
        public int get_corte()
        {
            int id_corte = 0;
            List<CorteModel> datos = local.GetCorte();
            foreach (CorteModel id in datos)
            {
                id_corte = id.id_corte;
            }
            return id_corte;
        }


    }
}
