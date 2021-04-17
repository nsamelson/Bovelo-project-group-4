using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bovelo
{
    public partial class Assembler_MainHome : Form
    {
        private App newApp = new App();

        private User user = new User(" ", false, false, false);
        internal Assembler_MainHome(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
            Assembler_Planning plng = new Assembler_Planning(user);// create new window
            plng.FormClosed += (s, args) => this.Close();
            plng.Show();// Showing the Assembler planning window
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void Assembler_MainHome_Load(object sender, EventArgs e)
        {
            string broken_path = @"../../View/Pictures/broken.png";
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\View\Pictures\Assembler\";
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.png"); //Getting Text files
            string message = "";
            try
            {
                string pictureName = Files.First().Name;
                pictureBox1.Image = Image.FromFile(path + pictureName);
                message = "The program could not find pictures of : '" + pictureName + "' in the directory : " + path;
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);

                var result = MessageBox.Show(this.Owner, message + "\n \n\n Would you like to add pictures?", "No image found!", MessageBoxButtons.YesNo);
                switch (result)
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        Manager.AddNewFolder(path);
                        System.Diagnostics.Process.Start(path);
                        break;
                }

            }
        }
    }
}
