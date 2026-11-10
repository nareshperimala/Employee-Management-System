using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePostController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeePostController(IConfiguration configuration)
        {
          _configuration= configuration;

        }
        [HttpPost]

        public IActionResult AddEmployee(EmployeeDetailsModel emp)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));
          
            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter db = new SqlParameter("@Command", System.Data.SqlDbType.VarChar, 20);
            db.Value = "Insert";
            cmd.Parameters.Add(db);
            SqlParameter p1 = new SqlParameter("@empid", System.Data.SqlDbType.Int);
            p1.Value = emp.EmpId;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@fname", SqlDbType.VarChar, 20);
            p2.Value = emp.Fn;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@lname", SqlDbType.VarChar, 20);
            p3.Value = emp.Ln;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@age", SqlDbType.Int);
            p4.Value = emp.Age;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@sal", SqlDbType.Int);
            p5.Value = emp.Salary;
            cmd.Parameters.Add(p5);
            SqlParameter p6 = new SqlParameter("@dep", SqlDbType.VarChar, 20);
            p6.Value = emp.Depart;
            cmd.Parameters.Add(p6);
            SqlParameter p7 = new SqlParameter("@doj", SqlDbType.DateTime);
            p7.Value = emp.DOJ;
            cmd.Parameters.Add(p7);
            con.Open();
            cmd.ExecuteReader();

            return Ok("Employee Record Successfully Added");
        }
        
       
    }
    
}
