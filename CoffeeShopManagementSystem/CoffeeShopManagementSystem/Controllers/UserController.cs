using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CoffeeShopManagementSystem.Models;

namespace CoffeeShopManagementSystem.Controllers
{
   
    public class UserController : Controller
    {
        private IConfiguration configuration;


        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;

        }
        #region User List
        public IActionResult UserList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_User_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            return View(dataTable);
        }
        #endregion

        #region Export To Excel
        public IActionResult ExportToExcel()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_User_SelectAll";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            var package = new OfficeOpenXml.ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Users");

            worksheet.Cells[1, 1].Value = "UserID";
            worksheet.Cells[1, 2].Value = "UserName";
            worksheet.Cells[1, 3].Value = "Email";
            worksheet.Cells[1, 4].Value = "Password";
            worksheet.Cells[1, 5].Value = "Mobile No";
            worksheet.Cells[1, 6].Value = "Address";
            worksheet.Cells[1, 7].Value = "Is Active";

            var row = 2;
            foreach (DataRow data in dataTable.Rows)
            {
                worksheet.Cells[row, 1].Value = data["UserID"];
                worksheet.Cells[row, 2].Value = data["UserName"];
                worksheet.Cells[row, 3].Value = data["Email"];
                worksheet.Cells[row, 4].Value = data["Password"];
                worksheet.Cells[row, 5].Value = data["MobileNo"];
                worksheet.Cells[row, 6].Value = data["Address"];
                worksheet.Cells[row, 7].Value = data["IsActive"];
                row++;

            }
            if (worksheet.Dimension != null)
            {
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }

            byte[] fileBytes = package.GetAsByteArray();
            string fileName = "Users.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(fileBytes, contentType, fileName);


        }
        #endregion

        #region User AddEdit
        public IActionResult UserAddEdit(int UserID) {

            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_User_SelectByPK";
            cmd.Parameters.AddWithValue("@UserId", UserID);
            
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            UserModel userModel = new UserModel();
            foreach(DataRow data in dataTable.Rows)
            {
                userModel.UserID = Convert.ToInt32(@data["UserID"]);
                userModel.UserName = @data["UserName"].ToString();
                userModel.Email = data["Email"].ToString();
                userModel.Password = data["Password"].ToString();
                userModel.MobileNumber = data["MobileNo"].ToString();
                userModel.Address = data["Address"].ToString();
                if (data["IsActive"] != DBNull.Value)
                {
                    userModel.IsActive = Convert.ToBoolean(data["IsActive"]);
                }
                else
                {
                    userModel.IsActive = false;  // or another default value as needed
                }
                
            }
            return View("userAddEdit" , userModel);
        }
        #endregion

        #region User Save
        public IActionResult UserSave(UserModel userModel) {
            if (userModel.UserID <= 0) {
                ModelState.AddModelError("UserID", "A valid user is required");
            }

            if (ModelState.IsValid) {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection( connectionString);
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (userModel.UserID == null) {
                    cmd.CommandText = "PR_User_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_User_Update";
                    cmd.Parameters.Add("@UserID",SqlDbType.Int).Value = userModel.UserID;
                }
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userModel.UserName;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = userModel.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = userModel.Password;
                cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = userModel.MobileNumber;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = userModel.Address;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = (bool)userModel.IsActive;

                if (Convert.ToBoolean(cmd.ExecuteNonQuery())) { 
                    if(userModel.UserID == null)
                    {
                        TempData["UserInsert"] = "User Inserted Successfullly!!";
                    }
                    else
                    {
                        TempData["UserInsert"] = "Record Updated Successfully";
                        return Redirect("UserList");
                    }
                }

            }
            return View("UserAddEdit" , userModel);
        }
        #endregion

        #region User Delete
       
        public IActionResult UserDelete(int UserID) {
            
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            string errorMessage;
            bool isDeleted = Database.Delete(connectionString, "PR_User_Delete", UserID, out errorMessage,"@UserID");


            if (isDeleted)
            {
                TempData["ErrorMessage"] = "User deleted successfully.";
                TempData["ErrorMessageType"] = "success";
            }
            else
            {
                TempData["ErrorMessage"] = errorMessage ?? "Error occurred while deleting the product.";
                TempData["ErrorMessageType"] = "error";
            }
            return RedirectToAction("UserList");
        }
        #endregion      

        public IActionResult UserRegister(UserRegisterModel userRegisterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = configuration.GetConnectionString("ConnectionString");
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand cmd = sqlConnection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_User_Register";
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userRegisterModel.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = userRegisterModel.Password;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = userRegisterModel.Email;
                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = userRegisterModel.MobileNo;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = userRegisterModel.Address;
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("UserLogin" , "User");
                }
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("UserRegister");
            }
            return View(userRegisterModel);
        }

        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                if (ModelState.IsValid) {
                    string connectionString = configuration.GetConnectionString("ConnectionString");
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand cmd = sqlConnection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_User_Login";
                    cmd.Parameters.Add("@UserName" , SqlDbType.VarChar).Value = userLoginModel.UserName;
                    cmd.Parameters.Add("@Password",SqlDbType.VarChar).Value = userLoginModel.Password;
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count > 0) {
                        foreach (DataRow dr in dt.Rows)
                        {
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        }
                        TempData["LoginSuccess"] = true;
                        return View(userLoginModel);
                    }
                    else
                    {
                        TempData["LoginFailed"] = true;

                    }
                }
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = ex.Message;
            }  

            return View(userLoginModel);
        }

        public IActionResult UserLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("UserLogin", "User");
        }

    }
}
