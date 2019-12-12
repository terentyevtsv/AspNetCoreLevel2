using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class EmployeesClient : BaseClient, IEmployeeService
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/employees";
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            var url = ServiceAddress;
            var employees = Get<List<EmployeeView>>(url);

            return employees;
        }

        public EmployeeView GetById(int id)
        {
            var employee = Get<EmployeeView>($"{ServiceAddress}/{id}");
            return employee;
        }

        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            var response = Put($"{ServiceAddress}/{id}", entity);
            return response.Content.ReadAsAsync<EmployeeView>().Result;
        }

        public void AddNew(EmployeeView model)
        {
            Post($"{ServiceAddress}", model);
        }

        public void Delete(int id)
        {
            Delete($"{ServiceAddress}/{id}");
        }

        public void Commit()
        {
            
        }
    }
}
