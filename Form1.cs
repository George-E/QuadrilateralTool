using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//George Eisa
//5/1/2015
//Quadrilateral
//Determines the type of quadrilateral

namespace Quadrilateral
{
    public partial class Form1 : Form
    {
        static Graphics formGraphics;
        Pen myPen = new Pen(Color.Black);
        SolidBrush myBrush = new SolidBrush(Color.White);
        SolidBrush textBrush = new SolidBrush(Color.Black);
        StringFormat stringFormat = new StringFormat();
        Font myFont;


        public Form1()
        {
            InitializeComponent();
            formGraphics = this.CreateGraphics();
            myFont = new Font(this.Font, FontStyle.Bold);
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            clearGraphics();

            float[] angles;
            float[] sides;

            try
            {
                angles = new float[] { float.Parse(txtAngle1.Text), float.Parse(txtAngle2.Text), 
                                  float.Parse(txtAngle3.Text), float.Parse(txtAngle4.Text) };
                sides = new float[] { float.Parse(txtSide1.Text), float.Parse(txtSide2.Text), 
                                 float.Parse(txtSide3.Text), float.Parse(txtSide4.Text) };
            }
            catch (Exception)
            {
                return;
            }
            double sumAngles = 0;
            int[] matchingAngles = new int[4];
            int[] matchingSides = new int[4];
            bool squareAngles = true;

            for (int i = 0; i < 4; i++)
            {
                for (int n = 0; n < 4; n++)
                {
                    if (n != i && angles[i] == angles[n])
                    {
                        matchingAngles[i]++;
                    }
                    if (n != i && sides[i] == sides[n])
                    {
                        matchingSides[i]++;
                    }
                }
                if (angles[i] <= 0)
                {
                    lblAnswer.Text = "Invalid Angle";
                    lblError.Text = "Angles cannot be negative or 0.";
                    lblError.Visible = true;
                    return;
                }
                if (angles[i] != 90)
                {
                    squareAngles = false;
                }
                sumAngles += angles[i];
            }

            int pairsOfAngles = 0;
            int pairsOfSides = 0;
            for (int i = 0; i < 4; i++)
            {
                if (matchingAngles[i] == 1)
                {
                    pairsOfAngles++;
                }
                if (matchingSides[i] == 1)
                {
                    pairsOfSides++;
                }
            }
            pairsOfAngles /= 2;
            pairsOfSides /= 2;

            if (sumAngles != 360)
            {
                lblAnswer.Text = "Error";
                lblError.Text = String.Format("Angles must add up to 360.\n\n( Current sum is {0:#.##} )", sumAngles);
                lblError.Visible = true;
                return;
            }
            else if (squareAngles && matchingSides[0] == 3)
            {
                lblAnswer.Text = "Square";
                drawRec(sides[0], sides[0]);
                return;
            }
            else if (squareAngles && pairsOfSides == 2)
            {
                lblAnswer.Text = "Rectangle";
                if (sides[0] == sides[1])
                {
                    drawRec(sides[0], sides[2]);
                }
                else
                {
                    drawRec(sides[0], sides[1]);
                }
                return;
            }
            else if (pairsOfAngles == 2 && matchingSides[0] == 3)
            {
                lblAnswer.Text = "Rhombus";
                if (angles[0] == angles[1])
                {
                    drawRhom(angles[0], angles[2], sides[0], sides[0]);
                }
                else
                {
                    drawRhom(angles[0], angles[1], sides[0], sides[0]);
                }
                return;
            }
            else if (pairsOfSides == 2 && pairsOfAngles == 2)
            {
                lblAnswer.Text = "Parallelogram";
                if (sides[0] == sides[1])
                {
                    if (angles[0] == angles[1])
                    {
                        drawRhom(angles[0], angles[2], sides[0], sides[2]);
                    }
                    else
                    {
                        drawRhom(angles[0], angles[1], sides[0], sides[2]);
                    }
                }
                else
                {
                    if (angles[0] == angles[1])
                    {
                        drawRhom(angles[0], angles[2], sides[0], sides[1]);
                    }
                    else
                    {
                        drawRhom(angles[0], angles[1], sides[0], sides[1]);
                    }
                }
                return;
            }
            else if (pairsOfSides == 2 && pairsOfAngles == 1)
            {
                lblAnswer.Text = "Kite";
                float small = 0, big = 0, side = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (matchingAngles[i] == 1)
                    {
                        side = angles[i];
                    }
                    else
                    {
                        if (angles[i] < small || small == 0)
                        {
                            small = angles[i];
                        }
                        if (angles[i] > big || big == 0)
                        {
                            big = angles[i];
                        }
                    }
                }
                if (sides[0] == sides[1])
                {
                    drawKite(side, big, small, sides[0], sides[2]);
                }
                else
                {
                    drawKite(side, big, small, sides[0], sides[1]);
                }
                return;
            }
            else if (pairsOfSides == 1 && pairsOfAngles == 2)
            {
                lblAnswer.Text = "Isosceles Trapezoid";
                float small = 0, big = 0, side = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (matchingSides[i] == 1)
                    {
                        side = sides[i];
                    }
                    else
                    {
                        if (sides[i] < small || small == 0)
                        {
                            small = sides[i];
                        }
                        if (sides[i] > big || big == 0)
                        {
                            big = sides[i];
                        }
                    }
                }
                if (angles[0] == angles[1])
                {
                    drawTrap(angles[0], angles[2], side, big, small);
                }
                else
                {
                    drawTrap(angles[0], angles[1], side, big, small);
                }
                return;
            }
            else if (pairsOfSides == 0 && pairsOfAngles == 0 && squareAngles == false)
            {
                lblAnswer.Text = "Trapezoid";
                return;
            }
            //lblAnswer.Text = (pairsOfSides + " " + pairsOfAngles + " " + squareAngles);
            lblAnswer.Text = "Not Defined";
        }

        public void drawRec(float length1, float length2)
        {
            if (length2 > length1)
            {
                float temp = length1;
                length1 = length2;
                length2 = temp;
            }
            float newlength2 = length2 * (120 / length1);
            formGraphics.FillRectangle(myBrush, new RectangleF(250, 145 - newlength2 / 2, 120, newlength2));
            formGraphics.DrawRectangle(myPen, 250, 145 - newlength2 / 2, 120, newlength2);
            formGraphics.DrawString("" + length2, myFont, textBrush, 235, 145, stringFormat);
            formGraphics.DrawString("" + length2, myFont, textBrush, 385, 145, stringFormat);
            formGraphics.DrawString("" + length1, myFont, textBrush, 310, 130 - newlength2 / 2, stringFormat);
            formGraphics.DrawString("" + length1, myFont, textBrush, 310, 160 + newlength2 / 2, stringFormat);
            double area = length1 * length2;
            double perimeter = length1 * 2 + length2 * 2;
            lblArea.Text = String.Format("This area is {0:#.##} un^2.\nThe perimeter is {1:#.##} un.", area, perimeter);
        }

        public void drawRhom(float angle1, float angle2, float length1, float length2)
        {
            if (angle2 > angle1)
            {
                float temp = angle1;
                angle1 = angle2;
                angle2 = temp;
            }
            if (length2 > length1)
            {
                float temp = length1;
                length1 = length2;
                length2 = temp;
            }
            float newlength2 = length2 * (120 / length1);
            double xChange = newlength2 * Math.Sin((double)(90 - angle2) * (Math.PI / 180));
            double yChange = newlength2 * Math.Cos((double)(90 - angle2) * (Math.PI / 180));
            double centerOffset = 310 - (xChange + 120) / 2;
            PointF[] points = new PointF[4];
            points[0] = new PointF((float)centerOffset, 145 - (float)yChange / 2);
            points[1] = new PointF((float)centerOffset + (float)xChange, 145 + (float)yChange / 2);
            points[2] = new PointF((float)centerOffset + 120 + (float)xChange, 145 + (float)yChange / 2);
            points[3] = new PointF((float)centerOffset + 120, 145 - (float)yChange / 2);
            formGraphics.FillPolygon(myBrush, points);
            formGraphics.DrawPolygon(myPen, points);
            formGraphics.DrawString("" + length2, myFont, textBrush, (float)centerOffset + (float)xChange / 2 - 15, 145, stringFormat);
            formGraphics.DrawString("" + length2, myFont, textBrush, (float)centerOffset + 135 + (float)xChange / 2, 145, stringFormat);
            formGraphics.DrawString("" + length1, myFont, textBrush, (float)centerOffset + 60, 130 - (float)yChange / 2, stringFormat);
            formGraphics.DrawString("" + length1, myFont, textBrush, (float)centerOffset + 60 + (float)xChange, 160 + (float)yChange / 2, stringFormat);
            double trueYChange = length2 * Math.Cos((double)(90 - angle2) * (Math.PI / 180));
            double area = length1 * trueYChange;
            double perimeter = length1 * 2 + length2 * 2;
            lblArea.Text = String.Format("This area is {0:#.##} un^2.\nThe perimeter is {1:#.##} un.", area, perimeter);
        }

        public void drawTrap(float angle1, float angle2, float length1, float length2, float length3)
        {
            if (angle2 > angle1)
            {
                float temp = angle1;
                angle1 = angle2;
                angle2 = temp;
            }
            float newlength1 = length1 * (120 / length2);
            float newlength3 = length3 * (120 / length2);

            double xChange = newlength1 * Math.Sin((double)(90 - angle2) * (Math.PI / 180));
            double actualXChange = (120 - newlength3) / 2;
            if (Math.Abs(xChange - actualXChange) < 0.5)
            {
                double yChange = newlength1 * Math.Cos((double)(90 - angle2) * (Math.PI / 180));
                PointF[] points = new PointF[4];
                points[0] = new PointF(250 + (float)xChange, 145 - (float)yChange / 2);
                points[1] = new PointF(250, 145 + (float)yChange / 2);
                points[2] = new PointF(370, 145 + (float)yChange / 2);
                points[3] = new PointF(250 + (float)xChange + newlength3, 145 - (float)yChange / 2);
                formGraphics.FillPolygon(myBrush, points);
                formGraphics.DrawPolygon(myPen, points);
                formGraphics.DrawString("" + length1, myFont, textBrush, 250 + (float)xChange / 2 - 15, 145, stringFormat);
                formGraphics.DrawString("" + length1, myFont, textBrush, 370 - (float)xChange / 2 + 15, 145, stringFormat);
                formGraphics.DrawString("" + length3, myFont, textBrush, 310, 130 - (float)yChange / 2, stringFormat);
                formGraphics.DrawString("" + length2, myFont, textBrush, 310, 160 + (float)yChange / 2, stringFormat);
                double trueYChange = length1 * Math.Cos((double)(90 - angle2) * (Math.PI / 180));
                double area = ((length2 + length3) * trueYChange) / 2;
                double perimeter = length1 * 2 + length2 + length3;
                lblArea.Text = String.Format("This area is {0:#.##} un^2.\nThe perimeter is {1:#.##} un.", area, perimeter);
            }
            else
            {
                double trueXChange = length1 * Math.Sin((double)(90 - angle2) * (Math.PI / 180));
                double rightLength3 = length2 - trueXChange * 2;
                if (rightLength3 > 1)
                {
                    lblError.Text = String.Format("This trapezoid is impossible.\n\nRecommendation:\nChange the side length {0} to {1:#.##}.", length3, rightLength3);
                    lblError.Visible = true;
                }
                else
                {
                    double rightLength2 = length3 + trueXChange * 2;
                    lblError.Text = String.Format("This trapezoid is impossible.\n\nRecommendation:\nChange the side length {0} to {1:#.##}.", length2, rightLength2);
                    lblError.Visible = true;
                }
            }
        }

        public void drawKite(float angle1, float angle2, float angle3, float length1, float length2)
        {
            double vertDiagonal = Math.Sqrt(length2 * length2 + length1 * length1 - 2 * length1 * length2 * Math.Cos((double)(angle1) * (Math.PI / 180)));
            double horzDiagonal1 = Math.Sqrt(2 * length2 * length2 - 2 * length2 * length2 * Math.Cos((double)(angle2) * (Math.PI / 180)));
            double horzDiagonal2 = Math.Sqrt(2 * length1 * length1 - 2 * length1 * length1 * Math.Cos((double)(angle3) * (Math.PI / 180)));
            if (Math.Abs(horzDiagonal1 - horzDiagonal2) < 0.5)
            {
                float newlength2 = length2 * (100 / (float)vertDiagonal);
                float newHorzDiagonal = (float)horzDiagonal1 * (100 / (float)vertDiagonal);
                float yChange = newlength2 * (float)Math.Sin((double)(90 - (angle2 / 2)) * (Math.PI / 180));
                PointF[] points = new PointF[4];
                points[0] = new PointF(310, 95);
                points[1] = new PointF(310 - newHorzDiagonal / 2, 95 + yChange);
                points[2] = new PointF(310, 195);
                points[3] = new PointF(310 + newHorzDiagonal / 2, 95 + yChange);
                formGraphics.FillPolygon(myBrush, points);
                formGraphics.DrawPolygon(myPen, points);
                formGraphics.DrawString("" + length1, myFont, textBrush, 310 + (float)newHorzDiagonal / 4 + 15, 210 - (float)(120 - yChange) / 2, stringFormat);
                formGraphics.DrawString("" + length1, myFont, textBrush, 310 - (float)newHorzDiagonal / 4 - 15, 210 - (float)(120 - yChange) / 2, stringFormat);
                formGraphics.DrawString("" + length2, myFont, textBrush, 310 - (float)newHorzDiagonal / 4 - 15, 80 + yChange / 2, stringFormat);
                formGraphics.DrawString("" + length2, myFont, textBrush, 310 + (float)newHorzDiagonal / 4 + 15, 80 + yChange / 2, stringFormat);
                double area = (vertDiagonal * horzDiagonal1) / 2;
                double perimeter = length1 * 2 + length2 * 2;
                lblArea.Text = String.Format("This area is {0:#.##} un^2.\nThe perimeter is {1:#.##} un.", area, perimeter);
            }
            else
            {
                double rightLength2 = (horzDiagonal2 / 2) / Math.Sin((angle2 / 2) * (Math.PI / 180));
                if (rightLength2 > 1)
                {
                    lblError.Text = String.Format("This kite is impossible.\n\nRecommendation:\nChange both side lengths {0} to {1:#.##}.", length2, rightLength2);
                    lblError.Visible = true;
                }
                else
                {
                    double rightLength1 = (horzDiagonal1 / 2) / Math.Sin((angle3 / 2) * (Math.PI / 180));
                    lblError.Text = String.Format("This kite is impossible.\n\nRecommendation:\nChange both side lengths {0} to {1:#.##}.", length1, rightLength1);
                    lblError.Visible = true;
                }
            }

        }


        public void clearGraphics()
        {
            lblArea.Text = "";
            lblAnswer.Text = "";
            formGraphics.Clear(Form1.ActiveForm.BackColor);
            lblError.Text = "";
            lblError.Visible = false;

        }

    }
}

