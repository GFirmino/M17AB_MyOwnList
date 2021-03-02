using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyOwnList {
    public partial class login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnDoRegister_Click(object sender, EventArgs e) {
            Response.Redirect("/Auth/Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            try {
                string input_email = tbEmailLogin.Text.Trim();
                string input_password = tbPasswordLogin.Text.Trim();

                DataTable check_user = Models.User.LoginVerify(input_email, input_password);

                if (check_user == null)
                    throw new Exception("Incorrect login.");

                if (check_user.Rows[0]["issuspended"].ToString() == "1")
                    throw new Exception("Suspended account.");

                var respRecatcha = Request.Form["g-Recaptcha-Response"];
                var captchavalidate = ReCaptcha.Validate(respRecatcha);

                if (!captchavalidate)
                    throw new Exception("Recaptcha's error.");

                int id = int.Parse(check_user.Rows[0]["iduser"].ToString());

                Session["user"] = Models.User.recieveUserByID(id);

                //(Session["user"] as Models.User).iduser

                if(Session["user"] != null) {
                    Response.Redirect("/index.aspx");
                }



            } catch (Exception error) {
                lbMessageLogin.Text = error.Message;
                lbMessageLogin.CssClass = "alert alert-danger";
            }
        }
    }
}