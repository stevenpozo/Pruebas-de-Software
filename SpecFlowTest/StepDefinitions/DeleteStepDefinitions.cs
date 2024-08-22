using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class DeleteStepDefinitions
    {
        private readonly ClientSqlDataAccessLayer clientSqlTest = new ClientSqlDataAccessLayer();
        private string cedulaToDelete;

        [Given(@"el cliente con los siguientes datos:")]
        public void GivenElClienteConLosSiguientesDatos(Table table)
        {
            var client = table.CreateInstance<ClientSql>();

            // Optionally, add the client to the database if not already present
            clientSqlTest.AddCliente(client);
        }

        [When(@"el cliente es eliminado de la BD")]
        public void WhenElClienteEsEliminadoDeLaBD(Table table)
        {
            var clientToDelete = table.CreateInstance<ClientSql>();
            cedulaToDelete = clientToDelete.Cedula;
            clientSqlTest.DeleteClienteByCedula(cedulaToDelete);
        }

        [Then(@"el cliente ya no debe existir en la BD")]
        public void ThenElClienteYaNoDebeExistirEnLaBD(Table table)
        {
            var clientToCheck = table.CreateInstance<ClientSql>();
            var actualClient = clientSqlTest.GetClienteByCedula(clientToCheck.Cedula);
            actualClient.Should().BeNull();
        }
    }
}

