using Catalog.API.Models;

namespace Catalog.UnitTests.Services
{
    public class BffItemServiceTest
    {
        private readonly IBffItemService _bffItemService;
        private readonly Mock<IBffItemRepository> _bffItemRepositoryMock;
        private readonly Mock<IDbContextWrapper<AppDbContext>> _dbContextWrapperMock;
        private readonly Mock<ILogger<BffItemService>> _loggerMock;

        public BffItemServiceTest()
        {
            _bffItemRepositoryMock = new Mock<IBffItemRepository>();
            _dbContextWrapperMock = new Mock<IDbContextWrapper<AppDbContext>>();
            _loggerMock = new Mock<ILogger<BffItemService>>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            _dbContextWrapperMock.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransactionMock.Object);

            _bffItemService = new BffItemService(_bffItemRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetByPageAsync_Success()
        {
            // Arrange
            _bffItemRepositoryMock.Setup(x => x.GetByPageAsync(It.IsAny<Int32>(), It.IsAny<Int32>(), It.IsAny<Int32?>(), It.IsAny<Int32?>())).ReturnsAsync(new PaginatedItems<Item>());

            // Act
            var result = await _bffItemService.GetByPageAsync(1, 1, null, null);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByPageAsync_Fail()
        {
            // Arrange
            _bffItemRepositoryMock.Setup(x => x.GetByPageAsync(It.IsAny<Int32>(), It.IsAny<Int32>(), It.IsAny<Int32?>(), It.IsAny<Int32?>())).ReturnsAsync((PaginatedItems<Item>)null);

            // Act
            var result = await _bffItemService.GetByPageAsync(1, 1, null, null);

            // Assert
            Assert.Null(result);
        }
    }
}
