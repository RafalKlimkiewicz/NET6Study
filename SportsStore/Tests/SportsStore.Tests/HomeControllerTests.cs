using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new() { ProductID = 1, Name = "P1"},
                new() { ProductID = 2, Name = "P2"}
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);

            //Act
            IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            //Assert
            Product[] productArray = result?.ToArray() ?? Array.Empty<Product>();

            Assert.True(productArray.Length == 2);
            Assert.Equal("P1", productArray[0].Name);
            Assert.Equal("P2", productArray[1].Name);
        }
    }
}