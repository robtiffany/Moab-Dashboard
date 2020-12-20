using MoabDashboard.Models;
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
    public partial class DigitalTwins : Form
    {
        public DigitalTwins()
        {
            InitializeComponent();
        }

        private void DigitalTwins_Load(object sender, EventArgs e)
        {
            string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();
            ClientSDK clientSDK = new ClientSDK();

            RetrieveGroups(credentials, clientSDK);

            RetrieveModels(credentials, clientSDK);

            RetrieveTwins(credentials, clientSDK);



        }

        private async void RetrieveGroups(string credentials, ClientSDK clientSDK)
        {
            try
            {
                //Send Data
                string uriString = Program.serverURL + "/Group";

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.Group>>(jsonResult);

                comboBoxGroup.DisplayMember = "Name";
                comboBoxGroup.ValueMember = "Id";
                comboBoxGroup.DataSource = objectResult;

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


        private async void RetrieveModels(string credentials, ClientSDK clientSDK)
        {
            try
            {
                //Send Data
                string uriString = Program.serverURL + "/DigitalTwinModel";

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.DigitalTwinModel>>(jsonResult);

                comboBoxDigitalTwinModel.DisplayMember = "Name";
                comboBoxDigitalTwinModel.ValueMember = "Id";
                comboBoxDigitalTwinModel.DataSource = objectResult;

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

        private async void RetrieveTwins(string credentials, ClientSDK clientSDK)
        {
            try
            {
                //Send Data
                string uriString = Program.serverURL + "/DigitalTwin";

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.DigitalTwinLimitedResponse>>(jsonResult);

                for (int i = 0; i < objectResult.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(objectResult[i].Id.ToString());
                    listViewItem.SubItems.Add(objectResult[i].Name);
                    listViewItem.SubItems.Add(objectResult[i].Description);
                    listViewTwins.Items.Add(listViewItem);
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

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtDescription.Text != "")
                {
                    //Capture Values
                    Models.DigitalTwinRequest digitalTwinRequest = new Models.DigitalTwinRequest();
                    digitalTwinRequest.Name = txtName.Text.Trim();
                    digitalTwinRequest.Description = txtDescription.Text.Trim();
                    digitalTwinRequest.DigitalTwinModel = (comboBoxDigitalTwinModel.SelectedItem as Models.DigitalTwinModel).Id;
                    digitalTwinRequest.Group = (comboBoxGroup.SelectedItem as Models.Group).Id;

                    if (checkBoxEnabled.Checked)
                    {
                        digitalTwinRequest.Enabled = 1;
                    }
                    else
                    {
                        digitalTwinRequest.Enabled = 0;
                    }

                    //Create JSON Document
                    var jsonString = JsonConvert.SerializeObject(digitalTwinRequest);

                    //Clear Values
                    txtName.Clear();
                    txtDescription.Clear();
                    comboBoxDigitalTwinModel.SelectedIndex = 0;
                    comboBoxGroup.SelectedIndex = 0;
                    checkBoxEnabled.Checked = false;

                    string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/DigitalTwin";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                    var objectResult = JsonConvert.DeserializeObject<Models.DigitalTwinResponse>(jsonResult);

                    //Add to Twin List
                    ListViewItem listViewItem = new ListViewItem(objectResult.Id.ToString());
                    listViewItem.SubItems.Add(digitalTwinRequest.Name);
                    listViewItem.SubItems.Add(digitalTwinRequest.Description);
                    listViewTwins.Items.Add(listViewItem);

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

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/DigitalTwin";
                string id = string.Empty;
                string name = string.Empty;
                string desc = string.Empty;

                if (listViewTwins.Items.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewTwins.SelectedItems)
                    {
                        //Get Selected Group
                        id = viewItem.SubItems[0].Text;
                        name = viewItem.SubItems[1].Text;
                        desc = viewItem.SubItems[2].Text;

                        var jsonResult = await clientSDK.Delete(uriString + "/" + id, credentials);
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

        private void btnStaticProperties_Click(object sender, EventArgs e)
        {
            string id = string.Empty;
            string name = string.Empty;

            if (listViewTwins.Items.Count > 0)
            {
                if (listViewTwins.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewTwins.SelectedItems)
                    {
                        //Get Selected Item
                        id = viewItem.SubItems[0].Text;
                        name = viewItem.SubItems[1].Text;

                        Views.DigitalTwinStaticProperties digitalTwinStaticProperties = new Views.DigitalTwinStaticProperties(id, name);
                        this.Hide();
                        digitalTwinStaticProperties.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Select a Digital Twin and try again.", "Information");
                }
            }
        }
    }


}
