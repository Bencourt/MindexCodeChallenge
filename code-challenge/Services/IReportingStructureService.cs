using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    //reporting structure service interface
    public interface IReportingStructureService
    {
        ReportingStructure GetReportingStructureById(String id);
       
    }
}
