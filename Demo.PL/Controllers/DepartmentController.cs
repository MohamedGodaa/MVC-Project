using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Demo.PL.Controllers
{

    // 1.inheritace : DepartmentController is a Controller;
    // 2.Association(Composition=>Required) : DepartmentController has a DepartmentRepository;
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)// create by clr 
        {
            //_departmentRepository = new DepartmentRepository(new DAL.Data.AppDbContext());

            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() // =>[HttpGet]
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                _unitOfWork.Complete();
                //if(Count > 0)
                //{
                //    return RedirectToAction(nameof(Index));
                //}
            }
            return View(department);
        }

        public IActionResult Details(int? id ,string viewName= "Details")
        {
            if(id == null)
            {
                return BadRequest();//400
            }
            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if(department == null)
            {
                return NotFound(); //404
            }

            return View(viewName,department);
        }
        // [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();//400
            //}
            //var department = _departmentRepository.Get(id.Value);
            //if (department == null)
            //{
            //    return NotFound();
            //}
            //return View(department);
            return Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id!=department.Id) 
            { 
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                    //if (count > 0)
                    //{
                    //    return RedirectToAction(nameof(Index));
                    //}
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id,Department department)
        {
            if(id!=department.Id)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);
        }
    }
}
