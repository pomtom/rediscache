using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApp.Api.Model
{
    public class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public ContactDetails Contact { get; set; }

    }

    public class ContactDetails
    {
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
