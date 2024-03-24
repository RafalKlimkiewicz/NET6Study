using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

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

            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Index()?.ViewData.Model as ProductsListViewModel ?? new();

            //Assert
            var productArray = result?.Products.ToArray() ?? Array.Empty<Product>();

            Assert.True(productArray.Length == 2);
            Assert.Equal("P1", productArray[0].Name);
            Assert.Equal("P2", productArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductID= 1, Name= "P1" },
                new Product{ProductID= 2, Name= "P2" },
                new Product{ProductID= 3, Name= "P3" },
                new Product{ProductID= 4, Name= "P4" },
                new Product{ProductID= 5, Name= "P5" },
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            //Act
            var result = controller.Index(2)?.ViewData.Model as ProductsListViewModel ?? new();

            //Assert
            var prodArrayy = result.Products.ToArray() ?? Array.Empty<Product>();

            Assert.True(prodArrayy.Length == 2);
            Assert.Equal("P4", prodArrayy[0].Name);
            Assert.Equal("P5", prodArrayy[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"},
            }).AsQueryable());

            var controller = new HomeController(mock.Object) { PageSize = 3 };

            //Act
            var result = controller.Index(2)?.ViewData.Model as ProductsListViewModel ?? new();

            //Assert
            var pagingInfo = result.PagingInfo;

            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }
    }
}