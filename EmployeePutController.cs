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
        public IActionResult UpdateEmployee( EmployeeDetailsModel emp)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));
            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Command", SqlDbType.VarChar,20).Value = "Update";
            cmd.Parameters.Add("@empid", SqlDbType.Int).Value = emp.EmpId;
            cmd.Parameters.Add("@fname", SqlDbType.VarChar,50).Value = emp.Fn;
            cmd.Parameters.Add("@lname", SqlDbType.VarChar,30).Value = emp.Ln;
            cmd.Parameters.Add("@age", SqlDbType.Int).Value = emp.Age;
            cmd.Parameters.Add("@sal", SqlDbType.Int).Value = emp.Salary;
            cmd.Parameters.Add("@dep", SqlDbType.VarChar,30).Value = emp.Depart;
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
