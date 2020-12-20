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
    public partial class Groups : Form
    {
        public Groups()
        {
            InitializeComponent();
        }

        private async void btnCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupName.Text != "" && txtGroupDescription.Text != "")
                {
                    //Capture Values
                    Models.GroupRequest groupRequest = new Models.GroupRequest();
                    groupRequest.Name = txtGroupName.Text.Trim();
                    groupRequest.Description = txtGroupDescription.Text.Trim();

                    //Create JSON Document
                    var jsonString = JsonConvert.SerializeObject(groupRequest);

                    //Clear Values
                    txtGroupName.Clear();
                    txtGroupDescription.Clear();

                    string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString(); 
                    
                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/Group";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                    var objectResult = JsonConvert.DeserializeObject<Models.Response>(jsonResult);

                    //Add to Group List
                    ListViewItem listViewItem = new ListViewItem(objectResult.Id.ToString());
                    listViewItem.SubItems.Add(groupRequest.Name);
                    listViewItem.SubItems.Add(groupRequest.Description);
                    listViewGroups.Items.Add(listViewItem);

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

        private async void Groups_Load(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/Group";
                
                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.Group>>(jsonResult);

                for (int i = 0; i < objectResult.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(objectResult[i].Id.ToString());
                    listViewItem.SubItems.Add(objectResult[i].Name);
                    listViewItem.SubItems.Add(objectResult[i].Description);
                    listViewGroups.Items.Add(listViewItem);
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

        private async void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/Group";
                string groupId = string.Empty;
                string groupName = string.Empty;
                string groupDesc = string.Empty;

                if (listViewGroups.Items.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewGroups.SelectedItems)
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
