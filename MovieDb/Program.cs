using Microsoft.EntityFrameworkCore;
using MovieDb;
using MoviesDB.Entities;

namespace MoviesDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppDbContext db = new AppDbContext();

            while (true)
            {
                Console.WriteLine("MENU");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. Sign up");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        User newUser = SignIn.SignInUser(db);
                        MovieMenu.ShowMenu(newUser, db);
                        break;
                    case "2":
                        User user = SignUp.SignUpUser(db);
                        MovieMenu.ShowMenu(user, db);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
