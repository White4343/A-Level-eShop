using AutoMapper;
using Basket.API.Services;
using Microsoft.Extensions.Logging;


namespace Basket.UnitTests.Services
{
    public class BasketServiceTest
    {
        private readonly IBasketService _basketService;
        private readonly Mock<IBasketRepository> _basketRepositoryMock;
        private readonly Mock<IDbContextWrapper<AppDbContext>> _dbContextWrapperMock;
        private readonly Mock<ILogger<BasketService>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly API.Data.Entities.Basket _basket = new API.Data.Entities.Basket
        {
            Id = 1,
            CreatedAt = DateTime.Now,
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1,
            UserLogin = "test"
        };

        private readonly API.Models.Request.BasketCreateRequest _basketCreateRequest = new API.Models.Request.BasketCreateRequest
        {
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1
        };

        private readonly API.Models.Request.BasketUpdateRequest _basketUpdateRequest = new API.Models.Request.BasketUpdateRequest
        {
            Id = 1,
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1
        };

        private readonly API.Models.Dtos.BasketDto _basketDto = new API.Models.Dtos.BasketDto
        {
            Id = 1,
            CreatedAt = DateTime.Now,
            Quantity = 1,
            ItemPrice = 1,
            ItemId = 1
        };

        public BasketServiceTest()
        {
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _dbContextWrapperMock = new Mock<IDbContextWrapper<AppDbContext>>();
            _loggerMock = new Mock<ILogger<BasketService>>();
            _mapperMock = new Mock<IMapper>();

            _basketService = new BasketService(_basketRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateBasketAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.CreateBasketAsync(It.IsAny<API.Data.Entities.Basket>())).ReturnsAsync(_basketDto);

            // Act
            var result = await _basketService.CreateBasketAsync(_basketCreateRequest, "test");

            // Assert
            Assert.Equal(_basketDto, result);
        }

        [Fact]
        public async Task CreateBasketAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.CreateBasketAsync(It.IsAny<API.Data.Entities.Basket>())).ReturnsAsync((API.Models.Dtos.BasketDto)null);

            // Act
            var result = await _basketService.CreateBasketAsync(_basketCreateRequest, "test");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBasketsAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetBasketsAsync()).ReturnsAsync(new List<API.Models.Dtos.BasketDto>());

            // Act
            var result = await _basketService.GetBasketsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBasketsAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetBasketsAsync()).ReturnsAsync((IEnumerable<API.Models.Dtos.BasketDto>)null);

            // Act
            var result = await _basketService.GetBasketsAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBasketByLoginAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetBasketByLoginAsync(It.IsAny<String>())).ReturnsAsync(new List<API.Models.Dtos.BasketDto>());

            // Act
            var result = await _basketService.GetBasketByLoginAsync("test");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBasketByLoginAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetBasketByLoginAsync(It.IsAny<String>())).ReturnsAsync((IEnumerable<API.Models.Dtos.BasketDto>)null);

            // Act
            var result = await _basketService.GetBasketByLoginAsync("test");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBasketByIdAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetBasketByIdAsync(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(_basketDto);

            // Act
            var result = await _basketService.GetBasketByIdAsync(1, "test");

            // Assert
            Assert.Equal(_basketDto, result);
        }

        [Fact]
        public async Task GetBasketByIdAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetBasketByIdAsync(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync((API.Models.Dtos.BasketDto)null);

            // Act
            var result = await _basketService.GetBasketByIdAsync(1, "test");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBasketAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.UpdateBasketAsync(It.IsAny<API.Data.Entities.Basket>(), It.IsAny<String>())).ReturnsAsync(_basketDto);

            // Act
            var result = await _basketService.UpdateBasketAsync(_basketUpdateRequest, "test");

            // Assert
            Assert.Equal(_basketDto, result);
        }

        [Fact]
        public async Task UpdateBasketAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.UpdateBasketAsync(It.IsAny<API.Data.Entities.Basket>(), It.IsAny<String>())).ReturnsAsync((API.Models.Dtos.BasketDto)null);

            // Act
            var result = await _basketService.UpdateBasketAsync(_basketUpdateRequest, "test");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteBasketByIdAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.DeleteBasketByIdAsync(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(true);

            // Act
            var result = await _basketService.DeleteBasketByIdAsync(1, "test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBasketByIdAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.DeleteBasketByIdAsync(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(false);

            // Act
            var result = await _basketService.DeleteBasketByIdAsync(1, "test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteBasketByLoginAsync_Success()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.DeleteBasketByLoginAsync(It.IsAny<String>())).ReturnsAsync(true);

            // Act
            var result = await _basketService.DeleteBasketByLoginAsync("test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBasketByLoginAsync_Fail()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.DeleteBasketByLoginAsync(It.IsAny<String>())).ReturnsAsync(false);

            // Act
            var result = await _basketService.DeleteBasketByLoginAsync("test");

            // Assert
            Assert.False(result);
        }
    }
}
