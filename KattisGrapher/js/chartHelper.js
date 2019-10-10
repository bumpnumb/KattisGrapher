var chart = null;

class ChartOptions {
    options = {
        chart: {
            type: "line",
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
            stacked: false,
            dropShadow: {
                enabled: true,
                enabledSeries: [0],
                top: -2,
                left: 2,
                blur: 5,
                opacity: 0.06
            }
        },
        colors: [],
        stroke: {
            curve: "smooth",
            width: 3
        },
        dataLabels: {
            enabled: true
        },
        series: [],
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
    }
    constructor(data) {
        //parse incomming message and get the different users
        var obj = JSON.parse(data);
        for (var i = 0; i < obj.length; i++) {
            var color = HSVtoRGB((i + 1) / obj.length, 0.3, 1);
            this.options.colors.push('#' + componentToHex(color.r) + componentToHex(color.g) + componentToHex(color.b));

            var dp = [];

            for (var j = 0; j < obj[i].DataPoints.length; j++) {
                dp.push([obj[i].DataPoints[j].Time, obj[i].DataPoints[j].Value]);
            }


            this.options.series.push({ name: obj[i].Name, data: dp });
        }
    }
}

function DrawChart(options) {
    if (chart === null) {
        chart = new ApexCharts(document.querySelector("#timeline-chart"), options.options);
        chart.render();
    }

    chart.updateOptions(options.options, true, true);
    document.getElementsByClassName("graph_holder")[0].classList.remove("hidden");
}

function HSVtoRGB(h, s, v) {
    var r, g, b, i, f, p, q, t;
    if (arguments.length === 1) {
        s = h.s, v = h.v, h = h.h;
    }
    i = Math.floor(h * 6);
    f = h * 6 - i;
    p = v * (1 - s);
    q = v * (1 - f * s);
    t = v * (1 - (1 - f) * s);
    switch (i % 6) {
        case 0: r = v, g = t, b = p; break;
        case 1: r = q, g = v, b = p; break;
        case 2: r = p, g = v, b = t; break;
        case 3: r = p, g = q, b = v; break;
        case 4: r = t, g = p, b = v; break;
        case 5: r = v, g = p, b = q; break;
    }
    return {
        r: Math.round(r * 255),
        g: Math.round(g * 255),
        b: Math.round(b * 255)
    };
}

function componentToHex(c) {
    var hex = c.toString(16);
    return hex.length === 1 ? "0" + hex : hex;
}

function rgbToHex(r, g, b) {
    return "#" + componentToHex(r) + componentToHex(g) + componentToHex(b);
}