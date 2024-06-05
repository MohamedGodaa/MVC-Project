using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _EmployeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork)// create by clr 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_EmployeeRepository = new EmployeeRepository(new DAL.Data.AppDbContext());

            //_EmployeeRepository = EmployeeRepository;
            //_departmentRepository = departmentRepository;
        }
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var Employees = _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>> (Employees);
                return View(mappedEmp);
            }
            else
            {
                var Employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(searchInp.ToLower());
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }
            
        }
        [HttpGet]
        public IActionResult Create() // =>[HttpGet]
        {
            //ViewData["Department"] = _departmentRepository.GetAll();
            //ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel EmployeeVm)
        {
            if (ModelState.IsValid)
            {
                EmployeeVm.ImageName = DocumentSettings.UploudFile(EmployeeVm.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(EmployeeVm);

                _unitOfWork.EmployeeRepository.Add(mappedEmp);
                _unitOfWork.Complete();
                //if (Count > 0)
                //{
                //    return RedirectToAction(nameof(Index));
                //}
            }
            return View(EmployeeVm);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id == null)
            {
                return BadRequest();//400
            }
            var Employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee,EmployeeViewModel>(Employee);
            if (mappedEmp == null)
            {
                return NotFound(); //404
            }

            return View(viewName, mappedEmp);
        }
        // [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();//400
            //}
            //var Employee = _EmployeeRepository.Get(id.Value);
            //if (Employee == null)
            //{
            //    return NotFound();
            //}
            //return View(Employee);
            //ViewBag.Departments = _departmentRepository.GetAll();
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVm)
        {
            if (id != EmployeeVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
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

            return View(EmployeeVm);
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel EmployeeVm)
        {
            if (id != EmployeeVm.Id)
            {
                return BadRequest();
            }
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);
                DocumentSettings.DeleteFile(EmployeeVm.ImageName, "Images");
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(EmployeeVm);
        }
    }
}
