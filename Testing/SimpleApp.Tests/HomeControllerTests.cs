using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Controllers;
using SimpleApp.Models;

namespace SimpleApp.Tests
{
    public class HomeControllerTests
    {
        class FakeDataSource : IDataSource
        {
            public IEnumerable<Product> Products { get; set; }

            public FakeDataSource(Product[] data) => Products = data;
        }

        [Fact]
        public void IndexActionModelIsComplete()
        {
            //Arrange
            var controller = new HomeController();
            Product[] products = new Product[]
            {
                new Product { Name="Kayak", Price = 275M},
                new Product { Name="LifeJacket", Price = 48.95M},
            };

            //Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            //Assert
            Assert.Equal(products, model, Comparer.Get<Product>((p1, p2) => p1?.Name == p2?.Name && p1?.Price == p2?.Price));
        }

        [Fact]
        public void IndexActionModelIsComplete2()
        {
            //Arrange
            var controller = new HomeController();

            Product[] testData = new Product[]
            {
                new Product { Name="P1", Price = 50M},
                new Product { Name="P2", Price = 60M},
                new Product { Name="P3", Price = 70M}
            };

            var data = new FakeDataSource(testData);

            controller.dataSource = data;

            //Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            //Assert
            Assert.Equal(data.Products, model, Comparer.Get<Product>((p1, p2) => p1?.Name == p2?.Name && p1?.Price == p2?.Price));
        }

        [Fact]
        public void IndexActionModelIsCompleteMock()
        {
            //Arrange
            var controller = new HomeController();

            Product[] testData = new Product[]
            {
                new Product { Name="P1", Price = 50M},
                new Product { Name="P2", Price = 60M},
                new Product { Name="P3", Price = 70M}
            };

            var mock = new Mock<IDataSource>();

            mock.SetupGet(m => m.Products).Returns(testData);

            controller.dataSource = mock.Object;

            //Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            //Assert
            Assert.Equal(testData, model, Comparer.Get<Product>((p1, p2) => p1?.Name == p2?.Name && p1?.Price == p2?.Price));

            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}