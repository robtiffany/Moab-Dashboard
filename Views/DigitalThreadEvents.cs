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
    public partial class DigitalThreadEvents : Form
    {
        Guid twinId;
        string twinName = string.Empty;

        public DigitalThreadEvents(string id, string name)
        {
            twinId = Guid.Parse(id);
            twinName = name;

            InitializeComponent();
        }

        private async void DigitalThreadEvents_Load(object sender, EventArgs e)
        {
            try
            {
                string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                //Send Data
                ClientSDK clientSDK = new ClientSDK();
                string uriString = Program.serverURL + "/DigitalThreadEvent/" + twinId;

                var jsonResult = await clientSDK.Read(uriString, credentials);
                var objectResult = JsonConvert.DeserializeObject<List<Models.DigitalThreadEvent>>(jsonResult);

                for (int i = 0; i < objectResult.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(objectResult[i].Id.ToString());
                    listViewItem.SubItems.Add(objectResult[i].Name);
                    listViewItem.SubItems.Add(objectResult[i].Value);
                    listViewEvents.Items.Add(listViewItem);
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


            textBoxDigitalTwin.Text = twinName;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtValue.Text != "")
                {
                    //Capture Values
                    Models.DigitalThreadEventRequest digitalThreadEventRequest = new Models.DigitalThreadEventRequest();
                    digitalThreadEventRequest.Name = txtName.Text.Trim();
                    digitalThreadEventRequest.Value = txtValue.Text.Trim();
                    digitalThreadEventRequest.DigitalTwin = twinId;
                    digitalThreadEventRequest.CreatedBy = Program.identity;

                    //Create JSON Document
                    var jsonString = JsonConvert.SerializeObject(digitalThreadEventRequest);

                    //Clear Values
                    txtName.Clear();
                    txtValue.Clear();

                    string credentials = Program.identity.ToString() + "." + Program.securityToken.ToString();

                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/DigitalThreadEvent";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                    var objectResult = JsonConvert.DeserializeObject<Models.Response>(jsonResult);

                    //Add to Group List
                    ListViewItem listViewItem = new ListViewItem(objectResult.Id.ToString());
                    listViewItem.SubItems.Add(digitalThreadEventRequest.Name);
                    listViewItem.SubItems.Add(digitalThreadEventRequest.Value);
                    listViewEvents.Items.Add(listViewItem);

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
                string uriString = Program.serverURL + "/DigitalThreadEvent";
                string id = string.Empty;
                string name = string.Empty;

                if (listViewEvents.Items.Count > 0)
                {
                    foreach (ListViewItem viewItem in listViewEvents.SelectedItems)
                    {
                        //Get Selected Item
                        id = viewItem.SubItems[0].Text;
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
    }
}

