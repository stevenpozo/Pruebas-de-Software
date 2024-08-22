using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Laboratorio1_3P.Controllers
{
    public class ClientSqlController : Controller
    {
        ClientSqlDataAccessLayer clientSQL = new ClientSqlDataAccessLayer();

        // GET: ClientSqlController
        public ActionResult Index()
        {
            List<ClientSql> listClient = new List<ClientSql>();
            listClient = clientSQL.GetAllClientes().ToList();
            return View(listClient);
        }

        // GET: ClientSqlController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientSqlController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientSqlController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientSql client)
        {
            try
            {
                // Verificar si ya existe un cliente con la misma cédula
                if (clientSQL.ClienteExists(client.Cedula))
                {
                    // Si existe, añadir un mensaje de error al modelo y devolver la vista
                    ModelState.AddModelError("Cedula", "Ya existe un cliente con este número de cédula.");
                    return View(client);
                }

                client.Saldo = 0;

                clientSQL.AddCliente(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }





        // GET: ClientSqlController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = clientSQL.GetClienteById(id);
            return View(client);
        }

        // POST: ClientSqlController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClientSql client)
        {
            try
            {
                // Validar que el saldo sea un número y no sea negativo
                if (!decimal.TryParse(client.Saldo.ToString(), out decimal saldoValue))
                {
                    ModelState.AddModelError("Saldo", "El saldo debe ser un número válido.");
                    return View(client);
                }

                if (saldoValue < 0)
                {
                    ModelState.AddModelError("Saldo", "El saldo no puede ser negativo.");
                    return View(client);
                }

                // Validación de la cédula (si es necesario, puedes mantener la validación)
                if (saldoValue < 1000 || saldoValue > 9999)
                {
                    ModelState.AddModelError("Saldo", "El saldo debe estar entre 1000 y 9999.");
                    return View(client);
                }

                // Actualizar el cliente en la base de datos
                clientSQL.UpdateCliente(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }


        // GET: ClientSqlController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = clientSQL.GetClienteById(id);
            return View(client);
        }

        // POST: ClientSqlController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                clientSQL.DeleteCliente(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
