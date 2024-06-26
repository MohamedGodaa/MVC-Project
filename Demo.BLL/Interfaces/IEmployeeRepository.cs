﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenaricRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeByAddress(string address);
        IQueryable<Employee> GetEmployeeByName(string name);
    }
}
