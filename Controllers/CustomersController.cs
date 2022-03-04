using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LibApp.Interfaces;
using LibApp.interfaces;

namespace LibApp.Controllers
{
    [Authorize(Roles = "Owner,StoreManager")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository repository;
        private readonly IMembershipTypeRepository membershipTypeRepository;

        public CustomersController(ICustomerRepository repository, IMembershipTypeRepository membershipTypeRepository)
        {
                 this.repository = repository;
            this.membershipTypeRepository = membershipTypeRepository;
        }

        public ViewResult Index()
        {          
            return View();
        }

        public IActionResult Details(string id)
        {
            var customer = repository.GetCustomerById(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult New()
        {
            var membershipTypes = membershipTypeRepository.GetMembershipTypes().ToList();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [Authorize(Roles = "Owner")]
        public IActionResult Edit(string id)
        {
            var customer = repository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = membershipTypeRepository.GetMembershipTypes().ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = membershipTypeRepository.GetMembershipTypes().ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id.ToString() == null)
            {
                customer.Id = Guid.NewGuid().ToString();
                repository.AddCustomer(customer);
            }
            else
            {
                var customerInDb = repository.GetCustomerById(customer.Id.ToString());
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.HasNewsletterSubscribed = customer.HasNewsletterSubscribed;
            }

            repository.Save();

            return RedirectToAction("Index", "Customers");
        }
    }
}