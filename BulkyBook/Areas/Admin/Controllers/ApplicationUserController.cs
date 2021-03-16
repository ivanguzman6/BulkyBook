using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;                
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            if(id==null)
            {
                //this is for create
                return View(applicationUser);
            }

            applicationUser = _unitOfWork.ApplicationUser.Get(id.GetValueOrDefault());
            if(applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ApplicationUser applicationUser)
        {
            if(ModelState.IsValid)
            {
                if(applicationUser.Id=="")
                {
                    _unitOfWork.ApplicationUser.Add(applicationUser);
                }
                else
                {
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }

            return View(applicationUser);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var _allObj = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company");
            return Json(new { data = _allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.ApplicationUser.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });


        }

        #endregion
    }
}
