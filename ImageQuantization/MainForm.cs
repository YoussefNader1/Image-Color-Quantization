using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            
        }

        private void btnQuantizedImage_Click(object sender, EventArgs e)
        {
            //OUR CODE ------------------------------------------------------

            //Getting desired number of clusters
            int K = int.Parse(textBox1.Text);

            // Starting the quantization process

            Graph g = new Graph(ImageMatrix);
            Console.WriteLine(g.distinctColors);

            Prim p = new Prim(g);
            Console.WriteLine(p.MST());

            Cluster c = new Cluster(p, g, K);
            ImageMatrix = c.GetQuantizedImage();

            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}