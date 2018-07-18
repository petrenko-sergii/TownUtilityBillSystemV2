//variables are declared in ShowMeterData.cshtml file

$(document)
	.ready(function ()
	{
		$.ajax({
			type: "GET",
			url: '/Meter/GetMeterDataForChart',
			data: {
				meterId: meterId
	},
		dataType: "json",
		success: function (data)
		{
			var series = [
				{
					type: 'column',
					name: utilityResourceName + usage,
					data: data
				}
			];	
			createChart("#chartArea", monthsUsageYearHistory, series);
		}
                });
        });
function createChart(chartcontainer, title, data)
{
	Highcharts.chart('chartArear', {
		chart: {
			type: 'column'
		},
		title: {
			text: title
		},
		xAxis: {
			categories: [
				data[0].data[0].MonthName,
				data[0].data[1].MonthName,
				data[0].data[2].MonthName,
				data[0].data[3].MonthName,
				data[0].data[4].MonthName,
				data[0].data[5].MonthName,
				data[0].data[6].MonthName,
				data[0].data[7].MonthName,
				data[0].data[8].MonthName,
				data[0].data[9].MonthName,
				data[0].data[10].MonthName,
				data[0].data[11].MonthName,
			],
			crosshair: true
		},
		yAxis: {
			min: 0,
			title: {
				text: value +', ' + unitName,
				align: 'high'
			},
			labels: {
				overflow: 'justify'
			}
		},
		tooltip: {
			valueSuffix: unitName
		},
		plotOptions: {
			bar: {
				dataLabels: {
					enabled: true
				}
			}
		},
		legend: {
			enabled: false,
		},
		credits: {
			enabled: false
		},
		series: [{
			name: utilityResourceName,
			data: [
				[data[0].data[0].MonthName, data[0].data[0].Value],
				[data[0].data[1].MonthName, data[0].data[1].Value],
				[data[0].data[2].MonthName, data[0].data[2].Value],
				[data[0].data[3].MonthName, data[0].data[3].Value],
				[data[0].data[4].MonthName, data[0].data[4].Value],
				[data[0].data[5].MonthName, data[0].data[5].Value],
				[data[0].data[6].MonthName, data[0].data[6].Value],
				[data[0].data[7].MonthName, data[0].data[7].Value],
				[data[0].data[8].MonthName, data[0].data[8].Value],
				[data[0].data[9].MonthName, data[0].data[9].Value],
				[data[0].data[10].MonthName, data[0].data[10].Value],
				[data[0].data[11].MonthName, data[0].data[11].Value]
			],
			dataLabels: {
				enabled: true,
				rotation: -90,
				color: '#FFFFFF',
				align: 'right',
				format: '{point.y:.1f}', // one decimal
				y: 10, // 10 pixels down from the top
				style: {
					fontSize: '13px',
					fontFamily: 'Verdana, sans-serif'
				}
			}
		}]
	});
}