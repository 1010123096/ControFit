using ControlFit.Domain.Entidad;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFit.Infrastructure.Repository
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly AppDbContext _context;

        public AsistenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarAsync(Asistencia asistencia)
        {
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> YaIngresoHoyAsync(int miembroId)
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            return await _context.Asistencias
                .AsNoTracking()
                .AnyAsync(x => x.MiembroId == miembroId && x.FechaHoraAcceso >= hoy && x.FechaHoraAcceso < manana);
        }

        public async Task<int> ObtenerIngresosSemanaAsync(int miembroId)
        {
            var inicioSemana = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek));

            return await _context.Asistencias
                .AsNoTracking()
                .CountAsync(x => x.MiembroId == miembroId && x.FechaHoraAcceso >= inicioSemana);
        }

        public async Task<List<Asistencia>> ObtenerTodosAsync()
        {
            return await _context.Asistencias
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Asistencia>> ObtenerTodosPorGimnasio(int gimnasioId)
        {
            return await (from asistencia in _context.Asistencias
                          join miembro in _context.Miembros on asistencia.MiembroId equals miembro.Id
                          where miembro.GimnasioId == gimnasioId
                          select asistencia)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Asistencia>> ObtenerPorMiembroEnRango(int miembroId, DateTime fechaInicio, DateTime fechaFin, int gimnasioId)
        {
            return await (from asistencia in _context.Asistencias
                          join miembro in _context.Miembros on asistencia.MiembroId equals miembro.Id
                          where asistencia.MiembroId == miembroId
                                && asistencia.FechaHoraAcceso >= fechaInicio
                                && asistencia.FechaHoraAcceso <= fechaFin
                                && miembro.GimnasioId == gimnasioId
                          select asistencia)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int[]> ObtenerConteoDiarioUltimos7DiasPorGimnasio(int gimnasioId)
        {
            var inicio = DateTime.Today.AddDays(-6);
            var fin = DateTime.Today.AddDays(1);

            var asistencias = await (from asistencia in _context.Asistencias
                                     join miembro in _context.Miembros on asistencia.MiembroId equals miembro.Id
                                     where miembro.GimnasioId == gimnasioId
                                           && asistencia.FechaHoraAcceso >= inicio
                                           && asistencia.FechaHoraAcceso < fin
                                     select asistencia.FechaHoraAcceso)
                .ToListAsync();

            var conteos = new int[7];
            for (var i = 0; i < 7; i++)
            {
                var dia = inicio.AddDays(i);
                var diaSiguiente = dia.AddDays(1);
                conteos[i] = asistencias.Count(f => f >= dia && f < diaSiguiente);
            }

            return conteos;
        }
    }
}