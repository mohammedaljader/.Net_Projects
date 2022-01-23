namespace ContainerVervoer.Forms
{
    partial class LogisticsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogisticsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.containerType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfirmContainerButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.addContainerWeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DeleteContainerButton = new System.Windows.Forms.Button();
            this.SortButton = new System.Windows.Forms.Button();
            this.cargoDeckBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ShipSizeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GMDButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.containerType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ConfirmContainerButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.addContainerWeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(95, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Container";
            // 
            // containerType
            // 
            this.containerType.FormattingEnabled = true;
            this.containerType.Location = new System.Drawing.Point(19, 66);
            this.containerType.Name = "containerType";
            this.containerType.Size = new System.Drawing.Size(150, 21);
            this.containerType.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "ContainerType:";
            // 
            // ConfirmContainerButton
            // 
            this.ConfirmContainerButton.Location = new System.Drawing.Point(38, 93);
            this.ConfirmContainerButton.Name = "ConfirmContainerButton";
            this.ConfirmContainerButton.Size = new System.Drawing.Size(108, 23);
            this.ConfirmContainerButton.TabIndex = 6;
            this.ConfirmContainerButton.Text = "Add Container";
            this.ConfirmContainerButton.UseVisualStyleBackColor = true;
            this.ConfirmContainerButton.Click += new System.EventHandler(this.ConfirmContainerButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ton";
            // 
            // addContainerWeight
            // 
            this.addContainerWeight.Location = new System.Drawing.Point(66, 19);
            this.addContainerWeight.Name = "addContainerWeight";
            this.addContainerWeight.Size = new System.Drawing.Size(71, 20);
            this.addContainerWeight.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Weight:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DeleteContainerButton);
            this.groupBox2.Controls.Add(this.SortButton);
            this.groupBox2.Controls.Add(this.cargoDeckBox);
            this.groupBox2.Location = new System.Drawing.Point(4, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 219);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Container List";
            // 
            // DeleteContainerButton
            // 
            this.DeleteContainerButton.Location = new System.Drawing.Point(215, 190);
            this.DeleteContainerButton.Name = "DeleteContainerButton";
            this.DeleteContainerButton.Size = new System.Drawing.Size(68, 22);
            this.DeleteContainerButton.TabIndex = 3;
            this.DeleteContainerButton.Text = "Delete";
            this.DeleteContainerButton.UseVisualStyleBackColor = true;
            this.DeleteContainerButton.Click += new System.EventHandler(this.DeleteContainerButton_Click);
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(9, 191);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(92, 22);
            this.SortButton.TabIndex = 2;
            this.SortButton.Text = "Sort Containers";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // cargoDeckBox
            // 
            this.cargoDeckBox.FormattingEnabled = true;
            this.cargoDeckBox.Location = new System.Drawing.Point(8, 23);
            this.cargoDeckBox.Name = "cargoDeckBox";
            this.cargoDeckBox.Size = new System.Drawing.Size(274, 160);
            this.cargoDeckBox.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ShipSizeLabel);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(90, 78);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CargoShip";
            // 
            // ShipSizeLabel
            // 
            this.ShipSizeLabel.AutoSize = true;
            this.ShipSizeLabel.Location = new System.Drawing.Point(6, 50);
            this.ShipSizeLabel.Name = "ShipSizeLabel";
            this.ShipSizeLabel.Size = new System.Drawing.Size(37, 13);
            this.ShipSizeLabel.TabIndex = 2;
            this.ShipSizeLabel.Text = "(SIZE)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Deck Size:";
            // 
            // GMDButton
            // 
            this.GMDButton.Location = new System.Drawing.Point(4, 87);
            this.GMDButton.Name = "GMDButton";
            this.GMDButton.Size = new System.Drawing.Size(75, 23);
            this.GMDButton.TabIndex = 3;
            this.GMDButton.Text = "GMD";
            this.GMDButton.UseVisualStyleBackColor = true;
            this.GMDButton.Click += new System.EventHandler(this.GMDButton_Click);
            // 
            // LogisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(299, 347);
            this.Controls.Add(this.GMDButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogisticsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = " ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ConfirmContainerButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.Button DeleteContainerButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox addContainerWeight;
        public System.Windows.Forms.ListBox cargoDeckBox;
        public System.Windows.Forms.Label ShipSizeLabel;
        private System.Windows.Forms.ComboBox containerType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GMDButton;
    }
}