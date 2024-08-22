using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class DeleteProductStepDefinitions
    {
        private readonly ClienteSqlPruebaAccessLayer clientSqlTest = new ClienteSqlPruebaAccessLayer();
        private string nameToDelete;
        [Given(@"el producto con los siguientes datos:")]
        public void GivenElProductoConLosSiguientesDatos(Table table)
        {
            var product = table.CreateInstance<ClienteSqlPrueba>();

            // Optionally, add the client to the database if not already present
            clientSqlTest.AddProduct(product);
        }

        [When(@"el producto es eliminado de la BD")]
        public void WhenElProductoEsEliminadoDeLaBD(Table table)
        {
            var productToDelete = table.CreateInstance<ClienteSqlPrueba>();
            nameToDelete = productToDelete.ProductName;
            clientSqlTest.DeleteProductByName(nameToDelete);
        }

        [Then(@"el producto ya no debe existir en la BD")]
        public void ThenElProductoYaNoDebeExistirEnLaBD(Table table)
        {
            var productToCheck = table.CreateInstance<ClienteSqlPrueba>();
            var actualProduct = clientSqlTest.GetProductByName(productToCheck.ProductName);
            actualProduct.Should().BeNull();
        }
    }
}
