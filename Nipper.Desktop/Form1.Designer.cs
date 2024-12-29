namespace Nipper.Desktop
{
    partial class NipperForm
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
            userNips = new TextBox();
            checkNipsButton = new Button();
            menuStrip1 = new MenuStrip();
            StripMenuOptions = new ToolStripMenuItem();
            outputBox = new TextBox();
            nipCheckProgressBar = new ProgressBar();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // userNips
            // 
            userNips.Location = new Point(12, 38);
            userNips.Multiline = true;
            userNips.Name = "userNips";
            userNips.ScrollBars = ScrollBars.Vertical;
            userNips.Size = new Size(128, 75);
            userNips.TabIndex = 0;
            // 
            // checkNipsButton
            // 
            checkNipsButton.Location = new Point(12, 119);
            checkNipsButton.Name = "checkNipsButton";
            checkNipsButton.Size = new Size(75, 23);
            checkNipsButton.TabIndex = 1;
            checkNipsButton.Text = "Check";
            checkNipsButton.UseVisualStyleBackColor = true;
            checkNipsButton.Click += button1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveCaption;
            menuStrip1.Items.AddRange(new ToolStripItem[] { StripMenuOptions });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "stripMenu";
            // 
            // StripMenuOptions
            // 
            StripMenuOptions.Name = "StripMenuOptions";
            StripMenuOptions.Size = new Size(50, 20);
            StripMenuOptions.Text = "Opcje";
            // 
            // outputBox
            // 
            outputBox.Location = new Point(209, 38);
            outputBox.Multiline = true;
            outputBox.Name = "outputBox";
            outputBox.ReadOnly = true;
            outputBox.ScrollBars = ScrollBars.Vertical;
            outputBox.Size = new Size(394, 75);
            outputBox.TabIndex = 4;
            // 
            // nipCheckProgressBar
            // 
            nipCheckProgressBar.ForeColor = Color.Chartreuse;
            nipCheckProgressBar.Location = new Point(209, 139);
            nipCheckProgressBar.Name = "nipCheckProgressBar";
            nipCheckProgressBar.Size = new Size(394, 23);
            nipCheckProgressBar.TabIndex = 5;
            nipCheckProgressBar.Visible = false;
            // 
            // NipperForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(nipCheckProgressBar);
            Controls.Add(outputBox);
            Controls.Add(checkNipsButton);
            Controls.Add(userNips);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "NipperForm";
            Text = "Nipper";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox userNips;
        private Button checkNipsButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem StripMenuOptions;
        private TextBox outputBox;
        private ProgressBar nipCheckProgressBar;
    }
}
