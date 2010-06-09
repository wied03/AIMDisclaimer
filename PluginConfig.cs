using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;

namespace tylerAIM
{
	/// <summary>
	/// Summary description for PluginConfig.
	/// </summary>

    public class PluginConfig : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_disclaimerText;
		private System.Windows.Forms.Label label3;

        // class attributes

		private ArrayList m_groupNames;
        public String theSelectedBuddy
        {
            get
            {
                return (String) m_comboBuddy.SelectedItem;
            }
            set
            {
                int index = m_comboBuddy.FindString(value);
                m_comboBuddy.SelectedIndex = index;
            }
        }

        public String theDisclaimerMessage
        {
            get
            {
                return m_disclaimerText.Text;
            }
            set
            {
                m_disclaimerText.Text = value;
            }
        }

        public int theTimeInterval
        {
            get
            {
                return (int)m_reminderInterval.Value;
            }
            set
            {
                m_reminderInterval.Value = value;
            }
        }

        // buttons

        private Button m_updateButton;
        private ComboBox m_comboBuddy;
        private Label label4;
        private NumericUpDown m_reminderInterval;
        private Button m_cancelButton;
        private Label label5;
        public bool formSaved;

        private void m_updateButton_Click(object sender, System.EventArgs e)
        {
            formSaved = true;
            this.Close();
        }

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public PluginConfig()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

		public PluginConfig(ArrayList buddyNames,
                            String disclaimerText,
                            String selectedBuddyGroup,
                            int reminderInterval)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            // set the controls based on the parameters
            m_groupNames = buddyNames;
            theTimeInterval = reminderInterval;            
            m_comboBuddy.DataSource = m_groupNames;
            theDisclaimerMessage = disclaimerText;
            theSelectedBuddy = selectedBuddyGroup;

            // set the form buttons
            this.AcceptButton = m_updateButton;
            this.CancelButton = m_cancelButton;

            // add event handlers for the buttons
            m_updateButton.Click += new EventHandler(m_updateButton_Click);
            m_cancelButton.Click += new EventHandler(m_cancelButton_Click);
		}

        void m_cancelButton_Click(object sender, EventArgs e)
        {
            formSaved = false;
            this.Close();
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.m_comboBuddy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_disclaimerText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_updateButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.m_reminderInterval = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_reminderInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(88, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Disclaimer Plugin 1.0";
            // 
            // m_comboBuddy
            // 
            this.m_comboBuddy.Location = new System.Drawing.Point(145, 59);
            this.m_comboBuddy.Name = "m_comboBuddy";
            this.m_comboBuddy.Size = new System.Drawing.Size(152, 21);
            this.m_comboBuddy.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Group to monitor:";
            // 
            // m_disclaimerText
            // 
            this.m_disclaimerText.Location = new System.Drawing.Point(29, 118);
            this.m_disclaimerText.Multiline = true;
            this.m_disclaimerText.Name = "m_disclaimerText";
            this.m_disclaimerText.Size = new System.Drawing.Size(336, 120);
            this.m_disclaimerText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Disclaimer Text:";
            // 
            // m_updateButton
            // 
            this.m_updateButton.Location = new System.Drawing.Point(93, 291);
            this.m_updateButton.Name = "m_updateButton";
            this.m_updateButton.Size = new System.Drawing.Size(75, 23);
            this.m_updateButton.TabIndex = 5;
            this.m_updateButton.Text = "OK";
            this.m_updateButton.UseVisualStyleBackColor = true;
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Location = new System.Drawing.Point(222, 291);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 6;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(90, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Minutes between reminders";
            // 
            // m_reminderInterval
            // 
            this.m_reminderInterval.Location = new System.Drawing.Point(257, 252);
            this.m_reminderInterval.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.m_reminderInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_reminderInterval.Name = "m_reminderInterval";
            this.m_reminderInterval.Size = new System.Drawing.Size(38, 20);
            this.m_reminderInterval.TabIndex = 8;
            this.m_reminderInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 333);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "(C) 2006 Brady Wied";
            // 
            // PluginConfig
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(395, 355);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_reminderInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_updateButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_disclaimerText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_comboBuddy);
            this.Controls.Add(this.label1);
            this.Name = "PluginConfig";
            this.Text = "Configure the Disclaimer Plugin";
            ((System.ComponentModel.ISupportInitialize)(this.m_reminderInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
