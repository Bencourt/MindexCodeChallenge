using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    //The compensation class is used to create compensation objects for storage
    public class Compensation
    {
        //When attempting to troubleshoot the exception being thrown in the GET request of CompensationController.cs, the solution I kept coming across was to declare a key value in the object's class
        //[Key]
        public Employee Employee { get; set; }
        public double Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
