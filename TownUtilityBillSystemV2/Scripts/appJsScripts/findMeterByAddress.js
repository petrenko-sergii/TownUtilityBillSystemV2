var localizatedWordsForSelectBoxes;
var localizatedWordsForMeterTable;
var columnHeaders;
var options;

$(document).ready(function ()
{
	$("#Town_Id").change(function ()
	{
		$.ajax({
			type: "GET",
			url: '/Meter/GetLocalizatedWordsForSelectBoxes',
			data: { },
			success: function (data)
			{
				localizatedWordsForSelectBoxes = data;
			}
		});

		$.ajax({
			type: "GET",
			url: '/Meter/GetLocalizatedWordsForMeterTable',
			data: {},
			success: function (data)
			{
				columnHeaders = data[0];
				options = data[1];
			}
		});

		$.get("/Meter/GetStreetList", { townId: $("#Town_Id").val() }, function (data)
		{
			var uDiv = document.getElementById("utilityDiv");
			uDiv.style.visibility = "hidden";

			var bildingImage = document.getElementById("buildingImage");
			bildingImage.style.visibility = "hidden";

			$("#Building_Id").empty();
			$("#Building_Id").append("<option value='" + 0 + "'>" + localizatedWordsForSelectBoxes[1] + "</option>")

			$("#Street_Id").empty();
			$("#Street_Id").append("<option value='" + 0 + "'>" + localizatedWordsForSelectBoxes[0] + "</option>")

			var f = document.getElementById("FlatPart_Id");
			$("#FlatPart_Id").empty();
			f.style.visibility = "hidden";

			$.each(data, function (index, row)
			{
				$("#Street_Id").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
			});
		});
	})
});



$(document).ready(function ()
{
	$("#Street_Id").change(function ()
	{
		$.get("/Meter/GetBuildingList", { streetId: $("#Street_Id").val() }, function (data)
		{

			var uDiv = document.getElementById("utilityDiv");
			uDiv.style.visibility = "hidden";

			var bildingImage = document.getElementById("buildingImage");
			bildingImage.style.visibility = "hidden";

			var f = document.getElementById("FlatPart_Id");
			$("#FlatPart_Id").empty();
			f.style.visibility = "hidden";

			$("#Building_Id").empty();
			$("#Building_Id").append("<option value='" + 0 + "'>" + localizatedWordsForSelectBoxes[1] + "</option>")
			$.each(data, function (index, row)
			{
				$("#Building_Id").append("<option value='" + row.Id + "'>" + row.Number + "</option>")
			});
		});
	})

});

$(document).ready(function ()
{
	$("#Building_Id").change(function ()
	{
		$.get("/Meter/GetFlatPartList", { buildingId: $("#Building_Id").val() }, function (data)
		{
			var f = document.getElementById("FlatPart_Id");
			$("#FlatPart_Id").empty();
			$("#FlatPart_Id").append("<option value='" + 0 + "'>" + localizatedWordsForSelectBoxes[2] + "</option>")
			f.style.visibility = "hidden";
			$.each(data, function (index, row)
			{
				if (row.Name != null && (row.Number == 0 || row.Number == null))
				{
					$("#FlatPart_Id").append("<option value='" + row.Id + "'>" + localizatedWordsForSelectBoxes[3] + row.Name + "</option>")
				}
				else if (row.Name == null && (row.Number != 0 || row.Number != null))
				{
					$("#FlatPart_Id").append("<option value='" + row.Id + "'>" + localizatedWordsForSelectBoxes[4] + row.Number + "</option>")
				}
				else if (row.Name != null && (row.Number != 0 || row.Number != null))
				{
					$("#FlatPart_Id").append("<option value='" + row.Id + "'>" + localizatedWordsForSelectBoxes[4] + row.Number + ", " + row.Name + "</option>")
				}
				if (row.Id != 0 || row.Id != null)
				{
					f.style.visibility = "visible";
				}
			});
		});
	})
});

$("#Building_Id").change(function ()
{
	$.ajax({
		type: "GET",
		url: '/Meter/GetBuildingImage',
		data: { buildingId: $("#Building_Id").val() },
		success: function (data)
		{
			var bildingImage = document.getElementById("buildingImage");
			bildingImage.style.visibility = "visible";
			$("#buildingImage").empty().append(data);
		}
	});
	$.ajax({
		type: "GET",
		url: '/Meter/GetMetersForBuilding',
		data: { buildingId: $("#Building_Id").val() },
		success: function (data)
		{
			var uDiv = document.getElementById("utilityDiv");

			if (data[0] != null)
			{
				prepareMetersDataTable(data, uDiv, columnHeaders, options);
			}
			else
			{
				uDiv.style.visibility = "hidden";
			}
		}
	});
});

$("#FlatPart_Id").change(function ()
{
	$.ajax({
		type: "GET",
		url: '/Meter/GetMetersForFlatPart',
		data: { flatPartId: $("#FlatPart_Id").val() },
		success: function (data)
		{
			var uDiv = document.getElementById("utilityDiv");

			if (data[0] != null)
			{
				prepareMetersDataTable(data, uDiv, columnHeaders, options);
			}
		}
	});
});
