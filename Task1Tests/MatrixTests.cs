using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void Test1()
        {
            var A = new Matrix(new int[,] {{ 1, 2 }, { 3, 4 } });
            var B = new Matrix(new int[,] {{ 1, 2}, { 3, 4 } });
            Assert.AreEqual(A, B);
        }

        [TestMethod()]
        public void Test2()
        {
            var A = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } });
            var B = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } });
            var MatrixSum = A + B;
            var MatrixCheck = new Matrix(new int[,] { {2,4}, {6,8} });
            Assert.AreEqual(MatrixSum, MatrixCheck);
        }

        [TestMethod()]
        public void Test3()
        {
            var A = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } });
            var B = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } });
            var MatrixSum = A + B;
            MatrixSum = MatrixSum.Transponation();
            var MatrixCheck = new Matrix(new int[,] { { 2, 6 }, { 4, 8 } });
            Assert.AreEqual(MatrixSum, MatrixCheck);
        }

        [TestMethod()]
        public void Test4()
        {
            var A = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } });
            var MatrixABelow = A.SumBelow();
            var MatrixAAbove = A.SumAbove();
            Assert.AreEqual(3, MatrixABelow);
            Assert.AreEqual(2, MatrixAAbove);
        }
    }
}