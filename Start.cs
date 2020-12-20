using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoabDashboard
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            txtServerURL.Text = Properties.Settings.Default.ServerURL;
        }

        private async void btnSubmitNewOrganization_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if(txtOrganizationName.Text != "" && txtOrganizationDescription.Text != ""
                    && txtAdminFirstName.Text != "" && txtAdminLastName.Text != ""
                    && txtAdminDescription.Text != "" && txtAdminEmailAddress.Text != ""
                    && txtAdminPassword.Text != "" && txtServerURL.Text != "" && txtRetypePassword.Text != "")
                {
                    if (txtAdminPassword.Text.Trim() == txtRetypePassword.Text.Trim())
                    {
                        //Capture Values
                        Models.OrganizationRequest organizationRequest = new Models.OrganizationRequest();
                        organizationRequest.OrganizationName = txtOrganizationName.Text.Trim();
                        organizationRequest.OrganizationDescription = txtOrganizationDescription.Text.Trim();
                        organizationRequest.FirstName = txtAdminFirstName.Text.Trim();
                        organizationRequest.LastName = txtAdminLastName.Text.Trim();
                        organizationRequest.UserDescription = txtAdminDescription.Text.Trim();
                        organizationRequest.UserEmailAddress = txtAdminEmailAddress.Text.Trim();
                        organizationRequest.UserPassword = txtAdminPassword.Text.Trim();

                        //Save Server URL and API Route
                        Program.serverURL = txtServerURL.Text.Trim();
                        Properties.Settings.Default.ServerURL = Program.serverURL;
                        Properties.Settings.Default.Save();

                        //Create JSON Document
                        var jsonString = JsonConvert.SerializeObject(organizationRequest);

                        //Clear Values
                        txtOrganizationName.Clear();
                        txtOrganizationDescription.Clear();
                        txtAdminFirstName.Clear();
                        txtAdminLastName.Clear();
                        txtAdminDescription.Clear();
                        txtAdminEmailAddress.Clear();
                        txtAdminPassword.Clear();
                        txtRetypePassword.Clear();

                        //Send Data
                        ClientSDK clientSDK = new ClientSDK();
                        string uriString = Program.serverURL + "/Organization";
                        var jsonResult = await clientSDK.Create(uriString, jsonString, "");
                        var objectResult = JsonConvert.DeserializeObject<Models.OrganizationResponse>(jsonResult);

                        //Save Return Values
                        if (objectResult.status == "success")
                        {
                            Program.identity = objectResult.id;
                            Program.securityToken = objectResult.securityToken;
                            MessageBox.Show("Your identity is " + Program.identity + " and your security token is " + Program.securityToken, "Information");

                            Views.Home home = new Views.Home();
                            this.Hide();
                            home.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(objectResult.message, "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The Password fields must match.", "Information");
                    }

                }
                else
                {
                    MessageBox.Show("All fields must be properly filled-in.", "Information");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private async void btnSubmitLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmailAddress.Text != "" && txtPassword.Text != "" && txtServerURL.Text != "")
                {
                    //Capture Values
                    Models.LoginRequest loginRequest = new Models.LoginRequest();
                    loginRequest.EmailAddress = txtEmailAddress.Text.Trim();
                    loginRequest.Password = txtPassword.Text.Trim();

                    //Save Server URL and API Route
                    Program.serverURL = txtServerURL.Text.Trim();
                    Properties.Settings.Default.ServerURL = Program.serverURL;
                    Properties.Settings.Default.Save();

                    //Create JSON Document
                    var jsonString = JsonConvert.SerializeObject(loginRequest);

                    //Clear Values
                    txtEmailAddress.Clear();
                    txtPassword.Clear();

                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/Login";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, "");
                    var objectResult = JsonConvert.DeserializeObject<Models.LoginResponse>(jsonResult);

                    //Save Return Values
                    Program.identity = Guid.Parse(objectResult.Id.ToString());
                    Program.securityToken = Guid.Parse(objectResult.SecurityToken.ToString());
                    //MessageBox.Show("Your identity is " + Program.identity + " and your security token is " + Program.securityToken, "Information");

                    Views.Home home = new Views.Home();
                    this.Hide();
                    home.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("All fields must be properly filled-in.", "Information");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "404")
                {
                    //No data returned
                }
                else if (ex.Message == "401")
                {
                    MessageBox.Show("The email address or password you entered is either incorrect or this user doesn't exist in the system", "Error");
                }
                else if (ex.Message == "An error occurred while sending the request.")
                {
                    MessageBox.Show("The Moab Platform is unreachable.", "Network Error");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            finally
            {

            }
        }

    }
}
