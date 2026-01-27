using Microsoft.EntityFrameworkCore;
using MoviesDB.Entities;

namespace MoviesDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppDbContext db = new AppDbContext();

            Title title1 = new Title
            {
                Name = "Inception",
                DurationInMinutes = 148
            };
            Title title2 = new Title
            {
                Name = "The Matrix",
                DurationInMinutes = 136
            };
            Title title3 = new Title
            {
                Name = "Interstellar",
                DurationInMinutes = 169
            };
            db.Titles.Add(title1);
            db.Titles.Add(title2);
            db.Titles.Add(title3);

            while (true)
            {
                Console.WriteLine("-----------MENU-------------");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Registration new user");
                Console.WriteLine("2 - View all users info");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        db.Add(new User { Name = name, Username = username, Password = password });
                        db.SaveChanges();
                        break;
                    case "2":
                        Console.WriteLine("Users in database:");

                        foreach (var user in db.Users)
                        {
                            Console.WriteLine($"Name: {user.Name}, Username: {user.Username}, Password: {user.Password}");
                        }
                        break;

                }
                Console.WriteLine();
            }
        }
    }
}
