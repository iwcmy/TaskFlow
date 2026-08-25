using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using TaskFlow.Api.Controllers;
using TaskFlow.Api.Data;
using TaskFlow.Api.Dtos;
using TaskFlow.Api.Models;
using Xunit;

namespace TaskFlow.Api.Tests
{
    public class ProyectosControllerTests
    {
        private TaskFlowDbContext CrearContextoEnMemoria()
        {
            var opciones = new DbContextOptionsBuilder<TaskFlowDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TaskFlowDbContext(opciones);
        }

        private void SimularUsuarioAutenticado(ControllerBase controller, int usuarioId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString())
            };
            var identidad = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identidad);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        [Fact]
        public async Task AgregarMiembro_CuandoUsuarioNoEsAdmin_DevuelveForbid()
        {
            // Arrange
            var context = CrearContextoEnMemoria();

            var admin = new Usuario { Nombre = "Admin", Email = "admin@test.com", PasswordHash = "x" };
            var miembroNoAdmin = new Usuario { Nombre = "Miembro", Email = "miembro@test.com", PasswordHash = "x" };
            var proyecto = new Proyecto { Nombre = "Proyecto de prueba" };

            context.Usuarios.AddRange(admin, miembroNoAdmin);
            context.Proyectos.Add(proyecto);
            await context.SaveChangesAsync();

            context.MiembrosProyecto.Add(new MiembroProyecto
            {
                UsuarioId = miembroNoAdmin.Id,
                ProyectoId = proyecto.Id,
                Rol = RolProyecto.Miembro
            });
            await context.SaveChangesAsync();

            var controller = new ProyectosController(context, NullLogger<ProyectosController>.Instance);
            SimularUsuarioAutenticado(controller, miembroNoAdmin.Id);

            var dto = new AgregarMiembroDto("otro@test.com", RolProyecto.Miembro);

            // Act
            var resultado = await controller.AgregarMiembro(proyecto.Id, dto);

            // Assert
            Assert.IsType<ForbidResult>(resultado);
        }

        [Fact]
        public async Task AgregarMiembro_CuandoUsuarioEsAdmin_AgregaMiembroExitosamente()
        {
            // Arrange
            var context = CrearContextoEnMemoria();

            var admin = new Usuario { Nombre = "Admin", Email = "admin@test.com", PasswordHash = "x" };
            var nuevoUsuario = new Usuario { Nombre = "Nuevo", Email = "nuevo@test.com", PasswordHash = "x" };
            var proyecto = new Proyecto { Nombre = "Proyecto de prueba" };

            context.Usuarios.AddRange(admin, nuevoUsuario);
            context.Proyectos.Add(proyecto);
            await context.SaveChangesAsync();

            context.MiembrosProyecto.Add(new MiembroProyecto
            {
                UsuarioId = admin.Id,
                ProyectoId = proyecto.Id,
                Rol = RolProyecto.Admin
            });
            await context.SaveChangesAsync();

            var controller = new ProyectosController(context, NullLogger<ProyectosController>.Instance);
            SimularUsuarioAutenticado(controller, admin.Id);

            var dto = new AgregarMiembroDto(nuevoUsuario.Email, RolProyecto.Miembro);

            // Act
            var resultado = await controller.AgregarMiembro(proyecto.Id, dto);

            // Assert
            Assert.IsType<NoContentResult>(resultado);

            var membresiaCreada = await context.MiembrosProyecto
                .FirstOrDefaultAsync(mp => mp.ProyectoId == proyecto.Id && mp.UsuarioId == nuevoUsuario.Id);
            Assert.NotNull(membresiaCreada);
            Assert.Equal(RolProyecto.Miembro, membresiaCreada.Rol);
        }

        [Fact]
        public async Task AgregarMiembro_CuandoUsuarioNoEsMiembroDelProyecto_DevuelveNotFound()
        {

            // Arrange
            var context = CrearContextoEnMemoria();

            var admin = new Usuario { Nombre = "Admin", Email = "admin@test.com", PasswordHash = "x" };
            var nuevoUsuario = new Usuario { Nombre = "Nuevo", Email = "nuevo@test.com", PasswordHash = "x" };
            var proyecto = new Proyecto { Nombre = "Proyecto de prueba" };

            context.Usuarios.AddRange(admin, nuevoUsuario);
            context.Proyectos.Add(proyecto);
            await context.SaveChangesAsync();

            var controller = new ProyectosController(context, NullLogger<ProyectosController>.Instance);
            SimularUsuarioAutenticado(controller, admin.Id);

            var dto = new AgregarMiembroDto(nuevoUsuario.Email, RolProyecto.Miembro);

            // Act
            var resultado = await controller.AgregarMiembro(proyecto.Id, dto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);


        }
    }
}