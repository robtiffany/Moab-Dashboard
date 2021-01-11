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
    public partial class TelemetrySimulator : Form
    {
        public TelemetrySimulator()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdentity.Text != "" && txtSecurityToken.Text != "" && txtPayload.Text != "")
                {
                    //Capture Credentials
                    string credentials = txtIdentity.Text.Trim() + "." + txtSecurityToken.Text.Trim();

                    //Capture Data Payload
                    var jsonString = txtPayload.Text.Trim();
                    
                    //Send Data
                    ClientSDK clientSDK = new ClientSDK();
                    string uriString = Program.serverURL + "/Telemetry";
                    var jsonResult = await clientSDK.Create(uriString, jsonString, credentials);
                    var objectResult = JsonConvert.DeserializeObject<Models.Response>(jsonResult);

                    MessageBox.Show("Telemetry successfully saved in Moab");
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
                    MessageBox.Show("The provided credentials are either incorrect or this entity doesn't exist in the system", "Error");
                }
                else if (ex.Message == "400")
                {
                    MessageBox.Show("The URL may be incorrect or the JSON is improperly formatted", "Error");
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
