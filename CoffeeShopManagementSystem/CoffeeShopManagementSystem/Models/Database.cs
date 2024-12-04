using System.Data.SqlClient;
using System.Data;
using Microsoft.CodeAnalysis;

namespace CoffeeShopManagementSystem.Models
{
    public class Database
    {
        #region DropDown Function
        public static List<DropDownModel> GetDropDown(string connectionString, string procedure, string idField, string valueField)
        {
            List<DropDownModel> dropDown = new List<DropDownModel>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = procedure;

                    using (SqlDataReader reader1 = sqlCommand.ExecuteReader())
                    {
                        DataTable dataTable1 = new DataTable();
                        dataTable1.Load(reader1);

                        foreach (DataRow data in dataTable1.Rows)
                        {
                            DropDownModel dropDownData = new DropDownModel();
                            dropDownData.id = Convert.ToInt32(data[idField]);
                            dropDownData.value = data[valueField].ToString();
                            dropDown.Add(dropDownData);
                        }
                    }

                }


            }
            return dropDown;

        }
        #endregion
        #region Delete Function
        public static bool Delete(string connectionString,string procedure , int ID ,out string errorMessage , string parametereName = "@ID")
        {
            errorMessage = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procedure;
                        command.Parameters.Add(parametereName, SqlDbType.Int).Value = ID;

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                           
                            return true;
                        }
                        else
                        {
                            errorMessage = "Error occurred during operation.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }

}
