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
        

        public ProductsController(IMapper mapper, IRepositoryService<Product> productService)
        {
            _mapper = mapper;
            _productService = productService;
           
        }

        // GET: Product
        public ActionResult Index()
        {
            var indexVm = new IndexProductVm();
            IEnumerable<Product> dbProducts = _productService.GetAllRecords();
            indexVm.Products = _mapper.Map<List<ProductVm>>(dbProducts);
            return View(indexVm);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var dbProduct = _productService.GetSingle(id);
            var productVm = _mapper.Map<ProductVm>(dbProduct);
            return View(productVm);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewProductVm productVm)
        {
            try
            {
                var x = _mapper.Map<Product>(productVm);
                _productService.Add(x);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw (e);
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var dbProduct = _productService.GetSingle(id);
            var productVm = _mapper.Map<FormProductVm>(dbProduct);
            return View(productVm);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormProductVm productVm)
        {
            try
            {
                var productToDb = _mapper.Map<Product>(productVm);
                _productService.Edit(productToDb);
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
            try
            {
                var selectedProduct = _productService.GetSingle(id);
                _productService.Delete(selectedProduct);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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