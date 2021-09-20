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
using TrendContext.Domain.Services;
using TrendContext.Shared.Repository;

namespace TrendContext.Tests.Handlers
{
    [TestClass]
    public class CreateSessionHandlerTest
    {
        private InMemoryAppContext appContext;
        private ITokenService tokenService;
        private IUserRepository userRepository;
        private ILogger<CreateSessionHandler> logger;


        private void InitialDependencies(string nomeBanco)
        {
            var options = new DbContextOptionsBuilder<InMemoryAppContext>()
               .UseInMemoryDatabase(databaseName: nomeBanco)
               .Options;

            appContext = new InMemoryAppContext(options);
            userRepository = new UserRepository(appContext);
            tokenService = new TokenService();
            logger = new Logger<CreateSessionHandler>(new LoggerFactory());

            InitialData.AddDefaultData(appContext);
        }

        [TestMethod]
        public async Task ShouldBeAbleToCreateSession()
        {
            InitialDependencies("ShouldBeAbleToCreateSession");

            var command = new CreateSessionRequest
            {
                CPF = "69686332804",
            };

            var handler = new CreateSessionHandler(userRepository, tokenService, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == 201);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Payload != null);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Payload.Token));
        }


        [TestMethod]
        public async Task ShouldNotBeAbleToCreateSessionCpfInvalid()
        {
            InitialDependencies("ShouldNotBeAbleToCreateSessionCpfInvalid");

            var command = new CreateSessionRequest
            {
                CPF = "69686332804987",
            };

            var handler = new CreateSessionHandler(userRepository, tokenService, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("CPF invalid."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToCreateSessionUserNotFound()
        {
            InitialDependencies("ShouldNotBeAbleToCreateSessionUserNotFound");

            var command = new CreateSessionRequest
            {
                CPF = "00000000191",
            };

            var handler = new CreateSessionHandler(userRepository, tokenService, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("User not found."));
            Assert.IsTrue(result.Payload == null);
        }
    }
}
