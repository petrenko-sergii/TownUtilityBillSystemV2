var map = new google.maps.Map(document.getElementById('map-canvas'), {
	center: {
		lat: 55.60,
		lng: 9.6
	},
	zoom: 7
});
var marker = new google.maps.Marker({
	position: {
		lat: 55.60,
		lng: 9.6
	},
	map: map,
	draggable: true
});
var country = "Denmark";
var town;
var street;
var building;
var mapDiv;
var address;
var geocoder;
var mapTownZoom = 10;
var mapStreetZoom = 14;
var mapBuildingZoom = 17;

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

$(document).ready(function ()
{
	$("#Street_Id").change(function ()
	{
		$.ajax({
			type: "GET",
			url: '/Meter/GetStreetName',
			data: { streetId: $("#Street_Id").val() },
			success: function (data)
			{
				street = data;
				mapDiv = document.getElementById('map-canvas');
				address = street + town + country;
				geocoder = new google.maps.Geocoder();

				geocoder.geocode({
					"address": address
				}, function (results, status)
					{
						map = new google.maps.Map(mapDiv, {
							center: results[0].geometry.location,
							zoom: mapStreetZoom,
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

$(document).ready(function ()
{
	$("#Building_Id").change(function ()
	{
		$.ajax({
			type: "GET",
			url: '/Meter/GetBuildingNumber',
			data: { buildingId: $("#Building_Id").val() },
			success: function (data)
			{
				building = data;
				mapDiv = document.getElementById('map-canvas');
				address = building + street + town + country;
				geocoder = new google.maps.Geocoder();

				geocoder.geocode({
					"address": address
				}, function (results, status)
					{
						map = new google.maps.Map(mapDiv, {
							center: results[0].geometry.location,
							zoom: mapBuildingZoom,
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