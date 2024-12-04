using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CoffeeShopManagementSystem.Models;

namespace CoffeeShopManagementSystem.Controllers
{
    [CheckAccess]
    public class CustomerController : Controller
    {
        private IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Customer List
        public IActionResult CustomerList()
        {
            string connectionstring = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Customer_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            return View(dataTable);
        }
        #endregion
        #region Customer AddEdit
        public IActionResult CustomerAddEdit(int CustomerID) {
            CustomerDropDown();
            string conncectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(conncectionString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Customer_SelectByPK";
            cmd.Parameters.AddWithValue("CustomerID", CustomerID);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            CustomerModel customerModel = new CustomerModel();

            foreach (DataRow data in dataTable.Rows)
            {
                customerModel.CustomerID = Convert.ToInt32(data["CustomerID"]);
                customerModel.CustomerName = data["CustomerName"].ToString();
                customerModel.HomeAddress = data["HomeAddress"].ToString();
                customerModel.Email = data["Email"].ToString();
                customerModel.MobileNo = data["MobileNO"].ToString();
                customerModel.GSTNo = data["GST_No"].ToString();
                customerModel.CityName = data["CityName"].ToString();
                customerModel.PinCode = data["PinCode"].ToString();
                customerModel.NetAmount =Convert.ToDecimal(data["NetAmount"]);
                customerModel.UserID = Convert.ToInt32(data["UserID"]);
            }
            return View("CustomerAddEdit", customerModel);
          
        }
        #endregion
        #region Customer Save
        public IActionResult CustomerSave(CustomerModel customerModel) {
            if (customerModel.UserID <= 0)
            {
                ModelState.AddModelError("UserID", "A valid User is required.");
            }
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (customerModel.CustomerID == null)
                {
                    cmd.CommandText = "PR_Customer_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_Customer_Update";
                    cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerModel.CustomerID;
                }
                cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customerModel.CustomerName;
                cmd.Parameters.Add("@HomeAddress", SqlDbType.VarChar).Value = customerModel.HomeAddress;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = customerModel.Email;
                cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customerModel.MobileNo;
                cmd.Parameters.Add("@GST_NO", SqlDbType.VarChar).Value = customerModel.MobileNo;
                cmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = customerModel.CityName;
                cmd.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = customerModel.PinCode;
                cmd.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = customerModel.NetAmount;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = customerModel.UserID;

                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    if (customerModel.CustomerID == null)
                    {
                        TempData["CustomerInsert"] = "Record Inserted Successfully";

                    }
                    else
                    {
                        TempData["CustomerInsert"] = "Record Updated Successfully";
                        return RedirectToAction("CustomerList");
                    }
                }

            }
            CustomerDropDown();
            return View("CustomerAddEdit", customerModel);
           
        }
        #endregion
        #region Customer Delete
        public ActionResult CustomerDelete(int CustomerID) {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            string errorMessage;
            bool isDeleted = Database.Delete(connectionString, "PR_Customer_Delete",CustomerID,out errorMessage,"@CustomerID");
            if (isDeleted)
            {
                TempData["ErrorMessage"] = "Customer Deleted Successfully !";
                TempData["ErrorType"] = "success";
            }
            else
            {
                TempData["ErrorMessage"] = errorMessage;
                TempData["ErrorType"] = "error";
            }
            return RedirectToAction("CustomerList");
        }
        #endregion
        #region Customer Dropdown
        public void CustomerDropDown()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            List<DropDownModel> userList = Database.GetDropDown(connectionString, "PR_User_DropDown", "UserID", "UserName");
            ViewBag.UserList = userList;
        }
        #endregion
    }
}
