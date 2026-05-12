namespace GreenLifeStore.UI
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.register_loginBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.register_fullName = new System.Windows.Forms.TextBox();
            this.labelFullName = new System.Windows.Forms.Label();
            this.register_email = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.register_username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.register_password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.register_cPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.register_showPass = new System.Windows.Forms.CheckBox();
            this.register_btn = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGreen;
            this.panel1.Controls.Add(this.register_loginBtn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 600);
            this.panel1.TabIndex = 0;
            // 
            // register_loginBtn
            // 
            this.register_loginBtn.BackColor = System.Drawing.Color.ForestGreen;
            this.register_loginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.register_loginBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.register_loginBtn.ForeColor = System.Drawing.Color.White;
            this.register_loginBtn.Location = new System.Drawing.Point(68, 531);
            this.register_loginBtn.Name = "register_loginBtn";
            this.register_loginBtn.Size = new System.Drawing.Size(271, 40);
            this.register_loginBtn.TabIndex = 10;
            this.register_loginBtn.Text = "SIGN IN";
            this.register_loginBtn.UseVisualStyleBackColor = false;
            this.register_loginBtn.Click += new System.EventHandler(this.register_loginBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(77, 487);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(262, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "Already have an account?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.2F);
            this.label2.ForeColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(543, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "REGISTER";
            // 
            // register_fullName
            // 
            this.register_fullName.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_fullName.Location = new System.Drawing.Point(450, 105);
            this.register_fullName.Name = "register_fullName";
            this.register_fullName.Size = new System.Drawing.Size(350, 30);
            this.register_fullName.TabIndex = 4;
            // 
            // labelFullName
            // 
            this.labelFullName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.labelFullName.Location = new System.Drawing.Point(450, 80);
            this.labelFullName.Name = "labelFullName";
            this.labelFullName.Size = new System.Drawing.Size(128, 22);
            this.labelFullName.TabIndex = 3;
            this.labelFullName.Text = "Full Name:";
            // 
            // register_email
            // 
            this.register_email.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_email.Location = new System.Drawing.Point(450, 165);
            this.register_email.Name = "register_email";
            this.register_email.Size = new System.Drawing.Size(350, 30);
            this.register_email.TabIndex = 6;
            // 
            // labelEmail
            // 
            this.labelEmail.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.labelEmail.Location = new System.Drawing.Point(450, 140);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(128, 22);
            this.labelEmail.TabIndex = 5;
            this.labelEmail.Text = "Email:";
            // 
            // register_username
            // 
            this.register_username.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_username.Location = new System.Drawing.Point(450, 225);
            this.register_username.Name = "register_username";
            this.register_username.Size = new System.Drawing.Size(350, 30);
            this.register_username.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.label3.Location = new System.Drawing.Point(450, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Username:";
            // 
            // register_password
            // 
            this.register_password.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_password.Location = new System.Drawing.Point(450, 285);
            this.register_password.Name = "register_password";
            this.register_password.PasswordChar = '*';
            this.register_password.Size = new System.Drawing.Size(350, 30);
            this.register_password.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.label4.Location = new System.Drawing.Point(450, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // register_cPassword
            // 
            this.register_cPassword.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register_cPassword.Location = new System.Drawing.Point(450, 345);
            this.register_cPassword.Name = "register_cPassword";
            this.register_cPassword.PasswordChar = '*';
            this.register_cPassword.Size = new System.Drawing.Size(350, 30);
            this.register_cPassword.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.label7.Location = new System.Drawing.Point(450, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 28);
            this.label7.TabIndex = 11;
            this.label7.Text = "Confirm Password:";
            // 
            // register_showPass
            // 
            this.register_showPass.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.register_showPass.Location = new System.Drawing.Point(450, 390);
            this.register_showPass.Name = "register_showPass";
            this.register_showPass.Size = new System.Drawing.Size(208, 24);
            this.register_showPass.TabIndex = 13;
            this.register_showPass.Text = "Show Password";
            this.register_showPass.CheckedChanged += new System.EventHandler(this.register_showPass_CheckedChanged);
            // 
            // register_btn
            // 
            this.register_btn.BackColor = System.Drawing.Color.DarkGreen;
            this.register_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.register_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.register_btn.ForeColor = System.Drawing.Color.White;
            this.register_btn.Location = new System.Drawing.Point(534, 450);
            this.register_btn.Name = "register_btn";
            this.register_btn.Size = new System.Drawing.Size(200, 45);
            this.register_btn.TabIndex = 14;
            this.register_btn.Text = "SIGN UP";
            this.register_btn.UseVisualStyleBackColor = false;
            this.register_btn.Click += new System.EventHandler(this.register_btn_Click);
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.close.ForeColor = System.Drawing.Color.Black;
            this.close.Location = new System.Drawing.Point(885, 10);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(25, 28);
            this.close.TabIndex = 1;
            this.close.Text = "X";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::GreenLifeStore.Properties.Resources.greenlife_logo2;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 484);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // RegisterForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(920, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelFullName);
            this.Controls.Add(this.register_fullName);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.register_email);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.register_username);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.register_password);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.register_cPassword);
            this.Controls.Add(this.register_showPass);
            this.Controls.Add(this.register_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GreenLife Register";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox register_fullName;
        private System.Windows.Forms.Label labelFullName;
        private System.Windows.Forms.TextBox register_email;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox register_username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox register_password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox register_cPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox register_showPass;
        private System.Windows.Forms.Button register_btn;
        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Button register_loginBtn;
    }
}
