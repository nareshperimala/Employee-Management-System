using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeGetSingleController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeeGetSingleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        [HttpGet("{EmpoyeId}")]
        public IActionResult DisplayEmployee(int EmpoyeId)
        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));
           
            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter db = new SqlParameter("@Command", System.Data.SqlDbType.VarChar, 20);
            db.Value = "Select";
            cmd.Parameters.Add(db);
            SqlParameter p1 = new SqlParameter("@empid",System.Data.SqlDbType.Int);
            p1.Value = EmpoyeId;
            cmd.Parameters.Add(p1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            List<EmployeeDetailsModel> emd = new List<EmployeeDetailsModel>();
            while (dr.Read())
            {
                EmployeeDetailsModel e = new EmployeeDetailsModel();
                e.EmpId = Convert.ToInt32(dr["EmployeId"]);
                e.Fn = Convert.ToString(dr["FirstName"]);
                e.Ln = Convert.ToString(dr["LastName"]);
                e.Age = Convert.ToInt32(dr["Age"]);
                e.Salary = Convert.ToInt32(dr["Salary"]);
                e.Depart = Convert.ToString(dr["Department"]);
                e.DOJ = Convert.ToDateTime(dr["DOJ"]);
                emd.Add(e);


            }
            if (emd.Count == 0)
            {
                return NotFound("Employee Records Not Found");
            }
            return Ok(emd);
        }
    }
}
