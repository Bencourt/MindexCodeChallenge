using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    //The compensation service is used in the compensation controller to call functions on the compensation repository
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        //Create compensation by id function takes two parameters, the id string, and the compensation object
        public void CreateCompensationById(string id, Compensation compensation)
        {
            //if the id is not null or empty and doesnt already exist in the DbSet, the compensation object is added with the compensation repository
            if (!String.IsNullOrEmpty(id))
            {
                if (_compensationRepository.GetById(id) == null)
                {
                    _compensationRepository.Add(compensation);
                    _compensationRepository.SaveAsync().Wait();
                }
            }
        }

        //Get compensation by id takes an id string as a parameter
        public Compensation GetCompensationById(string id)
        {
            //if the id string is not null or empty, getById is called from the compensation repository
            if(!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }
            else
            {
                return null;
            }
        }
    }
}
