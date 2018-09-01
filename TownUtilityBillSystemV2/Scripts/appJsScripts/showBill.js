//variables are declared in ShowBill.cshtml file

$(document).ready(function ()
{
	$.ajax({
		type: "GET",
		url: '/Bill/GetMeterDataHistoryForChart',
		data: {
			addressId: addressIdDTO,
			bill_Id: bill_IdDTO
		},
		dataType: "json",
		success: function (data)
		{
			for (var n = 0; n < 4; n++)
			{
				var containerName = 'container' + n;
				Highcharts.chart(containerName, {
					chart: {
						type: 'bar'
					},
					title: {
						text: data["UtilityResourceNames"][n] + ' ' + consumptionHistory
					},
					xAxis: {
						categories: [
							data["MetersChartData"][n][0].MonthName,
							data["MetersChartData"][n][1].MonthName,
							data["MetersChartData"][n][2].MonthName,
							data["MetersChartData"][n][3].MonthName,
							data["MetersChartData"][n][4].MonthName,
							data["MetersChartData"][n][5].MonthName,
							data["MetersChartData"][n][6].MonthName,
							data["MetersChartData"][n][7].MonthName,
							data["MetersChartData"][n][8].MonthName,
							data["MetersChartData"][n][9].MonthName,
							data["MetersChartData"][n][10].MonthName,
							data["MetersChartData"][n][11].MonthName,
							data["MetersChartData"][n][12].MonthName
						],
						title: {
							text: null
						},
					},
					yAxis: {
						min: 0,
						title: {
							text: value + ', ' + data["UnitNames"][n],
							align: 'high'
						},
						labels: {
							overflow: 'justify'
						}
					},
					tooltip: {
						valueSuffix: data["UnitNames"][n]
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
						name: value,
						data: [
							data["MetersChartData"][n][0].Value,
							data["MetersChartData"][n][1].Value,
							data["MetersChartData"][n][2].Value,
							data["MetersChartData"][n][3].Value,
							data["MetersChartData"][n][4].Value,
							data["MetersChartData"][n][5].Value,
							data["MetersChartData"][n][6].Value,
							data["MetersChartData"][n][7].Value,
							data["MetersChartData"][n][8].Value,
							data["MetersChartData"][n][9].Value,
							data["MetersChartData"][n][10].Value,
							data["MetersChartData"][n][11].Value,
							data["MetersChartData"][n][12].Value
						]
					}]
				});
			}

			Highcharts.chart('containerUtilitiesCharges', {
				chart: {
					plotBackgroundColor: null,
					plotBorderWidth: 0,
					plotShadow: false
				},
				title: {
					text: utilities + '<br>' + charges,
					align: 'center',
					verticalAlign: 'middle',
					y: 40
				},
				tooltip: {
					pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
				},
				plotOptions: {
					pie: {
						dataLabels: {
							enabled: true,
							distance: -50,
							style: {
								fontWeight: 'bold',
								color: 'white'
							}
						},
						startAngle: -90,
						endAngle: 90,
						center: ['50%', '75%']
					}
				},
				series: [{
					type: 'pie',
					name: 'Value',
					innerSize: '50%',
					data: [
						[data["UtilityResourceNames"][0], utilityChargesDTO[0]],
						[data["UtilityResourceNames"][1], utilityChargesDTO[1]],
						[data["UtilityResourceNames"][2], utilityChargesDTO[2]],
						[data["UtilityResourceNames"][3], utilityChargesDTO[3]],
						{
							name: 'Proprietary or Undetectable',
							y: 0.2,
							dataLabels: {
								enabled: false
							}
						}
					]
				}]
			});
		},
		error: function ()
		{ }
	});
})

function genPDF()
{
	html2canvas(document.getElementById("testDiv"), {
		onrendered: function (canvas)
		{
			var img = canvas.toDataURL("image/png");
			var doc = new jsPDF();
			doc.addImage(img, 'png', 10, 10, 200, 287);
			doc.save(pdfDocName);
		}
	});
}
