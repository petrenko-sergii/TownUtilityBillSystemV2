
function EnglishFlagPressed()
{
	debugger;
	$.ajax({
		
		url: '@Url.Action("Change", "Language")',
		data: { languageAbbreviation: "en" }
	}).done(function ()
	{
		location.reload(true);
	});
}

function DanishFlagPressed()
{
	debugger;
	$.ajax({
		url: '@Url.Action("Change", "Language")',
		data: { languageAbbreviation: "da" }
	}).done(function ()
	{
		location.reload(true);
	});
}

