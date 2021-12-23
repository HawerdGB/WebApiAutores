using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IMapper mapper;
        public ComentariosController(AppDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;


        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var comentarios = await context.Comentarios.Where(x => x.LibroId == libroId).ToListAsync();

            return mapper.Map<List<ComentarioDTO>>(comentarios);

        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCrearDTO comentarioCrearDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == libroId);

            if (!existeLibro)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentarioCrearDTO);
            comentario.LibroId = libroId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
