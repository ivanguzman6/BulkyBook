using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using BulkyBook.Models.ViewModels;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            //Esta Vista personalizada contiene los datos del produc
            ProductVM ProductVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                //this is for create
                return View(ProductVM);
            }

            ProductVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (ProductVM.Product == null)
            {
                return NotFound();
            }
            return View(ProductVM);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Product Product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Product.Id == 0)
        //        {
        //            _unitOfWork.Product.Add(Product);
        //        }
        //        else
        //        {
        //            _unitOfWork.Product.Update(Product);
        //        }
        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));

        //    }

        //    return View(Product);
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var _allObj = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = _allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });


        }

        #endregion
    }
}
