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
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IMapper mapper;
        public AutoresController(AppDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<List<AutorDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{nombre}")]
        public async Task<List<AutorDTO>> Get([FromRoute] string nombre )
        {
            var autores = await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<AutorDTO>>(autores);

        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTO>>  Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTO>(autor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCrearDTO autorCrearDTO)
        {
            var autor = mapper.Map<Autor>(autorCrearDTO);
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide");
            }

             context.Update(autor);
             await context.SaveChangesAsync();
             return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}
