using Microsoft.EntityFrameworkCore;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.DAL.Test
{
    /// <summary>
    /// Test in memory
    /// </summary>
    [TestFixture]
    public class TestRepository
    {
        [Test]
        public async Task AddAsync_ReturnAnID()
        {
            //Arrange
            var user = new UserModel
            {
                Avatar = "http://example.com/01.jpg",
                AccountID = 1,
                Address = "Quan 1, TPHCM",
                Email = "nva@example.com",
                Name = "Nguyen Van A",
                Salary = 2000000,
            };

            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "AddAsync_ReturnAnID")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();

            //Act
            await repo.AddAsync(user);

            //Assert
            Assert.AreNotEqual(0, user.ID, "add method does not return ID");
        }

        [Test]
        public async Task GetByIdAsync_ReturnNotNull()
        {
            //Arrange
            var user = new UserModel
            {
                Avatar = "http://example.com/01.jpg",
                AccountID = 1,
                Address = "Quan 1, TPHCM",
                Email = "nva@example.com",
                Name = "Nguyen Van A",
                Salary = 2000000,
            };
            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnNotNull")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();
            context.Set<UserModel>().Add(user);
            context.SaveChanges();

            //Act
            var result = await repo.GetByIdAsync<UserModel>(user.ID);

            //Assert
            Assert.IsNotNull(result, "GetByIdAsync() method return NULL");
        }



        [Test]
        public async Task GetByIdAsync_ReturnSameObject()
        {
            //Arrange
            var user = new UserModel
            {
                Avatar = "http://example.com/01.jpg",
                AccountID = 1,
                Address = "Quan 1, TPHCM",
                Email = "nva@example.com",
                Name = "Nguyen Van A",
                Salary = 2000000,
            };
            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnSameObject")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();
            context.Set<UserModel>().Add(user);
            context.SaveChanges();

            //Act
            var result = await repo.GetByIdAsync<UserModel>(user.ID);

            //Assert
            Assert.AreEqual(user.Name, result.Name, "a property of result object is not equal old one");
        }

        [Test]
        public void Update_ObjectIsUpdated()
        {
            //Arrange
            var user = new UserModel
            {
                Avatar = "http://example.com/01.jpg",
                AccountID = 1,
                Address = "Quan 1, TPHCM",
                Email = "nva@example.com",
                Name = "Nguyen Van A",
                Salary = 2000000,
            };
            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "Update_ObjectIsUpdated")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();
            context.Set<UserModel>().Add(user);
            context.SaveChanges();

            var edited = new UserModel
            {
                ID = 1,
                Avatar = "http://example.com/02.jpg",
                AccountID = 2,
                Address = "Quan 2, TPHCM",
                Email = "ntb@example.com",
                Name = "Nguyen Thi B",
                Salary = 1500000,
            };

            //Act
            repo.Update(edited);
            var updated = context.Set<UserModel>().Find(1);

            //Assert
            Assert.AreEqual(edited.Name, updated.Name, "Object is not updated");
        }

        [Test]
        public void DeleteAsync_ObjectIsDeleted()
        {
            //Arrange
            var user = new UserModel
            {
                Avatar = "http://example.com/01.jpg",
                AccountID = 1,
                Address = "Quan 1, TPHCM",
                Email = "nva@example.com",
                Name = "Nguyen Van A",
                Salary = 2000000,
            };
            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "DeleteAsync_ObjectIsDeleted")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();
            context.Set<UserModel>().Add(user);
            context.SaveChanges();

            //Act
            repo.Delete(user);
            var deleted = context.Set<UserModel>().Find(1);

            //Assert
            Assert.IsNull(deleted, "Object is not deleted");

        }

        [Test]
        public async Task GetAllAsync_WithoutParams_ReturnSameObjects()
        {
            //Arrange
            var user1 = new UserModel
            {
                Avatar = "http://example.com/01.jpg",
                AccountID = 1,
                Address = "Quan 1, TPHCM",
                Email = "nva@example.com",
                Name = "Nguyen Van A",
                Salary = 2000000,
            };
            var user2 = new UserModel
            {
                Avatar = "http://example.com/02.jpg",
                AccountID = 2,
                Address = "Quan 2, TPHCM",
                Email = "ntb@example.com",
                Name = "Nguyen Thi B",
                Salary = 1500000,
            };

            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "GetAllAsync_WithoutParams_ReturnSameObjects")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();
            context.Set<UserModel>().AddRange(user1, user2);
            context.SaveChanges();

            //Act
            var results = await repo.GetAllAsync<UserModel>();

            //Assert
            Assert.IsTrue(results.Contains(user1) && results.Contains(user2), "Did not find any object");

        }

        [Test]
        public async Task GetAllAsync_WithParams_ReturnSpecifiedObjects()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<NNiceContext>()
               .UseInMemoryDatabase(databaseName: "GetAllAsync_WithParams_ReturnSpecifiedObjects")
               .Options;
            var context = new NNiceContext(options);
            var repo = new Repository<NNiceContext>(context);
            context.Database.EnsureDeleted();
            var userList = new List<UserModel>();
            for (int i = 0; i < 6; i++)
            {
                var user = new UserModel() { Name = i.ToString() + "Object" };// 0 1 2 3 4 5
                userList.Add(user);
            }
            context.Set<UserModel>().AddRange(userList);
            context.SaveChanges();

            //Act
            var results = await repo.GetAllAsync<UserModel>(
                filter: x => !x.Name.StartsWith("5"),//remove 6th object => 0 1 2 3 4
                orderBy: x => x.OrderByDescending(e => e.Name), //reverse => 4 3 2 1 0
                skip: 2, //skip => 2 1 0
                take: 1); //take => 2

            //Assert
            Assert.IsTrue(results.Count() == 1 && results.ElementAt(0).Name == "2Object", "Result object is not specified one");

        }
    }
}
