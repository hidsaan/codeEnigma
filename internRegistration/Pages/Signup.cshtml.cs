using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace internRegistration.Pages
{
    public class UserInfo
    {
        public string firstname;
        public string lastname;
        public string dob;
        public string phone;
        public string email;
        public string address;
        public string password;
    }
    public class SignupModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            userInfo.firstname = Request.Form["firstname"];
            userInfo.lastname = Request.Form["lastname"];
            userInfo.dob = Request.Form["dob"];
            userInfo.phone = Request.Form["phone"];
            userInfo.email = Request.Form["email"];
            userInfo.address = Request.Form["address"];
            userInfo.password = Request.Form["password"];

            if (userInfo.firstname.Length == 0 || userInfo.lastname.Length==0 || userInfo.dob.Length==0 || userInfo.phone.Length == 0 || userInfo.email.Length == 0 || userInfo.address.Length == 0 || userInfo.password.Length==0 )
            {
                errorMessage = "All feilds are required";
                return;
            }

            if ( userInfo.phone.Length < 10 || userInfo.phone.Length > 12 )
            {
                errorMessage = "Enter correct phone number";
                return;
            }

            if (userInfo.password.Length < 8 || userInfo.phone.Length > 20)
            {
                errorMessage = "Password has to be 8-20 characters long";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-MIVMU6F;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users" +
                        "(firstname , lastname , dob ,phone, email, address, password ) VALUES " +
                        "( @firstname , @lastname , @dob ,@phone, @email, @address, @password );";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", userInfo.firstname);
                        command.Parameters.AddWithValue("@lastname", userInfo.lastname);
                        command.Parameters.AddWithValue("@dob", userInfo.dob);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.Parameters.AddWithValue("@password", userInfo.password);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                //errorMessage = ex.Message;

                Console.WriteLine(ex.Message); 
                return;
            }

            userInfo.firstname = "";
            userInfo.lastname = "";
            userInfo.dob = "";
            userInfo.phone = "";
            userInfo.email = "";
            userInfo.address = "";
            userInfo.password = "";

            /*            successMessage = "Sign In suceesful";
            */

            errorMessage = "There's an error in your inputs.";
            Response.Redirect("/Login");
        }
    }
}
