using Order.API.Models.Dtos;

namespace Order.UnitTests.Services
{
    public class OrderBasketServiceTest
    {
        private readonly IOrderBasketService _orderBasketService;
        private readonly Mock<IOrderBasketRepository> _orderBasketRepositoryMock;
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly Mock<IBasketService> _basketServiceMock;
        private readonly Mock<ILogger<OrderBasketService>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly API.Data.Entities.OrderBasket _orderBasket = new API.Data.Entities.OrderBasket
        {
            Id = 1,
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1,
            OrderId = 1
        };

        private readonly API.Models.Request.CreateOrderBasketRequest _createOrderBasketRequest = new API.Models.Request.CreateOrderBasketRequest
        {
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1
        };

        private readonly OrderBasketDto _orderBasketDto = new OrderBasketDto
        {
            Id = 1,
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1,
            OrderId = 1
        };

        public OrderBasketServiceTest()
        {
            _orderBasketRepositoryMock = new Mock<IOrderBasketRepository>();
            _orderServiceMock = new Mock<IOrderService>();
            _basketServiceMock = new Mock<IBasketService>();
            _loggerMock = new Mock<ILogger<OrderBasketService>>();
            _mapperMock = new Mock<IMapper>();

            _orderBasketService = new OrderBasketService(_orderBasketRepositoryMock.Object, _orderServiceMock.Object, _basketServiceMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetOrderBasketByIdAsync_Success()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.GetOrderBasketByIdAsync(It.IsAny<int>())).ReturnsAsync(_orderBasketDto);

            // Act
            var result = await _orderBasketService.GetOrderBasketByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_orderBasket.Id, result.Id);
            Assert.Equal(_orderBasket.Quantity, result.Quantity);
            Assert.Equal(_orderBasket.ItemPrice, result.ItemPrice);
            Assert.Equal(_orderBasket.ItemId, result.ItemId);
            Assert.Equal(_orderBasket.OrderId, result.OrderId);
        }

        [Fact]
        public async Task GetOrderBasketByIdAsync_Failed()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.GetOrderBasketByIdAsync(It.IsAny<int>())).ReturnsAsync((OrderBasketDto)null);

            // Act
            var result = await _orderBasketService.GetOrderBasketByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrdersBasketsAsync_Success()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.GetOrdersBasketAsync()).ReturnsAsync(new List<OrderBasketDto> { _orderBasketDto });

            // Act
            var result = await _orderBasketService.GetOrdersBasketAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetOrdersBasketsAsync_Failed()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.GetOrdersBasketAsync()).ReturnsAsync((List<OrderBasketDto>)null);

            // Act
            var result = await _orderBasketService.GetOrdersBasketAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateOrderBasketAsync_Success()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.UpdateOrderBasketAsync(It.IsAny<OrderBasketDto>())).ReturnsAsync(true);

            // Act
            var result = await _orderBasketService.UpdateOrderBasketAsync(_orderBasketDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateOrderBasketAsync_Failed()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.UpdateOrderBasketAsync(It.IsAny<OrderBasketDto>())).ReturnsAsync(false);

            // Act
            var result = await _orderBasketService.UpdateOrderBasketAsync(_orderBasketDto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteOrderBasketAsync_Success()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.DeleteOrderBasketAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _orderBasketService.DeleteOrderBasketAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOrderBasketAsync_Failed()
        {
            // Arrange
            _orderBasketRepositoryMock.Setup(x => x.DeleteOrderBasketAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _orderBasketService.DeleteOrderBasketAsync(1);

            // Assert
            Assert.False(result);
        }
    }
}