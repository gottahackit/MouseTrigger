namespace MouseTrigger
{
    partial class MonitorScreen
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorScreen));
            notifyIcon1 = new NotifyIcon(components);
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 25);
            label1.TabIndex = 0;
            label1.Text = "waiting...";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { ".33", ".5", ".66" });
            comboBox1.Location = new Point(285, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(114, 33);
            comboBox1.TabIndex = 1;
            comboBox1.Text = ".5";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(180, 3);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 2;
            label2.Text = "Slowdown:";
            // 
            // MonitorScreen
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 42);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "MonitorScreen";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Mouse Trigger";
            WindowState = FormWindowState.Minimized;
            Load += MonitorScreen_Load;
            Resize += MonitorScreen_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
    }
}