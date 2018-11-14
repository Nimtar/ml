using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        public const int CELL_SIZE = 10;

        public static Graphics Graphics { get; set; }
        public static SolidBrush MyBrush { get; set; }
        public Perceptron Perceptron { get; set; }
        public static Pen MyPen { get; set; }

        //myPen = new Pen(Color.FromArgb(255, r, g, b)); 

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DRAWING
        /// </summary>
        static void setColor(int r, int g, int b)
        {
            MyBrush = new SolidBrush(Color.FromArgb(255, r, g, b));
            MyPen = new Pen(Color.FromArgb(255, r, g, b));
        }

        /// <summary>
        /// That's all folks!
        /// </summary>

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Perceptron = new Perceptron();
            Graphics = this.CreateGraphics();
            Grid.setGrphcs(Graphics);
            Grid.clearGrid();
            Grid.drawGridOnly();
            //grphcs.DrawRectangle(myPen, 0, 0, 400, 400);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Grid.getInput(e.X, e.Y);
            }
            if (e.Button == MouseButtons.Right)
            {
                Grid.eraseInput(e.X, e.Y);
            }
            Grid.drawGridOnly();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Grid.getInput(e.X, e.Y);
            }
            if (e.Button == MouseButtons.Right)
            {
                Grid.eraseInput(e.X, e.Y);
            }
            Grid.drawGridOnly();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Grid.clearGrid();
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Grid.moveTheDrawedToCorner();
            Grid.scaletheDrawed();
            TeachBtn.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Grid.scaletheDrawed();
        }

        private void RecognizeBtn_Click(object sender, EventArgs e)
        {
            Grid.moveTheDrawedToCorner();
            Grid.scaletheDrawed();
            TeachBtn.Enabled = true;
            var hypothesis = Perceptron.Recognize() ? Perceptron.Category2 : Perceptron.Category1;
            label1.Text = "Я думаю, это " + hypothesis + ".";
            label1.Visible = true;
        }

        private void TeachBtn_Click(object sender, EventArgs e)
        {
            Perceptron.DeltaRule();
            label1.Text += "\nДельта-правило применено.";
            TeachBtn.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, Perceptron.SaveWeight());
            MessageBox.Show("Файл сохранен");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string[] w = System.IO.File.ReadAllText(filename).Split(' ');
            Perceptron.DownloadWeight(System.IO.File.ReadAllText(filename).Split(' '));
            MessageBox.Show("Файл открыт");
        }

        private void learn_button_Click(object sender, EventArgs e)
        {

        }
    }
}
