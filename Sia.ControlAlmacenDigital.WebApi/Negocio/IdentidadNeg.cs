using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sia.ControlAlmacenDigital.Entidades;
using Sia.ControlAlmacenDigital.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sia.ControlAlmacenDigital.WebApi.Negocio
{
    public class IdentidadNeg
    {
        public List<Entidades.AspNetUsers> Obtener()
        {
            List<Entidades.AspNetUsers> resultado = new List<Entidades.AspNetUsers>();

            try
            {
                using (var contexto = new ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.AspNetUsers
                        .Select(m => new Entidades.AspNetUsers()
                        {
                            Id = m.Id,
                            Nombres = m.Nombres,
                            ApellidoPaterno = m.ApellidoPaterno,
                            ApellidoMaterno = m.ApellidoMaterno,
                            TipoPrecio = m.TipoPrecio,
                            Email = m.Email,
                            EmailConfirmed = m.EmailConfirmed,
                            UserName = m.UserName,
                            Activo = m.Activo,
                            AspNetRoles = m.AspNetRoles.Select(r => new Entidades.AspNetRoles()
                            {
                                Id = r.Id,
                                Name = r.Name
                            }).ToList()
                        }).ToList();

                    foreach (var item in resultado)
                    {
                        item.NombreCompleto = item.Nombres + ' ' + item.ApellidoPaterno + ' ' + item.ApellidoMaterno;
                    }
                }
            }
            catch (Exception ex)
            {
                var msj = ex;

            }
            return resultado;
        }

        public Entidades.AspNetUsers ObtenerPorId(string id)
        {
            Entidades.AspNetUsers resultado = new Entidades.AspNetUsers();

            try
            {
                using (var contexto = new ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.AspNetUsers
                        .Where(m => m.Id == id)
                        .Select(m => new Entidades.AspNetUsers()
                        {
                            Id = m.Id,
                            Nombres = m.Nombres,
                            ApellidoPaterno = m.ApellidoPaterno,
                            ApellidoMaterno = m.ApellidoMaterno,
                            Email = m.Email,
                            TipoPrecio = m.TipoPrecio,
                            EmailConfirmed = m.EmailConfirmed,
                            UserName = m.UserName,
                            Activo = m.Activo,
                            AspNetRoles = m.AspNetRoles.Select(r => new Entidades.AspNetRoles()
                            {
                                Id = r.Id,
                                Name = r.Name
                            }).ToList()
                        }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var msj = ex;

            }
            return resultado;
        }

        public async Task<string> EditarUsuario(UsuarioEditar usuario)
        {
            Resultado<UsuarioEditar> resultado = new Resultado<UsuarioEditar>();
            var errorResult = "";
            var resultado2 = "";

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            //var currentUser = manager.FindByEmail(usuario.Email);
            var currentUser = await manager.FindByIdAsync(usuario.Id);
            currentUser.Email = usuario.Email;
            currentUser.UserName = usuario.UserName;
            currentUser.Nombres = usuario.Nombres;
            currentUser.ApellidoPaterno = usuario.ApellidoPaterno;
            currentUser.ApellidoMaterno = usuario.ApellidoMaterno;
            currentUser.TipoPrecio = (int)usuario.TipoPrecio;
            var result = await manager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                resultado.Exito = true;
                resultado.MensajeExito = WebApi.Recursos.ExitoEditar;
                resultado2 = "Ok";
            }
            else
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorEditar + ' ' + result.Errors;
                errorResult = result.Errors.ToString();
                resultado2 = errorResult;
            }
            return resultado2;
        }

        public async Task<string> EditarRolUsuario(EditarRolUsuario modelo)
        {
            Resultado<EditarRolUsuario> resultado = new Resultado<EditarRolUsuario>();
            var errorResult = "";
            var resultado2 = "";

            var usuarioId = modelo.UserId;
            var roleId = modelo.RoleId;
            var estatus = modelo.EstatusCheck;

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            IdentityResult result;
            if (estatus)
            {
                result = await manager.AddToRoleAsync(usuarioId, roleId);
            }
            else
            {
                result = await manager.RemoveFromRoleAsync(usuarioId, roleId);
            }

            if (result.Succeeded)
            {
                resultado.Exito = true;
                resultado.MensajeExito = WebApi.Recursos.ExitoEditar;
                resultado2 = "Ok";
            }
            else
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorEditar + ' ' + result.Errors;
                errorResult = result.Errors.ToString();
                resultado2 = errorResult;
            }
            return resultado2;
        }

        public async Task<string> EditarPasswordUsuario(EditarPasswordUsuario modelo)
        {
            Resultado<EditarPasswordUsuario> resultado = new Resultado<EditarPasswordUsuario>();
            var errorResult = "";
            var resultado2 = "";

            var usuarioId = modelo.Id;
            var password = modelo.Password;

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            IdentityResult result;
            manager.RemovePassword(usuarioId);
            result = await manager.AddPasswordAsync(usuarioId, password);

            if (result.Succeeded)
            {
                resultado.Exito = true;
                resultado.MensajeExito = WebApi.Recursos.ExitoEditar;
                resultado2 = "Ok";
            }
            else
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorEditar + ' ' + result.Errors;
                errorResult = result.Errors.ToString();
                resultado2 = errorResult;
            }
            return resultado2;
        }

        public async Task<string> EditarEstatusUsuario(UsuarioEditar usuario)
        {
            Resultado<UsuarioEditar> resultado = new Resultado<UsuarioEditar>();
            var errorResult = "";
            var resultado2 = "";

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = await manager.FindByIdAsync(usuario.Id);
            currentUser.Activo = !currentUser.Activo;
            var result = await manager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                resultado.Exito = true;
                resultado.MensajeExito = WebApi.Recursos.ExitoEditar;
                resultado2 = "Ok";
            }
            else
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorEditar + ' ' + result.Errors;
                errorResult = result.Errors.ToString();
                resultado2 = errorResult;
            }
            return resultado2;
        }


    }
}