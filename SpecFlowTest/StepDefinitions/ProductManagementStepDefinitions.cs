using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class ProductManagementStepDefinitions
    {
        private readonly ClienteSqlPruebaAccessLayer clientSqlTest = new ClienteSqlPruebaAccessLayer();

        [Given(@"I fill in the product details")]
        public void GivenIFillInTheProductDetails(Table table)
        {
            var dataTable = table.Rows.Count();
            dataTable.Should().BeGreaterThanOrEqualTo(1);
        }

        [When(@"the product record is inserted into the database")]
        public void WhenTheProductRecordIsInsertedIntoTheDatabase(Table table)
        {
            var client = table.CreateSet<ClienteSqlPrueba>().ToList();
            ClienteSqlPrueba clienteSql = new ClienteSqlPrueba();

            foreach (var item in client)
            {
                clienteSql.ProductName = item.ProductName;
                clienteSql.Category = item.Category;
                clienteSql.Price = item.Price;
                clienteSql.StockQuantity = item.StockQuantity;
            }
            clientSqlTest.AddProduct(clienteSql);
        }

        [Then(@"The result is stored in the database")]
        public void ThenTheResultIsStoredInTheDatabase(Table table)
        {
            var expectedClient = table.CreateInstance<ClienteSqlPrueba>();

            var actualClient = clientSqlTest.GetProductByName(expectedClient.ProductName);

            actualClient.ProductName.Should().Be(expectedClient.ProductName);
            actualClient.Category.Should().Be(expectedClient.Category);
            actualClient.Price.Should().Be(expectedClient.Price);
            actualClient.StockQuantity.Should().Be(expectedClient.StockQuantity);  
        }
    }
}
