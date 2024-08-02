// See https://aka.ms/new-console-template for more information

double[] dataX = { 1, 2, 3, 4, 5 };
double[] dataY = { 1, 4, 9, 16, 25 };

ScottPlot.Plot myPlot = new();
myPlot.Add.Scatter(dataX, dataY);

myPlot.SavePng("quickstart.png", 400, 300);