using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class EmployeeTests
    {
        private Employee employee1;
        private Employee employee2;
        private Employee supervisor;
        private Order order1;
        private Order order2;

        [SetUp]
        public void Setup()
        {
            Employee.ClearUsers();
            Order.ClearOrders();

            employee1 = new Employee("Alaric Saltzman", "111-222-3333", "alaric@mysticfalls.com",
                new DateTime(1980, 5, 6), "History Teacher");
            employee2 = new Employee("Caroline Forbes", "444-555-6666", "caroline@forbes.com",
                new DateTime(1992, 10, 10), "School Counselor");
            supervisor = new Employee("Damon Salvatore", "555-666-7777", "damon@salvatore.com",
                new DateTime(1840, 10, 1), "Manager");

            order1 = new Order(new DateTime(2024, 12, 1), "In Progress");
            order2 = new Order(new DateTime(2024, 12, 5), "Completed");
        }

        [Test]
        public void CheckEmployeeAttributes()
        {
            Assert.That(employee1.Name, Is.EqualTo("Alaric Saltzman"));
            Assert.That(employee1.PhoneNumber, Is.EqualTo("111-222-3333"));
            Assert.That(employee1.Email, Is.EqualTo("alaric@mysticfalls.com"));
            Assert.That(employee1.DateOfBirth, Is.EqualTo(new DateTime(1980, 5, 6)));
            Assert.That(employee1.Position, Is.EqualTo("History Teacher"));
        }

        [Test]
        public void AssignOrder_ShouldSetReverseConnection()
        {
            employee1.assignOrder(order1);

            Assert.That(employee1.getAssociatedOrders().Count, Is.EqualTo(1));
            Assert.That(employee1.getAssociatedOrders()[0], Is.EqualTo(order1));
            Assert.That(order1.getAssociatedEmployees().Contains(employee1), Is.True);
        }

        [Test]
        public void RemoveOrder_ShouldClearReverseConnection()
        {
            employee1.assignOrder(order1);
            employee1.removeAssignedOrder(order1);

            Assert.That(employee1.getAssociatedOrders().Count, Is.EqualTo(0));
            Assert.That(order1.getAssociatedEmployees().Contains(employee1), Is.False);
        }

        [Test]
        public void RemoveAllOrders_ShouldClearAllConnections()
        {
            employee1.assignOrder(order1);
            employee1.assignOrder(order2);
            employee1.removeAllOrders();

            Assert.That(employee1.getAssociatedOrders().Count, Is.EqualTo(0));
            Assert.That(order1.getAssociatedEmployees().Contains(employee1), Is.False);
            Assert.That(order2.getAssociatedEmployees().Contains(employee1), Is.False);
        }

        [Test]
        public void AssignEmployee_ShouldSetSupervisorAndSubordinate()
        {
            supervisor.assignEmployee(employee1);

            Assert.That(supervisor.getAssociatedEmployees().Count, Is.EqualTo(1));
            Assert.That(supervisor.getAssociatedEmployees()[0], Is.EqualTo(employee1));
            Assert.That(employee1.getAssociatedEmployees().Count, Is.EqualTo(0)); // Employee1 не имеет подчинённых
            Assert.That(employee1.getAssociatedOrders().Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAssignedEmployee_ShouldClearReverseConnection()
        {
            supervisor.assignEmployee(employee1);
            supervisor.removeAssignedEmployee(employee1);

            Assert.That(supervisor.getAssociatedEmployees().Count, Is.EqualTo(0));
            Assert.That(employee1.getAssociatedOrders().Count, Is.EqualTo(0));
        }

        [Test]
        public void AssignSupervisor_ShouldSetReverseConnection()
        {
            employee1.assignEmployeeWhoSupervises(supervisor);

            Assert.That(employee1.getAssociatedEmployees().Count, Is.EqualTo(0));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Employee.ClearUsers();
            Assert.That(Employee.GetUsers().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Employee> employees = Employee.GetUsers().ConvertAll(user => (Employee)user);
            Assert.That(employees.Count, Is.EqualTo(3));
            Assert.That(employees[0].Name, Is.EqualTo("Alaric Saltzman"));
        }
    }
}