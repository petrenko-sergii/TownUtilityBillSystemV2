
function ChangeLanguage(language)
{
	$.ajax({
		url: '/Language/Change',
		data: { languageAbbreviation: language }
	}).done(function ()
	{
		location.reload(true);
	});
}

function jsonDateFormatToJS(value)
{
	var dateString = value.substr(6);
	var currentTime = new Date(parseInt(dateString));
	var month = currentTime.getMonth() + 1;
	var day = currentTime.getDate();
	var year = currentTime.getFullYear();
	var date = year + "-" + month + "-" + day;

	return date;
}

function prepareMetersDataTable(data, uDiv, columnHeaders, options)
{
	uDiv.style.visibility = "visible";
	var meter_data = '';
	var rowNumber = 1;

	meter_data += '<tr><th>#</th><th>' + columnHeaders[0] +
		'</th><th>' + columnHeaders[1] +
		'</th><th>' + columnHeaders[2] +
		'</th><th>' + columnHeaders[3] +
		'</th><th>' + columnHeaders[4] +
		'</th><th>' + columnHeaders[5] +
		'</th></tr>';


	$.each(data, function (key, value)
	{
		var dateRelease = jsonDateFormatToJS(value.ReleaseDate);
		var dateVarif = jsonDateFormatToJS(value.VarificationDate);

		meter_data += '<tr>';
		meter_data += '<td align="center">' + rowNumber++ + '</td>';
		meter_data += '<td>' + value.SerialNumber + '</td>';
		meter_data += '<td>' + value.MeterType.Name + '</td>';
		meter_data += '<td>' + value.MeterType.Utility.Name + '</td>';
		meter_data += '<td>' + dateRelease + '</td>';
		meter_data += '<td>' + dateVarif + '</td>';
		meter_data += '<td>' + '<a href="/Meter/EditMeter?meterId=' + value.Id + '">' + options[0] + ' | </a>' + '<a href="/Meter/ShowMeterData?meterId=' + value.Id + '">' + options[1] + '</a>' + '</td>';
		meter_data += '</tr>';
	});
	$('#meters_table').empty().append(meter_data);
}
