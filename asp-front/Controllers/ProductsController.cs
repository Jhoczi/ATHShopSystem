using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_front.Models.Product;
using ath_server.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp_front.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private IRepositoryService<Product> _productService;
        private IndexProductVm _index;

        public ProductsController(IMapper mapper, IRepositoryService<Product> productService)
        {
            _mapper = mapper;
            _productService = productService;
            _index = new IndexProductVm();
        }

        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<Product> dbProducts = _productService.GetAllRecords();
            _index.Products = _mapper.Map<List<ProductVm>>(dbProducts);
            return View(_index);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}