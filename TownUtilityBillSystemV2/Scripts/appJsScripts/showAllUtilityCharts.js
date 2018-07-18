$(document).ready(function ()
{
	$.ajax({
		type: "GET",
		url: '/Meter/GetAllUtilitiesDataForChart',
		data: {
			addressId: addressIdDTO
		},
		dataType: "json",
		success: function (data)
		{
			Highcharts.chart('containerForThreeUtilities', {
				chart: {
					type: 'column'
				},
				title: {
					text: localizedWordsForContainerForThreeUtilities[0]
				},
				subtitle: {
					text: localizedWordsForContainerForThreeUtilities[1]
				},
				xAxis: {
					categories: [
						data[0].name,
						data[1].name,
						data[2].name,
						data[3].name,
						data[4].name,
						data[5].name,
						data[6].name,
						data[7].name,
						data[8].name,
						data[9].name,
						data[10].name,
						data[11].name
					],
					crosshair: true
				},
				yAxis: {
					min: 0,
					title: {
						text: localizedWordsForContainerForThreeUtilities[2]
					}
				},
				tooltip: {
					headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
					pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
						'<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
					footerFormat: '</table>',
					shared: true,
					useHTML: true
				},
				plotOptions: {
					pie: {
						allowPointSelect: true,
						cursor: 'pointer',
						dataLabels: {
							enabled: true,
							format: '<b>{point.name}</b>:{point.percentage:.1f}%',
							style: {
								color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black',
							},
						}
					}
				},
				series: [{
					name: utilities[1],
					data: [
						data[0].waterValue,
						data[1].waterValue,
						data[2].waterValue,
						data[3].waterValue,
						data[4].waterValue,
						data[5].waterValue,
						data[6].waterValue,
						data[7].waterValue,
						data[8].waterValue,
						data[9].waterValue,
						data[10].waterValue,
						data[11].waterValue,
					]
				}, {
						name: utilities[2],
					data: [
						data[0].heatValue,
						data[1].heatValue,
						data[2].heatValue,
						data[3].heatValue,
						data[4].heatValue,
						data[5].heatValue,
						data[6].heatValue,
						data[7].heatValue,
						data[8].heatValue,
						data[9].heatValue,
						data[10].heatValue,
						data[11].heatValue,
					]
				}, {
						name: utilities[3],
					data: [
						data[0].gasValue,
						data[1].gasValue,
						data[2].gasValue,
						data[3].gasValue,
						data[4].gasValue,
						data[5].gasValue,
						data[6].gasValue,
						data[7].gasValue,
						data[8].gasValue,
						data[9].gasValue,
						data[10].gasValue,
						data[11].gasValue,
					]
				}]
			});

			Highcharts.chart('containerForOneUtility', {
				chart: {
					type: 'column'
				},
				title: {
					text: localizedWordsForContainerForOneUtility[0]
				},
				xAxis: {
					categories: [
						data[0].name,
						data[1].name,
						data[2].name,
						data[3].name,
						data[4].name,
						data[5].name,
						data[6].name,
						data[7].name,
						data[8].name,
						data[9].name,
						data[10].name,
						data[11].name,
					],
					crosshair: true
				},
				yAxis: {
					min: 0,
					title: {
						text: localizedWordsForContainerForOneUtility[1],
						align: 'high'
					},
					labels: {
						overflow: 'justify'
					}
				},
				tooltip: {
					valueSuffix: localizedWordsForContainerForOneUtility[2]
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
					name: utilities[0],
					data: [
						[data[0].name, data[0].elValue],
						[data[1].name, data[1].elValue],
						[data[2].name, data[2].elValue],
						[data[3].name, data[3].elValue],
						[data[4].name, data[4].elValue],
						[data[5].name, data[5].elValue],
						[data[6].name, data[6].elValue],
						[data[7].name, data[7].elValue],
						[data[8].name, data[8].elValue],
						[data[9].name, data[9].elValue],
						[data[10].name, data[10].elValue],
						[data[11].name, data[11].elValue]
					],
					dataLabels: {
						enabled: true,
						rotation: -90,
						color: '#FFFFFF',
						align: 'right',
						format: '{point.y: .2f}', // one decimal
						y: 10, // 10 pixels down from the top
						style: {
							fontSize: '13px',
							fontFamily: 'Verdana, sans-serif'
						}
					}
				}]
			});
		},
		error: function ()
		{
			var warningTag = '<h2><strong>' + messageForWarningTag + '</strong></h2>';
			$("#customerData").append(warningTag);
		}
	});
})

