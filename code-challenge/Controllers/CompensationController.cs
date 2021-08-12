using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    //URL path for the compensation endpoints
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        //compensation controller makes use of the compensation service and the employee service
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _compensationService = compensationService;
        }

        //GET request takes the id parameter as a string from the url
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            //log the request
            _logger.LogDebug($"Received compensation get request for '{id}'");

            //This part of the get request for my compensation service was giving me issues and stating: The entity type 'Compensation' requires a primary key to be defined 
            //I was unable to figure out why this was happening in the time of the challenge, but I would expect it has something to do with how I am setting up the database for the compensation data
            try
            {
                var compensation = _compensationService.GetCompensationById(id);

                if (compensation == null)
                    return NotFound();

                return Ok(compensation);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"GetCompensationById request failed for '{id}' and threw exception: {e}");
                return null;
            }
        }

        //PUT request takes the compensation construction data as parameters
        [HttpPut("{id}/{compensation}/{date}")]
        public IActionResult CreateCompensationById(String id, double salary, String date)
        {
            //log the request
            _logger.LogDebug($"Recieved compensation create request for '{id}'");
            //creates a new compensation object
            Compensation newCompensation = new Compensation();
            //sets the new compensation object's parameters to the ones provided in the URL
            newCompensation.Employee = _employeeService.GetById(id);
            newCompensation.Salary = salary;

            //the DateTime for the compensation is attempted to be parsed from the string given in the URL
            DateTime dateConversion;
            if(DateTime.TryParse(date, out dateConversion))
            {
                newCompensation.EffectiveDate = dateConversion;
            }
            else
            {
                //if the DateTime cannot be parsed, the EffectiveDate parameter is set to the current date and time
                newCompensation.EffectiveDate = DateTime.Now;
            }

            //the new compensation object is added to the compensation database
            _compensationService.CreateCompensationById(id, newCompensation);

            return Ok(newCompensation);
        }
    }
}
