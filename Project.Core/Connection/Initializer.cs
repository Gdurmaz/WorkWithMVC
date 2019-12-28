using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Project.Core.Entities;

namespace Project.Core.Connection
{
    public class Initializer:CreateDatabaseIfNotExists<ConnectionDb>
    {
        //Adding Fake Data Seed...
        protected override void Seed(ConnectionDb context)
        {
            BlogUser admin = new BlogUser()
            {
                Name = "adminn",
                Surname = "Manager",
                Username = "Admin",
                Password = "12345",
                Email = "admin@hot",
                BirthOfDay = Convert.ToDateTime("18.10.1970"),
                IsActive = true,
                IsAdmin = true,
                ActivateGuid = Guid.NewGuid(),
                ProfileImageFilename = "user-icon.png",//inside the content file, under the image file... 
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUsername="System",
            };
            BlogUser normal = new BlogUser()
            {
                Name = "normall",
                Surname = "user",
                Username = "normal",
                Password = "123aa",
                Email = "normal@hot",
                BirthOfDay = Convert.ToDateTime("18.10.1970"),
                IsActive = true,
                IsAdmin = false,
                ActivateGuid = Guid.NewGuid(),
                ProfileImageFilename = "user-icon.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = admin.Username,
            };
            context.TableUser.Add(admin);
            context.TableUser.Add(normal);
            for (int i = 0; i < 8; i++)
            {
                BlogUser user = new BlogUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFilename = "user-icon.png",
                    ActivateGuid = Guid.NewGuid(),
                    BirthOfDay=FakeData.DateTimeData.GetDatetime(Convert.ToDateTime("01.01.1940"), Convert.ToDateTime("01.01.2000")),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = FakeData.NumberData.GetNumber(100000,9999999).ToString(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };
                context.TableUser.Add(user);
            }
            context.SaveChanges();
            // User list for using..
            List<BlogUser> userlist = context.TableUser.ToList();
            // Adding fake categories..
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = admin.Name
                };
                context.TableCategory.Add(cat);
                // Adding fake data notes..
                for (int k = 0; k < FakeData.NumberData.GetNumber(3, 7); k++)
                {
                    BlogUser user = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 20)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        BlogUser = user,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = user.Username,
                    };
                    cat.Notes.Add(note);
                    // Adding fake data comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        BlogUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            BlogUser = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username
                        };
                        note.Comments.Add(comment);
                    }
                    // Adding fake data likes..
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Like liked = new Like()
                        {
                            BlogUser = userlist[m]
                        };
                        note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
