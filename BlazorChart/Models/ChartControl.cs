using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorChart.Models
{
    public class ChartControl
    {
        public ChartModel[] ChartModels { get; set; }

        public ChartControl()
        {
            ChartModels = new ChartModel[2]
            {
                new ChartModel{ Id="chartdiv1"},
                new ChartModel{ Id="chartdiv2"}
            };
        }
    }
}
