using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Laboratorio1_3P.Controllers
{
    public class ClientMySqlController : Controller
    {
        private readonly ClientMySqlDataAccessLayer _dataAccessLayer = new ClientMySqlDataAccessLayer();

        // GET: ClientMySqlController
        public ActionResult Index()
        {
            var clients = _dataAccessLayer.GetAllClientes();
            return View(clients);
        }

        // GET: ClientMySqlController/Details/5
        public ActionResult Details(int id)
        {
            var client = _dataAccessLayer.GetClienteById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // GET: ClientMySqlController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientMySqlController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientMySql client)
        {
            try
            {
                // Verificar si ya existe un cliente con la misma cédula
                var existingClient = _dataAccessLayer.GetClienteByCedula(client.Cedula);
                if (existingClient != null)
                {
                    ModelState.AddModelError("cedula", "Ya existe un cliente con este número de cédula.");
                    return View(client);
                }

                // Si no existe, agregar el nuevo cliente
                _dataAccessLayer.InsertCliente(client);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al intentar agregar el cliente: " + ex.Message);
                return View(client);
            }
        }

        // GET: ClientMySqlController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = _dataAccessLayer.GetClienteById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: ClientMySqlController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClientMySql client)
        {
            try
            {
                // Verificar si ya existe un cliente con la misma cédula (excepto el que se está editando)
                var existingClient = _dataAccessLayer.GetClienteByCedula(client.Cedula);
                if (existingClient != null && existingClient.Codigo != id)
                {
                    ModelState.AddModelError("cedula", "Ya existe otro cliente con este número de cédula.");
                    return View(client);
                }

                _dataAccessLayer.UpdateCliente(client);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al intentar actualizar el cliente: " + ex.Message);
                return View(client);
            }
        }

        // GET: ClientMySqlController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = _dataAccessLayer.GetClienteById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: ClientMySqlController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _dataAccessLayer.DeleteCliente(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al intentar eliminar el cliente: " + ex.Message);
                return View();
            }
        }
    }
}
