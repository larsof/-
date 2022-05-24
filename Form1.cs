using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Diagnostics;
using System.Threading;


namespace WindowsForms_minimax
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("Добро пожаловать.\nДанная программа является решением минимаксной задачи.\nПодробное описанием в Справке ");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public int n;
        private void button1_Click(object sender, EventArgs e)
        {
            //проверка на корректность ввода числа N
            try 
            {
                n = Convert.ToInt32(textBox1.Text);
                if (n <2) 
                {
                    MessageBox.Show("N должно быть положительным и больше 0!");
                    textBox1.Focus();
                    return;
                }
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Focus();
                return;
            }

            //убедиться в том, что выбран способ задания элементов матрицы
            if (radioButton1.Checked == true || radioButton2.Checked==true)
            {
              
            }
            else
            {
                MessageBox.Show("Выберите способ задания элементов матрицы");
                //radioButton1.Focus();
            }

            if (n >= 50 && radioButton1.Checked == true)
                random_more50 ();
            if (n < 50 && radioButton1.Checked == true)
                random_less50();
            if (n <= 10 && radioButton2.Checked == true)
                hand_less10();
            if (n>10 && radioButton2.Checked== true)
                hand_more10 ();


            
        }
        
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public double[,] start_array;
        void random_more50()
        {
            //если случайно и N>50, то генерируем массив 
            // x-столбец y-строка
            //textBox2.Visible = true;
            button4.Visible = true;
            start_array = new double[n, n];
            Random rnd = new Random();
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    start_array[x, y] = Convert.ToDouble(rnd.Next(-1000, 1000) / 10.0);
                }
                
            }
        }

        void random_less50()
        {
            //если случайно и N<=50, то генерируем массив и отображаем матрицу в dataGridView
            // x-столбец y-строка
            //textBox2.Visible = true;
            button4.Visible = true;
            start_array = new double[n, n];
            Random rnd = new Random();
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    start_array[x, y] = Convert.ToDouble(rnd.Next(-1000,1000)/10.0);
                }

            }

            dataGridView1.Visible = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = n;
            int k = 0;
            for (int i = 1; i <= n; i++)
            {
                string num = i.ToString();
                dataGridView1.Columns[k].HeaderText = num;
                dataGridView1.Rows[k].HeaderCell.Value = num;
                k++;
            }
            //выводим данные из массива в dataGridView
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    dataGridView1.Rows[x].Cells[y].Value = start_array[x, y];
                }

            }
        }

        void hand_less10()
        {
            //если вручную и N<=10, то открываем dataGridView, пользоваетель вводит значения
            //(проверка на корректность!!!), данные выгружаем в массив
            start_array = new double[n, n];
            //textBox2.Visible = true;
            dataGridView1.Visible = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = n;
            int k = 0;
            for (int i = 1; i <= n; i++)
            {
                string num = i.ToString();
                dataGridView1.Columns[k].HeaderText = num;
                dataGridView1.Rows[k].HeaderCell.Value = num;
                k++;
            }

            try
            {
                for (int x = 0; x < n; x++)
                {
                    for (int y = 0; y < n; y++)
                    {
                        start_array[x, y]=Convert.ToDouble(dataGridView1.Rows[x].Cells[y].Value) ;
                    }
                }
            } catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n(Использование букв и символов недопустимо!)");
            }
        }


        void hand_more10()
        {
            //если вручную и N>10,то выходит сообщение, возвращаем на исходную форму
            //!!!!!!Доделать, чтобы возвращало на исхлдную форму!!!!!!!!!!!!!!!!

            MessageBox.Show("Создайте файл и заполните его вручную");
        }


        
        public double[] max_array;
        public void search_max()
        {
            //поиск максимальных значений в каждом столбце и выгрузка их в отдельный массив

            max_array = new double[n];
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            button1.Visible = true;
            button2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            search_max();
            
            double num=0;
            int k = n;
            for (int d=0;d<n;d++,k--)
            {
                num += max_array[d] * max_array[k - 1];
            }
            //time();
            //MessageBox.Show("Сумма произведений равна " + num.ToString() + "\n" + "Время работы программы=" + res);
            MessageBox.Show("Сумма произведений равна "+num.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible = false;

            int totalRows = 3;
            int totalColumns = 3;
            n = totalRows;

            try
            {
               DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    //OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    
                    ExcelPackage excelFile= new ExcelPackage(new FileInfo(openFileDialog1.FileName));

                    ExcelWorksheet worksheet = excelFile.Workbook.Worksheets[1];

                    totalRows=3;
                    totalColumns = 3;
                    //totalRows = worksheet.Dimension.End.Row;
                    //totalColumns = worksheet.Dimension.End.Column;

                    if (totalColumns != totalRows)
                    {
                        MessageBox.Show("Ошибка! Матрица должна быть квадратной!");
                        return;
                    }
                    start_array= new double[totalColumns,totalRows];
                    //считываем данные из таблицы
                    for (int rowIndex=1; rowIndex<= totalRows; rowIndex++)
                    {
                        IEnumerable<string> row = worksheet.Cells[rowIndex, 1, rowIndex, totalColumns].Select(c => c.Value == null ? string.Empty:c.Value.ToString ()) ;

                        List<string> list = row.ToList<string>();
                        for (int i=0; i<list.Count;i++)
                        {
                            start_array[rowIndex-1,i]=Convert.ToDouble(list[i].Replace('.',','));
                        }
                    }
                } else
                {
                    throw new Exception("Файл не выбран!");
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            search_max();

            double num = 0;
            int k = n;
            for (int d = 0; d < n; d++, k--)
            {
                num += max_array[d] * max_array[k - 1];
            }
            //time();
            MessageBox.Show("Сумма произведений равна " + num.ToString());
            //MessageBox.Show("Сумма произведений равна " + num.ToString() + "\n" + "Время работы программы=" + res);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2(this);
            newForm.Show();
        }

        //измеряем время работы программы
        public string StartTime;
        public string EndTime;
        public int res;
        public void time()
        {
            DateTime StartTime = DateTime.Now;
            Thread.Sleep(10000);
            DateTime EndTime = DateTime.Now;

            int start_time=Convert.ToInt32(StartTime);
            int end_time=Convert.ToInt32(EndTime);
            res = end_time - start_time;
            /*
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(10000);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);*/
        }

        private void времяРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3(this);
            newForm.Show();
        }
    }
}
