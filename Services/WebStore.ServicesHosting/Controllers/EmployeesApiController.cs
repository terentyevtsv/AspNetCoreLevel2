using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/employees")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? 
                               throw new ArgumentNullException(nameof(employeeService));
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employeeService.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeView GetById(int id)
        {
            return _employeeService.GetById(id);
        }

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody]EmployeeView model)
        {
            _employeeService.AddNew(model);
        }

        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeView UpdateEmployee(int id, [FromBody]EmployeeView entity)
        {
            return _employeeService.UpdateEmployee(id, entity);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeService.Delete(id);
        }

        [NonAction]
        public void Commit()
        {
            _employeeService.Commit();
        }
    }
}