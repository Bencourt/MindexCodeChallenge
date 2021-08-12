using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    //Compensation service interface
    public interface ICompensationService
    {
        Compensation GetCompensationById(String id);
        void CreateCompensationById(String id, Compensation compensation);
    }
}
