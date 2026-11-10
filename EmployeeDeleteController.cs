using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDeleteController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeeDeleteController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        [HttpDelete]
        public IActionResult DeleteData(EmployeeDetailsModel emp)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));

            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter db = new SqlParameter("@Command", System.Data.SqlDbType.VarChar, 20);
            db.Value = "Delete";
            cmd.Parameters.Add(db);
            SqlParameter p1 = new SqlParameter("@empid", System.Data.SqlDbType.Int);
            p1.Value = emp.EmpId;
            cmd.Parameters.Add(p1);
            con.Open();
            cmd.ExecuteNonQuery();

            return Ok("Employee Record Delete Successfully");
        }
    }
}
