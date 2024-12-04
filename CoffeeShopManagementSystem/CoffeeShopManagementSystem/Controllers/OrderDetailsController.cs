using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CoffeeShopManagementSystem.Models;

namespace CoffeeShopManagementSystem.Controllers
{
    [CheckAccess]
    public class OrderDetailsController : Controller
    {
        private IConfiguration _configuration;
        public OrderDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        #region OrderDetails List

        public IActionResult OrderDetailsList()
        {
            string connectionstring = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_OrderDetail_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            return View(dataTable);
        }
        #endregion

        #region OrderDetails Delete
        
        public ActionResult OrderDetailsDelete(int OrderDetailID) {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            string errorMessage;
            bool isDeleted = Database.Delete(connectionString, "PR_OrderDetail_Delete", OrderDetailID, out errorMessage,"OrderDetailID");
            if (isDeleted) {
                TempData["ErrorMessage"] = "OrderDetails Deleted Successfully !";
                TempData["ErrorType"] = "success";
            }
            else
            {
                TempData["ErrorMessage"] = errorMessage;
                TempData["ErrorType"] = "error";
            }
            return RedirectToAction("OrderDetailsList");
        }
        #endregion

        #region OrderDetails AddEdit
        public IActionResult OrderDetailsAddEdit(int OrderDetailID)
        {
            OrderDetailsDropDown();
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_OrderDetail_SelectByPK";
            cmd.Parameters.AddWithValue("@OrderDetailID" , OrderDetailID);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            OrderDetailModel orderDetails = new OrderDetailModel();

            foreach (DataRow data in dataTable.Rows) {

                orderDetails.OrderDetailID = Convert.ToInt32(data["OrderDetailID"]);
                orderDetails.OrderID = Convert.ToInt32(data["OrderID"]);
                orderDetails.ProductID = Convert.ToInt32(data["ProductID"]);
                orderDetails.Quantity = Convert.ToInt32(data["Quantity"]);
                orderDetails.Amount = Convert.ToDecimal(data["Amount"]);
                orderDetails.TotalAmount = Convert.ToDecimal(data["TotalAmount"]);
                orderDetails.UserID = Convert.ToInt32(data["UserID"]);
            }            
            return View("OrderDetailsAddEdit", orderDetails);
        }
        #endregion

        #region OrderDetails Save
        public IActionResult OrderDetailsSave(OrderDetailModel orderDetails)
        {
            if (orderDetails.UserID <= 0)
            {
                ModelState.AddModelError("UserID", "A valid User is required");
            }

            if (ModelState.IsValid) {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if(orderDetails.OrderDetailID == null)
                {
                    sqlCommand.CommandText = "PR_OrderDetail_Insert";
                }
                else
                {
                    sqlCommand.CommandText = "PR_OrderDetail_Update";
                    sqlCommand.Parameters.Add("@OrderDetailID" , SqlDbType.Int).Value = orderDetails.OrderDetailID;
                }
                sqlCommand.Parameters.Add("@OrderID",SqlDbType.Int).Value = orderDetails.OrderID;
                sqlCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = orderDetails.ProductID;
                sqlCommand.Parameters.Add("@Quantity",SqlDbType.Int).Value = orderDetails.Quantity;
                sqlCommand.Parameters.Add("@Amount",SqlDbType.Decimal).Value = orderDetails.Amount;
                sqlCommand.Parameters.Add("@TotalAmount",SqlDbType.Decimal).Value = orderDetails.TotalAmount;
                sqlCommand.Parameters.Add("@UserID",SqlDbType.Int).Value = orderDetails.UserID;

                if (Convert.ToBoolean(sqlCommand.ExecuteNonQuery())) {

                    if(orderDetails.OrderDetailID == null)
                    {
                        TempData["OrderDetailInsert"] = "Record Inserted Successfully";
                    }
                    else
                    {
                        TempData["OrderDetailInsert"] = "Record Updated Successfully";
                        return RedirectToAction("OrderDetailsList");
                    }
                }
            }
            OrderDetailsDropDown();
            return View("OrderDetailsAddEdit", orderDetails);
            
        }
        #endregion

        #region Orderdetails DropDown
        public void OrderDetailsDropDown()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            List<DropDownModel> userList = Database.GetDropDown(connectionString, "PR_User_DropDown", "UserID", "UserName");
            ViewBag.UserList = userList;

            List<DropDownModel> productList = Database.GetDropDown(connectionString, "PR_Product_DropDown", "ProductID", "ProductName");
            ViewBag.ProductList = productList;

            List<DropDownModel> orderList = Database.GetDropDown(connectionString, "PR_Order_DropDown", "OrderID", "OrderDate");
            ViewBag.OrderList = orderList;
         }

        #endregion
    }
}
