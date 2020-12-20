using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoabDashboard.Views
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private async void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstName.Text != "" && txtLastName.Text != "" && txtDescription.Text != "" && txtEmailAddress.Text != "" && txtPassword.Text != "" && txtPasswordRetype.Text != "")
                {
                    if(txtPassword.Text.Trim() == txtPasswordRetype.Text.Trim())
                    {
                        //Capture Values
                        Models.UserRequest userRequest = new Models.UserRequest();
                        userRequest.FirstName = txtFirstName.Text.Trim();
                        userRequest.LastName = txtLastName.Text.Trim();
                        userRequest.Description = txtDescription.Text.Trim();
                        userRequest.EmailAddress = txtEmailAddress.Text.Trim();
                        userRequest.Password = txtPassword.Text.Trim();

                        //Create JSON Document
                        var jsonString = JsonConvert.SerializeObject(userRequest);

                        //Clear Values
                        txtFirstName.Clear();
                        txtLastName.Clear();
                        txtDescription.Clear();
                        txtEmailAddress.Clear();
                        txtPassword.Clear();
                        txtPasswordRetype.Clear();

                        string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                        //Send Data
                        ClientSDK clientSDK = new ClientSDK();
                        string uriString = Program.serverURL + "/User";
                        var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                        var objectResult = JsonConvert.DeserializeObject<Models.UserResponse>(jsonResult);

                        //Add to User List
                        ListViewItem listViewItem = new ListViewItem(objectResult.Id.ToString());
                        listViewItem.SubItems.Add(userRequest.FirstName);
                        listViewItem.SubItems.Add(userRequest.LastName);
                        listViewItem.SubItems.Add(userRequest.Description);
                        listViewUsers.Items.Add(listViewItem);
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

        private async void Users_Load(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/User";

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.User>>(jsonResult);

                for (int i = 0; i < objectResult.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(objectResult[i].Id.ToString());
                    listViewItem.SubItems.Add(objectResult[i].FirstName);
                    listViewItem.SubItems.Add(objectResult[i].LastName);
                    listViewItem.SubItems.Add(objectResult[i].Description);
                    listViewUsers.Items.Add(listViewItem);
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

        private async void btnDeleteUser_Click(object sender, EventArgs e)
        {

            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/User";
                string groupId = string.Empty;
                string groupName = string.Empty;
                string groupDesc = string.Empty;

                if (listViewUsers.Items.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewUsers.SelectedItems)
                    {
                        //Get Selected Group
                        groupId = viewItem.SubItems[0].Text;
                        groupName = viewItem.SubItems[1].Text;
                        groupDesc = viewItem.SubItems[2].Text;
                        var jsonResult = await clientSDK.Delete(uriString + "/" + groupId, credentials);
                        var objectResult = JsonConvert.DeserializeObject<Models.Response>(jsonResult);
                        if (objectResult.Status == "success")
                        {
                            viewItem.Remove();
                        }
                    }



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
