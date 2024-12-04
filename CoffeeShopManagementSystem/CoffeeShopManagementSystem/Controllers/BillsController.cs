using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CoffeeShopManagementSystem.Models;

namespace CoffeeShopManagementSystem.Controllers
{
    [CheckAccess]
    public class BillsController : Controller
    {
        private IConfiguration _configuration;
        public BillsController(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        #region Bill List
        public IActionResult BillsList()
        {
            string connectionstring = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Bills_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            return View(dataTable);
        }
        #endregion
        #region Bills AddEdit
        public IActionResult BillsAddEdit(int BillID)
        {
            BillsDropDown();
            string conncectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(conncectionString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Bills_SelectByPK";
            cmd.Parameters.AddWithValue("BillID", BillID);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            BillsModel bills = new BillsModel();

            foreach (DataRow data in dataTable.Rows)
            {
                bills.BillID = Convert.ToInt32(data["BillID"]);
                bills.BillNumber = (data["BillID"]).ToString();
                bills.BillDate = Convert.ToDateTime(data["BillDate"]);
                bills.OrderID  = Convert.ToInt32(data["OrderID"]);
                bills.TotalAmount = Convert.ToDecimal(data["TotalAmount"]);
                bills.Discount = Convert.ToDecimal(data["Discount"]);
                bills.NetAmount = Convert.ToDecimal(data["NetAmount"]);
                bills.UserID = Convert.ToInt32(data["UserID"]);

            }
            return View("BillsAddEdit",bills);
        }
        #endregion
        #region Bills Save
        public IActionResult BillsSave(BillsModel bills) {
            if (bills.UserID <= 0) {
                ModelState.AddModelError("UserID", "A valid User is required.");
            }
            if (ModelState.IsValid) {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection( connectionString);
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (bills.BillID == null)
                {
                    cmd.CommandText = "PR_Bills_Insert";
                }
                else {
                    cmd.CommandText = "PR_Bills_Update";
                    cmd.Parameters.Add("@BillID",SqlDbType.Int).Value = bills.BillID;
                }
                cmd.Parameters.Add("@BillNumber",SqlDbType.VarChar).Value = bills.BillNumber;
                cmd.Parameters.Add("@BillDate",SqlDbType.DateTime).Value = bills.BillDate;
                cmd.Parameters.Add("@OrderID",SqlDbType.Int).Value = bills.OrderID;
                cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = bills.TotalAmount;
                cmd.Parameters.Add("@Discount",SqlDbType.Decimal).Value = bills.Discount;
                cmd.Parameters.Add("@NetAmount",SqlDbType.Decimal).Value = bills.NetAmount;
                cmd.Parameters.Add("@UserID",SqlDbType.Int).Value = bills.UserID;

                if (Convert.ToBoolean(cmd.ExecuteNonQuery())) {
                    if(bills.BillID == null)
                    {
                        TempData["BillsInsert"] = "Record Inserted Successfully";

                    }
                    else
                    {
                        TempData["BillsInsert"] = "Record Updated Successfully";
                        return RedirectToAction("BillsList");
                    }
                }
                
            }
            BillsDropDown();
            return View("BillsAddEdit",bills);

        }
        #endregion
        #region Bills DropDown
        public void BillsDropDown()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            List<DropDownModel> orderList = Database.GetDropDown(connectionString, "PR_Order_DropDown", "OrderID", "OrderID");
            ViewBag.OrderList = orderList;

            List<DropDownModel> userList = Database.GetDropDown(connectionString, "PR_User_DropDown", "UserID", "UserName");
            ViewBag.UserList = userList;
        }
        #endregion
        #region Bills Delete
        public IActionResult BillsDelete(int BillID)
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            string errorMessage;
            bool isDeleted = Database.Delete(connectionString, "PR_Bills_Delete", BillID, out errorMessage, "@BillID");
            if (isDeleted)
            {
                TempData["ErrorMessage"] = "Bills Deleted Successfully !";
                TempData["ErrorType"] = "success";
            }
            else
            {
                TempData["ErrorMessage"] = errorMessage;
                TempData["ErrorType"] = "error";
            }
            return RedirectToAction("BillsList");

        }
        }
    #endregion

}
