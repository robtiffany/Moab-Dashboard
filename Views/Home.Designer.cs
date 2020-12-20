namespace MoabDashboard.Views
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label12 = new System.Windows.Forms.Label();
            this.btnGroups = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnDigitalTwinModels = new System.Windows.Forms.Button();
            this.btnDigitalTwins = new System.Windows.Forms.Button();
            this.btnTelemetry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(191, 37);
            this.label12.TabIndex = 15;
            this.label12.Text = "Moab Home";
            // 
            // btnGroups
            // 
            this.btnGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroups.Location = new System.Drawing.Point(535, 125);
            this.btnGroups.Name = "btnGroups";
            this.btnGroups.Size = new System.Drawing.Size(200, 150);
            this.btnGroups.TabIndex = 16;
            this.btnGroups.Text = "Groups";
            this.btnGroups.UseVisualStyleBackColor = true;
            this.btnGroups.Click += new System.EventHandler(this.btnGroups_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.Location = new System.Drawing.Point(288, 125);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(200, 150);
            this.btnUsers.TabIndex = 17;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnDigitalTwinModels
            // 
            this.btnDigitalTwinModels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDigitalTwinModels.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDigitalTwinModels.Location = new System.Drawing.Point(288, 307);
            this.btnDigitalTwinModels.Name = "btnDigitalTwinModels";
            this.btnDigitalTwinModels.Size = new System.Drawing.Size(200, 150);
            this.btnDigitalTwinModels.TabIndex = 18;
            this.btnDigitalTwinModels.Text = "Digital Twin Models";
            this.btnDigitalTwinModels.UseVisualStyleBackColor = true;
            this.btnDigitalTwinModels.Click += new System.EventHandler(this.btnDigitalTwinModels_Click);
            // 
            // btnDigitalTwins
            // 
            this.btnDigitalTwins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDigitalTwins.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDigitalTwins.Location = new System.Drawing.Point(535, 307);
            this.btnDigitalTwins.Name = "btnDigitalTwins";
            this.btnDigitalTwins.Size = new System.Drawing.Size(200, 150);
            this.btnDigitalTwins.TabIndex = 19;
            this.btnDigitalTwins.Text = "Digital Twins";
            this.btnDigitalTwins.UseVisualStyleBackColor = true;
            this.btnDigitalTwins.Click += new System.EventHandler(this.btnDigitalTwins_Click);
            // 
            // btnTelemetry
            // 
            this.btnTelemetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTelemetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTelemetry.Location = new System.Drawing.Point(288, 484);
            this.btnTelemetry.Name = "btnTelemetry";
            this.btnTelemetry.Size = new System.Drawing.Size(200, 150);
            this.btnTelemetry.TabIndex = 20;
            this.btnTelemetry.Text = "Telemetry";
            this.btnTelemetry.UseVisualStyleBackColor = true;
            this.btnTelemetry.Click += new System.EventHandler(this.btnTelemetry_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.btnTelemetry);
            this.Controls.Add(this.btnDigitalTwins);
            this.Controls.Add(this.btnDigitalTwinModels);
            this.Controls.Add(this.btnUsers);
            this.Controls.Add(this.btnGroups);
            this.Controls.Add(this.label12);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGroups;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnDigitalTwinModels;
        private System.Windows.Forms.Button btnDigitalTwins;
        private System.Windows.Forms.Button btnTelemetry;
    }
}