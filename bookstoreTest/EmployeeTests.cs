using System;
using System.Collections.Generic;
using NUnit.Framework;
using Bookstore.@class;

namespace Bookstore.@class.Tests
{
    public class EmployeeTests
    {
        private readonly Employee employee1 = new Employee("Alaric Saltzman", "111-222-3333", "alaric@mysticfalls.com", new DateTime(1980, 5, 6), "History Teacher");
        private readonly Employee employee2 = new Employee("Caroline Forbes", "444-555-6666", "caroline@forbes.com", new DateTime(1992, 10, 10), "School Counselor");

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
        public void CheckEmptyPositionException()
        {
            Assert.Throws<ArgumentException>(() => new Employee("Bonnie Bennett", "333-444-5555", "bonnie@bennett.com", new DateTime(1992, 2, 11), ""));
        }

        [Test]
        public void CheckAnotherEmployee()
        {
            Assert.That(employee2.Name, Is.EqualTo("Caroline Forbes"));
            Assert.That(employee2.PhoneNumber, Is.EqualTo("444-555-6666"));
            Assert.That(employee2.Email, Is.EqualTo("caroline@forbes.com"));
            Assert.That(employee2.DateOfBirth, Is.EqualTo(new DateTime(1992, 10, 10)));
            Assert.That(employee2.Position, Is.EqualTo("School Counselor"));
        }

        [Test]
        public void CheckEmployeeExtent()
        {
            List<Employee> employees = Employee.GetUsers().ConvertAll(user => (Employee)user);
            Assert.That(employees.Count, Is.EqualTo(2));
            Assert.That(employees[0].Name, Is.EqualTo("Alaric Saltzman"));
            Assert.That(employees[1].Name, Is.EqualTo("Caroline Forbes"));
        }

        [Test]
        public void CheckEncapsulationInExtent()
        {
            employee1.Name = "Kai Parker";
            List<Employee> employees = Employee.GetUsers().ConvertAll(user => (Employee)user);
            Assert.That(employees[0].Name, Is.EqualTo("Kai Parker"));
        }

        [Test]
        public void CheckExtentPersistency()
        {
            BookstoreFileManager.SaveBookstore();
            Employee.ClearUsers();
            Assert.That(Employee.GetUsers().Count, Is.EqualTo(0));
            BookstoreFileManager.LoadBookstore();
            List<Employee> employees = Employee.GetUsers().ConvertAll(user => (Employee)user);
            Assert.That(employees.Count, Is.EqualTo(2));
            Assert.That(employees[0].Name, Is.EqualTo("Alaric Saltzman"));
            Assert.That(employees[1].Name, Is.EqualTo("Caroline Forbes"));
        }
    }
}
