﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory;
using FacturaWeb.ViewModels;
using LogicLayer;
using Models;

namespace FacturaWeb.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerLogic customerLogic = CustomerFactory.ManageCustomers();


        // GET: Customer
        public ActionResult Customer(string searchby, string search)
        {
            InvoiceCustomerViewModel viewModel = new InvoiceCustomerViewModel()
            {
                Customers = customerLogic.GetAllCustomers()
            };

            if (searchby == "lastname")
            {
                InvoiceCustomerViewModel searchModel = new InvoiceCustomerViewModel()
                {
                    Customers = customerLogic.GetCustomersByLastName(search)
                };

                return View(searchModel);
            }
            else if (searchby == "zipcode") //de zipcode search functie nog maken in de database
            {
                InvoiceCustomerViewModel searchModel = new InvoiceCustomerViewModel()
                {
                    Customers = customerLogic.GetCustomersByZipcode(search)
                };

                return View(searchModel);
            }

            return View(viewModel);
        }

        public ActionResult SaveCustomerToDb(FormCollection collection)
        {
            string firstname = collection["tbFirstname"];
            string prefix = collection["tbPrefix"];
            string surname = collection["tbSurname"];
            string street = collection["tbStreet"];
            string place = collection["tbPlace"];
            string zipcode = collection["tbZipcode"];
            string email = collection["tbEmail"];
            string phone = collection["tbPhonenumber"];

            var address = new Address(street, place, zipcode);
            var customer = new Customer(0, firstname, surname, prefix, email, phone, address);
            try
            {
                customerLogic.Insert(customer);
            }
            catch(CustomerException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            
            return RedirectToAction("Customer");
        }
        public ActionResult UpdateCustomerInDb(FormCollection collection)
        {
            int id = Convert.ToInt16(collection["lbId"]);
            string firstname = collection["tbFirstname"];
            string prefix = collection["tbPrefix"];
            string surname = collection["tbSurname"];
            string street = collection["tbStreet"];
            string place = collection["tbPlace"];
            string zipcode = collection["tbZipcode"];
            string email = collection["tbEmail"];
            string phone = collection["tbPhonenumber"];

            var address = new Address(street, place, zipcode);
            var customer = new Customer(id, firstname, surname, prefix, email, phone, address);

            customerLogic.Update(customer);

            return RedirectToAction("Customer");
        }


        public ActionResult UpdateCustomer(InvoiceCustomerViewModel customerViewModel)
        {
            if (customerViewModel.Id != 0)
            {
                var customer = customerLogic.GetById(customerViewModel.Id);
                customerViewModel.Customer = customer;
                return View(customerViewModel);
            }
            else
            {
                return RedirectToAction("Customer");
            }
               
        }

        public ActionResult CreateNewCustomer(InvoiceCustomerViewModel customerViewModel)
        {
            return PartialView("CreateNewCustomer");
        }
    }
}