using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CoffeeShopManagementSystem.Models;

namespace CoffeeShopManagementSystem.Controllers
{
    [CheckAccess]
    public class OrderController : Controller
    {
        private IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region OrderList
        public IActionResult OrderList()
        {
            string connectionstring = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Order_SelectAll";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            return View(dataTable);
        }
        #endregion
        #region Order Delete
        public IActionResult OrderDelete(int OrderID) {
            string errorMessage;
            string connectionstring = this._configuration.GetConnectionString("ConnectionString");
            bool isDeleted = Database.Delete(connectionstring, "PR_Order_Delete",OrderID,out errorMessage,"OrderID");
            if (isDeleted) {
                TempData["ErrorMessage"] = "Product deleted successfully.";
                TempData["ErrorMessageType"] = "success";
            }
            else
            {
                TempData["ErrorMessage"] = errorMessage;
                TempData["ErrorMessageType"] = "error";
            }
            return RedirectToAction("OrderList");
        }
        #endregion
        #region Order AddEdit
        public IActionResult OrderAddEdit(int OrderID)
        {
            OrderDropDown();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Order_SelectByPK";
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            OrderModel orderModel = new OrderModel();
            foreach (DataRow data in dataTable.Rows) {
                orderModel.OrderID = Convert.ToInt32(data["OrderID"]);
                orderModel.OrderDate = Convert.ToDateTime(data["OrderDate"]);
                orderModel.CustomerID = Convert.ToInt32(data["CustomerID"]);
                orderModel.PaymentMode = data["PaymentMode"].ToString();
                orderModel.TotalAmount = (int)Convert.ToDouble(data["TotalAmount"]);
                orderModel.ShippingAddress = data["ShippingAddress"].ToString();
                orderModel.UserID = Convert.ToInt32(data["UserID"]);
            }


            return View("OrderAddEdit",orderModel);
        }
        #endregion
        #region OrderSave
        public IActionResult OrderSave(OrderModel orderModel)
        {
            if(orderModel.UserID <= 0)
            {
                ModelState.AddModelError("UserID", "A valid User is required.");
            }

            if (ModelState.IsValid) {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                if(orderModel.OrderID == null)
                {
                    cmd.CommandText = "PR_Order_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_Order_Update";
                    cmd.Parameters.Add("@OrderID",SqlDbType.Int).Value = orderModel.OrderID;
                }
                cmd.Parameters.Add("@OrderDate",SqlDbType.Date).Value = orderModel.OrderDate;
                cmd.Parameters.Add("@CustomerID",SqlDbType.Int).Value=orderModel.CustomerID;
                cmd.Parameters.Add("@PaymentMode",SqlDbType.VarChar).Value = orderModel.PaymentMode;
                cmd.Parameters.Add("@TotalAmount" , SqlDbType.Decimal).Value = orderModel.TotalAmount;
                cmd.Parameters.Add("@ShippingAddress", SqlDbType.VarChar).Value = orderModel.ShippingAddress;
                cmd.Parameters.Add("UserID", SqlDbType.Int).Value = orderModel.UserID;


                if (Convert.ToBoolean(cmd.ExecuteNonQuery())) { 
                    if(orderModel.OrderID == null)
                    {
                        TempData["OrderInsert"] = "Record Inserted Successfully";
                    }
                    else
                    {
                        TempData["OrderInsert"] = "Record Updated Successfully";
                        return Redirect("OrderList");
                    }
                }
            }
            OrderDropDown();
            return View("OrderAddEdit" , orderModel);
        }
        #endregion
        #region Order DropDown
        public void OrderDropDown()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            List<DropDownModel> userList = Database.GetDropDown(connectionString, "PR_User_DropDown", "UserID", "UserName");
            ViewBag.UserList = userList;

            List<DropDownModel> customerList = Database.GetDropDown(connectionString, "PR_Customer_DropDown", "CustomerID", "CustomerName");
            ViewBag.CustomerList = customerList;
        }
        #endregion
    }
}
