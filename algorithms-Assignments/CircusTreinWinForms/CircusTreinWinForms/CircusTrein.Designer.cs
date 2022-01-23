
namespace CircusTreinWinForms
{
    partial class CircusTrein
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
            this.wagons = new System.Windows.Forms.ListBox();
            this.addedAnimals = new System.Windows.Forms.ListBox();
            this.lblCircusTrein = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.CBWeight = new System.Windows.Forms.ComboBox();
            this.GBAddAnimals = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.RBHerbivore = new System.Windows.Forms.RadioButton();
            this.lblType = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.RBCarnivore = new System.Windows.Forms.RadioButton();
            this.lblName = new System.Windows.Forms.Label();
            this.lblWagons = new System.Windows.Forms.Label();
            this.lblAddedAnimal = new System.Windows.Forms.Label();
            this.GBAddAnimals.SuspendLayout();
            this.SuspendLayout();
            // 
            // wagons
            // 
            this.wagons.FormattingEnabled = true;
            this.wagons.ItemHeight = 29;
            this.wagons.Location = new System.Drawing.Point(12, 81);
            this.wagons.Name = "wagons";
            this.wagons.Size = new System.Drawing.Size(293, 352);
            this.wagons.TabIndex = 0;
            // 
            // addedAnimals
            // 
            this.addedAnimals.FormattingEnabled = true;
            this.addedAnimals.ItemHeight = 29;
            this.addedAnimals.Location = new System.Drawing.Point(349, 81);
            this.addedAnimals.Name = "addedAnimals";
            this.addedAnimals.Size = new System.Drawing.Size(616, 352);
            this.addedAnimals.TabIndex = 1;
            // 
            // lblCircusTrein
            // 
            this.lblCircusTrein.AutoSize = true;
            this.lblCircusTrein.Font = new System.Drawing.Font("Microsoft JhengHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCircusTrein.Location = new System.Drawing.Point(339, 0);
            this.lblCircusTrein.Name = "lblCircusTrein";
            this.lblCircusTrein.Size = new System.Drawing.Size(259, 50);
            this.lblCircusTrein.TabIndex = 2;
            this.lblCircusTrein.Text = "Circus Trein ";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(219, 64);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 38);
            this.txtName.TabIndex = 3;
            // 
            // CBWeight
            // 
            this.CBWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBWeight.FormattingEnabled = true;
            this.CBWeight.Items.AddRange(new object[] {
            "1- Small",
            "3- Medium",
            "5- Large"});
            this.CBWeight.Location = new System.Drawing.Point(219, 134);
            this.CBWeight.Name = "CBWeight";
            this.CBWeight.Size = new System.Drawing.Size(300, 37);
            this.CBWeight.TabIndex = 4;
            // 
            // GBAddAnimals
            // 
            this.GBAddAnimals.Controls.Add(this.btnAdd);
            this.GBAddAnimals.Controls.Add(this.RBHerbivore);
            this.GBAddAnimals.Controls.Add(this.lblType);
            this.GBAddAnimals.Controls.Add(this.lblWeight);
            this.GBAddAnimals.Controls.Add(this.RBCarnivore);
            this.GBAddAnimals.Controls.Add(this.lblName);
            this.GBAddAnimals.Controls.Add(this.CBWeight);
            this.GBAddAnimals.Controls.Add(this.txtName);
            this.GBAddAnimals.Location = new System.Drawing.Point(12, 439);
            this.GBAddAnimals.Name = "GBAddAnimals";
            this.GBAddAnimals.Size = new System.Drawing.Size(953, 245);
            this.GBAddAnimals.TabIndex = 5;
            this.GBAddAnimals.TabStop = false;
            this.GBAddAnimals.Text = "Add Animals";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Black;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(644, 169);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(196, 55);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // RBHerbivore
            // 
            this.RBHerbivore.AutoSize = true;
            this.RBHerbivore.Location = new System.Drawing.Point(753, 105);
            this.RBHerbivore.Name = "RBHerbivore";
            this.RBHerbivore.Size = new System.Drawing.Size(144, 33);
            this.RBHerbivore.TabIndex = 10;
            this.RBHerbivore.TabStop = true;
            this.RBHerbivore.Text = "Herbivore";
            this.RBHerbivore.UseVisualStyleBackColor = true;
            this.RBHerbivore.CheckedChanged += new System.EventHandler(this.RBHerbivore_CheckedChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(633, 51);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(186, 29);
            this.lblType.TabIndex = 8;
            this.lblType.Text = "Type of Animal:";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(6, 137);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(213, 29);
            this.lblWeight.TabIndex = 6;
            this.lblWeight.Text = "Weight of Animal:";
            // 
            // RBCarnivore
            // 
            this.RBCarnivore.AutoSize = true;
            this.RBCarnivore.Location = new System.Drawing.Point(574, 105);
            this.RBCarnivore.Name = "RBCarnivore";
            this.RBCarnivore.Size = new System.Drawing.Size(140, 33);
            this.RBCarnivore.TabIndex = 9;
            this.RBCarnivore.TabStop = true;
            this.RBCarnivore.Text = "Carnivore";
            this.RBCarnivore.UseVisualStyleBackColor = true;
            this.RBCarnivore.CheckedChanged += new System.EventHandler(this.RBCarnivore_CheckedChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 67);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(199, 29);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name of Animal:";
            // 
            // lblWagons
            // 
            this.lblWagons.AutoSize = true;
            this.lblWagons.Location = new System.Drawing.Point(112, 49);
            this.lblWagons.Name = "lblWagons";
            this.lblWagons.Size = new System.Drawing.Size(105, 29);
            this.lblWagons.TabIndex = 12;
            this.lblWagons.Text = "Wagons";
            // 
            // lblAddedAnimal
            // 
            this.lblAddedAnimal.AutoSize = true;
            this.lblAddedAnimal.Location = new System.Drawing.Point(604, 49);
            this.lblAddedAnimal.Name = "lblAddedAnimal";
            this.lblAddedAnimal.Size = new System.Drawing.Size(168, 29);
            this.lblAddedAnimal.TabIndex = 13;
            this.lblAddedAnimal.Text = "Added animal";
            // 
            // CircusTrein
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(977, 696);
            this.Controls.Add(this.lblAddedAnimal);
            this.Controls.Add(this.lblWagons);
            this.Controls.Add(this.GBAddAnimals);
            this.Controls.Add(this.lblCircusTrein);
            this.Controls.Add(this.addedAnimals);
            this.Controls.Add(this.wagons);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "CircusTrein";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circus Trein ";
            this.GBAddAnimals.ResumeLayout(false);
            this.GBAddAnimals.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox wagons;
        private System.Windows.Forms.ListBox addedAnimals;
        private System.Windows.Forms.Label lblCircusTrein;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox CBWeight;
        private System.Windows.Forms.GroupBox GBAddAnimals;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.RadioButton RBCarnivore;
        private System.Windows.Forms.RadioButton RBHerbivore;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblWagons;
        private System.Windows.Forms.Label lblAddedAnimal;
    }
}

