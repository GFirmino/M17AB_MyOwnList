using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyOwnList {
    public partial class register : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }
        enum PasswordScore {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        private static PasswordScore CheckingPasswordStrength(string password) {
            int score = 1;
            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.IsMatch(password, @"[0-9]+(\.[0-9][0-9]?)?", RegexOptions.ECMAScript))   //number only //"^\d+$" if you need to match more than one digit.
                score++;
            if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$", RegexOptions.ECMAScript)) //both, lower and upper case
                score++;
            if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript)) //^[A-Z]+$
                score++;
            return (PasswordScore)score;
        }
        public static bool IsValidEmail(string email) {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match) {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            } catch (RegexMatchTimeoutException e) {
                return false;
            } catch (ArgumentException e) {
                return false;
            }

            try {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }
        public static bool hasSpecialChar(string input) {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar) {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        protected void btnRegister_Click(object sender, EventArgs e) {
            try {
                string username = tbUsernameRegister.Text.Trim();
                string email = tbEmailRegister.Text.Trim();
                string password = tbPasswordRegister.Text.Trim();
                string password2 = tbPassword2Register.Text.Trim();

                DataTable check_username = Models.User.recieveUserDataByUserName(username);

                if (username.Length == 0 && email.Length == 0 && password.Length == 0 && password2.Length == 0)
                    throw new Exception("Fill in the required fields.");

                if (check_username.Rows.Count != 0)
                    throw new Exception("Username already exists.");

                if (hasSpecialChar(username))
                    throw new Exception("Username cannot contain special characters.");

                if (username.Length > 20 || username.Length < 3)
                    throw new Exception("The length of the name must comprise between 3 and 20 characters.");


                DataTable check_email = Models.User.recieveUserDataByEmail(email);

                if (check_email.Rows.Count != 0)
                    throw new Exception("Email already registered.");

                if (!IsValidEmail(email))
                    throw new Exception("Invalid email.");

                if (CheckingPasswordStrength(password) == PasswordScore.Blank || CheckingPasswordStrength(password) == PasswordScore.Weak || CheckingPasswordStrength(password) == PasswordScore.VeryWeak)
                    throw new Exception($"Password {CheckingPasswordStrength(password)}");

                if (password != password2)
                    throw new Exception("Passwords don't match.");

                if (fuImage.HasFile == false)
                    throw new Exception("Image not found.");

                if (fuImage.PostedFile.ContentType != "image/jpeg" &&
                    fuImage.PostedFile.ContentType != "image/jpg" && 
                    fuImage.PostedFile.ContentType != "image/png")
                    throw new Exception("The image file format is not supported. (Only jpeg/jpg)");

                if (fuImage.PostedFile.ContentLength == 0 ||
                fuImage.PostedFile.ContentLength > 5000000)
                    throw new Exception("The file size is not valid.");

                var respRecatcha = Request.Form["g-Recaptcha-Response"];
                var captchavalidate = ReCaptcha.Validate(respRecatcha);
                if (!captchavalidate)
                    throw new Exception("Recaptcha's error.");


                Models.User user = new Models.User(username, email, password);
                int id = user.add();

                string file = Server.MapPath(@"~\Public\Images\Users\");

                file += id + ".jpg";
                fuImage.SaveAs(file);

            } catch (Exception error) {
                lbMessageRegister.Text = error.Message;
                lbMessageRegister.CssClass = "alert alert-danger";
            }
        }

        protected void btnDoLogin_Click(object sender, EventArgs e) {
            Response.Redirect("/Auth/login.aspx");
        }
    }
}