namespace ScottPlotWinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lineChart = new LineChart();
            lineChart.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var barChart = new BarChart();
            barChart.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var pieChart = new PieChart();
            pieChart.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var scatterChart = new ScatterChart();
            scatterChart.Show();
        }
    }
}