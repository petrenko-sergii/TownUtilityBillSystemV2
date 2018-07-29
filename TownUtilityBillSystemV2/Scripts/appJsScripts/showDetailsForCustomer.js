//variables country, town, street, building are assigned 
//in ShowDetailsForCustomer.cshtml file

var map;
var marker;

var mapDiv;
var address;
var geocoder;
var mapBuildingZoom = 13;

$(document).ready(function ()
{
	mapDiv = document.getElementById('mapCanvasDetailsForCustomer');
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
});
