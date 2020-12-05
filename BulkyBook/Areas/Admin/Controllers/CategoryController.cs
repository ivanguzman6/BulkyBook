using BulkyBook.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;                
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            var _allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = _allObj });
        }

        #endregion
    }
}
