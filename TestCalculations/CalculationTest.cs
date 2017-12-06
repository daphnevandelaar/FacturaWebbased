﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace TestCalculations
{
    [TestClass]
    public class CalculationTest
    {
        [TestMethod]
        public void Test_TotalInvoicePrice()
        {
            //Arrange
            decimal expected = 15.30m;
            Task task = new Task("Planten watergeven", 2, 7.65m, new DateTime());
            int invoiceId = 1;
            Customer customer = new Customer("Daphne", "Pouwels");

            List<Task> tasks = new List<Task>();
            tasks.Add(task);

            //Act
            Invoice invoice = new Invoice(invoiceId, customer, new DateTime(), new DateTime(), tasks);
    
            //Assert
            Assert.AreEqual(expected, invoice.TotalPrice);
        }

        [TestMethod]
        public void Test_TotalTaskPrice()
        {
            // Arrange
            decimal expected = 280.60m;


            // Act
            Task task = new Task("planten sproeien", 4, 70.15m, new DateTime());


            // Assert
            Assert.AreEqual(expected, task.TotalPrice);
        }

    }
}
