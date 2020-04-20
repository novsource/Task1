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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void CloseFormButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormInTrayButton_Click_1(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var MatrixInputForm = new Form2();

            MatrixInputForm.ShowDialog();

            if (MatrixInputForm.ShowDialog() == DialogResult.OK)
            {
                FirstTxt.Text = MatrixInputForm.ReturnFirstMatrix();
                SecondTxt.Text = MatrixInputForm.ReturnSecondMatrix();
                ThirdTxt.Text = MatrixInputForm.ReturnThirdMatrix();
            }
        }

        public void RandomFill(int Size)
        {
            FirstTxt.Text = "";
            SecondTxt.Text = "";
            ThirdTxt.Text = "";

            var rand = new Random();
            for (var i = 0; i < Size; ++i)
            {
                for (var j = 0; j < Size; ++j)
                {
                  var chislo = rand.Next() % 100;
                  this.FirstTxt.Text += chislo.ToString() + "     ";
                  chislo = rand.Next() % 100;
                  this.SecondTxt.Text += chislo.ToString() + "     ";
                  chislo = rand.Next() % 100;
                  this.ThirdTxt.Text += chislo.ToString() + "     ";
                }
                this.FirstTxt.Text += "\n";
                this.SecondTxt.Text += "\n";
                this.ThirdTxt.Text += "\n";
            }
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            var MatrixRandomGeneration = new Form3();

            MatrixRandomGeneration.ShowDialog();

            if (MatrixRandomGeneration.ShowDialog() == DialogResult.OK)
            {
                int size = Convert.ToInt32(MatrixRandomGeneration.ReturnSizeMatrixA);
                RandomFill(size);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResultBox.Text = "";
            Matrix MatrixA = new Matrix(Matrix.TextToMatrix(FirstTxt.Text));
            Matrix MatrixB = new Matrix(Matrix.TextToMatrix(SecondTxt.Text));
            Matrix MatrixC = new Matrix(Matrix.TextToMatrix(ThirdTxt.Text));

            MatrixA = Matrix.MatrixSwap(Matrix.TextToMatrix(FirstTxt.Text), MatrixA);
            MatrixB = Matrix.MatrixSwap(Matrix.TextToMatrix(SecondTxt.Text), MatrixB);
            MatrixC = Matrix.MatrixSwap(Matrix.TextToMatrix(ThirdTxt.Text), MatrixC);

            Matrix MatrixResult = new Matrix(MatrixA.Size);

            if (MatrixA.SumBelow() + (MatrixB.SumAbove()) > MatrixB.SumAbove() + MatrixC.SumAbove())
            {
                MatrixResult = (MatrixA + MatrixC).Transponation() * 2 + MatrixB;
            }
            else
            {
                MatrixResult = 3 * MatrixB + (MatrixA - MatrixC).Transponation();
            }

            for (var i = 0; i < MatrixResult.Size; ++i)
             {
                 for (var j = 0; j < MatrixResult.Size; ++j)
                 {
                     var chislo = MatrixResult[i,j];
                     this.ResultBox.Text += chislo.ToString() + "     ";
                 }
                 this.ResultBox.Text += "\n";
             }
        }
    }


    [Serializable]

        public class Matrix
        {
            public int[,] data;
            private int n;
           
            public Matrix(int n)
                {
                    this.data = new int[n, n];
                    this.n = n;
                }

        public Matrix(int[,] data)
        {
            this.data = data;
            this.n = data.GetLength(0);
        }

        public int Size 
            {
                get
                { 
                    return n;
                }
            }

            public int this[int i, int j]
            {
                get 
                {
                    return data[i, j]; 
                }
                set 
                {
                    data[i, j] = value; 
                }
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

        public static Matrix operator +(Matrix A, Matrix B)
            {
                var resultMatix = new Matrix(A.Size);
                for (var i = 0; i < A.Size; ++i)
                {
                    for (var j = 0; j < A.Size; ++j)
                    {
                        resultMatix[i, j] = A[i, j] + B[i, j];
                    }
                }
                return resultMatix;
            }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            var resultMatix = new Matrix(A.Size);
            for (var i = 0; i < A.Size; ++i)
            {
                for (var j = 0; j < A.Size; ++j)
                {
                    resultMatix[i, j] = A[i, j] - B[i, j];
                }
            }
            return resultMatix;
        }

        public static Matrix operator *(Matrix A, int c)
        {
            var resultMatrix = new Matrix(A.Size);
            for (var i = 0; i < A.Size; ++i)
            {
                for (var j = 0; j < A.Size; ++j)
                {
                    if (i == j) 
                    {
                        resultMatrix[i, j] = A[i, j] + c;
                    }
                    else 
                    {
                        resultMatrix[i, j] = A[i, j];
                    }
                }
            }
            return resultMatrix;
        }

        public static Matrix operator *(int c , Matrix A)
        {
            var resultMatrix = new Matrix(A.Size);
            for (var i = 0; i < A.Size; ++i)
            {
                for (var j = 0; j < A.Size; ++j)
                {
                    if (i == j) 
                    {
                        resultMatrix[i, j] = A[i, j] + c;
                    }
                    else 
                    {
                        resultMatrix[i, j] = A[i, j];
                    }
                }
            }
            return resultMatrix;
        }

        public static Matrix MatrixSwap (int[,] Matrix, Matrix resultMatrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    resultMatrix[i, j] = Matrix[i, j];
                }
            }
            return resultMatrix;
        }
        
        public Matrix Transponation()
        {
            var matrix = new Matrix(this.data);
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var temp = this[i, j];
                    this[i, j] = this[j, i];
                    this[j, i] = temp;
                }
            }
            return matrix;
        }

        public override bool Equals(object obj)
        {
            var B = obj as Matrix;

            if (B == null)
                return false; 
            
            for (var i = 0; i < this.Size; ++i)
            {
                for (var j = 0; j < this.Size; ++j)
                {
                    
                    if (this[i, j] != B[i, j])
                        return false; 
                }
            }
            return true; 
        }

        public int SumAbove()
        {
            var MatrixA = new Matrix(this.data);
            int sum = 0;

            for (int i = 0; i < MatrixA.Size; i++)
            {
                for (int j = 0; j < MatrixA.Size; j++)
                {
                    if (j > i)
                    {
                        sum += MatrixA[i, j];
                    }
                }
                
            }
            return sum;
        }

        public int SumBelow ()
        {
            var MatrixA = new Matrix(this.data);
            int sum = 0;    

            for (int i = 0; i < MatrixA.Size; i++)
            {
                for (int j = 0; j < MatrixA.Size; j++)
                {
                    if (j < i)
                    {
                        sum += MatrixA[i, j];
                    }
                }
                
            }
            return sum;
        }

        public void Print()
        {
            for (var i = 0; i < this.Size; ++i)
            {
                for (var j = 0; j < this.Size; ++j)
                {
                    Console.Write("{0}\t", this[i, j]);
                }
                Console.WriteLine();
            }
        }

    }
}

