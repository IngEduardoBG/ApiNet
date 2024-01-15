using EjercicioNet.Models;
using EjercicioNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace EjercicioNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibroController : ControllerBase
    {
        //Interface
        private readonly ILibros _libros;
        //Contructor de interface
        public LibroController(ILibros libros)
        {
            _libros = libros;
        }

       [HttpGet("{parametro}")]
        //Método busqueda por parametro
        public async Task<string> BuscaLibros(string parametro) => await _libros.BuscaLibro(parametro);

        //Método modificación de un libro
        [HttpPut("{parametro}")]
        public async Task<string> ModificaLibro(string parametro,Libro libro) => await _libros.ModificaLibro(parametro,libro);

        //método de eliminacion de un libro
        [HttpDelete("{parametro}")]
        public async Task<string> EliminaLibro(string parametro)=> await _libros.EliminaLibro(parametro);
        //Método para agregar un libro
        [HttpPost]
        public async Task<string> AgregarLibro(Libro libro) => await _libros.AgregarLibro(libro);
    }
}
