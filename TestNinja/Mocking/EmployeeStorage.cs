using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        void DeleteEmployee(int id);
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        private readonly EmployeeContext _context;
        public List<Employee> employees { get { return this._context.Employees.ToList(); } }

        public EmployeeStorage(EmployeeContext context)
        {
            _context = context;
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee is null)
                return;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}
