using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {
        public readonly IConfiguration _configuration;
     
        public EmployeeDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeData"));
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_employee_details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter db = new SqlParameter("Command",System.Data.SqlDbType.VarChar,20);
            db.Value = "SelectAll";
            cmd.Parameters.Add(db);
            SqlDataReader dr = cmd.ExecuteReader();
            List<EmployeeDetailsModel> emd = new List<EmployeeDetailsModel>();
            while(dr.Read())
            {
                EmployeeDetailsModel e=new EmployeeDetailsModel();
                e.EmpId = Convert.ToInt32(dr["EmployeId"]);
                e.Fn = Convert.ToString(dr["FirstName"]);
                e.Ln = Convert.ToString(dr["LastName"]);
                e.Age = Convert.ToInt32(dr["Age"]);
                e.Salary = Convert.ToInt32(dr["Salary"]);
                e.Depart = Convert.ToString(dr["Department"]);
                e.DOJ = Convert.ToDateTime(dr["DOJ"]);
                emd.Add(e);


            }
            if(emd.Count==0)
            {
                return NotFound("Employee Records Not Found");
            }
            return Ok(emd);

        }
    }
}
