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
