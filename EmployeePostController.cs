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
            _configuration = configuration;

        }
        [HttpPost]

        public IActionResult AddEmployee(EmployeeDetailsModel emp)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));

            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Command", SqlDbType.VarChar,20).Value = "Insert";
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = emp.EmpId;
            cmd.Parameters.Add("@fname", SqlDbType.VarChar,30).Value = emp.Fn;
            cmd.Parameters.Add("@lname", SqlDbType.VarChar, 30).Value = emp.Ln;
            cmd.Parameters.Add("@age", SqlDbType.Int).Value = emp.Age;
            cmd.Parameters.Add("@sal", SqlDbType.Int).Value = emp.Salary;
            cmd.Parameters.Add("@dep", SqlDbType.VarChar, 30).Value = emp.Depart;
            cmd.Parameters.Add("@doj", SqlDbType.Date).Value = emp.DOJ;
            con.Open();
            int res = cmd.ExecuteNonQuery();
            if (res == 0)
            {
                return NotFound("No Record Found");
            }
            else
            {
                return Ok("Employee Record Updated Successfully");
            }


        }
    }
}
