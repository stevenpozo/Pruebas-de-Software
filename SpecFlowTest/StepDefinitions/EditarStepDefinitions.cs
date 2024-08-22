using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class EditarStepDefinitions
    {
        private readonly ClientSqlDataAccessLayer _clientSqlTest = new ClientSqlDataAccessLayer();
        private ClientSql _originalClient;

        [Given(@"que el cliente con la cédula ""(.*)"" existe en la base de datos")]
        public void GivenQueElClienteConLaCedulaExisteEnLaBaseDeDatos(string cedula)
        {
            _originalClient = _clientSqlTest.GetClienteByCedula(cedula);
            _originalClient.Should().NotBeNull($"Cliente con cédula {cedula} debería existir en la base de datos.");
        }

        [When(@"edito los campos del cliente con los siguientes datos:")]
        public void WhenEditoLosCamposDelClienteConLosSiguientesDatos(Table table)
        {
            var updatedClient = table.CreateInstance<ClientSql>();

            // Aquí actualizas los campos del cliente existente
            _originalClient.Apellidos = updatedClient.Apellidos;
            _originalClient.Nombres = updatedClient.Nombres;
            _originalClient.FechaNacimiento = updatedClient.FechaNacimiento;
            _originalClient.Mail = updatedClient.Mail;
            _originalClient.Telefono = updatedClient.Telefono;
            _originalClient.Direccion = updatedClient.Direccion;
            _originalClient.Estado = updatedClient.Estado;
            _originalClient.Saldo = updatedClient.Saldo;

            // Llamada al método de actualización en la base de datos
            _clientSqlTest.UpdateCliente(_originalClient);
        }

        [Then(@"los cambios se almacenan en la base de datos correctamente")]
        public void ThenLosCambiosSeAlmacenanEnLaBaseDeDatosCorrectamente()
        {
            var updatedClient = _clientSqlTest.GetClienteByCedula(_originalClient.Cedula);

            // Verificar que los cambios se han realizado correctamente
            updatedClient.Should().NotBeNull("El cliente debería existir después de la actualización.");
            updatedClient.Apellidos.Should().Be(_originalClient.Apellidos);
            updatedClient.Nombres.Should().Be(_originalClient.Nombres);
            updatedClient.FechaNacimiento.Should().Be(_originalClient.FechaNacimiento);
            updatedClient.Mail.Should().Be(_originalClient.Mail);
            updatedClient.Telefono.Should().Be(_originalClient.Telefono);
            updatedClient.Direccion.Should().Be(_originalClient.Direccion);
            updatedClient.Estado.Should().Be(_originalClient.Estado);
            updatedClient.Saldo.Should().Be(_originalClient.Saldo);
        }

        [Then(@"los nuevos datos del cliente deberían ser los siguientes:")]
        public void ThenLosNuevosDatosDelClienteDeberianSerLosSiguientes(Table table)
        {
            var expectedClient = table.CreateInstance<ClientSql>();
            var actualClient = _clientSqlTest.GetClienteByCedula(expectedClient.Cedula);

            actualClient.Cedula.Should().Be(expectedClient.Cedula);
            actualClient.Apellidos.Should().Be(expectedClient.Apellidos);
            actualClient.Nombres.Should().Be(expectedClient.Nombres);
            actualClient.FechaNacimiento.Should().Be(expectedClient.FechaNacimiento);
            actualClient.Mail.Should().Be(expectedClient.Mail);
            actualClient.Telefono.Should().Be(expectedClient.Telefono);
            actualClient.Direccion.Should().Be(expectedClient.Direccion);
            actualClient.Estado.Should().Be(expectedClient.Estado);
            actualClient.Saldo.Should().Be(expectedClient.Saldo);
        }
    }
}
