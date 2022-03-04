using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CustomersController(ICustomerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = repository.GetCustomers()
                                          .ToList()
                                          .Select(_mapper.Map<Customer, CustomerDto>);
            return Ok(customers);
        }

        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            Console.WriteLine("START REQUEST");
            var customer = repository.GetCustomerById(id);
            await Task.Delay(2000);
            if (customer == null)
            {
                return NotFound();
            }

            Console.WriteLine("END REQUEST");
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = _mapper.Map<Customer>(customerDto);
            repository.AddCustomer(customer);
            repository.Save();
            customerDto.Id = customer.Id;

            return customerDto;
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public void UpdateCustomer(string id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = repository.GetCustomerById(id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _mapper.Map(customerDto, customerInDb);
            repository.UpdateCustomer(customerInDb);
            repository.Save();
        }

        // DELETE /api/customers
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public void DeleteCustomer(string id)
        {
            var customerInDb = repository.GetCustomerById(id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.DeleteCustomer(id);
            repository.Save();
        }

        private readonly ICustomerRepository repository;
        private readonly IMapper _mapper;
    }
}
