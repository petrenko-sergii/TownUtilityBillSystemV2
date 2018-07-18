//variables are declared in ShowCharges.cshtml file

$(document).ready(function ()
{
	Highcharts.chart('containerUtilitiesCharges', {

			chart: {
				plotBackgroundColor: null,
				plotBorderWidth: 0,
				plotShadow: false
			},
			title: {
				text: localizedUtilitiesChargesWord,
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
				name: localizedValueWord,
				innerSize: '50%',
				data: [
					[utilitiesResourceNames[0], utilitiesCharges[0]],
					[utilitiesResourceNames[1], utilitiesCharges[1]],
					[utilitiesResourceNames[2], utilitiesCharges[2]],
					[utilitiesResourceNames[3], utilitiesCharges[3]]
				]
			}]
		});
})
