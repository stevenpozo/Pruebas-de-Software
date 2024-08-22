using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class ActualizarProductoStepDefinitions
    {
        private readonly ClienteSqlPruebaAccessLayer _clientSqlTest = new ClienteSqlPruebaAccessLayer();
        private ClienteSqlPrueba _originalProduct;

        [Given(@"el producto con el nombre ""([^""]*)"" existe")]
        public void GivenElProductoConElNombreExiste(string exampleProd)
        {
            _originalProduct = _clientSqlTest.GetProductByName(exampleProd);
            _originalProduct.Should().NotBeNull($"Producto con el nombre {exampleProd} debería existir en la base de datos.");
        }

        [When(@"edito los campos del producto existente")]
        public void WhenEditoLosCamposDelProductoExistente(Table table)
        {
            var updatedProduct = table.CreateInstance<ClienteSqlPrueba>();

            // Actualiza los campos del producto existente
            _originalProduct.ProductName = updatedProduct.ProductName;
            _originalProduct.Category = updatedProduct.Category;
            _originalProduct.Price = updatedProduct.Price;
            _originalProduct.StockQuantity = updatedProduct.StockQuantity;

            // Llama al método de actualización en la base de datos
            _clientSqlTest.UpdateProduct(_originalProduct);
        }

        [Then(@"los cambios se almacenan en la base de datos existente")]
        public void ThenLosCambiosSeAlmacenanEnLaBaseDeDatosExistente()
        {
            var updatedProduct = _clientSqlTest.GetProductByName(_originalProduct.ProductName);

            // Verifica que los cambios se hayan realizado correctamente
            updatedProduct.Should().NotBeNull("El producto debería existir después de la actualización.");
            updatedProduct.ProductName.Should().Be(_originalProduct.ProductName);
            updatedProduct.Category.Should().Be(_originalProduct.Category);
            updatedProduct.Price.Should().Be(_originalProduct.Price);
            updatedProduct.StockQuantity.Should().Be(_originalProduct.StockQuantity);
        }

        [Then(@"los nuevos datos del producto existente")]
        public void ThenLosNuevosDatosDelProductoExistente(Table table)
        {
            var expectedProduct = table.CreateInstance<ClienteSqlPrueba>();
            var actualProduct = _clientSqlTest.GetProductByName(expectedProduct.ProductName);

            actualProduct.ProductName.Should().Be(expectedProduct.ProductName);
            actualProduct.Category.Should().Be(expectedProduct.Category);
            actualProduct.Price.Should().Be(expectedProduct.Price);
            actualProduct.StockQuantity.Should().Be(expectedProduct.StockQuantity);
        }
    }
}
