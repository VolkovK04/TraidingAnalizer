using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Draw();
        }
        List<Candle> data;
        Code code = Code.GAZP;
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            //string fileName = code.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            string fileName = code.ToString() + ".txt";
            DataRequester.GetDataFile(fileName, code, startDate, endDate, Period.m15);

        }
        private void Draw()
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            img.Draw(data);
            pictureBox1.Image = img;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            data = DataRequester.ToDataList(code.ToString()+".txt");
            Draw();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            data = DataRequester.ToDataList(code.ToString() + ".txt");
            Draw();
        }
    }
}
