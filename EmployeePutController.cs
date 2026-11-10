using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePutController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeePutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeDetailsModel empup)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));
            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter db = new SqlParameter("@Command", System.Data.SqlDbType.VarChar, 20);
            db.Value = "Update";
            cmd.Parameters.Add(db);
            SqlParameter p1 = new SqlParameter("@empid", System.Data.SqlDbType.Int);
            p1.Value = empup.EmpId;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@fname", SqlDbType.VarChar, 20);
            p2.Value = empup.Fn;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@lname", SqlDbType.VarChar, 20);
            p3.Value = empup.Ln;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@age", SqlDbType.Int);
            p4.Value = empup.Age;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@sal", SqlDbType.Int);
            p5.Value = empup.Salary;
            cmd.Parameters.Add(p5);
            SqlParameter p6 = new SqlParameter("@dep", SqlDbType.VarChar, 20);
            p6.Value = empup.Depart;
            cmd.Parameters.Add(p6);
            SqlParameter p7 = new SqlParameter("@doj", SqlDbType.Date);
            p7.Value = empup.DOJ;
            cmd.Parameters.Add(p7);
            con.Open();
            cmd.ExecuteReader();

            return Ok("Employee Record Updated Successfully");
        }
    }
}
