using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;


public partial class Pages_Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();

        userStore.Context.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyMobileShopDBConnectionString"].ConnectionString;

        UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

        // Create a new user and store in DB
        IdentityUser user = new IdentityUser();
        user.UserName = txtUserName.Text;


        if(txtPassword.Text == txtConfirmPassword.Text)
        {
            try
            {
                // Create user object
                IdentityResult result = manager.Create(user, txtPassword.Text);

                if (result.Succeeded)
                {

                    UserInformation info = new UserInformation
                    {
                        Adress = txtAddress.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        GUID = user.Id,
                        PostalCode = Convert.ToInt32(txtPostalCode.Text)
                    };

                    UserInfoModel model = new UserInfoModel();
                    model.InsertUserInformstion(info);

                    // Store user in DB
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                    // Set to log in new user by cookie
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    // Log in the new user and redirekt to homepage
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    litStatus.Text = result.Errors.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                litStatus.Text = ex.ToString();
            }
        }
        else
        {
            litStatus.Text = "Password must match";
        }
    }
}