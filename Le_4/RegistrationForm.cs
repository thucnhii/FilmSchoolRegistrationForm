// Programmer: Hoai Thuc Nhi Le
// Project: Assignment 4
// Date: 4/28/2023
// Description: Hopkins Film Registration Form

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Le_4
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }
        // Declare private constant variable for prices and 
        // maximum number of classes allowed
        private const decimal LIVE_ACTION_PRICE = 79.95m;   // Price per class in Live Action
        private const decimal ANIMATION_PRICE = 99.95m;     // Price per class in Animation
        private const int MAX_CLASSES = 4;

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                // Show the current date
                dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

                // Populate status of registrant combobox with an array
                // Create the array
                string[] statusArray = {"Actor",
                "Producer",
                "Director",
                "Animator",
                "Cinematographer",
                "Drama Teacher",
                "Light Technician",
                "Sound Technician" };

                // Add items in the array to the status combobox
                for (int i = 0; i < statusArray.Length; i++)
                {
                    statusComboBox.Items.Add(statusArray[i]);
                }

                // Set live action as the default class type
                liveActionRadioButton.Checked = true;

                // Display price for classes
                liveActionPriceLabel.Text = "(" + LIVE_ACTION_PRICE.ToString("c") + " per class)";
                animationPriceLabel.Text = "(" + ANIMATION_PRICE.ToString("c") + " per class)";

                //State the maximum number of classes to be chosen
                maxNumClassesLabel.Text = MAX_CLASSES + " classes are chosen at most";

                // Populate the class listbox
                PopulateList();

                // Update total labels in the registration information groupbox
                UpdateTotals();

                // Select cash as the default payment option
                cashRadioButton.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // PopulateList method to populate the listbox for classes
        private void PopulateList()
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                // Clear any items in the listbox
                classListBox.Items.Clear();

                // Sort the items in listbox alphabetically
                classListBox.Sorted = true;

                // A StreamReader variable
                StreamReader inputFile;

                // Variable to hold the string reads from the file
                string classes;

                // If live action is chosen, read the LiveActionClasses.txt file
                // Add items in that txt file to the classes listbox
                if (liveActionRadioButton.Checked == true)
                {
                    inputFile = File.OpenText("LiveActionClasses.txt");
                    while (!inputFile.EndOfStream)
                    {
                        classes = inputFile.ReadLine();
                        classListBox.Items.Add(classes);
                    }
                }

                // If animation is chosen, read the AnimationClasses.txt
                // Add items in that txt file to the classes listbox
                else
                {
                    inputFile = File.OpenText("AnimationClasses.txt");
                    while (!inputFile.EndOfStream)
                    {
                        classes = inputFile.ReadLine();
                        classListBox.Items.Add(classes);
                    }
                }

                // Close the file
                inputFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // UpdateTotals method to fill the summary labels of the registration information groupbox
        private void UpdateTotals()
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                // totalPrice variable to hold the calculated total price
                decimal totalPrice;

                // Number of class selected label
                numSelectedClassLabel.Text = classListBox.SelectedItems.Count.ToString();

                // Price per class label
                // If live action is chosen, display the LIVE_ACTION_PRICE
                if (liveActionRadioButton.Checked == true)
                {
                    totalPrice = LIVE_ACTION_PRICE * classListBox.SelectedItems.Count;
                    priceClassLabel.Text = LIVE_ACTION_PRICE.ToString("c");
                }

                // If animation is chosen, display the ANIMATION_PRICE
                else
                {
                    totalPrice = ANIMATION_PRICE * classListBox.SelectedItems.Count;
                    priceClassLabel.Text = ANIMATION_PRICE.ToString("c");
                }

                // Total Label
                totalLabel.Text = totalPrice.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        // Reset the form to its original state
        private void ResetForm()
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                emailTextBox.Text = "";
                dateOfBirthMaskedTextBox.Text = "";
                statusComboBox.Text = "";
                liveActionRadioButton.Checked = true;
                cashRadioButton.Checked = true;
                receiptCheckBox.Checked = false;
                classListBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Changing class type
        private void liveActionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                // When the class type option is changed, change the class type listbox accordingly.
                PopulateList();

                // When the class type option is changed, update the total labels
                UpdateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Choose classes in the classListBox
        private void classListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                // When the number of selected items changed, update the total labels
                UpdateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        // Click the save button in the menu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try-catch statement to avoid the program from crashing if an error occurs
            try
            {
                // classSelected variable to whole the value of selected class
                string classSelected = "";
                for (int i = 0; i < classListBox.Items.Count; i++)
                {
                    if (classListBox.GetSelected(i))
                    {
                        classSelected += classListBox.Items[i] + "\n";
                    }
                }

                // Payment type variable to hold the payment type
                string paymentType;
                if (cashRadioButton.Checked == true)
                {
                    paymentType = cashRadioButton.Text;
                }
                else
                {
                    paymentType = checkRadioButton.Text;
                }

                // receipt variable to hold the value of whether receipt is chosen or not
                string receipt = "No";
                if (receiptCheckBox.Checked == true)
                {
                    receipt = "Yes";
                }

                // Summary variable to hold the summary for the messagebox and the external file
                string summary;
                summary = "Registration Summary" + "\n" +
                    "Registration Date: " + dateMaskedTextBox.Text + "\n" +
                    "Registrant Name: " + firstNameTextBox.Text + " " + lastNameTextBox.Text + "\n" +
                    "Email Address: " + emailTextBox.Text + "\n" +
                    "Date of Birth: " + dateOfBirthMaskedTextBox.Text + "\n" +
                    "Status: " + statusComboBox.Text + "\n" +
                    "Class Selected: " + "\n" + classSelected +
                    "Number of Class Selected: " + classListBox.SelectedItems.Count + "\n" +
                    "Price per Class: " + priceClassLabel.Text + "\n" +
                    "Total Registration Price: " + totalLabel.Text + "\n" +
                    "Payment Type: " + paymentType + "\n" +
                    "Email Receipt Requested: " + receipt;

                // If first name or last name or email address or date of birth is incomplete or missing
                // Display error message
                if (firstNameTextBox.Text == "" ||
                    lastNameTextBox.Text == "" ||
                    emailTextBox.Text == "" ||
                    !dateOfBirthMaskedTextBox.MaskCompleted)
                {
                    MessageBox.Show("Below information is required:" + "\n" +
                        "First Name" + "\n" +
                        "Last Name" + "\n" +
                        "Email Address" + "\n" +
                        "Date of Birth",
                        "More Information Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                // If the number of classes selected are 0 or larger than 4
                // Show the error message
                else if (classListBox.SelectedIndex == -1 ||
                    classListBox.SelectedItems.Count > 4)
                {
                    MessageBox.Show("One to four class selections are required",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                // If all the requirements are met, display the summary messagebox and
                // Save the summary to the RegistrationData.txt external file
                else
                {
                    // Display the messagebox
                    MessageBox.Show(summary,
                    "Summary",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                    // Save the summary to RegistrationData.txt
                    StreamWriter outputFile;
                    outputFile = File.AppendText("RegistrationData.txt");
                    outputFile.WriteLine(summary + "\n");
                    outputFile.Close();

                    // Reset the form
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Close the form
            DialogResult dialog = MessageBox.Show("Are you sure you wish to exit the program?"
                                                , "Exit"
                                                , MessageBoxButtons.YesNo
                                                , MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the About Form
            AboutForm myAboutForm = new AboutForm();

            // Show the About Form
            myAboutForm.ShowDialog();
        }
    }
}