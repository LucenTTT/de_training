using de_training.Exceptions;
using de_training.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace de_training.UnitTests
{
    [TestClass]
    public class AuthorizationServiceTests
    {
        private readonly DbContextOptions<libraryContext> _contextOptions;

        public AuthorizationServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<libraryContext>()
                .UseInMemoryDatabase("AuthenticationServiceTests")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            using var context = new libraryContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(new Reader { Pib = "Kot O" },
                             new Book { Title = "Kobzar", ReaderId = 1 },
                             new User { Name = "user1", Password = "qwe"});
            context.SaveChanges();
        }

    [TestMethod]
    public void RegistrateUserAsync_UserExists_ThrowException()
    {
            //Arange
            var context = new libraryContext(_contextOptions);
            var configurationMock = new Mock<IConfiguration>();
            var authenticationService = new AuthenticationService(context, configurationMock.Object);

            //Act
            //Assert
            Assert.ThrowsExceptionAsync<UserAlreadyExistsException>(async () => await authenticationService.RegistrateUserAsync("user1", "qwe"));
        }

        [TestMethod]
        public async Task RegistrateUserAsync_UserDoesntExist_ResultNotNull()
        {
            //Arrange 
            var context = new libraryContext(_contextOptions);
            var configurationMock = new Mock<IConfiguration>();
            var authenticationService = new AuthenticationService(context, configurationMock.Object);

            //Act
            await authenticationService.RegistrateUserAsync("user2", "qwe");
            var findedUser = await context.Users.FirstOrDefaultAsync(x => x.Name.Equals("user2"));

            //Assert
            Assert.IsNotNull(findedUser);
        }

        [TestMethod]
        [DataRow("user0","qwe")]
        [DataRow("user1","qw")]
        public void AuthenticateUserAsync_UserNotFoundPasswordIsWrong_ThrowException(string username, string password)
        {
            //Arrange 
            var context = new libraryContext(_contextOptions);
            var configurationMock = new Mock<IConfiguration>();
            var authenticationService = new AuthenticationService(context, configurationMock.Object);

            Assert.ThrowsExceptionAsync<UserNotFoundException>(async () => await authenticationService.AuthenticateUserAsync(username, password));
        }

        [TestMethod]
        public async Task AuthenticateUserAsync_PasswordIsCorrect_ResultNotNull()
        {
            var context = new libraryContext(_contextOptions);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(x => x["JWT:Key"]).Returns("fdfskjflkdsjfldsjflksdjklfdsfjsdklfjsdlkfdslkfsd");
            var authenticateService = new AuthenticationService(context, configurationMock.Object);

            //Act
            var result = await authenticateService.AuthenticateUserAsync("user1", "qwe");

            Assert.IsNotNull(result);
            configurationMock.VerifyGet(x => x["JWT:Key"], Times.Once());
        }

    }
}