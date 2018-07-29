if (currentCulrure != 'en')
{
	$("#NewEditedValue").mouseover(function ()
	{
		$(this).attr("readonly", "readonly");
		$("#lblMeterDataNotEditable").css("visibility", "visible");
	});

	$("#NewEditedValue").mouseout(function ()
	{
		$(this).removeAttr("readonly");
		$("#lblMeterDataNotEditable").css("visibility", "hidden");
	});
}