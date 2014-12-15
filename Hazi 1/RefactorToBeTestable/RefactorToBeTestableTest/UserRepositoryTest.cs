using Moq;
using NUnit.Framework;
using RefactorToBeTestable.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;
using WebShopApiTest.MyFakes;

namespace RefactorToBeTestableTest
{
    [TestFixture]
    class UserRepositoryTest
    {

        Mock<DbSet<User>> mockSet;
        Mock<ShopContext> mockContext;

        IUserRepository uRepo;

        IQueryable<User> usersQ;
        List<User> usersL;

        User newUser;
        User oldUser;


        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitTestData();
        }


        [SetUp]
        public void SetUp()
        {
            Arrange();
        }

        [Test]
        public void Test_Create_Should_CreateANewUserInContext_When_EverithingIsAwsome()
        {

            // Act
            uRepo.Create(newUser);
            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        [ExpectedException(
            ExpectedException = typeof(ArgumentException),
            ExpectedMessage = "The given email address is already used by another customer")
        ]
        public void Test_Create_Moked_Should_ThrowArgumentException_When_TheGivenUserIsAlredyExists()
        {
            //Itt most a moq-t használtam

            // Act
            uRepo.Create(oldUser);

        }

        [Test]
        [ExpectedException(
            ExpectedException = typeof(ArgumentException),
            ExpectedMessage = "The given email address is already used by another customer")
        ]
        public void Test_Create_Faked_Should_ThrowArgumentException_When_TheGivenUserIsAlredyExists()
        {
            //Itt most egy saját faket használtam

            //Arrange
            uRepo = new UserRepository(new FakeShopContext());

            // Act
            uRepo.Create(usersL[0]);
            uRepo.Create(usersL[0]);
        }


        void Arrange()
        {
            mockSet = new Mock<DbSet<User>>();
            mockContext = new Mock<ShopContext>();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(usersQ.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(usersQ.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(usersQ.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(usersQ.GetEnumerator());

            uRepo = new UserRepository(mockContext.Object);
        }



        void InitTestData()
        {
            usersQ = new List<User>
            { 
                new User() { Name = "Pista", Email = "lovasistvan@outlook.com" }
            }.AsQueryable();
            

            newUser = new User() { Name = "Pityu", Email = "lovas_istvan@outlook.com" };
            oldUser = new User() { Name = "Pista", Email = "lovasistvan@outlook.com" };

            usersL = usersQ.ToList();
        }
    }
}
