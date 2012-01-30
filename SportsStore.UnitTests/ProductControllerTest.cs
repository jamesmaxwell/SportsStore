using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using Moq;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{


    /// <summary>
    ///这是 ProductControllerTest 的测试类，旨在
    ///包含所有 ProductControllerTest 单元测试
    ///</summary>
    [TestClass()]
    public class ProductControllerTest
    {
        private Product[] products;

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            products = new Product[]{
                new Product(){ ProductID = 1, Name = "p1", Price = 25},
                new Product(){ ProductID = 2, Name = "p2", Price = 179},
                new Product(){ ProductID = 3, Name = "p3", Price = 90},
                new Product(){ ProductID = 4, Name = "p4", Price = 25.88M},
                new Product(){ ProductID = 5, Name = "p5", Price = 159},
                new Product(){ ProductID = 6, Name = "p6", Price = 50.90M}
            };
        }
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void Can_Pagination()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;

            //action
            ProductListViewModel result = (ProductListViewModel)controller.List(1).Model;

            //Assert
            Product[] prods = result.Products.ToArray();
            Assert.IsTrue(prods.Length == 2);
            Assert.AreEqual(prods[0].Name, "p6");
            Assert.AreEqual(prods[1].Name, "p5");
        }

        [TestMethod()]
        public void Can_Generate_Page_Link()
        {
            HtmlHelper helper = null;
            PagingInfo pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                ItemsPerPage = 10,
                TotalItems = 28
            };

            Func<int, string> pageUrlDelegate = i => "page" + i;

            //Act
            MvcHtmlString result = PagingHelpers.PageLinks(helper, pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual<string>(@"<a href=""page1"">1</a><a class=""selected"" href=""page2"">2</a><a href=""page3"">3</a>", result.ToString());
        }
    }
}
