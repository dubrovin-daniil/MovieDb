using Microsoft.EntityFrameworkCore;
using MoviesDB;
using MoviesDB.Entities;

namespace MovieDb
{
    static public class SignIn
    {
        static public User SignInUser(AppDbContext db)
        {
            Console.WriteLine("Sign In:");
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "User" + new Random().Next().ToString();
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? username + "@gmail.com";
            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "0000";

            var users = db.Users.ToList();
            if (users.Any(u => u.Username == username))
            {
                Console.WriteLine("Username already exists. Please try again.");
                Console.ReadKey();
                Console.Clear();
                return SignInUser(db);
            }
            if (users.Any(u => u.Email == email))
            {
                Console.WriteLine("Email already exists. Please try again.");
                Console.ReadKey();
                Console.Clear();
                return SignInUser(db);
            }

            try 
            {
                db.Users.Add(new User
                {
                    Username = username,
                    Email = email,
                    Password = password
                });
                db.SaveChanges();
            }
            catch (DbUpdateException Dbex)
            {
                Console.WriteLine("An error occurred while saving the user to the database: " + Dbex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return SignInUser(db);
            }

            return new User { Username = username, Password = password, Email = email };
        }
    }
}
