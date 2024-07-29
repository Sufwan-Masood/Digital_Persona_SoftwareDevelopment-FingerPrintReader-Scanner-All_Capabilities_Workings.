namespace DigitalPersona_app
{
    partial class DP_Enterance
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            checkLabel = new Label();
            button1 = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(18, 18);
            label1.Name = "label1";
            label1.Size = new Size(785, 58);
            label1.TabIndex = 0;
            label1.Text = "Welcome To Digital Persona App";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 14F, FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(99, 126);
            label2.Name = "label2";
            label2.Size = new Size(208, 34);
            label2.TabIndex = 1;
            label2.Text = "Select Reader";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.GradientActiveCaption;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(313, 127);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(440, 33);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectionChangeCommitted;
            // 
            // checkLabel
            // 
            checkLabel.AutoSize = true;
            checkLabel.Font = new Font("Century Gothic", 9F, FontStyle.Italic, GraphicsUnit.Point);
            checkLabel.Location = new Point(79, 385);
            checkLabel.Name = "checkLabel";
            checkLabel.Size = new Size(20, 21);
            checkLabel.TabIndex = 3;
            checkLabel.Text = "4";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.Location = new Point(313, 193);
            button1.Name = "button1";
            button1.Size = new Size(144, 34);
            button1.TabIndex = 4;
            button1.Text = "Capabilites";
            button1.UseVisualStyleBackColor = false;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientActiveCaption;
            button2.Location = new Point(552, 193);
            button2.Name = "button2";
            button2.Size = new Size(144, 34);
            button2.TabIndex = 5;
            button2.Text = "Capture";
            button2.UseVisualStyleBackColor = false;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(653, 269);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 137);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // DP_Enterance
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(815, 450);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkLabel);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "DP_Enterance";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DP_Enterance";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private Label checkLabel;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
    }
}