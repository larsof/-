using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Threading;

namespace WindowsForms_minimax
{
    public partial class Form3 : Form
    {
        public Form3(Form1 f)
        {
            InitializeComponent();
        }
        public int n;
        public int[] n_array;
        public string[] time=new string[8];
        private void Form3_Load(object sender, EventArgs e)
        {
            //Form1 main = this.Owner as Form1;

            n_array = new int[] { 10, 100, 500, 1000, 3000, 5000, 8000, 10000 };

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowCount =n_array.Length ;
            dataGridView1.ColumnCount = 2;

            dataGridView1.RowHeadersWidth = 90;
            dataGridView1.Columns[0].HeaderText = "Размерность";
            dataGridView1.Columns[1].HeaderText = "Время работы";

            //n_array = new int[] { 10, 100, 500, 1000, 3000, 5000, 8000, 10000, 15000, 16000, 16315 };
            

            int k = 0;
            for (int i = 0; i < n_array.Length; i++)
            {
                dataGridView1.Rows[k].SetValues(n_array[i]);
                k++;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            int b = 0;
            int r=0;

            for (int i=0;i<n_array.Length;i++) 
            {
                string elapsedTime;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                n = n_array[i];
                double[,] start_array = new double[n, n];
                Random rnd = new Random();
                for (int x = 0; x < n; x++)
                {
                    for (int y = 0; y < n; y++)
                    {
                        start_array[x, y] = Convert.ToDouble(rnd.Next(-1000, 1000) / 10.0);
                    }

                }

                double[] max_array = new double[n];
                double a;
                int j = 0;
                for (int x = 0; x < n; x++)
                {
                    a = start_array[x, j];
                    for (int y = 0; y < n; y++)
                    {
                        if (start_array[x, y] > a)
                            a = start_array[x, y];

                    }
                    max_array[x] = a;
                }

                double num = 0;
                int k2 = n;
                for (int d = 0; d < n; d++, k2--)
                {
                    num += max_array[d] * max_array[k2 - 1];
                }


                Thread.Sleep(10000);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);


                time[r] = elapsedTime;
                r++;

                dataGridView1[1, b].Value = elapsedTime;
                b++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            //подписать оси
            Axis ax = new Axis();
            ax.Title = "time";
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "n";
            chart1.ChartAreas[0].AxisY = ay;

            for (int i = 0; i < n_array.Length; i++)
            {
                this.chart1.Series[0].Points.AddXY(time[i], n_array[i]);
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
