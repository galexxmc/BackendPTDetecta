// Capa: Infrastructure
// Archivo: Persistence/Interceptors/AuditoriaInterceptor.cs
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BackendPTDetecta.Infrastructure.Persistence.Interceptors
{
    public class AuditoriaInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditoriaInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

            var codigoUsuario = _currentUserService.ObtenerCodigoUsuario();
            
            // 1. OBTENER FECHA LOCAL Y LIMPIA (Sin milisegundos)
            // DateTime.Now usa la hora del servidor (asegúrate que tu PC/Server esté en hora Perú)
            var fechaSucia = DateTime.Now; 
            var fechaLimpia = new DateTime(
                fechaSucia.Year, fechaSucia.Month, fechaSucia.Day, 
                fechaSucia.Hour, fechaSucia.Minute, fechaSucia.Second);

            foreach (var entry in context.ChangeTracker.Entries<EntidadAuditable>())
            {
                // CASO 1: REGISTRO NUEVO
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UsuarioRegistro = codigoUsuario;
                    entry.Entity.FechaRegistro = fechaLimpia;
                    entry.Entity.EstadoRegistro = 1;
                }
                
                // CASO 2: MODIFICACIÓN O ELIMINACIÓN LÓGICA
                else if (entry.State == EntityState.Modified)
                {
                    // Detectamos si se está cambiando el estado a 0 (Eliminación Lógica)
                    var propEstado = entry.Property(x => x.EstadoRegistro);
                    
                    if (propEstado.IsModified && (int)propEstado.CurrentValue! == 0)
                    {
                        // ES UNA ELIMINACIÓN
                        entry.Entity.UsuarioEliminacion = codigoUsuario;
                        entry.Entity.FechaEliminacion = fechaLimpia;
                        // El MotivoEliminacion debe venir seteado desde el servicio/repo
                    }
                    else
                    {
                        // ES UNA MODIFICACIÓN NORMAL
                        entry.Entity.UsuarioModificacion = codigoUsuario;
                        entry.Entity.FechaModificacion = fechaLimpia;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}