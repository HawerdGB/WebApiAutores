using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Autores
            CreateMap<AutorCrearDTO, Autor>();
            CreateMap<Autor, AutorDTO>();

            //Libros
            CreateMap<LibroCrearDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));
            CreateMap<Libro, LibroDTO>();

            //Comentarios
            CreateMap<ComentarioCrearDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();



        }

        private List<AutorLibro> MapAutoresLibros(LibroCrearDTO libroCrearDTO, Libro libro)
        {
            var resultado = new List<AutorLibro>();

            if (libroCrearDTO.AutoresIds == null)
            {
                return resultado;
            }

            foreach(var autorId in libroCrearDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro() { AutorId = autorId });
            }
            
            
            return resultado;
        }
    }
}
