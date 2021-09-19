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
    public class CreateTrendHandlerTest
    {
        private InMemoryAppContext appContext;
        private  ITrendRepository trendRepository;
        private  IUnitOfWork unitOfWork;
        private  ILogger<CreateTrendHandler> logger;


        private void InitialDependencies(string nomeBanco)
        {
            var options = new DbContextOptionsBuilder<InMemoryAppContext>()
               .UseInMemoryDatabase(databaseName: nomeBanco)
               .Options;

            appContext = new InMemoryAppContext(options);
            trendRepository = new TrendRepository(appContext);
            unitOfWork = new UnitOfWork(appContext);
            logger = new Logger<CreateTrendHandler>(new LoggerFactory());

            InitialData.AddDefaultData(appContext);
        }

        [TestMethod]
        public async Task ShouldBeAbleToCreateTrend()
        {
            InitialDependencies("ShouldBeAbleToCreateTrend");

            var command = new CreateTrendRequest
            {
                Symbol = "PETR3",
                CurrentPrice = 36.15M,
            };

            var handler = new CreateTrendHandler(trendRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == 201);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Payload != null);
        }


        [TestMethod]
        public async Task ShouldNotBeAbleToCreateTrendEmpty()
        {
            InitialDependencies("ShouldNotBeAbleToCreateTrendEmpty");

            var command = new CreateTrendRequest
            {
                Symbol = string.Empty,
                CurrentPrice = 36.15M,
            };

            var handler = new CreateTrendHandler(trendRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("Symbol is required."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToCreateTrendInvalid()
        {
            InitialDependencies("ShouldNotBeAbleToCreateTrendInvalid");

            var command = new CreateTrendRequest
            {
                Symbol = "PETR378978789",
                CurrentPrice = 36.15M,
            };

            var handler = new CreateTrendHandler(trendRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("Symbol is invalid."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToCreateTrendExists()
        {
            InitialDependencies("ShouldNotBeAbleToCreateTrendExists");

            var command = new CreateTrendRequest
            {
                Symbol = "PETR4",
                CurrentPrice = 36.15M,
            };

            var handler = new CreateTrendHandler(trendRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("Already exists this Trend."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToCreateCurrentPriceZero()
        {
            InitialDependencies("ShouldNotBeAbleToCreateCurrentPriceZero");

            var command = new CreateTrendRequest
            {
                Symbol = "PETR3",
                CurrentPrice = 0M,
            };

            var handler = new CreateTrendHandler(trendRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message.Contains("CurrentPrice is invalid."));
            Assert.IsTrue(result.Payload == null);
        }
    }
}
