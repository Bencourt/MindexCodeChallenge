using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    //the compensation repository used to directly manipulate the data in the compensation context DbSet
    public class CompensationRespository : ICompensationRepository
    {
        private readonly CompensationContext _CompensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRespository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _CompensationContext = compensationContext;
            _logger = logger;
        }

        //the add method takes a compensation object as a parameter
        public Compensation Add(Compensation compensation)
        {
            //the compensation object is added to the compensations DbSet and the compensation object is returned
            _CompensationContext.Compensations.Add(compensation);
            return compensation;
        }

        //the GetById method takes an id string as a parameter
        public Compensation GetById(string id)
        {
            //loops through the compensations DbSet
            foreach(Compensation c in _CompensationContext.Compensations)
            {
                //returns the proper compensation object if found
                if(c.Employee.EmployeeId == id)
                {
                    return c;
                }
            }
            return null;
        }

        //compensation context SaveAsync
        public Task SaveAsync()
        {
            return _CompensationContext.SaveChangesAsync();
        }
    }
}
