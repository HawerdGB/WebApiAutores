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
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IMapper mapper;
        public LibrosController(AppDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<List<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();

            return mapper.Map<List<LibroDTO>>(libros);
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros
                .FirstOrDefaultAsync(x => x.Id == id);

               
            return mapper.Map<LibroDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LibroCrearDTO libroCrearDTO)
        {
            if (libroCrearDTO.AutoresIds == null) {
                return BadRequest("Debe asignar un autor al libro");
            }
            
            var autoresId = await context.Autores.Where( autorBD => libroCrearDTO.AutoresIds.Contains(autorBD.Id)).Select( x => x.Id).ToListAsync();

            if(libroCrearDTO.AutoresIds.Count != autoresId.Count)
            {
                return BadRequest("No existe uno de los autores enviados");
            
            }
            
            
            var libro = mapper.Map<Libro>(libroCrearDTO);

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Libro libro, int id)
        {
            if (libro.Id != id)
            {
                return BadRequest("El id del libro no coincide");
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Libro() { Id = id });
            await context.SaveChangesAsync();
            return Ok();

        }

    }

}
