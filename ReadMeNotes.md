Notes about my solution, thought process, and troubles with the code challenge

Starting the coding challenge

This was my first time ever completing a coding challenge quite like this. While I have done other coding challenges 
before for other positions, they have all been through a third party web application such as CoderByte, so simply being
given an application to modify was a new challenge. It took me a little while to get going (I couldn't even run the
application at first and needed to reinstall Visual studio with the proper modules) and a lot of reading and
re-reading the code, but once I got started and was able to run the application I was able to progress quickly. 

Task 1: the reporting structure

The goal for task 1 was to create a new type, reporting structure, that contains an employee and the number of their
direct reports. This information could be computed on the fly and did not need to be stored, so I acheived this by 
creating a new model called ReportingStructure, and creating a ReportingStructureService and 
ReportingStructureController. The model contained the required reporting structure and its fields and the
ReportingStructureService interfaced with the EmployeeContext DbSet to generate the reporting structures. The actual
generation of the reporting structure was relatively simple, as the reporting stucture itself is essentially a Tree.
To generate the reporting structure I created a recursive function called ReportingStructureBuild that would traverse
the tree of employees and return the number of reports for a given employee.

The Endpoint for the ReportingStructure is as follows:
* READ
    * HTTP Method: GET 
    * URL: localhost:8080/api/reportingstructure/{id}
    * RESPONSE: Employee + number of reports

In the email I recieved from Mark, he specifically mentioned that there was a "Bug/defect in the code to identify and
solve." I beleive I ran into this bug when testing the Task 1 when I noted that in the json being returned from the
employee database, all of the directReports were being returned null even if they were present in the EmployeeSeedData.json
file. My best hypothesis as to the reason behind this problem was that somewhere in the EmployeeDataSeeder.cs file, 
specifically the FixUpReferences function as this portion specifically manipulates the directReports data. I tried
changing how the directReports data was being read (from employee.DirectReports being a list of employees to being a
list of id strings) because there seemed to be a discrepancy between the information provided in the ReadMe.md and the
actual code. In the ReadMe it was stated that directReports was to be an array of id strings, but in the code itself 
the employees the id strings were referencing was what was being stored. Despite my best efforts, no matter what I did
to change the EmployeeDataSeeder.cs file, it still would not work and often caused more issues. I left a commented out
example of one of my attempts in the EmployeeDataSeeder.cs file.

Task 2: The compensation

The goal for task 2 was to create another new type, compensation, with its required fields and to make persistent REST
endpoints that would query from the persistence layer. This task caused me quite a lot of trouble as I hadn't ever
heard of persistent endpoints or a persistence layer. After quite a bit of google searching I determined that what
this would most likely entail is creating some way for the compensation data to be created and stored by the application
to be read at a later time without needing to recreate the data. In order to acheive this I attempted to create a DbSet
specifically for the compensation data, and store it in its own CompensationContext class. Along with the
CompensationContext, I attempted to follow the form of the Employee architecture and created a CompensationRepository,
CompensationService, CompensationController, and Compensation model. By following the architecture of the Employee 
data set, I was able to implement the storage of the Compensations pretty well. The problems started to arise when I
needed to then retrieve the data from the CompensationContext. When I attempted to retrieve the data, I would continually
have exceptions thrown that state: The entity type 'Compensation' requires a primary key to be defined. Despite my best
efforts, I was also unable to discover the cause of this exception so I included a try-catch statement with the 
CompensationController GetCompensationById function to handle any thrown exceptions. 

The Endpoints for the Compensation is as follows:
* CREATE
    * HTTP Method: PUT
    * URL: localhost:8080/api/compensation/{id}/{salary}/{date}
    * RESPONSE: Compensation
* READ
    * HTTP Method: GET 
    * URL: localhost:8080/api/compensation/{id}
    * RESPONSE: Compensation