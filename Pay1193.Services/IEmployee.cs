﻿using Pay1193.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Pay1193.Services
{
    public interface IEmployee
    {
        Task CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task UpdateById(int id);
        Employee GetById(int id);
        Task Delete(int employeeId);
        IEnumerable<Employee> GetAll();
        decimal UnionFee(int id);
        decimal StudentLoanRepaymentAmount(int id, decimal totalAmount);
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();

    }
}
