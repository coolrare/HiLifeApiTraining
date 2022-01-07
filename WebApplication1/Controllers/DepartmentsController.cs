using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentsController : ApiController
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities();

        public DepartmentsController()
        {
            //db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Departments
        public IQueryable<Department> GetDepartment()
        {
            return db.Department.Include(d => d.Course);
        }

        [Route("~/api/vwDepartment")]
        public IQueryable<VwDepartment> GetVwDepartment()
        {
            return db.Database.SqlQuery<VwDepartment>("SELECT c.*, d.Name as DepartmentName, d.StartDate as DepartmentStartDate FROM dbo.Course c LEFT JOIN dbo.Department d ON (c.DepartmentID = d.DepartmentID)").AsQueryable();
        }

        [Route("~/api/vwDepartment2")]
        public IQueryable<GetDepartmentWithCourses_Result> GetVwDepartment2()
        {
            return db.GetDepartmentWithCourses().AsQueryable();
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(int id)
        {
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(int id, DepartmentUpdate dept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = db.Department.Find(id);
            //if (department == null)
            //{
            //    return NotFound();
            //}

            department.Budget = dept.Budget;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departments
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment(DepartmentCreate dept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = db.Department.Create();

            department.Name = dept.Name;
            department.Budget = dept.Budget;
            department.StartDate = dept.StartDate;

            db.Department.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentID }, department);
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Department.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Department.Count(e => e.DepartmentID == id) > 0;
        }
    }
}