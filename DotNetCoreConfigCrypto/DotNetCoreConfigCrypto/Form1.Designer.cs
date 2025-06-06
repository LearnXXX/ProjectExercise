namespace DotNetCoreConfigCrypto
{
    partial class Form1
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
            fileLoader = new Button();
            providerNameText = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // fileLoader
            // 
            fileLoader.Font = new Font("Yu Gothic UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            fileLoader.Location = new Point(126, 120);
            fileLoader.Name = "fileLoader";
            fileLoader.Size = new Size(188, 93);
            fileLoader.TabIndex = 0;
            fileLoader.Text = "Load Config File";
            fileLoader.UseVisualStyleBackColor = true;
            fileLoader.Click += button1_Click;
            // 
            // providerNameText
            // 
            providerNameText.AcceptsTab = true;
            providerNameText.Location = new Point(231, 60);
            providerNameText.Name = "providerNameText";
            providerNameText.Size = new Size(231, 23);
            providerNameText.TabIndex = 1;
            providerNameText.Text = "PRODProvider";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(101, 63);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 2;
            label1.Text = "Provider Name:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(providerNameText);
            Controls.Add(fileLoader);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button fileLoader;
        private TextBox providerNameText;
        private Label label1;
    }
}