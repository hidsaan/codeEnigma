using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace internRegistration.Pages
{
    public class LoginInfo
    {
        public string Email;
        public string Password;
    }
    public class LoginModel : PageModel
    {
        public LoginInfo loginInfo = new LoginInfo();
        public string errorMessage = "";

        private readonly string connectionString;

        public LoginModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public void OnGet()
        {
        }


        public void OnPost()


        {
            loginInfo.Email = Request.Form["Email"];
            loginInfo.Password = Request.Form["Password"];


            if (loginInfo.Email.Length == 0 || loginInfo.Password.Length == 0)
            {
                errorMessage = "All feilds are required";
                return;
            }


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT Email, Password FROM Users WHERE Email = @Email";



                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@Email", loginInfo.Email);
/*                        command.Parameters.AddWithValue("@Password", loginInfo.Password);
*/

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                String storedHash = reader["Password"].ToString();

                                if (BCrypt.Net.BCrypt.Verify(loginInfo.Password, storedHash))

                                //if Succesful Login then redirect to the secured page 
                                {
                                    Response.Redirect("/List");
                                }
                                else
                                {
                                    errorMessage = "Database Error";
                                }
                            }
                        }
                    }
                }

                errorMessage = "Login failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
