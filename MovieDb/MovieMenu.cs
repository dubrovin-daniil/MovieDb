using Microsoft.EntityFrameworkCore;
using MoviesDB;
using MoviesDB.Entities;

namespace MovieDb
{
    static public class MovieMenu
    {
        static public void ShowMenu(User user, AppDbContext db)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Movie Menu");
                Console.WriteLine("1. View All Titles");
                Console.WriteLine("2. Add New Title");
                Console.WriteLine("3. Delete Title");
                Console.WriteLine("4. Change Profile");
                Console.WriteLine("5. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        db.Titles.ToList().ForEach(t => Console.WriteLine($"{t.Id}: {t.Name} ({t.ReleaseDate:d}) - UserId: {t.UserId}\n{t.Description}"));

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Write("Enter title name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter release date (yyyy-mm-dd): ");
                        DateTime releaseDate = DateTime.Parse(Console.ReadLine());

                        try
                        {
                            db.Titles.Add(new Title
                            {
                                Name = name,
                                Description = description,
                                ReleaseDate = releaseDate,
                                User = user
                            });
                            db.SaveChanges();
                        }
                        catch (DbUpdateException Dbex)
                        {
                            Console.WriteLine("An error occurred while saving the title to the database: " + Dbex.Message);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            ShowMenu(user, db);
                        }

                        Console.WriteLine("Title added successfully! Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        db.Titles.ToList().ForEach(t => Console.WriteLine($"{t.Id}: {t.Name} ({t.ReleaseDate:d}) - UserId: {t.User}\n{t.Description}"));
                        Console.Write("\nEnter the ID of the title to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());

                        Title? titleToDelete = db.Titles.FirstOrDefault(t => t.Id == deleteId);
                        
                        if (titleToDelete.UserId == user.Id)
                        {
                            if (titleToDelete != null)
                            {
                                db.Titles.Remove(titleToDelete);
                                db.SaveChanges();
                                Console.WriteLine("Title deleted successfully! Press any key to continue.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Title not found. Press any key to continue.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can only delete your own titles. Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case "4":
                        Console.WriteLine("Change Profile:");
                        Console.WriteLine("1. Username");
                        Console.WriteLine("2. Password");
                        Console.WriteLine("3. Email");
                        Console.Write("Select an option: ");
                        string profileChoice = Console.ReadLine();

                        try 
                        {
                            switch (profileChoice)
                            {
                                case "1":
                                    Console.Write("Enter new username: ");
                                    user.Username = Console.ReadLine();
                                    db.Users.Update(user);
                                    db.SaveChanges();
                                    Console.WriteLine("Username updated successfully! Press any key to continue.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "2":
                                    Console.Write("Enter new password: ");
                                    user.Password = Console.ReadLine();
                                    db.Users.Update(user);
                                    db.SaveChanges();
                                    Console.WriteLine("Password updated successfully! Press any key to continue.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "3":
                                    Console.Write("Enter new email: ");
                                    user.Email = Console.ReadLine();
                                    db.Users.Update(user);
                                    db.SaveChanges();
                                    Console.WriteLine("Email updated successfully! Press any key to continue.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("Invalid option. Press any key to try again.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                            }
                        }
                        catch (DbUpdateException Dbex)
                        {
                            Console.WriteLine("An error occurred while saving the title to the database: " + Dbex.Message);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            ShowMenu(user, db);
                        }

                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exit...");
                        Task.Delay(3000).Wait();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
