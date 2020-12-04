// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

charts = [];

window.chartBlazor = {
    
    init: function (chartModelsJson) {

        charts = [];
        
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_material);
            am4core.useTheme(am4themes_animated);
            // Themes end
            
            var chartModels = JSON.parse(chartModelsJson);

            chartModels.forEach((element) => {
                
                charts.push(createChart(element.Id));                  
            })

            function createChart(idChart) {

                // Create chart instance
                var chart = am4core.create(idChart, am4charts.XYChart);

                // Add data
                chart.data = generateChartData();

                // Create axes
                var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
                dateAxis.renderer.minGridDistance = 50;
                dateAxis.groupData = true;
                dateAxis.groupCount = 1000;
                //подписка на изменение масштаба и скрол графика
                dateAxis.events.on("startendchanged", dateAxisChanged);
                function dateAxisChanged(ev) {
                    
                    charts.forEach((element) => {
                        
                        element.cursor.xAxis.start = ev.target.start;
                        element.cursor.xAxis.end = ev.target.end;
                    })
                }

                var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

                // Create series
                var series = chart.series.push(new am4charts.LineSeries());
                series.minBulletDistance = 20;
                series.dataFields.valueY = "visits";
                series.dataFields.dateX = "date";
                series.strokeWidth = 2;                
                series.tooltipText = "{valueY}";
                series.tooltip.pointerOrientation = "vertical";
                series.tooltip.background.cornerRadius = 20;
                //series.tooltip.background.fillOpacity = 0.5;
                series.tooltip.label.padding(12, 12, 12, 12);                

                //рисование точек на графике
                var bullet = series.bullets.push(new am4charts.CircleBullet());
                //bullet.circle.stroke = am4core.color("#fff");
                //bullet.circle.strokeWidth = 1;

                // Add scrollbar
                chart.scrollbarX = new am4core.Scrollbar();
                //chart.scrollbarX = new am4charts.XYChartScrollbar();
                //chart.scrollbarX.series.push(series);

                // Add cursor
                chart.cursor = new am4charts.XYCursor();
                chart.cursor.xAxis = dateAxis;
                chart.cursor.snapToSeries = series;

                chart.svgContainer.autoResize = false;

                return chart;
            }
        });
    }
};

function generateChartData() {
    var chartData = [];
    var firstDate = new Date();
    firstDate.setDate(firstDate.getDate());//-1000
    var visits = 1200;
    for (var i = 0; i < 10000; i++) {
        // we create date objects here. In your data, you can have date strings
        // and then set format of your dates using chart.dataDateFormat property,
        // however when possible, use date objects, as this will speed up chart rendering.
        var newDate = new Date(firstDate);
        newDate.setMinutes(newDate.getMinutes() + i);
        //newDate.setDate(newDate.getDate() + i);

        visits += Math.round((Math.random() < 0.5 ? 1 : -1) * Math.random() * 10);

        chartData.push({
            date: newDate,
            visits: visits
        });
    }
    return chartData;
}
