using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Laboratorio1_3P.Controllers
{
    public class ClientPostgresController : Controller
    {
        private readonly ClientPostgresDataAccessLayer _dataAccess;

        // Constructor que recibe el Data Access Layer
        public ClientPostgresController()
        {
            _dataAccess = new ClientPostgresDataAccessLayer();
        }

        // GET: ClientPostgresController
        public ActionResult Index()
        {
            var clients = _dataAccess.GetAllClientes().ToList();
            return View(clients);
        }

        // GET: ClientPostgresController/Details/5
        public ActionResult Details(int id)
        {
            var client = _dataAccess.GetClienteById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // GET: ClientPostgresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientPostgresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientePostgres client)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, volver a mostrar el formulario con errores
                return View(client);
            }

            try
            {
                // Verificar si ya existe un cliente con la misma cédula
                if (_dataAccess.GetAllClientes().Any(c => c.cedula == client.cedula))
                {
                    ModelState.AddModelError("cedula", "Ya existe un cliente con este número de cédula.");
                    return View(client);
                }

                // Insertar el nuevo cliente
                _dataAccess.InsertCliente(client);
                return RedirectToAction(nameof(Index)); // Asegúrate de que la acción 'Index' exista
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                ModelState.AddModelError("", $"Error al guardar el cliente: {ex.Message}");
                return View(client);
            }
        }


        // GET: ClientPostgresController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = _dataAccess.GetClienteById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: ClientPostgresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClientePostgres client)
        {
            try
            {
                if (id != client.codigo)
                {
                    return BadRequest();
                }

                _dataAccess.UpdateCliente(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }

        // GET: ClientPostgresController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = _dataAccess.GetClienteById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: ClientPostgresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _dataAccess.DeleteCliente(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
