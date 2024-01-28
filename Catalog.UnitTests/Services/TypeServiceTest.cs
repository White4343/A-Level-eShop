namespace Catalog.UnitTests.Services
{
    public class TypeServiceTest
    {
        private readonly ITypeService _typeService;
        private readonly Mock<ITypeRepository> _typeRepositoryMock;
        private readonly Mock<IDbContextWrapper<AppDbContext>> _dbContextWrapperMock;
        private readonly Mock<ILogger<TypeService>> _loggerMock;

        private readonly Type _testType = new Type
        {
            Name = "Test Type"
        };

        public TypeServiceTest()
        {
            _typeRepositoryMock = new Mock<ITypeRepository>();
            _dbContextWrapperMock = new Mock<IDbContextWrapper<AppDbContext>>();
            _loggerMock = new Mock<ILogger<TypeService>>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            _dbContextWrapperMock.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransactionMock.Object);

            _typeService = new TypeService(_typeRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateTypeAsync_Success()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.CreateTypeAsync(It.IsAny<Type>())).ReturnsAsync(_testType);

            // Act
            var result = await _typeService.CreateTypeAsync(_testType);

            // Assert
            Assert.Equal(_testType, result);
        }

        [Fact]
        public async Task CreateTypeAsync_Fail()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.CreateTypeAsync(It.IsAny<Type>())).ReturnsAsync((Type)null);

            // Act
            var result = await _typeService.CreateTypeAsync(_testType);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTypeByIdAsync_Success()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.GetTypeByIdAsync(It.IsAny<Int32>())).ReturnsAsync(_testType);

            // Act
            var result = await _typeService.GetTypeByIdAsync(_testType.Id);

            // Assert
            Assert.Equal(_testType, result);
        }

        [Fact]
        public async Task GetTypeByIdAsync_Fail()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.GetTypeByIdAsync(It.IsAny<Int32>())).ReturnsAsync((Type)null);

            // Act
            var result = await _typeService.GetTypeByIdAsync(_testType.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTypesAsync_Success()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.GetTypesAsync()).ReturnsAsync(new List<Type> { _testType });

            // Act
            var result = await _typeService.GetTypesAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(_testType, result.First());
        }

        [Fact]
        public async Task GetTypesAsync_Fail()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.GetTypesAsync()).ReturnsAsync((IEnumerable<Type>)null);

            // Act
            var result = await _typeService.GetTypesAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTypeAsync_Success()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.UpdateTypeAsync(It.IsAny<Type>())).ReturnsAsync(_testType);

            // Act
            var result = await _typeService.UpdateTypeAsync(_testType);

            // Assert
            Assert.Equal(_testType, result);
        }

        [Fact]
        public async Task UpdateTypeAsync_Fail()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.UpdateTypeAsync(It.IsAny<Type>())).ReturnsAsync((Type)null);

            // Act
            var result = await _typeService.UpdateTypeAsync(_testType);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteTypeAsync_Success()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.DeleteTypeAsync(It.IsAny<Int32>())).ReturnsAsync(true);

            // Act
            var result = await _typeService.DeleteTypeAsync(_testType.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTypeAsync_Fail()
        {
            // Arrange
            _typeRepositoryMock.Setup(x => x.DeleteTypeAsync(It.IsAny<Int32>())).ReturnsAsync(false);

            // Act
            var result = await _typeService.DeleteTypeAsync(_testType.Id);

            // Assert
            Assert.False(result);
        }
    }
}