using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurosport.Kodakademi.Refactor
{
    public class Person
    {
        public string Name { get; set; }
        private string officeTelephoneNumber { get; set; }
        private string officeAreaCode { get; set; }

        public string GetTelephoneNumber()
        {
            return "(" + officeAreaCode + ") " + officeTelephoneNumber;
        }

        public Person(string officeTelephoneNumber, string officeAreaCode)
        {
            this.officeAreaCode = officeAreaCode;
            this.officeTelephoneNumber = officeTelephoneNumber;
        }

    }

    public class PersonRefactored
    {
        public string Name { get; set; }
        private Office office { get; set; }

        public string GetTelephoneNumber()
        {
            return office.GetTelephoneNumber();
        }

        public PersonRefactored(string officeTelephoneNumber, string officeAreaCode)
        {
            this.office = new Office(officeTelephoneNumber, officeAreaCode);
        }

    }

    public class Office
    {
        private string telephoneNumber { get; set; }
        private string areaCode { get; set; }

        public string GetTelephoneNumber()
        {
            return "(" + areaCode + ") " + telephoneNumber;
        }

        public Office(string telephoneNumber, string areaCode)
        {
            this.telephoneNumber = telephoneNumber;
            this.areaCode = areaCode;
        }

    }
}
