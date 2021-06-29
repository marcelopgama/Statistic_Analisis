using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MathNet.Numerics;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;




namespace Inferência
{
    public partial class Form1 : Form
    {
        int i, j, precisao = 3, n = 0, indiceDoTeste = 0;
        int cont = 0;
        double n2 = 0;
        double nivelDeSignificancia = 0.05;

        double[] outliersInferiores;
        double[] outliersSuperiores; 

        //Estatísticas
        int outInf,outSup;
        string[] listaTexto;
        double soma = 0, media = 0, amplitude = 0, variancia = 0, curtose, centilDez, centilNoventa;
        double desvioPadrao, coeficienteDeVariacao, coeficienteDeAssimetria, mediana, minimo, maximo;            
        double qInferior, qSuperior, quartilInferior, quartilSuperior,quantidade = 0, amplitudeInterQuartil = 0;
        string moda = "";
        double[] listaNumerica, listaNumericaOrdenada;
        //Estatísticas Sem Outliers
        double[] listaSemOutliers = new double[999];
        int outInf2, outSup2;
        string[] listaTexto2=new string[999];
        double soma2 = 0, media2 = 0, amplitude2 = 0, variancia2 = 0, curtose2, centilDez2, centilNoventa2;
        double desvioPadrao2, coeficienteDeVariacao2, coeficienteDeAssimetria2, mediana2, minimo2, maximo2;
        double qInferior2, qSuperior2, quartilInferior2, quartilSuperior2, quantidade2 = 0, amplitudeInterQuartil2 = 0;
        string moda2 = "";
        double[] listaNumerica2 = new double[999], listaNumericaOrdenada2 = new double[999];
           

        
        //Frequência
        double K;        
        double h;       
        double[] freqDasClassesAcum =new double[999];
        double[] probDasClasses = new double[999];
        double[] probDasClassesAcum = new double[999];
        double[] freqDasClasses = new double[999];
        double[] X = new double[999];
        double[] YOrdenado = new double[999];
        double[] limites = new double[999];
        double[] listaSemRepetidos = new double[999];
        double[] Sx;
        double[] freq;

        double quant = 0;
        double ampli = 0;
        double[] list = new double[999];
        //Qui-Quadrado          
        double KTemp;
        double[] probDasClassesEsp = new double[999];
        double[] removendo1 = new double[999];
        double[] removendo2 = new double[999];
        double[] ySemMenorQue5 = new double[999];
        double[] probabilidadeSemMenorQue5 = new double[999];       
        double[] limites2 = new double[999];
        double v = 0;        
         //KS
        double[] probabilidadeAcumuladaEsperada = new double[999];
        double[] probabilidade;

        string bestDistributionx2;
        string bestDistributionks;
        string posiveisDistX2;
        string posiveisDistKs;
        string negadasDistX2;
        string negadasDistKs;
        bool tabelaSelecionada = false;

        Chart graficoSelecionado;
        string endereço;
        PropriedadesDoGráfico configuração;
        PropertyGrid propriedades;

        string pasta;

        SizeF sizeOriginal;
        
        public Form1()
        {
            InitializeComponent();            
            tabControl1.Dock = DockStyle.Fill;             
            sizeOriginal = tabControl1.Size;
            checkBox1.Checked = true;
            checkBox2.Checked = false;  
            button2.Enabled = false; 
            checkBox4.Checked = false;
            tabControl1.Enabled = false;
            listBox1.Enabled = false;            
            comboBox1.Enabled = false;
            checkBox5.Enabled = false;
            exportarToolStripMenuItem.Enabled = false;
            dataGridView5.Enabled = true;
            dataGridView5.ReadOnly = false;
            dataGridView5.Rows.Add(15);

            dataGridView2.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.Columns[0].Width = 330;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Width = 695;

            dataGridView1.Rows.Add("Tamnaho da Amostra");
            dataGridView1.Rows.Add("Soma");
            dataGridView1.Rows.Add("Média");
            dataGridView1.Rows.Add("Mediana");
            dataGridView1.Rows.Add("Moda");
            dataGridView1.Rows.Add("Mínimo");
            dataGridView1.Rows.Add("Máximo");
            dataGridView1.Rows.Add("Amplitude");
            dataGridView1.Rows.Add("Desvio Padrão");
            dataGridView1.Rows.Add("Quartil Inferior");
            dataGridView1.Rows.Add("Quartil Superior");
            dataGridView1.Rows.Add("Outliers Inferiores");
            dataGridView1.Rows.Add("Outliers Superiores");
            dataGridView1.Rows.Add("Variância");
            dataGridView1.Rows.Add("Coeficiente de Variação");
            dataGridView1.Rows.Add("Coeficiente de Assimetria");
            dataGridView1.Rows.Add("Curtose");

            int linhas=dataGridView1.Rows.Count;
            i=0;
            for (i = 0; i < linhas;i++ )
            {
                dataGridView1.Rows[i].Cells[0].Style.Alignment=DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows[i].Height = 25;
            } i = 0;


            dataGridView2.Columns[0].Width = 52;
            dataGridView2.Columns[1].Width = 161;
            dataGridView2.Columns[2].Width = 209;
            dataGridView2.Columns[3].Width = 236;
            dataGridView2.Columns[4].Width = 180;
            dataGridView2.Columns[5].Width = 180;
            dataGridView2.Width = 1018;

            popularDataGridView3();     
           

            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView4.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView4.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView4.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();

            
        }
        public void popularDataGridView3()
        {
            dataGridView3.Rows.Add("Beta");
            dataGridView3.Rows.Add("Binomial");
            dataGridView3.Rows.Add("Continua Uniforme");
            dataGridView3.Rows.Add("Erlang");
            dataGridView3.Rows.Add("Exponencial");
            dataGridView3.Rows.Add("Gama");
            dataGridView3.Rows.Add("Geométrica");
            dataGridView3.Rows.Add("Gama Inversa");
            dataGridView3.Rows.Add("Laplace");
            dataGridView3.Rows.Add("LogNormal");
            dataGridView3.Rows.Add("Binomial Negativa");
            dataGridView3.Rows.Add("Normal");
            dataGridView3.Rows.Add("Pareto");
            dataGridView3.Rows.Add("Poisson");
            dataGridView3.Rows.Add("Rayleigh");
            dataGridView3.Rows.Add("Triangular");
            dataGridView3.Rows.Add("Weibull");
            dataGridView3.Rows.Add("Cauchy");
            dataGridView3.Rows.Add("Normal Dobrada");
            dataGridView3.Rows.Add("Gumbel");
        }

