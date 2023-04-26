using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System;
using AssetBreakdownRegisterV2.models;
using System.Globalization;

namespace AssetBreakdownRegisterV3.Utility
{
    public class LoadWeeklyBreakdownCostChart
    {

        CultureInfo myCI;
        Calendar myCal;
        CalendarWeekRule myCWR;
        DayOfWeek myFirstDOW;

        //variables to be assigned in the constructor
        Chart chart;
        SqliteDataAccess sqlData;

        //data used to build the chart
        List<Dictionary<string, double>> chartData;

        public LoadWeeklyBreakdownCostChart(Chart chart, SqliteDataAccess sqlData)
        {
            this.chart = chart;
            this.sqlData = sqlData;
            this.myCI = new CultureInfo(CultureInfo.CurrentCulture.Name);
            this.myCal = myCI.Calendar;
            this.myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            this.myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
        }

        public void Load()
        {
            //clear chart series and add serie "data"
            chart.Series.Clear();
            chart.Series.Add("data");

            DateTime today = DateTime.Now;
            int thisYear = today.Year;
            //number of weeks to be shown in the chart (per se number of bars)
            int numberOfWeeksToAnalyze = 5;

            this.chartData = new List<Dictionary<string, double>>();
            /*example of the data structure
             * [{"WeekOfYear": 5,            
             * "Costs": 25
             * },
             * {"WeekOfYear": 6,
             *  "Costs": 30
             * },
             * ....
               ]    
            */

            //counter to control the data inserted in the chartData
            int counter = 0;
            do
            {
                //get the datetime for the previous week, it will incresed by 7 days each time the counter increases
                DateTime previousWeek = today.AddDays(-counter * 7);

                //assign to the chartData "counter" index the key week of the year and the costs to be later increased
                chartData.Insert(counter, new Dictionary<string, double>()
                {
                    ["WeekOfYear"] = myCal.GetWeekOfYear(previousWeek, myCWR, myFirstDOW),
                    ["Costs"] = 0
                });
                counter++;

            } while (counter < numberOfWeeksToAnalyze);

            //Loop each breakdown from the sqlData
            foreach (BreakdownModel breakdown in sqlData.breakdown_list)
            {
                //Parse the Register_date to datetime
                DateTime breakdownDate = DateTime.ParseExact(breakdown.Request_date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //get the number of the week based on the date
                int breakdownWeekNumber = myCal.GetWeekOfYear(breakdownDate, myCWR, myFirstDOW);

                //calculate the days past from today till the register_date
                TimeSpan timeSpan = today - breakdownDate;
                double daysSpan = timeSpan.TotalDays;

                //check if the days past are in a range defined by "numberOfWeeksToAnalyze"
                if (daysSpan < numberOfWeeksToAnalyze * 7 + 1 && daysSpan > 0)
                {
                    //if this breakdown is within the range then update the costs in the chartData (in the correct key, checked by the next if statement, based on the week)
                    foreach (var key in chartData)
                    {
                        if (key["WeekOfYear"] == breakdownWeekNumber)
                        {
                            key["Costs"] += breakdown.Cost;
                        }
                    }
                }
            }

            //remove the first and last label in the x axis
            chart.ChartAreas["ChartArea1"].AxisX.LabelStyle.IsEndLabelVisible = false;

            //reverse the list so that the older weeks appear first
            chartData.Reverse();

            //Loop the chartData to fill the series of the chart
            foreach (var key in chartData)
            {
                DataPoint Dp = new DataPoint();
                double costs = key["Costs"];
                //set the datapoint, x as string so that the chart don't create gaps when it has to displays weeks from different years
                Dp.SetValueXY(key["WeekOfYear"].ToString(), costs);
                Dp.Color = Color.FromArgb(2, 97, 174);
                //don't show label in top of the columns where the value is 0
                if (costs > 0) Dp.Label = costs.ToString();
                //add the datapoint to the chart series
                chart.Series["data"].Points.Add(Dp);
            }
        }
    }
}

