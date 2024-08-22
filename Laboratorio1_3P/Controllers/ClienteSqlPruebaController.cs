using Laboratorio1_3P.Data;
using Laboratorio1_3P.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Text.RegularExpressions;

namespace Laboratorio1_3P.Controllers
{
    public class ClienteSqlPruebaController : Controller
    {
        ClienteSqlPruebaAccessLayer clienteSqlPrueba = new ClienteSqlPruebaAccessLayer();
        // GET: ClienteSqlPruebaController
        public ActionResult Index()
        {
            List<ClienteSqlPrueba> listClient = new List<ClienteSqlPrueba>();
            listClient = clienteSqlPrueba.GetAllProducts().ToList();
            return View(listClient);
        }

        // GET: ClienteSqlPruebaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteSqlPruebaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteSqlPruebaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteSqlPrueba client)
        {
            try
            {
                // Validaciones del lado del servidor
                // Validar longitud del nombre del producto
                if (client.ProductName.Length > 20)
                {
                    ModelState.AddModelError("ProductName", "El nombre del producto no puede tener más de 20 caracteres.");
                }

                // Validar caracteres especiales en el nombre del producto
                var nameRegex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!nameRegex.IsMatch(client.ProductName))
                {
                    ModelState.AddModelError("ProductName", "El nombre del producto no debe contener caracteres especiales.");
                }

                // Validar el precio
                if (client.Price < 0 || client.Price > 9000)
                {
                    ModelState.AddModelError("Price", "El precio debe ser un número positivo y no mayor a 9000.00.");
                }

                // Validar la cantidad en stock
                if (client.StockQuantity < 0 || client.StockQuantity > 1000)
                {
                    ModelState.AddModelError("StockQuantity", "La cantidad en stock debe ser un número positivo y no mayor a 1000.");
                }

                // Verificar si ya existe un producto con el mismo nombre
                if (ModelState.IsValid && clienteSqlPrueba.ProductExists(client.ProductName))
                {
                    // Si existe, añadir un mensaje de error al modelo y devolver la vista
                    ModelState.AddModelError("ProductName", "Ya existe un producto con este nombre.");
                    return View(client);
                }

                // Si el modelo es válido, insertar el cliente/producto en la base de datos
                if (ModelState.IsValid)
                {
                    clienteSqlPrueba.AddProduct(client);
                    return RedirectToAction(nameof(Index));
                }

                // Devolver la vista con los mensajes de error en caso de que el modelo no sea válido
                return View(client);
            }
            catch
            {
                // Manejo de excepciones
                return View(client);
            }
        }


        // GET: ClienteSqlPruebaController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = clienteSqlPrueba.GetProductById(id);
            return View(client);
        }

        // POST: ClienteSqlPruebaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClienteSqlPrueba client)
        {
            try
            {
                // Validaciones del lado del servidor
                // Validar longitud del nombre del producto
                if (client.ProductName.Length > 20)
                {
                    ModelState.AddModelError("ProductName", "El nombre del producto no puede tener más de 20 caracteres.");
                }

                // Validar caracteres especiales en el nombre del producto
                var nameRegex = new Regex("^[a-zA-Z0-9 ]*$");
                if (!nameRegex.IsMatch(client.ProductName))
                {
                    ModelState.AddModelError("ProductName", "El nombre del producto no debe contener caracteres especiales.");
                }

                // Validar el precio
                if (client.Price < 0 || client.Price > 9000)
                {
                    ModelState.AddModelError("Price", "El precio debe ser un número positivo y no mayor a 9000.00.");
                }

                // Validar la cantidad en stock
                if (client.StockQuantity < 0 || client.StockQuantity > 1000)
                {
                    ModelState.AddModelError("StockQuantity", "La cantidad en stock debe ser un número positivo y no mayor a 1000.");
                }
                // Actualizar el cliente en la base de datos
                clienteSqlPrueba.UpdateProduct(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }

        // GET: ClienteSqlPruebaController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = clienteSqlPrueba.GetProductById(id);
            return View(client);
        }

        // POST: ClienteSqlPruebaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                clienteSqlPrueba.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
