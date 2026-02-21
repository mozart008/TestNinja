using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            var employeeStorage = new Mock<EmployeeStorage>();

            var employeeController = new EmployeeController(employeeStorage.Object);
            var result = employeeController.DeleteEmployee(1);

            employeeStorage.Verify(s => s.DeleteEmployee(1));
        }

    }
}
