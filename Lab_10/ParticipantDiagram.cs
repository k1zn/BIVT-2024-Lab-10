using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lab_10
{
    public partial class ParticipantDiagram : MyForm
    {
        public ParticipantDiagram(decimal moneySpentOnTickets, decimal moneyWinOnTickets)
        {
            InitializeComponent();
            InitializeChart(moneySpentOnTickets, moneyWinOnTickets);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeChart(decimal moneySpentOnTickets, decimal moneyWinOnTickets)
        {
            var chart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var area = new ChartArea("MainArea");
            chart.ChartAreas.Add(area);

            var series = new Series()
            {
                ChartType = SeriesChartType.Column
            };
            series.Points.AddXY("Денег потрачено", moneySpentOnTickets);
            series.Points.AddXY("Денег выиграно", moneyWinOnTickets);

            chart.Series.Add(series);

            this.Controls.Add(chart);
        }
    }
}
