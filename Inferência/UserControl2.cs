using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using Accord.Statistics;
using Accord.Statistics.Distributions.Univariate;
using System.Windows.Forms.DataVisualization.Charting;

namespace SampleAnalyzer
{
    public partial class Regressão : UserControl
    {
        double[] xValues;
        double[] yValues;
        int i=0;
        int j=0;
        int precisao = 3;

        public Regressão()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {

        }

        public void Plot()
        {
            chart7.Series[0].Points.Clear(); chart7.Series[1].Points.Clear();
            i = 0;
            foreach (double item in yValues)
            {  
                chart7.Series[0].Points.AddXY(xValues[i], yValues[i]); i++;
            }           
            
            i = 0;
            textBox19.Text = "";

        }
        public void Reta()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] reta = new double[yValues.Length];
            i = 0;
            j = 0;

            Tuple<double, double> p = Fit.Line(xValues, yValues);
            double a = p.Item1;
            double b = p.Item2;

            foreach (double item in yValues)
            {

                reta[i] = a + b * xValues[i];

                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]);i++;
            }
            double count = xValues[0];
            do
            {
                double regressValue = a + b * count;
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = reta;
            double R2 = determinação.CalculoR2();
            

            textBox19.Text = "";
            textBox19.Text += "Y ="+ Math.Round(a,precisao).ToString() +" + "+ Math.Round(b,precisao).ToString()+"*X \r\n";
             textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";

            
        }
        public void Exponencial()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] exponencial = new double[xValues.Length];
            double[] lnY = new double[xValues.Length];
            i = 0;
            j = 0;

            Tuple<double, double> p = Fit.Exponential(xValues, yValues);
            double a = p.Item1;
            double r = p.Item2;

            foreach (double item in xValues)
            {
                exponencial[i] = a * Math.Exp(r * xValues[i]);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]);lnY[i] = yValues[i];
                i++;
            }
            i = 0;
            foreach (double item in exponencial)
            {
                if (exponencial[i] != 0 & yValues[i] != 0)
                {
                    exponencial[i] = Math.Log(exponencial[i], Math.E);
                    lnY[i] = Math.Log(lnY[i], Math.E);
                    i++;
                }
                else
                {
                    List<double> lista1 = exponencial.ToList<double>();
                    List<double> lista2 = lnY.ToList<double>();
                    lista1.RemoveAt(i);
                    lista2.RemoveAt(i);
                    exponencial = lista1.ToArray();
                    lnY = lista2.ToArray();
                }


            }

            double count = xValues[0];
            do
            {
                double regressValue = a * Math.Exp(r * count);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = lnY;
            determinação.ValoresEsperadosY = exponencial;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y = "+Math.Round(a,precisao).ToString()+"*exp("+Math.Round(r,precisao).ToString()+"x)" + "\r\n";
           textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void Logaritmica()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] logaritmica = new double[xValues.Length];
            i = 0;
            j = 0;

            Tuple<double, double> p = Fit.Logarithm(xValues, yValues);
            double a = p.Item1;
            double b = p.Item2;

            foreach (double item in xValues)
            {
                logaritmica[i] = a + b * Math.Log(xValues[i], Math.E);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]);i++;
            }
            double count = xValues[0];
            do
            {
                double regressValue = a + b * Math.Log(count, Math.E);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = logaritmica;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y  ="+Math.Round(a,precisao).ToString()+" + " + Math.Round(b,precisao).ToString()+"*Ln(x)" + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau2()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] polinomial2 = new double[xValues.Length];
            double[] p = new double[3];

            i = 0;
            j = 0;

            p = Fit.Polynomial(xValues, yValues, 2);
            double a = p[0];
            double b = p[1];
            double c = p[2];

            foreach (double item in xValues)
            {
                polinomial2[i] = a + b * xValues[i] + c * Math.Pow(xValues[i], 2);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]); i++;
            }
            double count = xValues[0];
            do
            {
                double regressValue = a + b * count + c * Math.Pow(count, 2);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = polinomial2;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y = "+Math.Round(p[0],precisao).ToString()+" +"+Math.Round(p[1],precisao).ToString()+"*X + "+Math.Round(p[2],precisao).ToString()+"*X²" + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau3()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] polinomial3 = new double[xValues.Length];
            double[] p = new double[3];

            i = 0;
            j = 0;

            p = Fit.Polynomial(xValues, yValues, 3);
            double a = p[0];
            double b = p[1];
            double c = p[2];
            double d = p[3];

            foreach (double item in xValues)
            {
                polinomial3[i] = a + b * xValues[i] + c * Math.Pow(xValues[i], 2) + d * Math.Pow(xValues[i], 3);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]);i++;
            }

            double count = xValues[0];
            do
            {
                double regressValue = a + b * count + c * Math.Pow(count, 2) + d * Math.Pow(count, 3);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = polinomial3;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y = "+Math.Round(p[0],precisao).ToString()+" + "+Math.Round(p[1],precisao).ToString()+"*X + "+Math.Round(p[2],precisao).ToString()+"*X²+ "+Math.Round(p[3],precisao).ToString()+"*X³" + "\r\n";
           textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau4()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] polinomial4 = new double[xValues.Length];
            double[] p = new double[3];
            i = 0;
            j = 0;

            p = Fit.Polynomial(xValues, yValues, 4);
            double a = p[0];
            double b = p[1];
            double c = p[2];
            double d = p[3];
            double e = p[4];

            foreach (double item in xValues)
            {
                polinomial4[i] = a + b * xValues[i] + c * Math.Pow(xValues[i], 2) + d * Math.Pow(xValues[i], 3) + e * Math.Pow(xValues[i], 4);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]);i++;
            }
            double count = xValues[0];
            do
            {
                double regressValue = a + b * count + c * Math.Pow(count, 2) + d * Math.Pow(count, 3) + e * Math.Pow(count, 4);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = polinomial4;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y = "+Math.Round(p[0],precisao).ToString()+" + "+Math.Round(p[1],precisao).ToString()+"*X + "+Math.Round(p[2],precisao).ToString()+"*X² + "+Math.Round(p[3],precisao).ToString()+"*X³ + "+Math.Round(p[4],precisao).ToString()+"*X^4" + "\r\n";
           textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau5()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
            double[] polinomial5 = new double[xValues.Length];
            double[] p = new double[3];
            i = 0;
            j = 0;

            p = Fit.Polynomial(xValues, yValues, 5);
            double a = p[0];
            double b = p[1];
            double c = p[2];
            double d = p[3];
            double e = p[4];
            double f = p[5];

            foreach (double item in xValues)
            {
                polinomial5[i] = a + b * xValues[i] + c * Math.Pow(xValues[i], 2) + d * Math.Pow(xValues[i], 3) + e * Math.Pow(xValues[i], 4) + f * Math.Pow(xValues[i], 5);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]); i++;
            }

            double count = xValues[0];
            do
            {
                double regressValue = a + b * count + c * Math.Pow(count, 2) + d * Math.Pow(count, 3) + e * Math.Pow(count, 4) + f * Math.Pow(count, 5);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;                

            } while (count <= xValues[xValues.Length - 1]);

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = polinomial5;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y = "+Math.Round(p[0],precisao).ToString()+" + "+Math.Round(p[1],precisao).ToString()+"*X + "+Math.Round(p[2],precisao).ToString()+"*X² + "+Math.Round(p[3],precisao).ToString()+"*X³ + "+Math.Round(p[4],precisao).ToString()+"*X^4 + "+Math.Round(p[5],precisao).ToString()+"*X^5" + "\r\n";
           textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";

        }
        public void Potência()
        {
            chart7.Series[0].Points.Clear();chart7.Series[1].Points.Clear();double intervalo=(xValues.Max()-xValues.Min())/100.0;
           

            double[] potencia = new double[xValues.Length];

            i = 0;
            j = 0;

            Tuple<double, double> p = Fit.Power(xValues, yValues);
            double a = p.Item1;
            double b = p.Item2;

            foreach (double item in xValues)
            {
                potencia[i] = a * Math.Pow(xValues[i], b);
                chart7.Series[0].Points.AddXY(xValues[i],yValues[i]);
                i++;
            }
            double count = xValues[0];
            do
            {
                double regressValue = a * Math.Pow(count, b);
                chart7.Series[1].Points.AddXY(count, regressValue);
                count += intervalo;                

            } while (count <= xValues[xValues.Length - 1]);



            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = yValues;
            determinação.ValoresEsperadosY = potencia;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "Y = "+Math.Round(a,precisao).ToString()+"*x^"+Math.Round(b,precisao).ToString()+"\r\n";            
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string [] xString = new string[dataGridView1.Rows.Count];
            string[] yString = new string[dataGridView1.Rows.Count];

            i = 0; j = 0;
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null & dataGridView1.Rows[i].Cells[1].Value != null & dataGridView1.Rows[i].Cells[0].Value != "" & dataGridView1.Rows[i].Cells[1].Value != "")
                {
                    xString[j] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    yString[j] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    j++;
                }
                else { Array.Resize(ref xString, xString.Length - 1); Array.Resize(ref yString, yString.Length - 1); }
            }
            i = 0; j = 0;          


            xValues = new double[xString.Length];
            yValues = new double[yString.Length];

            for (i = 0; i < xString.Length; i++)
            {
                xValues[i] = Convert.ToDouble(xString[i]);
            }

            for (i = 0; i < yString.Length; i++)
            {
               yValues[i] = Convert.ToDouble(yString[i]);
            }
            
            
            try
            {
                if (comboBox1.SelectedIndex == 0) { Plot(); }
                try
                {
                    if (comboBox1.SelectedIndex == 1) { Reta(); }
                    else if (comboBox1.SelectedIndex == 2) { Exponencial(); }
                    else if (comboBox1.SelectedIndex == 3) { Logaritmica(); }
                    else if (comboBox1.SelectedIndex == 4) { PolinomialGrau2(); }
                    else if (comboBox1.SelectedIndex == 5) { PolinomialGrau3(); }
                    else if (comboBox1.SelectedIndex == 6) { PolinomialGrau4(); }
                    else if (comboBox1.SelectedIndex == 7) { PolinomialGrau5(); }
                    else if (comboBox1.SelectedIndex == 8) { Potência(); }
                }
                catch { MessageBox.Show("Não foi possível fazer a regressão selecionada", "Erro:", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                textBox19.Text=textBox19.Text.Replace("+-", "- ");
                textBox19.Text=textBox19.Text.Replace("+ -", "- ");
            }
            catch { MessageBox.Show("Existe um problema com os valores fornecidos", "Erro:", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            
        }
        public class CoeficienteDeDeterminaçãoR2
        {
            private double[] valoresEsperadosY;
            public double[] ValoresEsperadosY
            {
                get { return valoresEsperadosY; }
                set { valoresEsperadosY = value; }
            }

            private double[] valoresDeY;
            public double[] ValoresDeY
            {
                get { return valoresDeY; }
                set { valoresDeY = value; }
            }

            public double mediaY;
            public double SQtot = 0;
            public double SQres = 0;
            public double R2;

            public double CalculoR2()
            {
                mediaY = MathNet.Numerics.Statistics.Statistics.Mean(ValoresDeY);
                int i = 0;
                for (i = 0; i < ValoresDeY.Length; i++)
                {
                    SQres += Math.Pow((ValoresDeY[i] - ValoresEsperadosY[i]), 2);
                    SQtot += Math.Pow((ValoresDeY[i] - mediaY), 2);
                }
                R2 = 1 - (SQres / SQtot);
                return R2;
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete | e.KeyData==Keys.Back)
            {
                               
                foreach(DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = null;

                }

            }
            
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyDown -= dataGridView1_KeyDown;
                tb.PreviewKeyDown -= dataGridView1_PreviewKeyDown;
                tb.KeyDown += dataGridView1_KeyDown;
                tb.PreviewKeyDown += dataGridView1_PreviewKeyDown;
            }
        }
        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                     
                    string s = Clipboard.GetText();
                    Clipboard.Clear();
                    string[] separador = { "\r\n" };               
                    string[] lines = s.Split(separador, StringSplitOptions.None);
                    int row = dataGridView1.CurrentCell.RowIndex;
                    int col = dataGridView1.CurrentCell.ColumnIndex;

                    i = dataGridView1.CurrentCell.RowIndex;
                    foreach (string line in lines)
                    {
                        
                        if (i == dataGridView1.Rows.Count) { dataGridView1.Rows.Add(); }
                        i++;
                    }
                    
                    i=0;
                    for (i=0;i<lines.Length-1;i++)
                    {
                        string[] cells = lines[i].Split('\t');                        
                        dataGridView1.Rows[row+i].Cells[0].Value = cells[0];
                        dataGridView1.Rows[row+i].Cells[1].Value = cells[1];
                        
                    }
                                    
                    dataGridView1.CancelEdit();
                    dataGridView1.Enabled = false;
                    if (s != null)
                    {
                        Clipboard.SetText(s);
                    }
                    dataGridView1.Enabled = true; 
                    
                    
               
            }
            
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chart7_Click(object sender, EventArgs e)
        {

        }

       

        
    }
}
