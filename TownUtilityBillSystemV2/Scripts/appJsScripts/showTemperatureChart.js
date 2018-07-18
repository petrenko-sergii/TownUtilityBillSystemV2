var localizatedWordsForFirstPageLoad;
var defaultTown = "Copenhagen";

$(document)
	.ready(function ()
	{
		$.ajax({
			type: "GET",
			url: '/Chart/GetLocalizatedWordsTemperatureChartForFirstLoad',
			data: {},
			success: function (data)
			{
				localizatedWordsForFirstPageLoad = data;
			}
		});

		$.ajax({
			type: "GET",
			url: '/Chart/GetTemperatureChartData',
			dataType: "json",
			success: function (data)
			{
				createChart("#chartArea", localizatedWordsForFirstPageLoad[0]  + ' ' + localizatedWordsForFirstPageLoad[1] + ' ' + defaultTown, data);
			}
		});

		$("#Town_Id").change(function ()
		{
			$.ajax({
				type: "GET",
				url: '/Chart/GetTemperatureChartData',
				data: { townId: $("#Town_Id").val() },
				dataType: "json",
				success: function (data)
				{
					createChart("#chartArea", localizatedWordsForFirstPageLoad[0], data);
				}
			});
		})
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
				data[0].MonthName,
				data[1].MonthName,
				data[2].MonthName,
				data[3].MonthName,
				data[4].MonthName,
				data[5].MonthName,
				data[6].MonthName,
				data[7].MonthName,
				data[8].MonthName,
				data[9].MonthName,
				data[10].MonthName,
				data[11].MonthName,
			],
			crosshair: true
		},
		yAxis: {
			min: 0,
			title: {
				text: localizatedWordsForFirstPageLoad[2] + ', °C',
				align: 'high'
			},
			labels: {
				overflow: 'justify'
			}
		},
		tooltip: {
			valueSuffix: ' °C'
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
			name: 'Temperature',
			data: [
				[data[0].MonthName, data[0].AverageValue],
				[data[1].MonthName, data[1].AverageValue],
				[data[2].MonthName, data[2].AverageValue],
				[data[3].MonthName, data[3].AverageValue],
				[data[4].MonthName, data[4].AverageValue],
				[data[5].MonthName, data[5].AverageValue],
				[data[6].MonthName, data[6].AverageValue],
				[data[7].MonthName, data[7].AverageValue],
				[data[8].MonthName, data[8].AverageValue],
				[data[9].MonthName, data[9].AverageValue],
				[data[10].MonthName, data[10].AverageValue],
				[data[11].MonthName, data[11].AverageValue]
			],
			dataLabels: {
				enabled: true,
				rotation: -90,
				color: '#aaaaa',
				align: 'right',
				format: '{point.y:.1f}', // one decimal
				y: -35, // 10 pixels down from the top
				style: {
					fontSize: '13px',
					fontFamily: 'Verdana, sans-serif'
				}
			}
		}]
	});
}

var map = new google.maps.Map(document.getElementById('map-canvas'), {
	center: {
		lat: 55.60,
		lng: 10.9
	},
	zoom: 7
});

var marker = new google.maps.Marker({
	position: {
		lat: 55.6877,
		lng: 12.5839
	},
	map: map,
	draggable: true
});

var country = "Denmark";
var town;

var mapDiv;
var address;
var geocoder;
var mapTownZoom = 8;

$(document).ready(function ()
{
	$("#Town_Id").change(function ()
	{
		$.ajax({
			type: "GET",
			url: '/Meter/GetTownName',
			data: { townId: $("#Town_Id").val() },
			success: function (data)
			{
				town = data;
				mapDiv = document.getElementById('map-canvas');
				address = town + country;
				geocoder = new google.maps.Geocoder();

				geocoder.geocode({
					"address": address
				}, function (results, status)
					{
						map = new google.maps.Map(mapDiv, {
							center: results[0].geometry.location,
							zoom: mapTownZoom,
							mapTypeId: google.maps.MapTypeId.ROADMAP,
						})
						marker = new google.maps.Marker({
							position: results[0].geometry.location,
							map: map,
							draggable: true
						});
					});
			}
		});
	})
});
