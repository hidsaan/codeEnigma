using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

namespace internRegistration.Pages
{
    public class LoginModel : PageModel
    {
       /* private readonly string connectionString;

        public LoginModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }*/

        public LoginInfo loginInfo = new LoginInfo();
        public string errorMessage = "";

       

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
                string connectionString = "Data Source=DESKTOP-MIVMU6F;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();



                    String sql = "SELECT email, password FROM users WHERE email = @email";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", loginInfo.Email);
                        //command.Parameters.AddWithValue("@Password", loginInfo.Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                String storedHash = reader["Password"].ToString();

                                if (BCrypt.Net.BCrypt.Verify(loginInfo.Password, storedHash))
                                {
                                    loginInfo.Email = "";
                                    loginInfo.Password = "";
                                    Response.Redirect("/List");
                                }
                              
                            }
                        }
                    }
                }

                //errorMessage = "Login failed";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //errorMessage = ex.Message;
                
            }
        }
    }
}
