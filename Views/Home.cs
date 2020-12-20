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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            Views.Groups groups = new Views.Groups();
            this.Hide();
            groups.ShowDialog();
            this.Show();

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Views.Users users = new Views.Users();
            this.Hide();
            users.ShowDialog();
            this.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void btnDigitalTwinModels_Click(object sender, EventArgs e)
        {
            Views.DigitalTwinModels digitalTwinModels = new Views.DigitalTwinModels();
            this.Hide();
            digitalTwinModels.ShowDialog();
            this.Show();
        }

        private void btnDigitalTwins_Click(object sender, EventArgs e)
        {
            Views.DigitalTwins digitalTwins = new Views.DigitalTwins();
            this.Hide();
            digitalTwins.ShowDialog();
            this.Show();
        }

        private void btnTelemetry_Click(object sender, EventArgs e)
        {
            Views.Telemetry telemetry = new Views.Telemetry();
            this.Hide();
            telemetry.ShowDialog();
            this.Show();
        }
    }
}
