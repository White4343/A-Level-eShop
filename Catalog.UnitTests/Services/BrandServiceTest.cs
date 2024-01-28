namespace Catalog.UnitTests.Services
{
    public class BrandServiceTest
    {
        private readonly IBrandService _brandService;
        private readonly Mock<IBrandRepository> _brandRepositoryMock;
        private readonly Mock<IDbContextWrapper<AppDbContext>> _dbContextWrapperMock;
        private readonly Mock<ILogger<BrandService>> _loggerMock;

        private readonly Brand _testBrand = new Brand
        {
            Name = "Test Brand"
        };

        public BrandServiceTest()
        {
            _brandRepositoryMock = new Mock<IBrandRepository>();
            _dbContextWrapperMock = new Mock<IDbContextWrapper<AppDbContext>>();
            _loggerMock = new Mock<ILogger<BrandService>>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            _dbContextWrapperMock.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransactionMock.Object);

            _brandService = new BrandService(_brandRepositoryMock.Object, _loggerMock.Object);
        }
        
        [Fact]
        public async Task CreateBrandAsync_Success()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.CreateBrandAsync(It.IsAny<Brand>())).ReturnsAsync(_testBrand);

            // Act
            var result = await _brandService.CreateBrandAsync(_testBrand);

            // Assert
            Assert.Equal(_testBrand, result);
        }

        [Fact]
        public async Task CreateBrandAsync_Fail()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.CreateBrandAsync(It.IsAny<Brand>())).ReturnsAsync((Brand)null);

            // Act
            var result = await _brandService.CreateBrandAsync(_testBrand);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBrandByIdAsync_Success()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.GetBrandByIdAsync(It.IsAny<Int32>())).ReturnsAsync(_testBrand);

            // Act
            var result = await _brandService.GetBrandByIdAsync(_testBrand.Id);

            // Assert
            Assert.Equal(_testBrand, result);
        }

        [Fact]
        public async Task GetBrandByIdAsync_Fail()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.GetBrandByIdAsync(It.IsAny<Int32>())).ReturnsAsync((Brand)null);

            // Act
            var result = await _brandService.GetBrandByIdAsync(_testBrand.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBrandsAsync_Success()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.GetBrandsAsync()).ReturnsAsync(new List<Brand> { _testBrand });

            // Act
            var result = await _brandService.GetBrandsAsync();

            // Assert
            Assert.Equal(new List<Brand> { _testBrand }, result);
        }

        [Fact]
        public async Task GetBrandsAsync_Fail()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.GetBrandsAsync()).ReturnsAsync((List<Brand>)null);

            // Act
            var result = await _brandService.GetBrandsAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBrandAsync_Success()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.UpdateBrandAsync(It.IsAny<Brand>())).ReturnsAsync(_testBrand);

            // Act
            var result = await _brandService.UpdateBrandAsync(_testBrand);

            // Assert
            Assert.Equal(_testBrand, result);
        }

        [Fact]
        public async Task UpdateBrandAsync_Fail()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.UpdateBrandAsync(It.IsAny<Brand>())).ReturnsAsync((Brand)null);

            // Act
            var result = await _brandService.UpdateBrandAsync(_testBrand);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteBrandAsync_Success()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.DeleteBrandAsync(It.IsAny<Int32>())).ReturnsAsync(true);

            // Act
            var result = await _brandService.DeleteBrandAsync(_testBrand.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBrandAsync_Fail()
        {
            // Arrange
            _brandRepositoryMock.Setup(x => x.DeleteBrandAsync(It.IsAny<Int32>())).ReturnsAsync(false);

            // Act
            var result = await _brandService.DeleteBrandAsync(_testBrand.Id);

            // Assert
            Assert.False(result);
        }
    }
}
