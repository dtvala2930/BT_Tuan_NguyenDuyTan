﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Pay1193.Entity;
using Pay1193.Persistence;


namespace Pay1193.Services.Implement
{
    public class PayService : IPayService
    {
        private decimal contractualEarnings;
        private decimal overtimeHours;
        private readonly ApplicationDbContext _context;

        public PayService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarning(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }
            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() => _context.PaymentRecords.OrderBy(a => a.EmployeeId).ToList();


        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(taxYears => new SelectListItem
            {
                Text = taxYears.YearOfTax,
                Value = taxYears.Id.ToString()
            });
            return allTaxYear;
        }

        public PaymentRecord GetById(int id)
        {
            return _context.PaymentRecords.Where(payment => payment.Id == id).FirstOrDefault();

        }
        public decimal NetPay(decimal totalEarnings, decimal totalDeduction)
        {
            return totalEarnings - totalDeduction;
        }


        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours)
        {
            return overtimeHours * overtimeRate;
        }

        public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
            {
                overtimeHours = 0.00m;
            }
            else if (hoursWorked > contractualHours)
            {
                overtimeHours = hoursWorked - contractualHours;
            }
            return overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) {
            return hourlyRate * 1.5m;
        }

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees)
        {
            return tax + nic + studentLoanRepayment + unionFees;
        }

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        {
            return overtimeEarnings + contractualEarnings;
        }

        public TaxYear GetTaxYearById(int id)
        {
            return _context.TaxYears.Where(year => year.Id == id).FirstOrDefault();
        }
    }
}
