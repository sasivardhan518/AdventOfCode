namespace MMUserCreation
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.serversDropdown = new System.Windows.Forms.ComboBox();
            this.databaseDropdown = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addUserBtn = new System.Windows.Forms.Button();
            this.deleteUserBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name:";
            // 
            // serversDropdown
            // 
            this.serversDropdown.FormattingEnabled = true;
            this.serversDropdown.Location = new System.Drawing.Point(112, 42);
            this.serversDropdown.Name = "serversDropdown";
            this.serversDropdown.Size = new System.Drawing.Size(223, 21);
            this.serversDropdown.TabIndex = 2;
            this.serversDropdown.SelectedIndexChanged += new System.EventHandler(this.serversDropdown_SelectedIndexChanged);
            // 
            // databaseDropdown
            // 
            this.databaseDropdown.FormattingEnabled = true;
            this.databaseDropdown.Location = new System.Drawing.Point(518, 42);
            this.databaseDropdown.Name = "databaseDropdown";
            this.databaseDropdown.Size = new System.Drawing.Size(228, 21);
            this.databaseDropdown.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(425, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Choose DB:";
            // 
            // addUserBtn
            // 
            this.addUserBtn.Location = new System.Drawing.Point(25, 80);
            this.addUserBtn.Name = "addUserBtn";
            this.addUserBtn.Size = new System.Drawing.Size(75, 23);
            this.addUserBtn.TabIndex = 5;
            this.addUserBtn.Text = "Add User";
            this.addUserBtn.UseVisualStyleBackColor = true;
            this.addUserBtn.Click += new System.EventHandler(this.addUserBtn_Click);
            // 
            // deleteUserBtn
            // 
            this.deleteUserBtn.Location = new System.Drawing.Point(130, 79);
            this.deleteUserBtn.Name = "deleteUserBtn";
            this.deleteUserBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteUserBtn.TabIndex = 6;
            this.deleteUserBtn.Text = "Delete User";
            this.deleteUserBtn.UseMnemonic = false;
            this.deleteUserBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 378);
            this.Controls.Add(this.deleteUserBtn);
            this.Controls.Add(this.addUserBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.databaseDropdown);
            this.Controls.Add(this.serversDropdown);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MM User Creation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox serversDropdown;
        private System.Windows.Forms.ComboBox databaseDropdown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addUserBtn;
        private System.Windows.Forms.Button deleteUserBtn;
    }
}

