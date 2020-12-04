using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorChart.Models
{
    public class ChartControl
    {
        public ChartModel[] ChartModels { get; set; }

        public ChartControl(int count)
        {
            ChartModels = new ChartModel[count];
            for (int i = 0; i < count; i++)
            {
                ChartModels[i] = new ChartModel { Id = $"chartdiv{i}" };
            }
            /*ChartModels = new ChartModel[2]
            {
                new ChartModel{ Id="chartdiv1"},
                new ChartModel{ Id="chartdiv2"}
            };*/
        }
    }
}
