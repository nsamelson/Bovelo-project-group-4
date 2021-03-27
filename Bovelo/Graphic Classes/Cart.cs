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
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace Bovelo
{
    public partial class Cart : Form
    {
        
        public string[] row;
        private User _currentUser;

        private App app = new App();
        internal Cart(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
        }
        private void Cart_Load() 
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (ItemBike elem in _currentUser.cart)
            {
                dataGridView1.Rows.Add();
                int price = elem.getPrice();
                dataGridView1.Rows[i].Cells[0].Value = elem.bike.Type;
                dataGridView1.Rows[i].Cells[1].Value = elem.bike.Size;
                dataGridView1.Rows[i].Cells[2].Value = elem.bike.Color;
                dataGridView1.Rows[i].Cells[3].Value = elem.quantity;
                dataGridView1.Rows[i].Cells[4].Value = "0 in stock";//elem.bike.TotalTime.ToString();
                dataGridView1.Rows[i].Cells[5].Value = price.ToString();
                i++;
            }
            Decimal B = _currentUser.getCartPrice();
            this.labelPrice.Text = B.ToString() + "€";
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            Cart_Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            Console.WriteLine(i);
            Console.WriteLine(_currentUser.cart[i].quantity);
            Console.WriteLine(Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
            Console.WriteLine(Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()).GetType());
            _currentUser.cart[i].quantity = Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            this.Cart_Load();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the Explorerbike Window
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string client = textBox1.Text;        
            int RowCount = _currentUser.getCartList().Count();

            if (client != ""&&RowCount!=0)
            {
                string text ="";
                app.setNewOrderBike(_currentUser.getCartList(), client, _currentUser.getCartPrice());
                dataGridView1.Rows.Clear();
                foreach(var elem in _currentUser.getCartList())
                {                   
                    foreach(var value in elem)
                    {
                        text += value + ";";
                    }
                    text += "\n";
                }
                string path = @"../../Classes/list_part.csv";               
                File.WriteAllText(path, text);
                printInvoice(client);
                _currentUser.emptyCart();
                this.labelPrice.Text = "0€";               
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a valid name or add items to the cart!");
            }
            
        }

        private void printInvoice(string client)
        {
            string FileName = DateTime.Now.ToString();
            FileName = FileName.Replace('/', '_');
            FileName = FileName.Replace(' ', '_');
            FileName = FileName.Replace(':', '_');
            string path = Environment.CurrentDirectory;
            PdfWriter writer = new PdfWriter(@"../../facture/" + client + FileName + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("Bovelo")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));
            document.Add(newline);
            document.Add(header);
            // Add sub-header
            Paragraph subheader = new Paragraph(client)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            document.Add(subheader);
            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Récapitulatif de commande");
            paragraph1.SetTextAlignment(TextAlignment.CENTER);
            document.Add(paragraph1);
            document.Add(ls);
            document.Add(newline);
            // Table
            Table table = new Table(5, false);
            List<string> column = new List<string>();
            column.Add("Bike");
            column.Add("Size");
            column.Add("Color");
            column.Add("Quantity");
            column.Add("Price");
            foreach (var elem in column)
            {
                Cell cell = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetWidth(100)
                    .Add(new Paragraph(elem));
                table.AddCell(cell);
            }
            foreach (var item in _currentUser.getCartList())
            {
                for (int e = 0; e < 5; e++)
                {

                    Cell cell = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(item[e]));
                    table.AddCell(cell);
                }
            }
            document.Add(table);
            //Total
            document.Add(newline);
            document.Add(ls);
            document.Add(newline);
            Paragraph paragraph2 = new Paragraph("Total : " + this.labelPrice.Text);
            paragraph2.SetTextAlignment(TextAlignment.RIGHT);
            document.Add(paragraph2);
            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i + "/" + n)),
                   559, 806, i, TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            _currentUser.emptyCart();
            this.labelPrice.Text = "0€";
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
