using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class Grid
    {
        public static bool[,] input;// = new bool[80, 80];
        const int cellSize = Form1.cellSize;
        public const int gridSize = 400 / cellSize;
        static Graphics g;
        static SolidBrush myBrush;
        static Pen myPen;

        public static void setGrphcs(Graphics gr)
        {
            g = gr;
        }

        static int round(double a)
        {
            if (a - (int)a >= 0.5)
            {
                return (int)a + 1;
            }
            else
            {
                return (int)a;
            }
        }

        public static void setColor(int r, int g, int b)
        {
            myBrush = new SolidBrush(Color.FromArgb(255, r, g, b));
            myPen = new Pen(Color.FromArgb(255, r, g, b));
        }

        public static void clearGrid()
        {
            input = new bool[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    input[i, j] = false;
            setColor(255, 255, 255);
            g.FillRectangle(myBrush, 0, 0, 400, 400);
            drawGridOnly();
        }

        public static void getInput(int x, int y)
        {
            int rx = (int)(x / cellSize);
            int ry = (int)(y / cellSize);
            if (rx < 0 || ry < 0 || rx >= gridSize || ry >= gridSize)
                return;
            input[rx, ry] = true;
            setColor(32, 32, 48);
            g.FillRectangle(myBrush, rx * cellSize, ry * cellSize, cellSize, cellSize);
        }

        public static void eraseInput(int x, int y)
        {
            int rx = (int)(x / cellSize);
            int ry = (int)(y / cellSize);
            if (rx < 0 || ry < 0 || rx >= gridSize || ry >= gridSize)
                return;
            input[rx, ry] = false;
            setColor(255, 255, 255);
            g.FillRectangle(myBrush, rx * cellSize, ry * cellSize, cellSize, cellSize);
        }

        public static int getLeftmostFilledCell()
        {
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    if (input[i, j])
                        return i;
            return 0;
        }

        public static int getUppermostFilledCell()
        {
            for (int j = 0; j < gridSize; j++)
                for (int i = 0; i < gridSize; i++)
                    if (input[i, j])
                        return j;
            return 0;
        }

        public static int getRightmostFilledCell()
        {
            for (int i = gridSize-1; i>=0 ; i--)
                for (int j = gridSize - 1; j >= 0; j--)
                    if (input[i, j])
                        return i;
            return 0;
        }

        public static int getLowermostFilledCell()
        {
            for (int j = gridSize - 1; j >= 0; j--)
                for (int i = gridSize - 1; i >= 0; i--)
                    if (input[i, j])
                        return j;
            return 0;
        }

        public static void moveTheDrawedToCorner()
        {
            int leftm = getLeftmostFilledCell();
            int rightm = getRightmostFilledCell();
            int upm = getUppermostFilledCell();
            int lowm = getLowermostFilledCell();
            int width = rightm - leftm + 1;
            int height = lowm - upm + 1;
            bool[,] imageToMove = new bool[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    imageToMove[i, j] = input[i + leftm, j + upm];
                }
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    input[i, j] = false;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    input[i, j] = imageToMove[i, j];
            redrawCells();
            drawGridOnly();
        }

        public static void scaletheDrawed()
        {
            int leftm = getLeftmostFilledCell();
            int rightm = getRightmostFilledCell();
            int upm = getUppermostFilledCell();
            int lowm = getLowermostFilledCell();
            int width = rightm - leftm + 1;
            int height = lowm - upm + 1;
            bool[,] imageToMove = new bool[width+1, height+1];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    imageToMove[i, j] = input[i + leftm, j + upm];
                }
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    input[i, j] = false;

            //scaling: 
            double VertScaleFactor = (double)gridSize / (double)height;
            double HorizScaleFactor = (double)gridSize / (double)width;
            Console.Write("\nWidth/Height: " + width + "/" + height);
            Console.Write("\nHor/Vert Scale Factor: " + HorizScaleFactor + "/" + VertScaleFactor);
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    input[i, j] = imageToMove[round(i/HorizScaleFactor), round(j/VertScaleFactor)];
            redrawCells();
            drawGridOnly();
        }

        static void redrawCells()
        {
            setColor(255, 255, 255);
            g.FillRectangle(myBrush, 0, 0, 400, 400);
            setColor(32, 32, 48);
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    if (input[i, j])
                        g.FillRectangle(myBrush, i * cellSize, j * cellSize, cellSize, cellSize);
        }

        public static void drawGridOnly()
        {
            //Console.Write("Oh blyat!");
            //setColor(32, 32, 48);
            //for (int i = 0; i < 80; i++)
            //    for (int j = 0; j < 80; j++)
            //    {
            //        if (input[i, j])
            //            g.FillRectangle(myBrush, i * 5, j * 5, 5, 5);
            //    }
            setColor(212, 212, 212);
            for (int i = 0; i <= 400; i += cellSize)
            {
                g.DrawLine(myPen, 0, i, 400, i);
                g.DrawLine(myPen, i, 0, i, 400);
            }
            Console.Write(")\nЛВ: (" + getLeftmostFilledCell() + "," + getUppermostFilledCell() 
                + ") ПН: (" + getRightmostFilledCell() + "," + getLowermostFilledCell());
        }
    }
}
