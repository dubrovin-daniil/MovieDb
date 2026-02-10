using MoviesDB;
using MoviesDB.Entities;

namespace MyTestsApp
{
    public class UserTitleTest
    {
        [Test]
        public void AddUserAndTitle_ShouldSaveToDatabase()
        {
            
            using (var db = new AppDbContext())
            {
                // ARRANGE
                var user = new User() { Username = "TestUser", Email = "test@gmail.com", Password = "1234" };

                // ACT
                db.Users.Add(user);
                db.SaveChanges();

                // ARRANGE
                var title = new Title() { Name = "TestTitle", ReleaseDate = DateTime.Now, Description = "TEST", AddedDay = DateTime.Now, UserId = user.Id };

                // ACT
                db.Titles.Add(title);
                db.SaveChanges();
            }

            using (var db = new AppDbContext())
            {
                // ASSERT
                var User = db.Users.FirstOrDefault(u => u.Username == "TestUser");
                var Title = db.Titles.FirstOrDefault(t => t.Name == "TestTitle");
                Assert.That(User, Is.Not.Null);
                Assert.That(Title, Is.Not.Null);

                Assert.That(Title.Name, Is.EqualTo("TestTitle"));
                Assert.That(User.Username, Is.EqualTo("TestUser"));

                Assert.That(Title.UserId, Is.EqualTo(User.Id));
            }
        }
    }
}
