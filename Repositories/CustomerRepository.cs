using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public void AddCustomer(Customer customer)
        {
           context.Customers.Add(customer);
        }
        
        public void DeleteCustomer(string customerId)
        {
           context.Customers.Remove(GetCustomerById(customerId));
        }

        public Customer GetCustomerById(string customerId)
        {
            return context.Customers.Include(c => c.MembershipType).Where(c => c.Id == customerId).SingleOrDefault();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return  context.Customers.Include(c => c.MembershipType);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            this.context.Entry(customer).State = EntityState.Modified;
        }
    }
}