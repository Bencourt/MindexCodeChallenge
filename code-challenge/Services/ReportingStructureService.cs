using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    //the reporting structure service uses the employee repository to build the reporting structure from employee data
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        //Get reporting structure by id takes an id string as a parameter
        public ReportingStructure GetReportingStructureById(string id)
        {
            //if the id string is not null or empty, a new reporting structure is created
            if(!String.IsNullOrEmpty(id))
            {
                ReportingStructure report = new ReportingStructure();
                //the properties of the reporting structure are then set by the employee repository and the ReportingStructureBuild function
                report.Employee = _employeeRepository.GetById(id);
                report.NumberOfReports = ReportingStructureBuild(report.Employee);
                //the reporting structure is then returned
                return report;
            }
            return null;
        }

        //reporting structure build takes an employee object as a parameter
        private int ReportingStructureBuild(Employee employee)
        {
            //the reporting number is initialized as -1 to prevent the employee from counting itself
            int reportingNumber = -1;
            //check if a given employee has direct reports
            if(employee.DirectReports != null)
            {
                //increment the reporting number by one for the parent employee
                reportingNumber++;
                //for every direct report employee, recursively call ReportingStructureBuild with that employee
                foreach(Employee e in employee.DirectReports)
                {
                    //increase reporting number by the result of those recursive calls
                    reportingNumber += ReportingStructureBuild(e);
                }
            }
            //the recursion base case is if there are no direct reports for a given employee
            else
            {
                //increment the reporting number by 1 and return the reporting number
                reportingNumber++;
                return reportingNumber;
            }
            //finally, return the reporting number for the original employee
            return reportingNumber;
        }
    }
}
