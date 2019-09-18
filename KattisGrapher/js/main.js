window.onload = function () {

    var w = window.innerWidth - 200;
    console.log(w);
    var x = document.getElementById("chart");
    x.style.maxWidth = w + "px";



    var chart = new ApexCharts(document.querySelector("#timeline-chart"), options);

    chart.render();
};

var options = {
    chart: {
        type: "area",
        height: 400,
        foreColor: "#999",
        scroller: {
            enabled: true,
            track: {
                height: 7,
                background: '#e0e0e0'
            },
            thumb: {
                height: 10,
                background: '#94E3FF'
            },
            scrollButtons: {
                enabled: false,
                size: 9,
                borderWidth: 2,
                borderColor: '#008FFB',
                fillColor: '#008FFB'
            },
            padding: {
                left: 30,
                right: 20
            }
        },
        stacked: true,
        dropShadow: {
            enabled: true,
            enabledSeries: [0],
            top: -2,
            left: 2,
            blur: 5,
            opacity: 0.06
        }
    },
    colors: ['#00E396', '#0090FF'],
    stroke: {
        curve: "smooth",
        width: 3
    },
    dataLabels: {
        enabled: true
    },
    series: [{
        name: 'Stig',
        data: generateDayWiseTimeSeries(0, 18)
    }, {
        name: 'John',
        data: generateDayWiseTimeSeries(1, 18)
    }],
    markers: {
        size: 0,
        strokeColor: "#fff",
        strokeWidth: 3,
        strokeOpacity: 1,
        fillOpacity: 1,
        hover: {
            size: 6
        }
    },
    xaxis: {
        type: "datetime",
        axisBorder: {
            show: false
        },
        axisTicks: {
            show: false
        }
    },
    yaxis: {
        labels: {
            offsetX: 24,
            offsetY: -5
        },
        tooltip: {
            enabled: true
        }
    },
    grid: {
        padding: {
            left: -5,
            right: 5
        }
    },
    tooltip: {
        x: {
            format: "dd MMM yyyy"
        }
    },
    legend: {
        position: 'top',
        horizontalAlign: 'left'
    },
    fill: {
        type: "solid",
        fillOpacity: 0.7
    }
};



function generateDayWiseTimeSeries(s, count) {
    var values = [[
        4, 3, 10, 9, 29, 19, 25, 9, 12, 7, 19, 5, 13, 9, 17, 2, 7, 5
    ], [
        2, 3, 8, 7, 22, 16, 23, 7, 11, 5, 12, 5, 10, 4, 15, 2, 6, 2
    ]];
    var i = 0;
    var series = [];
    var x = new Date("11 Nov 2012").getTime();
    while (i < count) {
        series.push([x, values[s][i]]);
        x += 86400000;
        i++;
    }
    return series;
}

