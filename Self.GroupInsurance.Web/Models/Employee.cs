using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Self.GroupInsurance.Web.Models
{
    public class Employee
    {
        public string ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public State EmployeeState { get; set; }
    }
}
