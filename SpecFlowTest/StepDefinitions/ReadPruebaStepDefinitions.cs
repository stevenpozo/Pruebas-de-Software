using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class ReadPruebaStepDefinitions
    {
        private readonly ClienteSqlPruebaAccessLayer _productSqlTest = new ClienteSqlPruebaAccessLayer();
        private ClienteSqlPrueba _product;
        private IList<ClienteSqlPrueba> _allProducts;

        [Given(@"que hay productos en la base de datos")]
        public void GivenQueHayProductosEnLaBaseDeDatos()
        {
            // Puedes preparar algunos productos de prueba si es necesario.
            // Esto podría involucrar insertar productos de prueba en la base de datos.
            // Por ejemplo:
            //_productSqlTest.InsertTestProducts();
        }

        [When(@"consulto todos los productos")]
        public void WhenConsultoTodosLosProductos()
        {
            _allProducts = _productSqlTest.GetAllProducts().ToList();
        }

        [Then(@"todos los productos deberían ser devueltos")]
        public void ThenTodosLosProductosDeberianSerDevueltos()
        {
            _allProducts.Should().NotBeEmpty("Deberían devolverse productos.");
        }

        [Given(@"que un producto con el ID ""([^""]*)"" existe en la base de datos")]
        public void GivenQueUnProductoConElIDExisteEnLaBaseDeDatos(string id)
        {
            int productId = int.Parse(id);
            _product = _productSqlTest.GetProductById(productId);
            _product.Should().NotBeNull($"El producto con el ID {productId} debería existir en la base de datos.");
        }

        [When(@"busco el producto con el ID ""([^""]*)""")]
        public void WhenBuscoElProductoConElID(string id)
        {
            int productId = int.Parse(id);
            _product = _productSqlTest.GetProductById(productId);
        }

        [Then(@"el producto con el ID ""([^""]*)"" debería ser devuelto")]
        public void ThenElProductoConElIDDeberiaSerDevuelto(string id)
        {
            int productId = int.Parse(id);
            _product.Should().NotBeNull($"El producto con el ID {productId} debería ser devuelto.");
        }

        [Then(@"el producto debería tener los siguientes datos:")]
        public void ThenElProductoDeberiaTenerLosSiguientesDatos(Table table)
        {
            var expectedProduct = table.CreateInstance<ClienteSqlPrueba>();

            _product.Should().NotBeNull("El producto no debería ser null.");
            _product.ProductId.Should().Be(expectedProduct.ProductId);
            _product.ProductName.Should().Be(expectedProduct.ProductName);
            _product.Category.Should().Be(expectedProduct.Category);
            _product.Price.Should().Be(expectedProduct.Price);
            _product.StockQuantity.Should().Be(expectedProduct.StockQuantity);
        }
    }
}
