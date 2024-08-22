using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class IngresoStepDefinitions
    {
        private readonly ClientSqlDataAccessLayer clientSqlTest = new ClientSqlDataAccessLayer();

        [Given(@"llenar los campos de la bd")]
        public void GivenLlenarLosCamposDeLaBd(Table table)
        {
            var dataTable = table.Rows.Count();
            dataTable.Should().BeGreaterThanOrEqualTo(1);
        }

        [When(@"el registro de ingresa en la BD")]
        public void WhenElRegistroDeIngresaEnLaBD(Table table)
        {
            var client = table.CreateSet<ClientSql>().ToList();
            ClientSql clienteSql = new ClientSql();

            foreach(var item in client)
            {
                clienteSql.Cedula = item.Cedula;
                clienteSql.Nombres = item.Nombres;
                clienteSql.Apellidos = item.Apellidos;
                clienteSql.FechaNacimiento = item.FechaNacimiento;
                clienteSql.Mail = item.Mail;
                clienteSql.Telefono = item.Telefono;
                clienteSql.Direccion = item.Direccion;
                clienteSql.Estado = item.Estado;
                clienteSql.Saldo = item.Saldo;    
            }
            clientSqlTest.AddCliente(clienteSql);
        }

        [Then(@"El resultado se almacena en la BD")]
        public void ThenElResultadoSeAlamacenaEnLaBD(Table table)
        {
            var expectedClient = table.CreateInstance<ClientSql>();

            var actualClient = clientSqlTest.GetClienteByCedula(expectedClient.Cedula);

            actualClient.Cedula.Should().Be(expectedClient.Cedula);
            actualClient.Nombres.Should().Be(expectedClient.Nombres);
            actualClient.Apellidos.Should().Be(expectedClient.Apellidos);
            actualClient.FechaNacimiento.Should().Be(expectedClient.FechaNacimiento);
            actualClient.Mail.Should().Be(expectedClient.Mail);
            actualClient.Telefono.Should().Be(expectedClient.Telefono);
            actualClient.Direccion.Should().Be(expectedClient.Direccion);
            actualClient.Estado.Should().Be(expectedClient.Estado);
            actualClient.Saldo.Should().Be(expectedClient.Saldo);
        }

    }
}
