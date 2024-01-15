using EjercicioNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace EjercicioNet.Services
{
    public class LibroServices:ILibros
    {
        string path = @"C:\Users\AdministradorTI\source\repos\EjercicioNet\EjercicioNet\Libros.json";

        //Método get de busqueda por bookName, author o releaseDate
        public async Task<string> BuscaLibro(string parametro)
        {


            try
            {

                string content = await File.ReadAllTextAsync(path);
                DataTable Data = (DataTable)JsonConvert.DeserializeObject(content, (typeof(DataTable)));
                var libro = new Libro();
                for (int i = 0; i < Data.Rows.Count; i++)
            {
                if (parametro == Data.Rows[i]["author"].ToString() || parametro == Data.Rows[i]["bookName"].ToString() || parametro == Data.Rows[i]["releaseDate"].ToString())
                {
                    libro.author = Data.Rows[i]["author"].ToString();
                    libro.releaseDate = Data.Rows[i]["releaseDate"].ToString();
                    libro.bookName = Data.Rows[i]["bookName"].ToString();
                }

            }
            if (libro.author != null)
            {
                return "Autor: "+ libro.author+ "\n" + "Nombre Libro: " + libro.bookName +"\n" +" Fecha publicación: " + libro.releaseDate;
            }
            else
            {
                return "Libro no encontrado";
            }
            }
            catch 
            {

                return "No se pudo completar la operacion con éxito, revisa los datos ingresados";
            }

        }
        //Método de modificacion de un libro 
        public async Task<string> ModificaLibro(string parametro, Libro libro)
        {
            try
            {
            string content = await File.ReadAllTextAsync(path);
            DataTable Data = (DataTable)JsonConvert.DeserializeObject(content, (typeof(DataTable)));
                bool bandera = false;
                var Nuevo = new Libro
            {
                author = libro.author,
                releaseDate = libro.releaseDate,
                bookName = libro.bookName
            };
        
                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    if (parametro == Data.Rows[i]["author"].ToString() || parametro == Data.Rows[i]["bookName"].ToString() || parametro == Data.Rows[i]["releaseDate"].ToString())
                    {
                        Data.Rows[i]["author"] = libro.author;
                        Data.Rows[i]["releaseDate"] = libro.releaseDate;
                        Data.Rows[i]["bookName"] = libro.bookName;
                        bandera = true;
                    }
                   
                }
                if (bandera)
                {
                    string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
                    File.WriteAllText("libros.json", json);


                    return "Se ha modificado el libro correctamente! \n" +
                            "Titulo: " + libro.bookName + "\n" +
                            "Autor: " + libro.author + "\n" +
                            "Fecha publicación: " + libro.releaseDate;
                }
                else
                {
                    return "El libro no existe";
                }
                
      
            }
            catch 
            {

                return "No se pudo completar la operacion con éxito, revisa los datos ingresados";
            }
        }
        //Método para la eliminacion de un libro
        public async Task<string> EliminaLibro(string parametro)
        {
            try { 
           
            
            string content =await File.ReadAllTextAsync(path);
            DataTable Data = (DataTable)JsonConvert.DeserializeObject(content, (typeof(DataTable)));
            string bookName="";
            bool bandera=false;
                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    if (parametro == Data.Rows[i]["author"].ToString() || parametro == Data.Rows[i]["bookName"].ToString() || parametro == Data.Rows[i]["releaseDate"].ToString())
                    {
                       bookName= Data.Rows[i]["bookName"].ToString();
                        Data.Rows.RemoveAt(i);
                        bandera = true;
                    }
                  
                }
                if (bandera)
                {
                    string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
                    File.WriteAllText("libros.json", json);
                    return $"El Libro {bookName} ha sido borrado satisfactoriamente!";
                }
                else
                {
                    return "El libro no existe";
                }

       
            }
            catch {
                return "No se pudo completar la operacion con éxito, revisa los datos ingresados";
            }
        }
        //Método para agregar un libro
        public async Task<string> AgregarLibro(Libro libro)
        {
            try
            {
                string content = await File.ReadAllTextAsync(path);
                DataTable Data = (DataTable)JsonConvert.DeserializeObject(content, (typeof(DataTable)));

                if (libro.author==null || libro.bookName==null || libro.releaseDate==null)
                {
                    return "Faltan datos para agregar el libro";
                }
                else
                {
                    Data.Rows.Add(new object[] { libro.bookName.ToString(), libro.author.ToString(), libro.releaseDate.ToString() });

                    string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
                    File.WriteAllText("libros.json", json);
                    return "Ha sido agregado con éxito el libro con la siguien información: " +
                            " Título: " + libro.bookName +
                            " Autor: " + libro.author +
                            " Fecha de publicación: " + libro.releaseDate;
                }
            }
               
                catch 
                {
                    return "No se pudo completar la operacion con éxito, revisa los datos ingresados";
                    
                }
            }

        }
    }

