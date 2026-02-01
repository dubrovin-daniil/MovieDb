using MoviesDB;
using MoviesDB.Entities;

namespace MovieDb
{
    static public class SignUp
    {
        static public User SignUpUser(AppDbContext db)
        {
            Console.WriteLine("Sign Up:");
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();
            
            var users = db.Users.ToList();
            if (users.Any(u => u.Username == username))
            {
                if (users.Any(u => u.Password == password))
                {
                    Console.WriteLine("Sucseccfully signed up! Wait...");
                    Task.Delay(5000).Wait();
                    return users.FirstOrDefault(u => u.Username == username);
                }
                else
                {
                    Console.WriteLine("Wrong password! Try again.");
                    Console.ReadKey();
                    Console.Clear();
                    return SignUpUser(db);
                }
            }
            else
            {
                Console.WriteLine("User does not exist!");
                Console.ReadKey();
                Console.Clear();
                return SignUpUser(db);
            }
        }
    }
}
