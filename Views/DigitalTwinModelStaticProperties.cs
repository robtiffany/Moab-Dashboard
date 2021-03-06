﻿using Newtonsoft.Json;
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
    public partial class DigitalTwinModelStaticProperties : Form
    {
        Guid modelId;
        string modelName = string.Empty;

        public DigitalTwinModelStaticProperties(string id, string name)
        {
            modelId = Guid.Parse(id);
            modelName = name;

            InitializeComponent();
        }

        private async void DigitalTwinModelStaticProperties_Load(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/DigitalTwinModelStaticProperty/" + modelId;

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.DigitalTwinModelStaticProperty>>(jsonResult);

                for (int i = 0; i < objectResult.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(objectResult[i].Id.ToString());
                    listViewItem.SubItems.Add(objectResult[i].Name);
                    listViewItem.SubItems.Add(objectResult[i].Value);
                    listViewProperties.Items.Add(listViewItem);
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


            textBoxDigitalTwinModel.Text = modelName;

        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtValue.Text != "")
                {
                    //Capture Values
                    Models.DigitalTwinModelStaticPropertyRequest staticPropertyRequest = new Models.DigitalTwinModelStaticPropertyRequest();
                    staticPropertyRequest.Name = txtName.Text.Trim();
                    staticPropertyRequest.Value = txtValue.Text.Trim();
                    staticPropertyRequest.DigitalTwinModel = modelId;
                    staticPropertyRequest.CreatedBy = Program.identity;

                    //Create JSON Document
                    var jsonString = JsonConvert.SerializeObject(staticPropertyRequest);

                    //Clear Values
                    txtName.Clear();
                    txtValue.Clear();

                    string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/DigitalTwinModelStaticProperty";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                    var objectResult = JsonConvert.DeserializeObject<Models.Response>(jsonResult);

                    //Add to Group List
                    ListViewItem listViewItem = new ListViewItem(objectResult.Id.ToString());
                    listViewItem.SubItems.Add(staticPropertyRequest.Name);
                    listViewItem.SubItems.Add(staticPropertyRequest.Value);
                    listViewProperties.Items.Add(listViewItem);

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
                string uriString = Program.serverURL + "/DigitalTwinModelStaticProperty";
                string id = string.Empty;
                string name = string.Empty;

                if (listViewProperties.Items.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewProperties.SelectedItems)
                    {
                        //Get Selected Item
                        id = viewItem.SubItems[0].Text;
                        var jsonResult = await clientSDK.Delete(uriString + "/" + id + "/" + modelId, credentials);
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
