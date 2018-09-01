import * as $ from 'jquery'

$("img.header-flag").css('cursor', 'pointer');

$(document).ready(function ()
{
	$("#indicator-0").addClass("active");
	$("#slide-0").addClass("active");
});

$("a.btn-green").hover(function ()
{
	$(this).toggleClass('btn-dark-green');
});

$("a.btn-brown").hover(function ()
{
	$(this).toggleClass('btn-dark-brown');
});

$(document).ready(function ()
{
	$.get("/Language/GetCurrentLanguage", function (data)
	{
		let flag: string;

		switch (data)
		{
			case "da": {
				flag = "danishFlag"; 
				break;
			}
			case "de": {
				flag = "germanFlag"; 
				break;
			}
			default: {
				flag = "englishFlag"; 
				break;
			}
		} 

		$('#' + flag).addClass('flag-highlighted');
	});
});
