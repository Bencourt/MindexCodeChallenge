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
    //URL path for the reporting structure endpoints
    [Route("api/reportingstructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        //the reporting structure controller uses the reporting structure service
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<EmployeeController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        //GET request takes the id parameter as a string from the URL
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            //log the request
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            //creating a new report for the given id
            var report = _reportingStructureService.GetReportingStructureById(id);

            //returns the result
            if (report == null)
                return NotFound();

            return Ok(report);
        }
    }
}