        //Amostra
        private void dataGridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete | e.KeyData == Keys.Back)
            {
                if (dataGridView5.ReadOnly == false)
                {
                    foreach (DataGridViewCell cell in dataGridView5.SelectedCells)
                    {
                        dataGridView5.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = null;                        
                    }
                }

            }
        }
        private void dataGridView5_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyDown -= dataGridView5_KeyDown;
                tb.PreviewKeyDown -= dataGridView5_PreviewKeyDown;
                tb.KeyDown += dataGridView5_KeyDown;
                tb.PreviewKeyDown += dataGridView5_PreviewKeyDown;
            }
        }
        private void dataGridView5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
            {

                string s = Clipboard.GetText();
                Clipboard.Clear();
                string[] separador = { "\r\n" };
                string[] lines = s.Split(separador, StringSplitOptions.None);                
                int row = dataGridView5.CurrentCell.RowIndex;
                int col = dataGridView5.CurrentCell.ColumnIndex;

                i = dataGridView5.CurrentCell.RowIndex;
                foreach (string line in lines)
                {

                    if (i == dataGridView5.Rows.Count) { dataGridView5.Rows.Add(); }
                    i++;
                }

                i = 0;
                for (i = 0; i < lines.Length; i++)
                {
                    string[] cells = lines[i].Split('\t');
                    dataGridView5.Rows[row + i].Cells[0].Value = cells[0];                    

                }

                dataGridView5.CancelEdit();
                dataGridView5.Enabled = false;
                if (s != null)
                {
                    Clipboard.SetText(s);
                }
                dataGridView5.Enabled = true;



            }
        }
        public void ValoresDaAmostra()
        {
            listaTexto = new string[dataGridView5.Rows.Count];
            listaNumerica = new double[dataGridView5.Rows.Count];
            listaNumericaOrdenada = new double[dataGridView5.Rows.Count];

            i = 0; j = 0;
            for (i = 0; i < dataGridView5.Rows.Count; i++)
            {
                if (dataGridView5.Rows[i].Cells[0].Value != null & dataGridView5.Rows[i].Cells[0].Value != "")
                {
                    listaTexto[j] = dataGridView5.Rows[i].Cells[0].Value.ToString();
                    try
                    {
                        listaNumerica[j] = Convert.ToDouble(listaTexto[j]);
                        listaNumericaOrdenada[j] = Convert.ToDouble(listaTexto[j]);
                    }
                    catch { MessageBox.Show("Verifique a sua amostra"); }
                    j++;
                }
                else { Array.Resize(ref listaTexto, listaTexto.Length - 1); }
                Array.Resize(ref listaNumerica, listaTexto.Length);
                Array.Resize(ref listaNumericaOrdenada, listaTexto.Length);

            }
            i = 0; j = 0;

        }
             
        //Estatísticas
        public void calculoDasEstatisticas()
        {
           
            Array.Sort(listaNumericaOrdenada);
            i = 0;

            var estatísticas = new MathNet.Numerics.Statistics.DescriptiveStatistics(listaNumerica);

            quantidade = estatísticas.Count;            
            media = Math.Round(estatísticas.Mean,precisao);
            soma = media * quantidade;
            maximo = estatísticas.Maximum;
            minimo = estatísticas.Minimum;
            curtose = Math.Round(estatísticas.Kurtosis, precisao);            
            variancia = Math.Round(estatísticas.Variance,precisao);
            desvioPadrao = Math.Round(estatísticas.StandardDeviation,precisao);
            amplitude = maximo - minimo;
            mediana = MathNet.Numerics.Statistics.SortedArrayStatistics.Median(listaNumericaOrdenada);
            quartilInferior = Math.Round(MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada,25),precisao);
            quartilSuperior = Math.Round(MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada,75),precisao);
            coeficienteDeAssimetria = Math.Round(MathNet.Numerics.Statistics.Statistics.Skewness(listaNumerica),precisao);
            centilDez = MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada, 10);
            centilNoventa = MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada, 90);
            amplitudeInterQuartil=MathNet.Numerics.Statistics.SortedArrayStatistics.InterquartileRange(listaNumericaOrdenada);
            qInferior=Math.Round(quartilInferior-1.5*amplitudeInterQuartil,precisao);
            qSuperior = Math.Round(quartilSuperior + 1.5 * amplitudeInterQuartil,precisao);

            
            foreach (double item in listaNumericaOrdenada)
            {
                if (item < qInferior){outInf++;}
                else if(item>qSuperior){outSup++;}
            }
            i = 0;
            j = 0;          

                        

            
            //moda
            int[] aparicoes = new int[Convert.ToInt32(quantidade)];
            int maximoAparicoes = 0;
            double[] repetidos = new double[Convert.ToInt32(quantidade)];
            i = 0;

            foreach (var count in aparicoes)
            {
                aparicoes[i] = 1;
                i++;
            }
            i = 0;
            j = 0;

            for (i = 0; i < quantidade; i++)
            {
                j = i + 1;
                for (j = i + 1; j < quantidade; j++)
                {
                    if (listaNumericaOrdenada[i] == listaNumericaOrdenada[j])
                    {
                        aparicoes[i] = aparicoes[i] + 1;
                        
                        
                    }
                }

            }
            maximoAparicoes = aparicoes.Max();
            

            i = 0;
            j = 0;

            for (i = 0; i < quantidade; i++)
            {
                if (aparicoes[i] == 2)
                {
                    repetidos[j] = listaNumericaOrdenada[i];
                    j++;
                }

            }

            var repetidos2 = new HashSet<double>(repetidos);

            double[] repetidos3 = repetidos2.ToArray();
            Array.Resize(ref repetidos3, repetidos3.Length - 1);
            moda = string.Join("; ", repetidos3);

            n = 0;
            foreach (int item in aparicoes)
            {
                if (maximoAparicoes == item)
                {
                    n++;
                }
            }
            if (n == aparicoes.Length) { moda = "NA"; }
            n = 0;
            i = 0;
            j = 0;
            

            //Coeficiente de Variação
            coeficienteDeVariacao = (desvioPadrao / media) * 100;
            coeficienteDeVariacao = Math.Round(coeficienteDeVariacao, precisao);            
            
            
        }
        public void calculoDasEstatisticas2()
        {

            

            i = 0;
            for (i = 0; i < quantidade2; i++)
            {
                listaNumerica2[i] = Convert.ToDouble(listaTexto2[i]);                
            }            
            i = 0;           
            for (i = 0; i < quantidade2; i++)
            {
                listaNumericaOrdenada2[i] = listaNumerica2[i];
                
            }
           
            Array.Sort(listaNumericaOrdenada2);
            
            i = 0;            

            var estatísticas2 = new MathNet.Numerics.Statistics.DescriptiveStatistics(listaNumerica2);

            quantidade2 = estatísticas2.Count;
            media2 = Math.Round(estatísticas2.Mean, precisao);
            soma2 = media2 * quantidade2;
            maximo2 = estatísticas2.Maximum;
            minimo2 = estatísticas2.Minimum;
            curtose2 = Math.Round(estatísticas2.Kurtosis, precisao);
            variancia2 = Math.Round(estatísticas2.Variance, precisao);
            desvioPadrao2 = Math.Round(estatísticas2.StandardDeviation, precisao);
            amplitude2 = maximo2 - minimo2;
            mediana2 = MathNet.Numerics.Statistics.SortedArrayStatistics.Median(listaNumericaOrdenada2);            
            quartilInferior2 = Math.Round(MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada2, 25), precisao);
            quartilSuperior2 = Math.Round(MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada2, 75), precisao);
            coeficienteDeAssimetria2 = Math.Round(MathNet.Numerics.Statistics.Statistics.Skewness(listaNumerica2), precisao);
            centilDez2 = MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada2, 10);
            centilNoventa2 = MathNet.Numerics.Statistics.SortedArrayStatistics.Percentile(listaNumericaOrdenada2, 90);
            amplitudeInterQuartil2 = MathNet.Numerics.Statistics.SortedArrayStatistics.InterquartileRange(listaNumericaOrdenada2);
            qInferior2 = Math.Round(quartilInferior2 - 1.5 * amplitudeInterQuartil2, precisao);
            qSuperior2 = Math.Round(quartilSuperior2 + 1.5 * amplitudeInterQuartil2, precisao);


            foreach (double item in listaNumericaOrdenada2)
            {
                if (item < qInferior2) { outInf2++; }
                else if (item > qSuperior2) { outSup2++; }
            }
            i = 0;
            j = 0;




            //moda
            int[] aparicoes2 = new int[Convert.ToInt32(quantidade2)];
            int maximoAparicoes2 = 0;
            double[] repetidos2 = new double[Convert.ToInt32(quantidade2)];
            i = 0;

            foreach (var count in aparicoes2)
            {
                aparicoes2[i] = 1;
                i++;
            }
            i = 0;
            j = 0;

            for (i = 0; i < quantidade2; i++)
            {
                j = i + 1;
                for (j = i + 1; j < quantidade2; j++)
                {
                    if (listaNumericaOrdenada2[i] == listaNumericaOrdenada2[j])
                    {
                        aparicoes2[i] = aparicoes2[i] + 1;


                    }
                }

            }
            maximoAparicoes2 = aparicoes2.Max();


            i = 0;
            j = 0;

            for (i = 0; i < quantidade2; i++)
            {
                if (aparicoes2[i] == 2)
                {
                    repetidos2[j] = listaNumericaOrdenada2[i];
                    j++;
                }

            }

            var repetidos22 = new HashSet<double>(repetidos2);

            double[] repetidos32 = repetidos22.ToArray();
            Array.Resize(ref repetidos32, repetidos32.Length - 1);
            moda2 = string.Join("; ", repetidos32);



            n = 0;
            foreach (int item in aparicoes2)
            {
                if (maximoAparicoes2 == item)
                {
                    n++;
                }
            }
            if (n == aparicoes2.Length) { moda2 = "NA"; }
            n = 0;
            i = 0;
            j = 0;


            //Coeficiente de Variação
            coeficienteDeVariacao2 = (desvioPadrao2 / media2) * 100;
            coeficienteDeVariacao2 = Math.Round(coeficienteDeVariacao2, precisao);


        }
        public void calculoCompleto()
        {
            
            //listaTexto = textBox1.Text.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);      
            quantidade = listaTexto.Length;
            Array.Resize(ref listaNumerica, Convert.ToInt32(quantidade));
            Array.Resize(ref listaNumericaOrdenada, Convert.ToInt32(quantidade));
            dataGridView4.Rows.Clear();
                      

            //-----
            calculoDasEstatisticas();
            //-----
           


            //Outliers
            outliersInferiores = new double[outInf];
            outliersSuperiores = new double[outSup];            
            Array.Resize(ref listaSemOutliers, Convert.ToInt32(quantidade) - outInf - outSup);
                          
            i = 0;
            j = 0;

            foreach (double item in listaSemOutliers)
            {
                if (listaNumerica[i] <= qSuperior & listaNumericaOrdenada[i] >= qInferior)
                {
                    listaSemOutliers[j] = listaNumerica[i];                 
                    dataGridView4.Rows.Add(Convert.ToString(listaSemOutliers[j]));
                    j++;
                    i++;
                }
                else
                {
                    i++;
                }
            }

            i = 0;
            j = 0;      

            foreach (double item in listaNumericaOrdenada)
            {
                if (listaNumericaOrdenada[i] < qInferior)
                {
                    outliersInferiores[j] = listaNumericaOrdenada[i]; 
                    dataGridView4.Rows[j].Cells[1].Value = Convert.ToString(outliersInferiores[j]);
                    j++;
                    i++;
                }
                else
                {
                    i++;
                }
            }
            i = 0;
            j = 0;
            
            foreach (double item in listaNumericaOrdenada)
            {
                if (listaNumericaOrdenada[i] > qSuperior)
                {
                    outliersSuperiores[j] = listaNumericaOrdenada[i];                    
                    dataGridView4.Rows[j].Cells[2].Value = Convert.ToString(outliersSuperiores[j]);
                    j++;
                    i++;
                }
                else
                {
                    i++;
                }
            }
            i = 0;
            j = 0;
            
            
            //Gráfico            
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            
            foreach (string item in listaTexto)
            {

                if (Convert.ToDouble(listaTexto[i]) < qInferior)
                {
                    chart2.Series[1].Points.AddXY(i + 1, Convert.ToDouble(listaTexto[i]));
                    i++;
                }

                else if (Convert.ToDouble(listaTexto[i]) >= qInferior & Convert.ToDouble(listaTexto[i]) <=qSuperior)
                {

                    chart2.Series[0].Points.AddXY(i + 1, Convert.ToDouble(listaTexto[i]));
                    listaTexto2[j] = listaTexto[i];                    
                    j++;
                    i++;
                }
                else if (Convert.ToDouble(listaTexto[i]) > qSuperior)
                {
                    chart2.Series[1].Points.AddXY(i + 1, Convert.ToDouble(listaTexto[i]));
                    i++;
                }
            }
            
            
            i = 0;            
            j = 0;

            //BoxPlot                  

            chart1.Series[0].Points.Clear();
            double limiteInferior = 0;
            double limiteSuperior = 0;

            if (minimo >= qInferior) { limiteInferior = minimo; }
            else { limiteInferior = qInferior; }

            if (maximo <= qSuperior) { limiteSuperior = maximo; }
            else { limiteSuperior = qSuperior; }

            chart1.Series[0].Points.AddXY(0,limiteInferior,limiteSuperior,quartilInferior,quartilSuperior,mediana,media);
            
            //Recalcular sem Outliers
            quantidade2 = Convert.ToInt32(quantidade) - outInf - outSup;
            Array.Resize(ref listaNumerica2,Convert.ToInt32(quantidade2));
            Array.Resize(ref listaTexto2, Convert.ToInt32(quantidade2));
            Array.Resize(ref listaNumericaOrdenada2, Convert.ToInt32(quantidade2));               
            calculoDasEstatisticas2();
            //------

            //Valores das Estatisticas                       
            tabelaDeEstatísticas();

            comboBox1.Enabled = true;
            checkBox5.Enabled = true;            
            checkBox5.Checked = true;
            checkBox5.Checked = false;           

            Frequências();

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
            
            
            
        }
        public void Frequências()
        {
            if (checkBox5.Checked == false)
            {
                dataGridView2.Rows.Clear();
                quant = quantidade2;
                ampli = amplitude2;
                Array.Resize(ref list, listaNumerica2.Length);
                i = 0;
                for (i = 0; i < listaNumerica2.Length; i++) { list[i] = listaNumerica2[i]; }

            }
            else
            {
                dataGridView2.Rows.Clear();
                quant = quantidade;
                ampli = amplitude;
                Array.Resize(ref list, listaNumerica.Length);
                i = 0;
                for (i = 0; i < listaNumerica.Length; i++) { list[i] = listaNumerica[i]; }
            }

            //Separar em frequencia           
            K = (Math.Round(1 + (3.3 * Math.Log10(quant))));
            Array.Resize(ref freqDasClasses, Convert.ToInt32(K));
            Array.Resize(ref X, Convert.ToInt32(quant));
            h = Math.Round(ampli / K,precisao);

            var histograma = new MathNet.Numerics.Statistics.Histogram(list,Convert.ToInt32(K));
             
            textBox10.Text += Convert.ToString(K) + "\r\n";
            textBox10.Text += Convert.ToString(h);

            
            
            //Tabela das Frequências                 
            
            Array.Resize(ref freqDasClassesAcum, Convert.ToInt32(K));
            Array.Resize(ref probDasClasses, Convert.ToInt32(K));
            Array.Resize(ref probDasClassesAcum, Convert.ToInt32(K));
            freqDasClassesAcum[0] = histograma[0].Count;
            probDasClasses[0]=Math.Round(histograma[0].Count/quant,precisao,MidpointRounding.AwayFromZero);
            probDasClassesAcum[0] = Math.Round(probDasClasses[0], precisao, MidpointRounding.AwayFromZero);


            i = 1;
            for (i = 1; i < K; i++)
            {
                freqDasClassesAcum[i] = histograma[i].Count + freqDasClassesAcum[i - 1];
                probDasClasses[i] = Math.Round(histograma[i].Count / quant, precisao, MidpointRounding.AwayFromZero);
                probDasClassesAcum[i] = Math.Round(probDasClassesAcum[i - 1] + probDasClasses[i], precisao, MidpointRounding.AwayFromZero);
            }

            i = 0;           
           
            for (i = 0; i < K; i++)
            {
                dataGridView2.Rows.Add(i + 1, String.Format("{0}", Math.Round(histograma[i].LowerBound, precisao) + "-" + Math.Round(histograma[i].UpperBound, precisao)),
                    histograma[i].Count.ToString(), freqDasClassesAcum[i].ToString(), probDasClasses[i].ToString(), probDasClassesAcum[i].ToString());
                
            }
            i=0;

            //remover repetidos           
            var semrepetidos = new HashSet<double>(list);
            listaSemRepetidos = semrepetidos.ToArray();
                       
            probabilidade = new double[listaSemRepetidos.Length];
            Sx=new double[listaSemRepetidos.Length];
            freq = new double[listaSemRepetidos.Length];
            for(i=0;i<listaSemRepetidos.Length;i++){
                for(j=0;j<list.Length;j++){
                    if (listaSemRepetidos[i] == list[j]) { n2++; freq[i] = n2; probabilidade[i] = n2 / quant; n++; Sx[i] = n / quant; }
                }
                j=0;
                n2 = 0;
            }
            
            Array.Sort(listaSemRepetidos);
            Array.Sort(probabilidade);
            j = 0; n = 0; n2 = 0;

            //Histograma
            i = 0;
            
            chart3.Series["Frequência"].Points.Clear();            
            chart4.Series[0].Points.Clear();
            chart4.Series[1].Points.Clear();
            chart5.Series[0].Points.Clear();
            chart5.Series[1].Points.Clear();

           
            Array.Resize(ref limites, Convert.ToInt32(K+1));
            Array.Resize(ref limites2, Convert.ToInt32(K + 1));
            limites[0] = Math.Round(histograma[0].LowerBound,precisao);
            limites2[0] = limites[0];

            for (i = 0; i < K; i++)
            {
                chart3.Series["Frequência"].Points.AddXY(Math.Round(histograma[i].LowerBound, precisao) + "-" + Math.Round(histograma[i].UpperBound, precisao), histograma[i].Count);
                limites[i+1] = Math.Round(histograma[i].UpperBound, precisao);
                limites2[i + 1] = limites[i + 1];
                chart4.Series[0].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), histograma[i].Count);
               }
            i = 0;
            for(i=0;i<listaSemRepetidos.Length;i++){
                chart5.Series[0].Points.AddXY(listaSemRepetidos[i],Sx[i]);
            }

            i = 0;
            j = 0;



            for (i = 0; i < K; i++)
            {
                freqDasClasses[i] = histograma[i].Count;
                YOrdenado[i] = freqDasClasses[i];                
            }
            for (i = 0; i < list.Length; i++)
            {
                X[i] = i + 1;               
            }

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
           
           
                        
        }
        public void tabelaDeEstatísticas()
        {
            
            dataGridView1.Rows[0].Cells[1].Value=(quantidade.ToString());
            dataGridView1.Rows[1].Cells[1].Value=(soma.ToString());
            dataGridView1.Rows[2].Cells[1].Value=(media.ToString());
            dataGridView1.Rows[3].Cells[1].Value=(mediana.ToString());
            dataGridView1.Rows[4].Cells[1].Value=(moda.ToString());
            dataGridView1.Rows[5].Cells[1].Value=(minimo.ToString());
            dataGridView1.Rows[6].Cells[1].Value=(maximo.ToString());
            dataGridView1.Rows[7].Cells[1].Value=(amplitude.ToString());
            dataGridView1.Rows[8].Cells[1].Value=(desvioPadrao.ToString());
            dataGridView1.Rows[9].Cells[1].Value=(quartilInferior.ToString());
            dataGridView1.Rows[10].Cells[1].Value=(quartilSuperior.ToString());
            dataGridView1.Rows[11].Cells[1].Value=(outInf.ToString());
            dataGridView1.Rows[12].Cells[1].Value=(outSup.ToString());
            dataGridView1.Rows[13].Cells[1].Value=(variancia.ToString());
            dataGridView1.Rows[14].Cells[1].Value=(coeficienteDeVariacao.ToString() + "%");
            dataGridView1.Rows[15].Cells[1].Value=(coeficienteDeAssimetria.ToString());
            dataGridView1.Rows[16].Cells[1].Value=(curtose.ToString());

            dataGridView1.Rows[0].Cells[2].Value=(quantidade2.ToString());
            dataGridView1.Rows[1].Cells[2].Value=(soma2.ToString());
            dataGridView1.Rows[2].Cells[2].Value=(media2.ToString());
            dataGridView1.Rows[3].Cells[2].Value=(mediana2.ToString());
            dataGridView1.Rows[4].Cells[2].Value=(moda2.ToString());
            dataGridView1.Rows[5].Cells[2].Value=(minimo2.ToString());
            dataGridView1.Rows[6].Cells[2].Value=(maximo2.ToString());
            dataGridView1.Rows[7].Cells[2].Value=(amplitude2.ToString());
            dataGridView1.Rows[8].Cells[2].Value=(desvioPadrao2.ToString());
            dataGridView1.Rows[9].Cells[2].Value=(quartilInferior2.ToString());
            dataGridView1.Rows[10].Cells[2].Value=(quartilSuperior2.ToString());
            dataGridView1.Rows[11].Cells[2].Value=(outInf2.ToString());
            dataGridView1.Rows[12].Cells[2].Value=(outSup2.ToString());
            dataGridView1.Rows[13].Cells[2].Value=(variancia2.ToString());
            dataGridView1.Rows[14].Cells[2].Value=(coeficienteDeVariacao2.ToString() + "%");
            dataGridView1.Rows[15].Cells[2].Value=(coeficienteDeAssimetria2.ToString());
            dataGridView1.Rows[16].Cells[2].Value=(curtose2.ToString());

            int colunas = dataGridView1.Rows.Count;
            i = 0;
            for (i = 0; i < colunas;i++ )
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Contains("NaN") == true)
                {
                    dataGridView1.Rows[i].Cells[1].Value = "NA";
                }
                if (dataGridView1.Rows[i].Cells[2].Value.ToString().Contains("NaN") == true)
                {
                    dataGridView1.Rows[i].Cells[2].Value = "NA";
                }
                
            }
            i = 0;
           
        }      

        //Regrgressão Linear
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
            public double SQtot=0;
            public double SQres=0;
            public double R2;

            public double CalculoR2()
            {
                mediaY = MathNet.Numerics.Statistics.Statistics.Mean(ValoresDeY);
                int i = 0;
                for (i = 0; i < ValoresDeY.Length; i++)
                {
                    SQres += Math.Pow((ValoresDeY[i]- ValoresEsperadosY[i]), 2);
                    SQtot += Math.Pow((ValoresDeY[i] - mediaY), 2);
                }
                R2 = 1-(SQres / SQtot);
                return R2;
            }

        }
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex == 0) { Reta(); }
                else if (listBox1.SelectedIndex == 1) { Exponencial(); }
                else if (listBox1.SelectedIndex == 2) { Logaritmica(); }
                else if (listBox1.SelectedIndex == 3) { PolinomialGrau2(); }
                else if (listBox1.SelectedIndex == 4) { PolinomialGrau3(); }
                else if (listBox1.SelectedIndex == 5) { PolinomialGrau4(); }
                else if (listBox1.SelectedIndex == 6) { PolinomialGrau5(); }
                else if (listBox1.SelectedIndex == 7) { Potência(); }
            }
            catch { MessageBox.Show("Não foi possível fazer a regressão selecionada", "Erro:", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                chart2.Series[2].Enabled = true;
                listBox1.Enabled = true;

            }
            else if (checkBox4.Checked == false)
            {
                chart2.Series[2].Enabled = false;
                listBox1.Enabled = false;
            }
        }
        public void Reta()
        {
            chart2.Series[2].Points.Clear();
            double[] reta = new double[list.Length];            
            i = 0;
            j = 0;
            
            Tuple<double, double> p = Fit.Line(X, list);
                double a = p.Item1;
                double b = p.Item2;                 

                foreach (double item in list)
                {

                    reta[i] = a + b * X[i];
                    
                    chart2.Series[2].Points.AddXY(X[i], reta[i]);
                    i++;
                }
            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = reta;
            double R2=determinação.CalculoR2();

                textBox19.Text = "";
                textBox19.Text += "freqDasClasses = a + b*x" + "\r\n";
                textBox19.Text += "a = " + Math.Round(a,precisao).ToString()+"\r\n";
                textBox19.Text += "b = " + Math.Round(b,precisao).ToString()+"\r\n";
                textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";

          
        }
        public void Exponencial() 
        {
            chart2.Series[2].Points.Clear();
            double[] exponencial = new double[X.Length];            
            double[] lnY = new double[X.Length];
            i = 0;
            j = 0;

            Tuple<double, double> p = Fit.Exponential(X, list);
            double a = p.Item1;
            double r = p.Item2;            

            foreach (double item in X)
            {
                exponencial[i] = a * Math.Exp(r * X[i]);
                chart2.Series[2].Points.AddXY(X[i], exponencial[i]);                
                lnY[i]=list[i];
                i++;
            }
            i = 0;
            foreach (double item in exponencial)
            {
                if (exponencial[i] != 0 & list[i] != 0)
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



            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = lnY;
            determinação.ValoresEsperadosY = exponencial;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses = a*exp(rx)" + "\r\n";
            textBox19.Text += "a = " + Math.Round(a,precisao).ToString()+"\r\n";
            textBox19.Text += "r = " + Math.Round(r, precisao).ToString() + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void Logaritmica()
        {
            chart2.Series[2].Points.Clear();
            double[] logaritmica = new double[X.Length];            
            i = 0;
            j = 0;
            
            Tuple<double, double> p = Fit.Logarithm(X, list);
            double a = p.Item1;
            double b = p.Item2;

            foreach (double item in X)
            {
                logaritmica[i] = a + b * Math.Log(X[i], Math.E);
                chart2.Series[2].Points.AddXY(X[i], logaritmica[i]);
                i++;
            }
            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = logaritmica;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses  =a + b*Ln(x)" + "\r\n";
            textBox19.Text += "a = " + Math.Round(a, precisao).ToString() + "\r\n";
            textBox19.Text += "b = " + Math.Round(b, precisao).ToString() + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau2()
        {
            chart2.Series[2].Points.Clear();
            double[] polinomial2 = new double[X.Length];
            double[] p=new double[10];
            
            i = 0;
            j = 0;

            p = Fit.Polynomial(X, list, 2);
            double a = p[0];
            double b = p[1];
            double c = p[2];

            foreach (double item in X)
            {
                polinomial2[i] = a+ b * X[i] + c * Math.Pow(X[i], 2);
                chart2.Series[2].Points.AddXY(X[i], polinomial2[i]);
                i++;
            }

            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = polinomial2;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses = p0 +p1*X + p2*X²"+"\r\n";
            textBox19.Text += "p0 = " + Math.Round(p[0], precisao).ToString() + "\r\n";
            textBox19.Text += "p1 = " + Math.Round(p[1], precisao).ToString() + "\r\n";
            textBox19.Text += "p2 = " + Math.Round(p[2], precisao).ToString() + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau3()
        {
            chart2.Series[2].Points.Clear();
            double[] polinomial3 = new double[X.Length];
            double[] p = new double[10];
            
            i = 0;
            j = 0;

            p = Fit.Polynomial(X, list, 3);
            double a = p[0];
            double b = p[1];
            double c = p[2];
            double d = p[3];

            foreach (double item in X)
            {
                polinomial3[i] = a + b * X[i] + c * Math.Pow(X[i], 2) + d * Math.Pow(X[i], 3);
                chart2.Series[2].Points.AddXY(X[i], polinomial3[i]);
                i++;
            }
            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = polinomial3;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses = p0 + p1*X + p2*X²+ p3*X³" + "\r\n";
            textBox19.Text += "p0 = " + Math.Round(p[0], precisao).ToString() + "\r\n";
            textBox19.Text += "p1 = " + Math.Round(p[1], precisao).ToString() + "\r\n";
            textBox19.Text += "p2 = " + Math.Round(p[2], precisao).ToString() + "\r\n";
            textBox19.Text += "p3 = " + Math.Round(p[3], precisao).ToString() + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau4()
        {
            chart2.Series[2].Points.Clear();
            double[] polinomial4 = new double[X.Length];
            double[] p = new double[10];            
            i = 0;
            j = 0;

            p = Fit.Polynomial(X, list, 4);
            double a = p[0];
            double b = p[1];
            double c = p[2];
            double d = p[3];
            double e = p[4];

            foreach (double item in X)
            {
                polinomial4[i] = a + b * X[i] + c * Math.Pow(X[i], 2) + d * Math.Pow(X[i], 3) + e * Math.Pow(X[i], 4);
                chart2.Series[2].Points.AddXY(X[i], polinomial4[i]);
                i++;
            }
            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = polinomial4;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses = p0 + p1*X + p2*X² + p3*X³ + p4*X^4" + "\r\n";
            textBox19.Text += "p0 = " + Math.Round(p[0], precisao).ToString() + "\r\n";
            textBox19.Text += "p1 = " + Math.Round(p[1], precisao).ToString() + "\r\n";
            textBox19.Text += "p2 = " + Math.Round(p[2], precisao).ToString() + "\r\n";
            textBox19.Text += "p3 = " + Math.Round(p[3], precisao).ToString() + "\r\n";
            textBox19.Text += "p4 = " + Math.Round(p[4], precisao).ToString() + "\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }
        public void PolinomialGrau5()
        {
            chart2.Series[2].Points.Clear();
            double[] polinomial5 = new double[X.Length];
            double[] p = new double[10];           
            i = 0;
            j = 0;

            p = Fit.Polynomial(X, list, 5);
            double a = p[0];
            double b = p[1];
            double c = p[2];
            double d = p[3];
            double e = p[4];
            double f = p[5];

            foreach (double item in X)
            {
                polinomial5[i] = a + b * X[i] + c * Math.Pow(X[i], 2) + d * Math.Pow(X[i], 3) + e * Math.Pow(X[i], 4) + f * Math.Pow(X[i], 5);
                chart2.Series[2].Points.AddXY(X[i], polinomial5[i]);
                i++;
            }
            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = polinomial5;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses = p0 + p1*X + p2*X² + p3*X³ + p4*X^4 + p4*X^5" + "\r\n";
            textBox19.Text += "p0 = " + Math.Round(p[0], precisao).ToString() + "\r\n";          
            textBox19.Text += "p1 = " + Math.Round(p[1], precisao).ToString() + "\r\n";
            textBox19.Text += "p2 = " + Math.Round(p[2], precisao).ToString() + "\r\n";
            textBox19.Text += "p3 = " + Math.Round(p[3], precisao).ToString() + "\r\n";
            textBox19.Text += "p4 = " + Math.Round(p[4], precisao).ToString() + "\r\n";
            textBox19.Text+=  "p5 = " + Math.Round(p[5], precisao).ToString()+"\r\n";
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
                        
        }
        public void Potência()
        {
            chart2.Series[2].Points.Clear();


            double[] potencia = new double[X.Length];           
            
            i = 0;
            j = 0;
            
            Tuple<double,double> p = Fit.Power(X, list);
            double a = p.Item1;
            double b = p.Item2;           

            foreach (double item in X)
            {
                potencia[i] = a * Math.Pow(X[i], b);
                chart2.Series[2].Points.AddXY(X[i], potencia[i]);
                i++;
            }
            CoeficienteDeDeterminaçãoR2 determinação = new CoeficienteDeDeterminaçãoR2();
            determinação.ValoresDeY = list;
            determinação.ValoresEsperadosY = potencia;
            double R2 = determinação.CalculoR2();

            textBox19.Text = "";
            textBox19.Text += "freqDasClasses = a*x^b" + "\r\n";
            textBox19.Text += "a = " + Math.Round(a, precisao).ToString() + "\r\n";
            textBox19.Text += "b = " + Math.Round(b, precisao).ToString() + "\r\n";            
            textBox19.Text += "R² = " + Math.Round(R2, precisao).ToString() + "\r\n";
        }       

        //Distribuições Estatísticas
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceDoTeste = listBox2.SelectedIndex;
            tabelaSelecionada = false;
            try
            {
                if (listBox2.SelectedIndex == 0) { Beta(); }
                else if (listBox2.SelectedIndex == 1) { Binomial(); }
                else if (listBox2.SelectedIndex == 2) { ContinuaUniforme(); }
                else if (listBox2.SelectedIndex == 3) { Erlang(); }
                else if (listBox2.SelectedIndex == 4) { DistribuiçãoExponencial(); }
                else if (listBox2.SelectedIndex == 5) { Gama(); }
                else if (listBox2.SelectedIndex == 6) { Geométrica(); }
                else if (listBox2.SelectedIndex == 7) { GamaInversa(); }
                else if (listBox2.SelectedIndex == 8) { Laplace(); }
                else if (listBox2.SelectedIndex == 9) { LogNormal(); }
                else if (listBox2.SelectedIndex == 10) { BinomialNegativa(); }
                else if (listBox2.SelectedIndex == 11) { Normal(); }
                else if (listBox2.SelectedIndex == 12) { Pareto(); }
                else if (listBox2.SelectedIndex == 13) { Poisson(); }
                else if (listBox2.SelectedIndex == 14) { Rayleigh(); }
                else if (listBox2.SelectedIndex == 15) { Triangular(); }
                else if (listBox2.SelectedIndex == 16) { Weibull(); }
                else if (listBox2.SelectedIndex == 17) { Cauchy(); }
                else if (listBox2.SelectedIndex == 18) { NormalDobrada(); }
                else if (listBox2.SelectedIndex == 19) { Gumbel(); }
            }
            catch
            {
                textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";
                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[listBox2.SelectedIndex].Cells[1].Value="NA"; dataGridView3.Rows[listBox2.SelectedIndex].Cells[2].Value = "NA";
            }

        }        
        public void Beta()
        {
            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double[] X = new double[listaSemRepetidos.Length];
            double[] limites2 = new double[limites.Length];
            double a = -M + ((M * M) / V) - (Math.Pow(M, 3) / V);
            double b = (a / M) - a;
           
            
            if(listaSemRepetidos.Max()>1 | listaSemRepetidos.Min()<0){                
                i=0;
                foreach (double item in listaSemRepetidos) { X[i] = (listaSemRepetidos[i] - listaSemRepetidos[0]) / amplitude2; i++; }
                i=0;
                foreach (double item in limites) { limites2[i] = (limites[i] - listaSemRepetidos[0]) / amplitude2; i++;}
                i = 0;
                M = Accord.Statistics.Measures.Mean(X);
                V = Accord.Statistics.Measures.Variance(X);
                a = -M + ((M * M) / V) - (Math.Pow(M, 3) / V);
                b = (a / M) - a;
            }
            a = Math.Round(a,precisao);
            b = Math.Round(b,precisao);

            var distribuição = new Accord.Statistics.Distributions.Univariate.BetaDistribution(a,b);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.DistributionFunction(X[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.DistributionFunction(limites2[i], limites2[i + 1]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Binomial()
        {
            chart4.Series[1].Points.Clear();            
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = ""; 

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double p = Math.Round(M / list.Length,precisao);      

            var distribuição = new MathNet.Numerics.Distributions.Binomial(p,Convert.ToInt32(listaSemRepetidos.Length));
            var distribuição2=new Accord.Statistics.Distributions.Univariate.BinomialDistribution(listaSemRepetidos.Length,p);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);          
            }            
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i+1])-distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }

            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
           
            
        }
        public void ContinuaUniforme()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double a = (2 * M - Math.Sqrt(12 * V)) / 2;
            double b = (2 * M + Math.Sqrt(12 * V)) / 2;
            a = Math.Round(a, precisao);
            b = Math.Round(b, precisao);

            var distribuição = new Accord.Statistics.Distributions.Univariate.UniformContinuousDistribution(a, b);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.DistributionFunction(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);                
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.DistributionFunction(limites[i + 1]) - distribuição.DistributionFunction(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Erlang()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            int shape = Convert.ToInt32(Math.Round(Math.Pow(M, 2) / V, 0));
            double rate = M / V;
            rate = Math.Round(rate, precisao);

            var distribuição = new MathNet.Numerics.Distributions.Erlang(shape, rate);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void DistribuiçãoExponencial()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
                    
            double rate = 1/M;
            rate = Math.Round(rate, precisao);

            var distribuição = new MathNet.Numerics.Distributions.Exponential(rate);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Gama()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double rate = Math.Round(M / V, precisao);
            double shape = Math.Round(Math.Pow(M,2) / V, precisao);
            
            var distribuição = new MathNet.Numerics.Distributions.Gamma(shape, rate);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Geométrica()
        {
            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double p = Math.Round(1 / (M + 1),precisao);

            var distribuição = new MathNet.Numerics.Distributions.Geometric(p);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }

            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;


        }
        public void GamaInversa()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double shape = Math.Round((Math.Pow(M, 2) / V) + 2);
            double scale = Math.Round((Math.Pow(M, 3) / V) + M);

            var distribuição = new MathNet.Numerics.Distributions.InverseGamma(shape, scale);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Laplace()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double location = Math.Round(M,precisao);
            double scale = Math.Round(Math.Sqrt(V/2),precisao);

            var distribuição = new MathNet.Numerics.Distributions.Laplace(location, scale);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void LogNormal()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double sigma = Math.Round(Math.Log((V / (Math.Exp(2 * Math.Log(M, Math.E)))+1), Math.E),precisao);
            double mi = Math.Round(Math.Log(M, Math.E) - (Math.Pow(sigma, 2) / 2),precisao);
            

            var distribuição = new MathNet.Numerics.Distributions.LogNormal(mi, sigma);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void BinomialNegativa()
        {
            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);
            double p = Math.Round(M/(V+M),precisao);
            double r = Math.Round(M*p,precisao);
            var distribuição = new MathNet.Numerics.Distributions.NegativeBinomial(r, p);
            if (p < 0 | r <= 0) { MessageBox.Show("Não foi possível calcular os parâmetros a distribuição de Pascal", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); };


            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;

        }
        public void Normal()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Math.Round(Accord.Statistics.Measures.Mean(list),precisao);
            double SDV = Math.Round(Accord.Statistics.Measures.StandardDeviation(list),precisao);
           

            var distribuição = new MathNet.Numerics.Distributions.Normal(M, SDV);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Pareto()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);

            double shape = Math.Round((2 + Math.Sqrt(4 * (1 + (M / V)))) / 2,precisao);
            double scale = Math.Round((M * (shape - 1)) / shape,precisao);

            var distribuição = new MathNet.Numerics.Distributions.Pareto(scale, shape);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                if (listaSemRepetidos[i] < scale) { probabilidadeAcumuladaEsperada[i] = 0; }
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);                
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²  
                if (limites[i] >= scale) { probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]); }
                else if(limites[i]<scale & limites[i+1]>=scale){ probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(scale); }
                else if (limites[i] < scale & limites[i + 1] < scale) { probDasClassesEsp[i] = 0; }               
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Poisson()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);            

            double lambda = Math.Round(M,precisao);

            var distribuição = new MathNet.Numerics.Distributions.Poisson(lambda);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Rayleigh()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);

            double scale = Math.Round(M/(Math.Sqrt(Math.PI/2)),precisao);

            var distribuição = new MathNet.Numerics.Distributions.Rayleigh(scale);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Triangular()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double lower = Math.Round(MathNet.Numerics.Statistics.ArrayStatistics.Minimum(list),precisao);
            double upper = Math.Round(MathNet.Numerics.Statistics.ArrayStatistics.Maximum(list),precisao);
            double mode = Math.Round((3 * M) - upper-lower,precisao);

            
                var distribuição = new MathNet.Numerics.Distributions.Triangular(lower, upper, mode);
                i = 0;
                for (i = 0; i < listaSemRepetidos.Length; i++)
                {
                    //Ks                
                    probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                    chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
                }
                i = 0;
                for (i = 0; i < K; i++)
                {
                    //X²              
                    probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                    probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                    chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
                }
                RemovendoMenorQue5();
                v = KTemp - 3;
                Testes();
                textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
            
            
        }
        public void Weibull()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double[] x = new double[listaSemRepetidos.Length];
            double[] y = new double[listaSemRepetidos.Length];
            

            i=0;
            j = 0;
            for(i=0;i<x.Length;i++)
            {
                if (Sx[i] != 1 & Sx[i] != 0 & listaSemRepetidos[i]!=0)
                {
                    x[j] = Math.Log(listaSemRepetidos[i], Math.E);
                    y[j] = Math.Log(-Math.Log(1 - Sx[i], Math.E), Math.E);
                    j++;
                }
                else { Array.Resize(ref x, x.Length - 1); Array.Resize(ref y, y.Length - 1); }
                 }
            i = 0;
            Tuple<double, double> p = Fit.Line(x, y);
            double c = p.Item1;
            double m = p.Item2;

            double shape = Math.Round(m,precisao);
            double scale = Math.Round(Math.Exp(-c / shape),precisao);

            var distribuição = new MathNet.Numerics.Distributions.Weibull(shape, scale);
            
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Cauchy()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double mediana = Accord.Statistics.Measures.Median(list);           

            double[] lambda = new double[listaSemRepetidos.Length];          
            double location=Math.Round(mediana,precisao);
              

            i = 0;
            j = 0;
            for (i=0; i < lambda.Length; i++)
            {
                
                if (Sx[i] != 0.5 & Sx[i]!=1)
                {
                    lambda[j] = (listaSemRepetidos[i] - location) / Math.Tan((Sx[i] - 0.5) * Math.PI);
                    j++;
                    
                }
                else { Array.Resize(ref lambda, lambda.Length - 1); }
            }
                      
            

            double scale = Math.Round(Accord.Statistics.Measures.Median(lambda),precisao);

            var distribuição = new MathNet.Numerics.Distributions.Cauchy(location, scale);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.CumulativeDistribution(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.CumulativeDistribution(limites[i + 1]) - distribuição.CumulativeDistribution(limites[i]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void NormalDobrada()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Math.Round(Accord.Statistics.Measures.Mean(list),precisao);
            double SDV=Math.Round(Accord.Statistics.Measures.StandardDeviation(list),precisao);
            

            var distribuição = new Accord.Statistics.Distributions.Univariate.FoldedNormalDistribution(M, SDV);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.DistributionFunction(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.DistributionFunction(limites[i], limites[i + 1]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
        }
        public void Gumbel()
        {

            chart4.Series[1].Points.Clear();
            chart5.Series[1].Points.Clear();
            chart4.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart5.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;


            Array.Resize(ref probDasClassesEsp, 0);
            Array.Resize(ref probDasClassesEsp, Convert.ToInt32(K));
            Array.Resize(ref probabilidadeAcumuladaEsperada, 0);
            Array.Resize(ref probabilidadeAcumuladaEsperada, Convert.ToInt32(listaSemRepetidos.Length));
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            //----------------------------------
            double M = Accord.Statistics.Measures.Mean(list);
            double V = Accord.Statistics.Measures.Variance(list);

            double shape = Math.Sqrt((6 * V) / (Math.PI * Math.PI));
            double location = M - (shape * 0.57721);
            shape = Math.Round(shape, precisao);
            location = Math.Round(location, precisao);

            var distribuição = new Accord.Statistics.Distributions.Univariate.GumbelDistribution(location, shape);
            i = 0;
            for (i = 0; i < listaSemRepetidos.Length; i++)
            {
                //Ks                
                probabilidadeAcumuladaEsperada[i] = Math.Round(distribuição.DistributionFunction(listaSemRepetidos[i]), precisao);
                chart5.Series[1].Points.AddXY(listaSemRepetidos[i], probabilidadeAcumuladaEsperada[i]);
            }
            i = 0;
            for (i = 0; i < K; i++)
            {
                //X²              
                probDasClassesEsp[i] = distribuição.DistributionFunction(limites[i], limites[i + 1]);
                probDasClassesEsp[i] = Math.Round(probDasClassesEsp[i] * quantidade2, precisao);
                chart4.Series[1].Points.AddXY(limites[i].ToString() + "-" + limites[i + 1].ToString(), probDasClassesEsp[i]);
            }
            RemovendoMenorQue5();
            v = K - 1;;
            Testes();
            textBox4.Text = distribuição.ToString()+"     Nível de Significância = "+ nivelDeSignificancia;
            
                      
        }
        
        //Configuração dos gráficos
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)
            {
                chart3.Series[0].IsValueShownAsLabel = true;


            }
            else
            {
                chart3.Series[0].IsValueShownAsLabel = false;

            }

        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                chart4.Series[0].IsValueShownAsLabel = true;
                chart4.Series[1].IsValueShownAsLabel = true;

            }
            else if (checkBox3.Checked == false)
            {
                chart4.Series[0].IsValueShownAsLabel = false;
                chart4.Series[1].IsValueShownAsLabel = false;

            }
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                chart2.Series[1].MarkerSize = 5;


            }
            else
            {
                chart2.Series[1].MarkerSize = 0;

            }

        }                    

       
        //Botões        
        private void botãoCalcular(object sender, EventArgs e)
        {
            nivelDeSignificancia = Convert.ToDouble(comboBox1.Text);
            ValoresDaAmostra();
            try
           {
                calculoCompleto();
            }
           catch { MessageBox.Show("Não foi possível calcular os parâmetros, verifique sua amostra.", "Erro:", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView5.ReadOnly = true;            
            button1.Enabled = false;
            button2.Enabled = true;
            tabControl1.Enabled = true;
            exportarToolStripMenuItem.Enabled = true;

            dataGridView2.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
        }
        private void botãoRecalcular(object sender, EventArgs e)
        {
            i = 0;
            j = 0;            
            outInf = 0;
            outInf2 = 0;
            outSup = 0;
            outSup2 = 0;
            dataGridView5.ReadOnly = true;

            int linhas = dataGridView1.Rows.Count;
            i = 0;
            for (i = 0; i < linhas; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = null;
                dataGridView1.Rows[i].Cells[2].Value=null;                
            } i = 0;

            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            popularDataGridView3();
            KTemp = 0;

            dataGridView5.Rows.Clear();
            foreach (string restante in listaTexto2)
            {
                dataGridView5.Rows.Add();
                dataGridView5.Rows[i].Cells[0].Value = listaTexto2[i];      
                i++;
            }
            ValoresDaAmostra();
            calculoCompleto();
        }
        private void botãoReiniciar(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Restart();
        }
        
        //Removendo classes com menos de 5 itens para teste X²
        private void VerificandoMenorQue5()
        {
            if (j == 0)
            {
                foreach (double z in removendo1)
                {
                    if (z < 5)
                    {
                        ArrumandoClasses();
                    }
                    else { i = 0; }
                }
            }
            else if (j == 1)
            {
                foreach (double z in removendo2)
                {
                    if (z < 5)
                    {
                        ArrumandoClasses2();
                    }
                    else { i = 0; }
                }
            }
            Array.Resize(ref removendo1, Convert.ToInt32(KTemp));
            Array.Resize(ref removendo2, Convert.ToInt32(KTemp));
        }
        private void ArrumandoClasses()
        {
            i = 0;
            for (i = 0; i < 1; i++)
            {
                if (KTemp == 1) { break; }
                else if (removendo1[0] < 5)
                {
                    removendo1[i + 1] += removendo1[i];                    
                    removendo2[i+1]+=removendo2[i];                    
                    var lista = removendo1.ToList();
                    var lista2 = removendo2.ToList();
                    lista.RemoveAt(i);
                    lista2.RemoveAt(i);
                    removendo1 = lista.ToArray();
                    removendo2 = lista2.ToArray();                    
                    KTemp = removendo1.Length;
                    VerificandoMenorQue5();

                }

            }

            
            for (i = 1; i < KTemp; i++)
            {
                if (KTemp == 1) { break; }
                else if (removendo1[Convert.ToInt32(KTemp-i)] < 5)
                {
                    removendo1[Convert.ToInt32(KTemp - i - 1)] += removendo1[Convert.ToInt32(KTemp - i)];
                    removendo2[Convert.ToInt32(KTemp - i - 1)] += removendo2[Convert.ToInt32(KTemp - i)];
                    var lista = removendo1.ToList();
                    var lista2 = removendo2.ToList();
                    lista.RemoveAt(Convert.ToInt32(KTemp - i));
                    lista2.RemoveAt(Convert.ToInt32(KTemp - i));                    
                    removendo1 = lista.ToArray();
                    removendo2 = lista2.ToArray();
                    
                    KTemp = removendo1.Length;
                    VerificandoMenorQue5();
                }

            }

        }        
        private void ArrumandoClasses2()
        {
            i = 0;
            for (i = 0; i < 1; i++)
            {
                if (KTemp == 1) { break; }
                
                else if (removendo2[0] < 5)
                {
                    removendo2[i + 1] += removendo2[i];                    
                    removendo1[i+1]+=removendo1[i];                    
                    var lista = removendo1.ToList();
                    var lista2 = removendo2.ToList();
                    lista.RemoveAt(i);
                    lista2.RemoveAt(i);
                    removendo1 = lista.ToArray();
                    removendo2 = lista2.ToArray();
                    KTemp = removendo2.Length;
                    VerificandoMenorQue5();

                }

            }
            
           
            for (i = 1; i < KTemp; i++)
            {
                if (KTemp == 1) { break; }
                else if (removendo2[Convert.ToInt32(KTemp - i)] < 5)
                {
                    removendo1[Convert.ToInt32(KTemp - i - 1)] += removendo1[Convert.ToInt32(KTemp - i)];
                    removendo2[Convert.ToInt32(KTemp - i - 1)] += removendo2[Convert.ToInt32(KTemp - i)];
                    var lista = removendo1.ToList();
                    var lista2 = removendo2.ToList();  
                    lista.RemoveAt(Convert.ToInt32(KTemp - i));
                    lista2.RemoveAt(Convert.ToInt32(KTemp - i));
                    removendo1 = lista.ToArray();
                    removendo2 = lista2.ToArray();
                    KTemp = removendo1.Length;
                    VerificandoMenorQue5();
                }

            }

        }
        public void RemovendoMenorQue5()
        {
            
            KTemp = K;
            Array.Resize(ref removendo1, Convert.ToInt32(K));
            Array.Resize(ref removendo2, Convert.ToInt32(K));

            for (i = 0; i < K; i++) { removendo1[i] = freqDasClasses[i]; removendo2[i] = probDasClassesEsp[i]; }
            j = 0;
            ArrumandoClasses();            
            j = 1;
            ArrumandoClasses2();
            j = 0;                 
        }                
        
        //Copiar Itens das Tabelas
        private void copiar_Click(object sender, EventArgs e)
        {
            CopyKey();
        }
        private void CopyKey()
        {
            if (Clipboard.ContainsText() == true)
            {
                SendKeys.Send("^{c}");
            }
        }


        //Testes de Aderência
        private void Testes()
        {
                        
            try
            {
                var X2 = new Accord.Statistics.Testing.ChiSquareTest(removendo2, removendo1, Convert.ToInt32(v));
                X2.Size = nivelDeSignificancia;                             
                var Chi = new Accord.Statistics.Distributions.Univariate.ChiSquareDistribution(Convert.ToInt32(v));                
                double valorCrítico1 = Chi.InverseDistributionFunction(1.0-nivelDeSignificancia);
                
                string P1="";
                if (X2.PValue < (1 / Math.Pow(10, precisao))) 
                { 
                    P1 = X2.PValue.ToString("0.000E0"); 
                }               
                else
                {
                    P1 = Math.Round(X2.PValue, precisao).ToString();
                }
                Console.WriteLine("vbabva");
                if (tabelaSelecionada == true) 
                {
                    dataGridView3.Rows[indiceDoTeste].Cells[1].Value = P1;                   
                }
                else { textBox5.Text = "Teste Qui-Quadrado (" + X2.DegreesOfFreedom.ToString() + " graus de liberdade; " + "Valor Crítico = " + Math.Round(valorCrítico1, precisao).ToString() + "; X² = " + Math.Round(X2.Statistic, precisao).ToString() + "; Valor P = " + P1 + ")"; }
            }
            catch { textBox5.Text = "Não foi possível realizar o teste X²"; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; }

            try
            {
                var ks = new Accord.Statistics.Testing.TwoSampleKolmogorovSmirnovTest(Sx, probabilidadeAcumuladaEsperada);
                ks.Size = nivelDeSignificancia;
                var Kolmogorov = new Accord.Statistics.Distributions.Univariate.KolmogorovSmirnovDistribution(Sx.Length);
                double valorCrítico2 = Kolmogorov.InverseDistributionFunction(1.0 - nivelDeSignificancia);
                
                string P2="";
                if (ks.PValue < (1 / Math.Pow(10, precisao)))
                {
                    P2 = ks.PValue.ToString("0.000E0");
                }
                else { P2 = Math.Round(ks.PValue, precisao).ToString(); }

               
                if (tabelaSelecionada == true) 
                {
                    dataGridView3.Rows[indiceDoTeste].Cells[2].Value = P2;                   
                }
                else { textBox7.Text = "Teste Kolmogorov-Smirnov (" + listaSemRepetidos.Length.ToString() + " amostras; " + "Valor Crítico = " + Math.Round(valorCrítico2, precisao) + "; D máximo = " + Math.Round(ks.Statistic, precisao).ToString() + "; Valor P = " + P2 + ")"; }
            }
            catch { textBox7.Text = " Não foi possível realizar o teste KS"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value="NA"; }
            
            

        }

       //Config        
        public void ListaDasEstatisticas()
        {
            if (indiceDoTeste == 0) {try{ Beta(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 1) {try{ Binomial(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 2) {try{ ContinuaUniforme(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 3) {try{ Erlang(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 4) {try{ DistribuiçãoExponencial(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 5) {try{ Gama(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 6) {try{ Geométrica(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 7) {try{ GamaInversa(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 8) {try{ Laplace(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 9) {try{ LogNormal(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 10) {try{ BinomialNegativa(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 11) {try{ Normal(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 12) {try{ Pareto(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 13) {try{ Poisson(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 14) {try{ Rayleigh(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 15) {try{ Triangular(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 16) {try{ Weibull(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 17) {try{ Cauchy(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 18) {try{ NormalDobrada(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
            else if (indiceDoTeste == 19) {try{ Gumbel(); }catch{textBox4.Text = "Não foi possível utilizar a distribuição selecionada.";                textBox5.Text = "";textBox7.Text = ""; dataGridView3.Rows[indiceDoTeste].Cells[1].Value="NA"; dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";}}
        }
        public void CalcularEstatisticas()
        {
            indiceDoTeste = 0;

            for (indiceDoTeste = 0; indiceDoTeste < dataGridView3.Rows.Count; indiceDoTeste++)
            {
                try
                {
                    ListaDasEstatisticas();
                }
                catch
                {
                    dataGridView3.Rows[indiceDoTeste].Cells[1].Value = "NA";
                    dataGridView3.Rows[indiceDoTeste].Cells[2].Value = "NA";
                }
            }
            
            indiceDoTeste = 0;
            textBox4.Text = "";

            double[] x2 = new double[dataGridView3.Rows.Count];
            double[] ks = new double[dataGridView3.Rows.Count];
            string[] posiDistx2 = new string[dataGridView3.Rows.Count];
            string[] denyDistx2 = new string[dataGridView3.Rows.Count];
            string[] posiDistks = new string[dataGridView3.Rows.Count];
            string[] denyDistks = new string[dataGridView3.Rows.Count];

            try
            {
                i = 0;
                for (i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    string cell1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                    if (cell1 != "NA")
                    {
                        if (Convert.ToDouble(cell1) <= nivelDeSignificancia) { dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.IndianRed; }
                        else { dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Black; }
                        x2[i] = Convert.ToDouble(cell1);
                    }
                    else { dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.IndianRed; x2[i] = -100; }

                    string cell2 = dataGridView3.Rows[i].Cells[2].Value.ToString();
                    if (cell2 != "NA")
                    {
                        if (Convert.ToDouble(cell2) <= nivelDeSignificancia) { dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.IndianRed; }
                        else { dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Black; }
                        ks[i] = Convert.ToDouble(cell2);
                    }
                    else { dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.IndianRed; ks[i] = -100; }
                }
                i = 0;
            }
            catch { }
           
            
            List<double> x2List = x2.ToList<double>();
            double x2max=x2List.Max();
            int distIndex1 = x2List.IndexOf(x2max);
            indiceDoTeste = distIndex1;
            tabelaSelecionada = false;
            ListaDasEstatisticas();
            bestDistributionx2 = textBox4.Text+"\r\n"+textBox5.Text;

            List<double> ksList = ks.ToList<double>();
            double ksmax = ksList.Max();
            int distIndex2 = ksList.IndexOf(ksmax);
            indiceDoTeste = distIndex2;
            tabelaSelecionada = false;
            ListaDasEstatisticas();
            bestDistributionks = textBox4.Text + "\r\n" + textBox7.Text;

            i = 0;
            int c1 = 0, c2 = 0, c3 = 0, c4 = 0;
            for (i = 0; i < dataGridView3.Rows.Count; i++)
            {               
                if (x2List[i]>nivelDeSignificancia)
                {
                    if (x2List[i] != null)
                    {
                        posiDistx2[c1] = dataGridView3.Rows[i].Cells[0].Value.ToString();
                        c1++;
                    }
                    else { x2List.RemoveAt(i); }
                }
                else { denyDistx2[c2] = dataGridView3.Rows[i].Cells[0].Value.ToString(); c2++; }
                
                if (ksList[i]>nivelDeSignificancia)
                {
                    if (ksList[i] != null)
                    {
                        posiDistks[c3] = dataGridView3.Rows[i].Cells[0].Value.ToString();
                        c3++;
                    }
                    else { ksList.RemoveAt(i); }
                }
                else { denyDistks[c4] = dataGridView3.Rows[i].Cells[0].Value.ToString(); c4++; }

            } i = 0; 

            Array.Resize(ref posiDistx2, c1);
            Array.Resize(ref denyDistx2, c2);
            Array.Resize(ref posiDistks, c3);
            Array.Resize(ref denyDistks, c4);
            c1 = 0; c2 = 0; c3 = 0; c4 = 0;

            posiveisDistX2=String.Join("; ",posiDistx2);
            posiveisDistKs=String.Join("; ",posiDistks);
            negadasDistX2=String.Join("; ",denyDistx2);
            negadasDistKs=String.Join("; ",denyDistks);

            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
            
        }
        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
           
            Frequências();
            nivelDeSignificancia = Convert.ToDouble(comboBox1.Text);            
            tabelaSelecionada = true;            
            CalcularEstatisticas();
            tabelaSelecionada = false;
            Resultado();
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frequências();
            nivelDeSignificancia = Convert.ToDouble(comboBox1.Text);
            tabelaSelecionada = true;
            CalcularEstatisticas();           
            tabelaSelecionada = false;
            Resultado();
            
        }
        public void Resultado()
        {
            richTextBox1.Text = "";

            richTextBox1.Text += "Teste do Qui-Quadrado \r\n";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Melhor Distribuição:  ";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += bestDistributionx2;
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Distribuições Possíveis: \r\n";
            richTextBox1.Text += posiveisDistX2;
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Distribuições Rejeitadas: \r\n";
            richTextBox1.Text += negadasDistX2;
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "------------------------------------------------------------------------------------------------------------------------";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Teste de Kolmogorov-Smirnov \r\n";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Melhor Distribuição:  ";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += bestDistributionks;
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Distribuições Possíveis: \r\n";
            richTextBox1.Text += posiveisDistKs;
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += "Distribuições Rejeitadas: \r\n";
            richTextBox1.Text += negadasDistKs;
            richTextBox1.Text += "\r\n";

        }

    
        //Expandir e Salvar Gráfico
        private void expandirToolStripMenuItem_Click(object sender, EventArgs e)
     {
         cont++;
            
          Chart newChart=CopyChart(graficoSelecionado);         
         Form3 newWindow = new Form3();
        newWindow.Name = "Gráfico " + cont.ToString();
        newWindow.Text = "Gráfico " + cont.ToString();
        newWindow.AutoSize = true;
         newWindow.Controls.Add(newChart);
         newWindow.Show();
         newChart.Dock = DockStyle.Fill;
         
     }
        private void chart2_MouseDown(object sender, MouseEventArgs e)
     {
         if (e.Button == MouseButtons.Right) { graficoSelecionado = (Chart)sender;}

     }
         private void chart1_MouseDown(object sender, MouseEventArgs e)
     {
         if (e.Button == MouseButtons.Right) { graficoSelecionado = (Chart)sender;}

     }
        private void chart3_MouseDown(object sender, MouseEventArgs e)
     {
         if (e.Button == MouseButtons.Right) { graficoSelecionado = (Chart)sender; }
     }
        private void chart4_MouseDown(object sender, MouseEventArgs e)
     {
         if (e.Button == MouseButtons.Right) { graficoSelecionado = (Chart)sender; }
     }
        private void chart5_MouseDown(object sender, MouseEventArgs e)
     {
         if (e.Button == MouseButtons.Right) { graficoSelecionado = (Chart)sender; }
         
     }
        public Chart CopyChart(Chart chartOriginal)
     {
         
         Chart newChart = new Chart();
         //newChart = chartOriginal;
         newChart.BackColor = chartOriginal.BackColor;
         newChart.ChartAreas.Add(chartOriginal.ChartAreas[0].Name);        
         newChart.ClientSize = chartOriginal.ClientSize;
         newChart.ContextMenu = chartOriginal.ContextMenu;
         newChart.ContextMenuStrip = chartOriginal.ContextMenuStrip;
         newChart.ForeColor = chartOriginal.ForeColor;         
         newChart.Padding = chartOriginal.Padding;
         newChart.Size = new Size(1200, 600);         
         newChart.Legends.Add(chartOriginal.Legends[0].Name);
         newChart.Legends[0].Alignment = chartOriginal.Legends[0].Alignment;
         newChart.Legends[0].Enabled = true;
         newChart.Legends[0].Docking = Docking.Bottom;
         newChart.Legends[0].IsTextAutoFit = false;
         newChart.Legends[0].IsDockedInsideChartArea = false;
         newChart.Legends[0].DockedToChartArea = newChart.ChartAreas[0].Name;
         newChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
         newChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
         i = 0;
         
            for (i = 0; i < chartOriginal.Series.Count; i++)
         {
             newChart.Series.Add(chartOriginal.Series[i].Name);             
             newChart.Series[i].Enabled = true;
             newChart.Series[i] = chartOriginal.Series[i];
             
         }i=0;
         newChart.Name = "newChart" + cont.ToString();

         return newChart;

     }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Bitmap|*.BMP";
            saveFileDialog1.Filter +="|PNG|*.PNG";
            saveFileDialog1.Filter += "|GIF|*.GIF";
            saveFileDialog1.Filter += "|JPEG|*.JPEG";
            saveFileDialog1.Filter += "|All Files|*.*";
            //saveFileDialog1.Filter += "All files (*.*)|*.*|";
            
            saveFileDialog1.ShowDialog();
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            endereço = saveFileDialog1.FileName;
            SalvarGráfico();            
            
        }
        public void SalvarGráfico()
        {
            
            Chart chartNovo2 = CopyChart(graficoSelecionado);
            chartNovo2.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            chartNovo2.ChartAreas[0].AxisY.IsLabelAutoFit = false;

            chartNovo2.Size = new Size(1366, 768);

            Bitmap imagem = new Bitmap(chartNovo2.Size.Width, chartNovo2.Size.Height);
            imagem.SetResolution(600.0F, 600.0F);
            chartNovo2.DrawToBitmap(imagem, new Rectangle(0, 0, 1366, 768));
            
            imagem.Save(endereço, System.Drawing.Imaging.ImageFormat.Png);


        }

        //Configurar Gráfico
        private void configurarStripMenuItem_Click(object sender, EventArgs e)
        {
            
            configuração=new PropriedadesDoGráfico();
            configuração.Gráfico=graficoSelecionado;
            configuração.ItemsParaMostrar();       


            propriedades = new PropertyGrid();
            propriedades.SelectedObject = configuração;      
            propriedades.HelpVisible = false;
            propriedades.Dock = DockStyle.Fill;
            propriedades.Font = new Font("Century Gothic", 10.0f);
            propriedades.PropertyValueChanged += propriedades_PropertyValueChanged;            

            Form3 config = new Form3();
            config.FormBorderStyle = FormBorderStyle.FixedSingle;
            config.Size = new Size(400, 300);
            config.Font = Form1.DefaultFont;
            config.Controls.Add(propriedades);
            config.ShowIcon = false;
            config.ShowDialog();           
           
            
        }
        public class  PropriedadesDoGráfico
        {
            Chart gráfico;           
            string título;
            Color backGroundColor;
            Color[] corDasSeries;           
            SeriesCollection series;
            MarkerStyle[] marcador;           
            public enum TiposDeGráfico{Ponto,Retas,Curvas,Coluna,Degrau,Boxplot};
            public TiposDeGráfico[] tipos;            
            string[] nomeDaSérie;
            bool[] visívelNaLegenda;

           
            public void ItemsParaMostrar()
            {
                int i = 0;
                if (Gráfico.Titles.Count != 0)
                {
                    Título = Gráfico.Titles[0].Text;
                }
                else { Gráfico.Titles.Add(""); Título = Gráfico.Titles[0].Text; }

                BackGroundColor = Gráfico.ChartAreas[0].BackColor;
                series = Gráfico.Series;
                CorDasSeries = new Color[series.Count];                
                Marcador=new MarkerStyle[series.Count];
                Tipos = new TiposDeGráfico[series.Count];                
                NomeDaSérie = new string[series.Count];
                VisívelNaLegenda = new bool[series.Count];
                

                for (i = 0; i < series.Count; i++)
                {
                    Marcador[i] = series[i].MarkerStyle;
                    CorDasSeries[i] = series[i].Color;
                    NomeDaSérie[i] = series[i].Name;
                    VisívelNaLegenda[i] = series[i].IsVisibleInLegend;
                    
                    if (series[i].ChartType == SeriesChartType.Column) { Tipos[i] = TiposDeGráfico.Coluna; }
                    else if (series[i].ChartType == SeriesChartType.Point) { Tipos[i] = TiposDeGráfico.Ponto; }
                    else if (series[i].ChartType == SeriesChartType.Line) { Tipos[i] = TiposDeGráfico.Retas; }
                    else if (series[i].ChartType == SeriesChartType.Spline) { Tipos[i] = TiposDeGráfico.Curvas; }
                    else if (series[i].ChartType == SeriesChartType.StepLine) { Tipos[i] = TiposDeGráfico.Degrau; }
                    else if (series[i].ChartType == SeriesChartType.BoxPlot) { Tipos[i] = TiposDeGráfico.Boxplot; }

                }
                
            }
    
            [Browsable(false)]
            public Chart Gráfico
            {
                get { return gráfico; }
                set { gráfico = value; }
            }

            [Category("Informação")]
            [DisplayName("Título")]            
            public string Título
            {
                get { return título; }
                set { título = value; }
            }

            [Category("Área")]
            [DisplayName("Cor de Fundo")]
            public Color BackGroundColor
            {
                get { return backGroundColor; }
                set { backGroundColor = value; }
            }

            [Category("Séries")]
            [DisplayName("Nome")]             
            public string[] NomeDaSérie
            {
               
                get { return nomeDaSérie; }
                set { nomeDaSérie = value; }
            }           
            
            [Category("Séries")]
            [DisplayName("Cor")]             
            public Color[] CorDasSeries
            {
                get { return corDasSeries; }
                set { corDasSeries = value; }
            }

            [Category("Séries")]
            [DisplayName("Marcador")]            
            public MarkerStyle[] Marcador
            {
                get { return marcador; }
                set { marcador = value; }
            }

            [Category("Séries")]
            [DisplayName("Tipo de Gráfico")]                
            public TiposDeGráfico[] Tipos
            {
                get { return tipos; }
                set { tipos = value; }
            }

            [Category("Séries")]
            [DisplayName("Mostrar na Legenda")]
            public bool[] VisívelNaLegenda
            {
                get { return visívelNaLegenda; }
                set { visívelNaLegenda = value; }
            }

            

                       
        }
        public void propriedades_PropertyValueChanged(object sender, EventArgs e)
        {
            
            graficoSelecionado.ChartAreas[0].BackColor = configuração.BackGroundColor;
            graficoSelecionado.Titles[0].Text = configuração.Título;
            
            i = 0;
            for (i = 0; i < graficoSelecionado.Series.Count; i++)
            {
                graficoSelecionado.Series[i].MarkerStyle = configuração.Marcador[i];
                graficoSelecionado.Series[i].Color = configuração.CorDasSeries[i];
                
                if (configuração.Tipos[i].ToString() =="Ponto" ) { graficoSelecionado.Series[i].ChartType = SeriesChartType.Point; }
                else if (configuração.Tipos[i].ToString() == "Coluna") { graficoSelecionado.Series[i].ChartType = SeriesChartType.Column; }
                else if (configuração.Tipos[i].ToString() == "Retas") { graficoSelecionado.Series[i].ChartType = SeriesChartType.Line; }
                else if (configuração.Tipos[i].ToString() == "Curvas") { graficoSelecionado.Series[i].ChartType = SeriesChartType.Spline; }
                else if (configuração.Tipos[i].ToString() == "Degrau") { graficoSelecionado.Series[i].ChartType = SeriesChartType.StepLine; }
                else if (configuração.Tipos[i].ToString() == "Boxplot") { graficoSelecionado.Series[i].ChartType = SeriesChartType.BoxPlot; }

                graficoSelecionado.Series[i].LegendText = configuração.NomeDaSérie[i];
                graficoSelecionado.Series[i].IsVisibleInLegend = configuração.VisívelNaLegenda[i];
            } i = 0;
            
         
        }
        
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Cronômetro
        private void cronômetroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cronômetro cronometro = new Cronômetro();
            Form timer = new Form();
            timer.Controls.Add(cronometro);
            timer.ShowIcon = false;
            timer.Text = "";
            timer.FormBorderStyle = FormBorderStyle.FixedSingle;            
            timer.Size = new Size(405, 280);
            timer.MaximumSize = new Size(405, 280);
            timer.AcceptButton = (Button)cronometro.Controls["startButton"];
            cronometro.Controls["dataGridView1"].ContextMenuStrip = contextMenuStrip1;
            timer.Show();
            
        }        

        //Exportar
        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder=new FolderBrowserDialog();
            folder.Description = "Exportar";
            

            if ( folder.ShowDialog()== DialogResult.OK)
            {
                pasta = folder.SelectedPath;
                SalvandoGráficosComoImagem();
                try
                {
                    TabelasNoExcel();
                }
                catch { MessageBox.Show("Necessário Excel Instalado", "Missing Components", MessageBoxButtons.OK); }
                try
                {
                    ResultadoNoWord();
                }
                catch { MessageBox.Show("Necessário Word Instalado", "Missing Components", MessageBoxButtons.OK); }
            }
            

        }
        public void SalvandoGráficosComoImagem()
        {
            
            graficoSelecionado = chart1;
            endereço = Path.Combine(pasta, "Box-Plot.png");
            SalvarGráfico();
            graficoSelecionado = chart2;
            endereço = Path.Combine(pasta, "Amostra.png");
            SalvarGráfico();
            graficoSelecionado = chart3;
            endereço = Path.Combine(pasta,"Histograma.png");
            SalvarGráfico();
            graficoSelecionado = chart4;
            endereço = Path.Combine(pasta,"Distribuição por Classe.png");
            SalvarGráfico();
            graficoSelecionado = chart5;
            endereço = Path.Combine(pasta,"Distribuição Acumulada.png");
            SalvarGráfico();
        }
        public void TabelasNoExcel()
        {
            //Tabelas no Excel
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = Excel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);

            //Tabela Valor P
            Microsoft.Office.Interop.Excel.Worksheet ws4 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add();
            ws4.Name = "Valor P";
            ws4.Cells[1, 1] = "Distribuição";
            ws4.Cells[1, 2] = "P(X²)";
            ws4.Cells[1, 3] = "P(Ks)";

            i = 0;
            j = 0;
            for (i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    ws4.Cells[i + 2, j + 1] = dataGridView3.Rows[i].Cells[j].Value;
                } j = 0;
            } i = 0; j = 0;

            ws4.get_Range("A1", "C1").Font.Bold = true;
            ws4.get_Range("A1", "A" + (dataGridView3.Rows.Count + 1).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ws4.get_Range("B1", "C" + (dataGridView3.Rows.Count + 1).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws4.get_Range("A1", "C" + (dataGridView3.Rows.Count + 1).ToString()).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws4.Columns.AutoFit();

            //Tabela de Frequências
            Microsoft.Office.Interop.Excel.Worksheet ws3 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add();
            ws3.Name = "Frequências";

            ws3.get_Range("A1", "C1").Merge();

            ws3.get_Range("A1", "C1").Merge();
            ws3.Cells[1, 1] = "K (número de classes)";
            ws3.get_Range("A3", "C3").Merge();
            ws3.Cells[3, 1] = "h (tamanho da classe)";
            ws3.Cells[1, 4] = K;
            ws3.Cells[3, 4] = h;
            ws3.get_Range("A1", "A3").Font.Bold = true;
            ws3.get_Range("A1", "A3").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ws3.get_Range("D1", "D3").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            i = 0;
            for (i = 0; i < dataGridView2.Columns.Count; i++)
            {
                ws3.Cells[5, i + 1] = dataGridView2.Columns[i].HeaderText.ToString();

            }

            i = 0;
            j = 0;
            for (i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    ws3.Cells[i + 6, j + 1] = dataGridView2.Rows[i].Cells[j].Value;
                } j = 0;
            }
            ws3.get_Range("A5", "F5").Font.Bold = true;
            object endCell2 = "F" + (dataGridView2.Rows.Count + 5).ToString();
            ws3.get_Range("A5", endCell2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws3.get_Range("A5", endCell2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            ws3.Columns.AutoFit();

            //Tabela de Estatísticas
            Microsoft.Office.Interop.Excel.Worksheet ws2 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add();
            ws2.Name = "Estatísticas";
            ws2.get_Range("A1", "C1").Merge();
            ws2.get_Range("A1", "C1").Font.Bold = true;
            ws2.get_Range("A2", "C2").Font.Bold = true;
            ws2.get_Range("A1", "C1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws2.get_Range("B2", "C18").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws2.get_Range("A1", "C18").Borders.Value = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            ws2.get_Range("A1", "C18").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            ws2.Cells[1, 1] = "Tabela de Estatísticas";
            ws2.Cells[2, 1] = "Estatísticas";
            ws2.Cells[2, 2] = "Com Outliers";
            ws2.Cells[2, 3] = "Sem Outliers";

            i = 0;
            j = 0;
            for (i = 0; (i + 1) < dataGridView1.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    ws2.Cells[i + 3, j + 1] = dataGridView1.Rows[i + 1].Cells[j].Value;

                } j = 0;
            }
            ws2.Columns.AutoFit();
            i = 0; j = 0;

            
            //Tabela Amostra
            Microsoft.Office.Interop.Excel.Worksheet ws1 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add();
            ws1.Name = "Amostra";
            ws1.Cells[1, 1] = "Amostra Original";
            ws1.Cells[1, 2] = "Amostra Sem Outliers";
            ws1.Cells[1, 3] = "Outliers Superiores";
            ws1.Cells[1, 4] = "Outliers Inferiores";

            i = 0;
            for (i = 0; i < listaNumerica.Length; i++)
            {
                ws1.Cells[i + 2, 1] = listaNumerica[i];
            } i = 0;
            for (i = 0; i < listaNumerica2.Length; i++)
            {
                ws1.Cells[i + 2, 2] = listaNumerica2[i];
            } i = 0;
            for (i = 0; i < outliersSuperiores.Length; i++)
            {
                ws1.Cells[i + 2, 3] = outliersSuperiores[i];
            } i = 0;
            for (i = 0; i < outliersInferiores.Length; i++)
            {
                ws1.Cells[i + 2, 4] = outliersInferiores[i];
            } i = 0;

            object Cell2 = "D" + (listaNumerica.Length+1).ToString();
            ws1.get_Range("A1", "D1").Font.Bold = true;
            ws1.get_Range("A1", Cell2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws1.get_Range("A1", Cell2).Borders.Value = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            ws1.get_Range("A1", Cell2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws1.Columns.AutoFit();

            
            

            //Salvando Excel             
            object misValue=Type.Missing;         
            endereço = Path.Combine(pasta,"Tabelas.xls");            
            wb.SaveAs(endereço, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, false, misValue,Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue,misValue, misValue, misValue, misValue);
            
            Excel.Quit();
        }
        public void ResultadoNoWord()
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = word.Documents.Add();        
            
            object miss=Type.Missing;

            Microsoft.Office.Interop.Word.Range rng = doc.Range(0,0);
            doc.Paragraphs.Add(rng);
            doc.Paragraphs[1].Range.Text=richTextBox1.Text;
            doc.Paragraphs[1].Range.Font.Bold = 1;
            doc.Paragraphs[1].Range.Font.Name = "Times New Roman";            
           
            endereço = Path.Combine(pasta,"Resultado.pdf");
            doc.SaveAs2(endereço, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss);
            word.Quit();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SampleAnalyzer.Regressão regressão = new SampleAnalyzer.Regressão();            
            Form3 regress = new Form3();
            regress.Text = "";
            regress.ShowIcon = false;
            regress.FormBorderStyle = FormBorderStyle.FixedSingle;
            regress.Controls.Add(regressão);
            regress.Controls[0].Dock = DockStyle.Fill;
            regress.Size = new Size(750, 350);
            regress.Show();
        }

       
       
    }
}
