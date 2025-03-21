﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models;

namespace DataLayer
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ICustomerContext customerContext;
        public CustomerRepository(ICustomerContext context)
            : base(context)
        {
            customerContext = context;
        }

        public Customer GetById(int id)
        {
            return customerContext.GetById(id);
        }

        public IEnumerable<Customer> GetCustomersByLastName(string lastname)
        {
            return customerContext.GetCustomersByLastName(lastname);
        }

        public IEnumerable<Customer> GetCustomersByZipcode(string zipcode)
        {
            return customerContext.GetCustomersByZipcode(zipcode);
        }

    }
}
