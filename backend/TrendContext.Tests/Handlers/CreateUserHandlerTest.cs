using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Data;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Handlers;
using TrendContext.Domain.Repositories.Implementations;
using TrendContext.Domain.Repositories.Interfaces;
using TrendContext.Shared.Repository;

namespace TrendContext.Tests.Handlers
{
    [TestClass]
    public class CreateUserHandlerTest
    {
        private InMemoryAppContext appContext;
        private IUserRepository userRepository;
        private IUnitOfWork unitOfWork;
        private ILogger<CreateUserHandler> logger;


        private void InitialDependencies(string nomeBanco)
        {
            var options = new DbContextOptionsBuilder<InMemoryAppContext>()
               .UseInMemoryDatabase(databaseName: nomeBanco)
               .Options;

            appContext = new InMemoryAppContext(options);
            userRepository = new UserRepository(appContext);
            unitOfWork = new UnitOfWork(appContext);
            logger = new Logger<CreateUserHandler>(new LoggerFactory());

            InitialData.AddDefaultData(appContext);
        }

        [TestMethod]
        public async Task ShouldBeAbleToCreateUser()
        {
            InitialDependencies("ShouldBeAbleToCreateUser");

            var command = new CreateUserRequest
            {
                Name = "User Test",
                CPF = "00000000191",
            };

            var handler = new CreateUserHandler(userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == 201);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Payload != null);
        }


        [TestMethod]
        public async Task ShouldNotBeAbleToCreateUserCpfInvalid()
        {
            InitialDependencies("ShouldNotBeAbleToCreateUserCpfInvalid");

            var command = new CreateUserRequest
            {
                Name = "User Test",
                CPF = "00000000191987",
            };

            var handler = new CreateUserHandler(userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("CPF invalid."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToCreateSessionUserAlreadyExists()
        {
            InitialDependencies("ShouldNotBeAbleToCreateSessionUserAlreadyExists");

            var command = new CreateUserRequest
            {
                Name = "User Test",
                CPF = "69686332804",
            };

            var handler = new CreateUserHandler(userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("Already exists this CPF."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToCreateSessionUserNameEmpty()
        {
            InitialDependencies("ShouldNotBeAbleToCreateSessionUserNameEmpty");

            var command = new CreateUserRequest
            {
                Name = "",
                CPF = "69686332804",
            };

            var handler = new CreateUserHandler(userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("Name is required."));
            Assert.IsTrue(result.Payload == null);
        }
    }
}
