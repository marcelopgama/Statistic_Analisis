using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Inferência
{
    public partial class Cronômetro : UserControl
    {
        int milésimos=0;
        int segundos=0;
        int minutos=0;
        int indice = 0;
        double segundosTotais = 0;

        int milésimos2 = 0;
        int segundos2 = 0;
        int minutos2 = 0;
        double segundosTotais2 = 0;
        Stopwatch stopwatch = new Stopwatch();
        Stopwatch intervalStopwatch = new Stopwatch();
        TimeSpan time;        
        string tempoReal;
        string intervalo;

        
        
        public Cronômetro()
        {
            InitializeComponent();
            time = stopwatch.Elapsed;
            restartButton.Enabled = false;
            Adicionar.Enabled = false;            
            startButton.Font = new Font(startButton.Font.Name, 12.0F, startButton.Font.Style, startButton.Font.Unit);

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Iniciar")
            {
                timer1.Enabled = true;
                startButton.Text = "Pausar";
                stopwatch.Start();
                intervalStopwatch.Start();
                restartButton.Enabled = true;
                Adicionar.Enabled = true;
                startButton.Font=new Font(startButton.Font.Name, 12.0F,startButton.Font.Style, startButton.Font.Unit);

                
            }
            else if (startButton.Text == "Pausar") 
            { 
                stopwatch.Stop(); intervalStopwatch.Stop(); 
                startButton.Text = "Continuar";
                startButton.Font = new Font(startButton.Font.Name, 10.0F, startButton.Font.Style, startButton.Font.Unit);
            }
            else if (startButton.Text == "Continuar") 
            { 
                stopwatch.Start(); 
                intervalStopwatch.Start(); 
                startButton.Text = "Pausar";
                startButton.Font = new Font(startButton.Font.Name, 10.0F, startButton.Font.Style, startButton.Font.Unit);
            }

            
        }               

        private void restartButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            dataGridView1.Rows.Clear();
            stopwatch.Reset();
            intervalStopwatch.Reset();
            label2.Text = "00:00:000"+"              "+"00:00:000";
            milésimos = 0;
            segundos = 0;
            minutos = 0;
            milésimos2 = 0;
            segundos2 = 0;
            minutos2 = 0;
            indice = 0;
            segundosTotais = 0;
            segundosTotais2 = 0;
            startButton.Text = "Iniciar";
            restartButton.Enabled = false;
            Adicionar.Enabled=false;

        }
               
        private void Adicionar_Click(object sender, EventArgs e)
        {
            indice++;           
            dataGridView1.Rows.Add(indice,segundosTotais, segundosTotais2);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[indice - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            intervalStopwatch.Restart();
            stopwatch.Start();
            startButton.Text = "Pausar";
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            milésimos = stopwatch.Elapsed.Milliseconds;
            segundos = stopwatch.Elapsed.Seconds;
            minutos = stopwatch.Elapsed.Minutes;
            segundosTotais = Math.Round(stopwatch.Elapsed.TotalSeconds,3);

            milésimos2 = intervalStopwatch.Elapsed.Milliseconds;
            segundos2 = intervalStopwatch.Elapsed.Seconds;
            minutos2 = intervalStopwatch.Elapsed.Minutes;
            segundosTotais2 = Math.Round(intervalStopwatch.Elapsed.TotalSeconds,3);
            
            tempoReal = minutos.ToString().PadLeft(2, '0') + ":" + segundos.ToString().PadLeft(2, '0') + ":" + milésimos.ToString().PadLeft(3, '0');
            intervalo = minutos2.ToString().PadLeft(2, '0') + ":" + segundos2.ToString().PadLeft(2, '0') + ":" + milésimos2.ToString().PadLeft(3, '0');

            label2.Text =tempoReal+"              "+intervalo;
        }

        
    }
}
