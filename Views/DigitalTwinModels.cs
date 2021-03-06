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
    public partial class DigitalTwinModels : Form
    {
        public DigitalTwinModels()
        {
            InitializeComponent();
        }

        private async void DigitalTwinModels_Load(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/DigitalTwinModel";

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.DigitalTwinModel>>(jsonResult);

                for (int i = 0; i < objectResult.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(objectResult[i].Id.ToString());
                    listViewItem.SubItems.Add(objectResult[i].Name);
                    listViewItem.SubItems.Add(objectResult[i].Description);
                    listViewItem.SubItems.Add(objectResult[i].Version.ToString());
                    listViewModels.Items.Add(listViewItem);
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
                    Models.DigitalTwinModelRequest digitalTwinModelRequest = new Models.DigitalTwinModelRequest();
                    digitalTwinModelRequest.Name = txtName.Text.Trim();
                    digitalTwinModelRequest.Description = txtDescription.Text.Trim();
                    digitalTwinModelRequest.Version = Convert.ToInt64(numericUpDownVersion.Value);
                    digitalTwinModelRequest.CreatedBy = Program.identity;


                    //Create JSON Document
                    var jsonString = JsonConvert.SerializeObject(digitalTwinModelRequest);

                    //Clear Values
                    txtName.Clear();
                    txtDescription.Clear();
                    numericUpDownVersion.Value = 1;

                    string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/DigitalTwinModel";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                    var objectResult = JsonConvert.DeserializeObject<Models.Response>(jsonResult);

                    //Add to Group List
                    ListViewItem listViewItem = new ListViewItem(objectResult.Id.ToString());
                    listViewItem.SubItems.Add(digitalTwinModelRequest.Name);
                    listViewItem.SubItems.Add(digitalTwinModelRequest.Description);
                    listViewItem.SubItems.Add(digitalTwinModelRequest.Version.ToString());
                    listViewModels.Items.Add(listViewItem);

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
                string uriString = Program.serverURL + "/DigitalTwinModel";
                string id = string.Empty;
                string name = string.Empty;
                string desc = string.Empty;

                if (listViewModels.Items.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewModels.SelectedItems)
                    {
                        //Get Selected Item
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

            if (listViewModels.Items.Count > 0)
            {
                if (listViewModels.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewModels.SelectedItems)
                    {
                        //Get Selected Item
                        id = viewItem.SubItems[0].Text;
                        name = viewItem.SubItems[1].Text;

                        Views.DigitalTwinModelStaticProperties digitalTwinModelStaticProperties = new Views.DigitalTwinModelStaticProperties(id, name);
                        this.Hide();
                        digitalTwinModelStaticProperties.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Select a Digital Twin Model and try again.", "Information");
                }
            }



        }
    }
}
