using EjercicioNet.Models;

namespace EjercicioNet.Services
{
    public class UserServices : IUser
    {
        List<User> users = new List<User>()

        {
            new User(){Email="BasicPass@hotmail.com",Password="Prueba"}
        };

        public bool IsUser(string Email, string Password)=>users.Where(d=>d.Email==Email && d.Password==Password).Count()>0;
    }
}
