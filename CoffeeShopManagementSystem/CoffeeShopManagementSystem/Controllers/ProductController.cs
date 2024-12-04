using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CoffeeShopManagementSystem.Models;
using Microsoft.CodeAnalysis;

namespace CoffeeShopManagementSystem.Controllers
{
    [CheckAccess]
    public class ProductController : Controller
    {
        private IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region ProductList
        public IActionResult ProductList()
        {
            string connectionString = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_Product_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            return View(dataTable);
        }
        #endregion

        #region ProductAddEdit
        public IActionResult ProductAddEdit(int? ProductID) {
            UserDropDown();
            ProductModel productModel = new ProductModel();
            #region ProductByID
            if (ProductID != null) {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_SelectByPK";
                command.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                

                foreach (DataRow dataRow in table.Rows)
                {
                    productModel.ProductID = Convert.ToInt32(@dataRow["ProductID"]);
                    productModel.ProductName = @dataRow["ProductName"].ToString();
                    productModel.ProductCode = @dataRow["ProductCode"].ToString();
                    productModel.ProductPrice = (int)Convert.ToDouble(@dataRow["ProductPrice"]);
                    productModel.Description = @dataRow["Description"].ToString();
                    productModel.UserID = Convert.ToInt32(@dataRow["UserID"]);
                }
                return View("ProductAddEdit", productModel);
            }
            
                
            #endregion

            return View("ProductAddEdit", productModel);
        }
        #endregion

        #region ProductSave
        public IActionResult ProductSave(ProductModel productModel)
        {
            if (productModel.UserID <= 0)
            {
                ModelState.AddModelError("UserID", "A valid User is required.");
            }

            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (productModel.ProductID == null)
                {
                    command.CommandText = "PR_Product_Insert";
                }
                else
                {
                    command.CommandText = "PR_Product_Update";
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productModel.ProductID;
                }
                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productModel.ProductName;
                command.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = productModel.ProductCode;
                command.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = productModel.ProductPrice;
                command.Parameters.Add("@Description", SqlDbType.VarChar).Value = productModel.Description;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = productModel.UserID;
               

                if (Convert.ToBoolean(command.ExecuteNonQuery())) {
                    if (productModel.ProductID == null) {
                        TempData["ProductInsert"] = "Record Inserted Successfully";
                    }
                    else
                    {
                        TempData["ProductInsert"] = "Record Updated Successfully";
                        return RedirectToAction("ProductList");

                    }
                }
                
            }
            
            UserDropDown();
            return View("ProductAddEdit", productModel);
        }
        #endregion

        #region Dropdown
        public void UserDropDown()
        {
            string connectionString = this._configuration.GetConnectionString("ConnectionString");

            List<DropDownModel> dropDown = Database.GetDropDown(connectionString, "PR_User_DropDown", "UserID", "UserName");
            ViewBag.UserList = dropDown;
        }
        #endregion

        #region ProductDelete
        public IActionResult ProductDelete(int ProductID)
        {
                try
                {
                    string connectionString = this._configuration.GetConnectionString("ConnectionString");
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Product_Delete";
                
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["ErrorMessage"] = "Product deleted successfully.";
                        TempData["ErrorMessageType"] = "success";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error occurred while deleting the product.";
                        TempData["ErrorMessageType"] = "error";
                    }   
            }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    TempData["ErrorMessageType"] = "error";
                    
                }
                return RedirectToAction("ProductList");
        }
    }
#endregion
}
