namespace Catalog.UnitTests.Services
{
    public class ItemServiceTest
    {
        private readonly IItemService _itemService;
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly Mock<IDbContextWrapper<AppDbContext>> _dbContextWrapperMock;
        private readonly Mock<ILogger<ItemService>> _loggerMock;

        private readonly Item _testItem = new Item
        {
            Name = "Test Item",
            Description = "Test Description",
            Price = 1.99m,
            BrandId = 1,
            TypeId = 1
        };

        public ItemServiceTest()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            _dbContextWrapperMock = new Mock<IDbContextWrapper<AppDbContext>>();
            _loggerMock = new Mock<ILogger<ItemService>>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            _dbContextWrapperMock.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransactionMock.Object);

            _itemService = new ItemService(_itemRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateItemAsync_Success()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.CreateItemAsync(It.IsAny<Item>())).ReturnsAsync(_testItem);

            // Act
            var result = await _itemService.CreateItemAsync(_testItem);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateItemAsync_Fail()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.CreateItemAsync(It.IsAny<Item>())).ReturnsAsync((Item)null);

            // Act
            var result = await _itemService.CreateItemAsync(_testItem);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetItemByIdAsync_Success()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.GetItemByIdAsync(It.IsAny<Int32>())).ReturnsAsync(_testItem);

            // Act
            var result = await _itemService.GetItemByIdAsync(_testItem.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetItemByIdAsync_Fail()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.GetItemByIdAsync(It.IsAny<Int32>())).ReturnsAsync((Item)null);

            // Act
            var result = await _itemService.GetItemByIdAsync(_testItem.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetItemsAsync_Success()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.GetItemsAsync()).ReturnsAsync(new List<Item> { _testItem });

            // Act
            var result = await _itemService.GetItemsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetItemsAsyncFail()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.GetItemsAsync()).ReturnsAsync((IEnumerable<Item>)null);

            // Act
            var result = await _itemService.GetItemsAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateItemAsync_Success()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.UpdateItemAsync(It.IsAny<Item>())).ReturnsAsync(_testItem);

            // Act
            var result = await _itemService.UpdateItemAsync(_testItem);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateItemAsync_Fail()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.UpdateItemAsync(It.IsAny<Item>())).ReturnsAsync((Item)null);

            // Act
            var result = await _itemService.UpdateItemAsync(_testItem);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteItemAsync_Success()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.DeleteItemAsync(It.IsAny<Int32>())).ReturnsAsync(true);

            // Act
            var result = await _itemService.DeleteItemAsync(_testItem.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteItemAsync_Fail()
        {
            // Arrange
            _itemRepositoryMock.Setup(x => x.DeleteItemAsync(It.IsAny<Int32>())).ReturnsAsync(false);

            // Act
            var result = await _itemService.DeleteItemAsync(_testItem.Id);

            // Assert
            Assert.False(result);
        }
    }
}
