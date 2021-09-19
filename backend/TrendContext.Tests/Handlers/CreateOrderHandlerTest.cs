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
    public class CreateOrderHandlerTest
    {
        private InMemoryAppContext appContext;

        private  IRepository<Order> orderRepository;
        private  ITrendRepository trendRepository;
        private  IUserRepository userRepository;
        private  IUnitOfWork unitOfWork;
        private  ILogger<CreateOrderHandler> logger;


        private void InitialDependencies(string nomeBanco)
        {
            var options = new DbContextOptionsBuilder<InMemoryAppContext>()
               .UseInMemoryDatabase(databaseName: nomeBanco)
               .Options;

            appContext = new InMemoryAppContext(options);
            orderRepository = new Repository<Order>(appContext);
            trendRepository = new TrendRepository(appContext);
            userRepository = new UserRepository(appContext);
            unitOfWork = new UnitOfWork(appContext);
            logger = new Logger<CreateOrderHandler>(new LoggerFactory());

            InitialData.AddDefaultData(appContext);
        }

        [TestMethod]
        public void ShouldBeAbleToCalculateTotalOrder()
        {
            var expect = 100M;
            var result = Order.CalculateTotalOrder(50, 2);

            Assert.IsTrue(expect == result);
            
            expect = 45.39M;

            result = Order.CalculateTotalOrder(15.13M, 3);

            Assert.IsTrue(expect == result);

        }

        [TestMethod]
        public async Task ShouldBeAbleToCreateOrder()
        {
            InitialDependencies("ShouldBeAbleToCreateOrder");

            var command = new CreateOrderRequest
            {
                CPF = "69686332804",
                Symbol = "PETR4",
                Amount = 4,
            };

            var handler = new CreateOrderHandler(orderRepository, trendRepository, userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.StatusCode == 201);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Payload != null);
        }

        [TestMethod]
        public async Task ShouldBeAbleNotCreateOrderCpfInvalid()
        {
            InitialDependencies("ShouldBeAbleNotCreateOrderCpfInvalid");
            var command = new CreateOrderRequest
            {
                CPF = "69686332804321548",
                Symbol = "PETR4",
                Amount = 4,
            };

            var handler = new CreateOrderHandler(orderRepository, trendRepository, userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("CPF invalid."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldBeAbleNotCreateOrderTrendInvalid()
        {
            InitialDependencies("ShouldBeAbleNotCreateOrderTrendInvalid");
            var command = new CreateOrderRequest
            {
                CPF = "69686332804",
                Symbol = "PETR4123",
                Amount = 4,
            };

            var handler = new CreateOrderHandler(orderRepository, trendRepository, userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 404);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Trend not found."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldBeAbleNotCreateOrderUserNotFound()
        {
            InitialDependencies("ShouldBeAbleNotCreateOrderUserNotFound");
            var command = new CreateOrderRequest
            {
                CPF = "00000000191",
                Symbol = "PETR4",
                Amount = 4,
            };

            var handler = new CreateOrderHandler(orderRepository, trendRepository, userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 404);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("User not found."));
            Assert.IsTrue(result.Payload == null);
        }

        [TestMethod]
        public async Task ShouldBeAbleNotCreateOrderAmountZero()
        {
            InitialDependencies("ShouldBeAbleNotCreateOrderAmountZero");
            var command = new CreateOrderRequest
            {
                CPF = "69686332804",
                Symbol = "PETR4",
                Amount = 0,
            };

            var handler = new CreateOrderHandler(orderRepository, trendRepository, userRepository, unitOfWork, logger);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.IsTrue(result != null);
            Assert.IsTrue(!result.Success);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Amount is invalid."));
            Assert.IsTrue(result.Payload == null);
        }
    }
}
