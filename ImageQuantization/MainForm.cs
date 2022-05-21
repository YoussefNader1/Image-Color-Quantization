using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Graph g = new Graph(ImageMatrix);
            Console.WriteLine(g.distinctColors);

            Prim p = new Prim(g);
            Console.WriteLine(p.MST());
            
            Cluster c = new Cluster(p, g, K);
            ImageMatrix = c.GetQuantizedImage();
            stopwatch.Stop();
            
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            txtColor.Text = g.distinctColors.ToString();
            textBox2.Text = p.MST().ToString();

            ShowTime(stopwatch.ElapsedMilliseconds);
        }

        public static void ShowTime(double time)
        {
            double ms = time;
            double s = time /1000;
            MessageBox.Show("Elapsed Time is { " + ms.ToString() + " } in MiliSecond and { " + s.ToString() + " } in Seconds" );
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}