using EjercicioNet.Models;
using System.ComponentModel.DataAnnotations;

namespace EjercicioNet.Services
{
    public interface ILibros
    {
        public Task<string> BuscaLibro(string parametro);
        public Task<string> ModificaLibro(string parametro,Libro libro);
        public Task<string> EliminaLibro(string parametro);
        public Task<string> AgregarLibro(Libro libro);
    }
}
