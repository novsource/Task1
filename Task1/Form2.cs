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


namespace Task1
{
    public partial class Form2 : Form
    {   
        public Form2()
        {
            InitializeComponent();
        }
 

        public static int[,] TextToMatrix(string text)
        {
            var lines = text.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var colsCount = lines[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
            var rowsCount = lines.Length;

            var matrix = new int[rowsCount, colsCount];
            for (var i = 0; i < lines.Length; i++)
            {
                var elements = lines[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < elements.Length; j++)
                {
                    matrix[i, j] = int.Parse(elements[j]);
                }
            }
            return matrix;
        }

          
        private void button1_Click_1(object sender, EventArgs e)
        {
            FirstMatrix.Text = "";
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var text = File.ReadAllText(openFileDialog.FileName);

            int[,] matrix = TextToMatrix(text);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.FirstMatrix.Text += matrix[i, j].ToString() + " ";
                }
                this.FirstMatrix.Text += "\n";
            }
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SecondMatrix.Text = "";
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var text = File.ReadAllText(openFileDialog.FileName);

            int[,] matrix = TextToMatrix(text);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.SecondMatrix.Text += matrix[i, j].ToString() + " ";
                }
                this.SecondMatrix.Text += "\n";
            }
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThirdMatrix.Text = "";
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var text = File.ReadAllText(openFileDialog.FileName);

            int[,] matrix = TextToMatrix(text);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.ThirdMatrix.Text += matrix[i, j].ToString() + " ";
                }
                this.ThirdMatrix.Text += "\n";
            }
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInTrayButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private int x = 0; private int y = 0;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X; y = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Location = new System.Drawing.Point(this.Location.X + (e.X - x), this.Location.Y + (e.Y - y));
            }
        }

        private void FirstMatrix_TextChanged(object sender, EventArgs e)
        {

        }

        public void OK_Click(object sender, EventArgs e)
        {
            var A = TextToMatrix(FirstMatrix.Text);
            var B = TextToMatrix(SecondMatrix.Text);
            var C = TextToMatrix(ThirdMatrix.Text);

            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(0) != C.GetLength(0) || B.GetLength(0) != C.GetLength(0))
                MessageBox.Show(
                "Матрицы разной размерности! Измените матрицы и загрузите их снова!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            else 
                this.DialogResult = DialogResult.OK;
        }

            public string ReturnFirstMatrix()
            {
                return (FirstMatrix.Text);
            }

        public string ReturnSecondMatrix()
        {
            return (SecondMatrix.Text);
        }

        public string ReturnThirdMatrix()
        {
            return (ThirdMatrix.Text);
        }
    }
    }


