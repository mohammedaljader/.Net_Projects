using CircusTrain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircusTreinWinForms
{
    public partial class CircusTrein : Form
    {
        private Train train = new Train();
        private Diet diet;

        public CircusTrein()
        {
            InitializeComponent();
        }

        //Update the interfaces
        public void UpdateListBox()
        {
            wagons.Items.Clear();
            addedAnimals.Items.Clear();
            foreach (Animal animal in train.Animals)
            {
                addedAnimals.Items.Add(animal);
            }
            foreach (Wagon wagon in train.Wagons)
            {
                wagons.Items.Add(wagon);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckForDataBeforeStart();
            try
            {
                int toEnum = Convert.ToInt32(CBWeight.Text.Substring(0, 1));
                //Create and add an animal to a wagon
                if (train.AddAnimalToTrain(txtName.Text, (Weight) toEnum, diet))
                {
                    UpdateListBox();
                    //
                }
                //Indicate a new wagon was generated
                else
                {
                    MessageBox.Show("New wagon generated!", "Information", MessageBoxButtons.OK , MessageBoxIcon.Information);
                    UpdateListBox();
                }
            }
            //Prevents the programm from crashing when fields are left empty
            catch
            {
                MessageBox.Show("Something went wrong did you fill in al the fields?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //check if the data has been entered or not
        private void CheckForDataBeforeStart()
        {
            if(txtName.Text == string.Empty)
            {
                MessageBox.Show("You have to enter the name of the animal!!", "Something is missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(CBWeight.SelectedIndex < 0)
            {
                MessageBox.Show("You have to choose the weight of the animal!!", "Something is missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!RBCarnivore.Checked && !RBHerbivore.Checked)
            {
                MessageBox.Show("You have to choose the type of the animal!!","Something is missing" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
                return;
            }
        }

        private void RBCarnivore_CheckedChanged(object sender, EventArgs e)
        {
            diet = Diet.Carnivore;
        }

        private void RBHerbivore_CheckedChanged(object sender, EventArgs e)
        {
            diet = Diet.Herbivore;
        }
    }
}
