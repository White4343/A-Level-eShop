namespace Order.UnitTests.Services
{
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ILogger<OrderService>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly API.Data.Entities.Order _order = new API.Data.Entities.Order
        {
            Id = 1,
            Status = "test",
            CreatedAt = DateTime.Now,
            TotalPrice = 1,
            UserLogin = "test"
        };

        public OrderServiceTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _loggerMock = new Mock<ILogger<OrderService>>();
            _mapperMock = new Mock<IMapper>();

            _orderService = new OrderService(_orderRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_Success()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.CreateOrderAsync(It.IsAny<API.Data.Entities.Order>())).ReturnsAsync(_order);

            // Act
            var result = await _orderService.CreateOrderAsync("test", 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_order.Id, result.Id);
            Assert.Equal(_order.Status, result.Status);
            Assert.Equal(_order.CreatedAt, result.CreatedAt);
            Assert.Equal(_order.TotalPrice, result.TotalPrice);
            Assert.Equal(_order.UserLogin, result.UserLogin);
        }

        [Fact]
        public async Task CreateOrderAsync_Failed()
        { 
            // Arrange
            _orderRepositoryMock.Setup(x => x.CreateOrderAsync(It.IsAny<API.Data.Entities.Order>())).ThrowsAsync(new Exception());

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _orderService.CreateOrderAsync("test", 1));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetOrderByIdAsync_Success()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_order);

            // Act
            var result = await _orderService.GetOrderByIdAsync(1, "test");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_order.Id, result.Id);
            Assert.Equal(_order.Status, result.Status);
            Assert.Equal(_order.CreatedAt, result.CreatedAt);
            Assert.Equal(_order.TotalPrice, result.TotalPrice);
            Assert.Equal(_order.UserLogin, result.UserLogin);
        }

        [Fact]
        public async Task GetOrderByIdAsync_Failed()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _orderService.GetOrderByIdAsync(1, "test"));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetOrdersAsync_Success()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrdersAsync()).ReturnsAsync(new List<API.Data.Entities.Order> { _order });

            // Act
            var result = await _orderService.GetOrdersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(_order.Id, result.First().Id);
            Assert.Equal(_order.Status, result.First().Status);
            Assert.Equal(_order.CreatedAt, result.First().CreatedAt);
            Assert.Equal(_order.TotalPrice, result.First().TotalPrice);
            Assert.Equal(_order.UserLogin, result.First().UserLogin);
        }

        [Fact]
        public async Task GetOrdersAsync_Failed()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrdersAsync()).ThrowsAsync(new Exception());

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _orderService.GetOrdersAsync());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetOrdersByUserAsync_Success()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrdersByUserAsync(It.IsAny<string>())).ReturnsAsync(new List<API.Data.Entities.Order> { _order });

            // Act
            var result = await _orderService.GetOrdersByUserAsync("test");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(_order.Id, result.First().Id);
            Assert.Equal(_order.Status, result.First().Status);
            Assert.Equal(_order.CreatedAt, result.First().CreatedAt);
            Assert.Equal(_order.TotalPrice, result.First().TotalPrice);
            Assert.Equal(_order.UserLogin, result.First().UserLogin);
        }

        [Fact]
        public async Task GetOrdersByUserAsync_Failed()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrdersByUserAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _orderService.GetOrdersByUserAsync("test"));

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateOrderAsync_Success()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_order);
            _orderRepositoryMock.Setup(x => x.UpdateOrderAsync(It.IsAny<API.Data.Entities.Order>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _orderService.UpdateOrderAsync(_order, "test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateOrderAsync_Failed()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_order);
            _orderRepositoryMock.Setup(x => x.UpdateOrderAsync(It.IsAny<API.Data.Entities.Order>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _orderService.UpdateOrderAsync(_order, "test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateOrderAsync_Failed_OrderNotFound()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((API.Data.Entities.Order)null);

            // Act
            var result = await _orderService.UpdateOrderAsync(_order, "test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteOrderAsync_Success()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.DeleteOrderAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _orderService.DeleteOrderAsync(1, "test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOrderAsync_Failed()
        {
            // Arrange
            _orderRepositoryMock.Setup(x => x.DeleteOrderAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _orderService.DeleteOrderAsync(1, "test");

            // Assert
            Assert.False(result);
        }
    }
}