using AntaresProyecto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AntaresProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly AntaresContext _dbcontext;

        public ProyectoController(AntaresContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        [Route("GetProyecto")]
        public async Task<IEnumerable<Proyecto>> GetProyecto()
        {
            return await _dbcontext.Proyectos.ToListAsync();
        }
        [HttpGet]
        [Route("GetCliente")]
        public async Task<IEnumerable<Cliente>> GetCliente()
        {
            return await _dbcontext.Clientes.ToListAsync();
        }
        [HttpGet]
        [Route("GetEtapa")]
        public async Task<IEnumerable<Etapa>> GetEtapa()
        {
            return await _dbcontext.Etapas.ToListAsync();
        }

        [HttpPost]
        [Route("AgregarCliente")]
        public async Task<IActionResult> AgregarCliente([FromBody] Cliente nuevoCliente)
        {
            if (nuevoCliente == null)
            {
                return BadRequest();
            }

            // Asegúrate de que Id no se esté estableciendo aquí

            nuevoCliente.Estado = true; // Estado por defecto

            _dbcontext.Clientes.Add(nuevoCliente);
            _dbcontext.SaveChanges();

            return Ok(nuevoCliente);
        }


        [HttpPost]
        [Route("AddEtapa")]

        public async Task<Etapa> AddEtapa([FromBody] Etapa objEtapa)
        {
            _dbcontext.Etapas.Add(objEtapa);
            await _dbcontext.SaveChangesAsync();
            return objEtapa;
        }


        [HttpPost]
        [Route("AddProyecto")]

        public async Task<Proyecto> AddProyecto([FromBody] Proyecto objProyecto)
        {
            _dbcontext.Proyectos.Add(objProyecto);
            await _dbcontext.SaveChangesAsync();
            return objProyecto;
        }

        [HttpPatch]
        [Route("UpdateProyecto/{id}")]
        public async Task<Proyecto> UpdateProyecto(Proyecto objProyecto)
        {
            _dbcontext.Entry(objProyecto).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return objProyecto;
        }

        [HttpPatch]
        [Route("UpdateEtapa/{etapasId}")]
        public async Task<Etapa> UpdateEtapa(Etapa objEtapa)
        {
            _dbcontext.Entry(objEtapa).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return objEtapa;
        }
        [HttpPatch]
        [Route("UpdateUnidad/{UnidadId}")]
        public async Task<UnidadesDeNegocio> UpdateUnidad(UnidadesDeNegocio objUnidad)
        {
            _dbcontext.Entry(objUnidad).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return objUnidad;
        }
        [HttpPatch]
        [Route("UpdateArea/{AreaId}")]
        public async Task<Area> UpdateArea(Area objArea)
        {
            _dbcontext.Entry(objArea).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return objArea;

        }
        [HttpPatch]
        [Route("UpdateEstado/{EstadoId}")]
        public async Task<Estado> UpdateEstado(Estado objEstado)
        {
            _dbcontext.Entry(objEstado).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return objEstado;

        }
        [HttpPatch]
        [Route("UpdateTipo/{TypeId}")]
        public async Task<ProjectType> UpdateTipo(ProjectType objTipo)
        {
            _dbcontext.Entry(objTipo).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return objTipo;

        }
        /*[HttpPatch]
        [Route("UpdateArea/{areaId}")]
        public async Task<ActionResult<Area>> UpdateArea(int areaId, [FromBody] AreaUpdateModel model)
        {
            try
            {
                // Crea una entidad con el áreaId proporcionado
                var entity = new Area { AreaId = areaId };
                _dbcontext.Attach(entity);

                // Actualiza las propiedades del área con los valores del modelo
                entity.NombreArea = model.NombreArea; // Asegúrate de tener la propiedad NombreArea en tu modelo

                // Marca el área como modificado en el contexto
                _dbcontext.Entry(entity).State = EntityState.Modified;

                // Guarda los cambios en la base de datos
                await _dbcontext.SaveChangesAsync();

                // Devuelve el área actualizado
                return entity;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }*/

        [HttpDelete]
        [Route("Delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var proyecto = _dbcontext.Proyectos.Find(id);

            if (proyecto != null)
            {
                _dbcontext.Entry(proyecto).State = EntityState.Deleted;
                _dbcontext.SaveChanges();

                return Ok("Proyecto eliminado exitosamente.");
            }

            return NotFound("Proyecto no encontrado.");
        }

        [HttpPut("{id}/activar")]
        public async Task<IActionResult> ActivarCliente(int id)
        {
            var cliente = _dbcontext.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Estado = true;
            _dbcontext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> DesactivarCliente(int id)
        {
            var cliente = _dbcontext.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Estado = false;
            _dbcontext.SaveChanges();

            return NoContent();
        }
        [HttpGet("activos")]
        public ActionResult<IEnumerable<Cliente>> ObtenerClientesActivos()
        {
            var clientesActivos = _dbcontext.Clientes.Where(c => c.Estado.HasValue && c.Estado.Value).ToList();
            return Ok(clientesActivos);
        }

        [HttpGet("unidades-negocio")]
        public ActionResult<IEnumerable<UnidadDTO>> ObtenerUnidadesNegocioConAreas()
        {
            var unidadesNegocio = _dbcontext.UnidadesDeNegocios.Include(u => u.Areas).ToList();
            var unidadesDTO = unidadesNegocio.Select(u => new UnidadDTO
            {
                UnidadId = u.UnidadId,
                NombreUnidad = u.NombreUnidad,
                Areas = u.Areas.Select(a => new AreaDTO
                {
                    AreaId = a.AreaId,
                    NombreArea = a.NombreArea
                }).ToList()
            });

            return Ok(unidadesDTO);
        }
        [HttpPost]
        [Route("AddUnidad")]

        public async Task<UnidadesDeNegocio> AddUnidad([FromBody] UnidadesDeNegocio objUnidad)
        {
            _dbcontext.UnidadesDeNegocios.Add(objUnidad);
            await _dbcontext.SaveChangesAsync();
            return objUnidad;
        }
        [HttpPost]
        [Route("AgregarUnidad")]
        public async Task<ActionResult<UnidadDTO>> CrearUnidad(UnidadDTO unidadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevasAreas = unidadDTO.Areas?.Select(areaDTO => new Area
            {
                NombreArea = areaDTO.NombreArea,
                UnidadId = unidadDTO.UnidadId
            }).ToList();

            var nuevaUnidad = new UnidadesDeNegocio
            {
                NombreUnidad = unidadDTO.NombreUnidad
            };

            if (nuevasAreas != null)
            {
                foreach (var area in nuevasAreas)
                {
                    // Asigna la nueva área a la unidad de negocio
                    nuevaUnidad.Areas.Add(area);
                }
            }

            _dbcontext.UnidadesDeNegocios.Add(nuevaUnidad);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(CrearUnidad), new { id = nuevaUnidad.UnidadId }, unidadDTO);
        }

        [HttpPost]
        [Route("AddArea")]

        public async Task<Area> AddArea([FromBody] Area objArea)
        {
            _dbcontext.Areas.Add(objArea);
            await _dbcontext.SaveChangesAsync();
            return objArea;
        }

        [HttpPost]
        [Route("AddEstado")]

        public async Task<Estado> AddEstado([FromBody] Estado objEstado)
        {
            _dbcontext.Estados.Add(objEstado);
            await _dbcontext.SaveChangesAsync();
            return objEstado;
        }
        [HttpGet]
        [Route("GetEstados")]
        public async Task<IEnumerable<Estado>> GetEstados()
        {
            return await _dbcontext.Estados.ToListAsync();
        }

        [HttpPost]
        [Route("AddType")]

        public async Task<ProjectType> AddType([FromBody] ProjectType objType)
        {
            _dbcontext.ProjectTypes.Add(objType);
            await _dbcontext.SaveChangesAsync();
            return objType;
        }
        [HttpGet]
        [Route("GetType")]
        public async Task<IEnumerable<ProjectType>> GetType()
        {
            return await _dbcontext.ProjectTypes.ToListAsync();
        }
    }
}
