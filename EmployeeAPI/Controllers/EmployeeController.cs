using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using DataEmployee;

namespace EmployeeAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            using (EmployeeDBEntities dbEntities=new EmployeeDBEntities())
            {
                return dbEntities.Employees.ToList();
            }
        }

        public HttpResponseMessage Get(int Id)
        {            
            using(EmployeeDBEntities dBEntities=new EmployeeDBEntities())
            {
                var entity= dBEntities.Employees.FirstOrDefault(m => m.EmpId == Id);

                if (entity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {            
                using (EmployeeDBEntities dbEntities = new EmployeeDBEntities())
                {
                    dbEntities.Employees.Add(employee);
                    dbEntities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.EmpId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody]Employee employee, int Id)
        {
            try
            {
                using (EmployeeDBEntities dbEntities = new EmployeeDBEntities())
                {
                    var entity = dbEntities.Employees.FirstOrDefault(m => m.EmpId == Id);

                    if (entity != null)
                    {
                        entity.EmpName = employee.EmpName;
                        entity.EmpSal = employee.EmpSal;
                        entity.EmpDesig = employee.EmpDesig;
                        dbEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee could not found");
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                using (EmployeeDBEntities dBEntities = new EmployeeDBEntities())
                {
                    var entity = dBEntities.Employees.FirstOrDefault(m => m.EmpId == Id);

                    if (entity != null)
                    {
                        dBEntities.Employees.Remove(entity);
                        dBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Requested employee could not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
