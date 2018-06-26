import * as $ from 'jquery'

$("img.header-flag").css('cursor', 'pointer');

$(document).ready(function ()
{
	$("#indicator-0").addClass("active");
	$("#slide-0").addClass("active");
});

$("a.btn-green").mouseover(function ()
{
	$(this).addClass('btn-dark-green');
});

$("a.btn-green").mouseout(function ()
{
	$(this).removeClass('btn-dark-green');
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
				alert("GermanFlag pressed! At this moment -- not inplemented");
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


//function ChangeLanguage(language)
//{
//	debugger;
//	$.ajax({
//		url: '/Language/Change',
//		data: { languageAbbreviation: language }
//	}).done(function ()
//	{
//		location.reload(true);
//	});
//}

//$(document).ready(function ()
//{
//	alert("Current language: " + userLangAbbr);
//});

//method works  **************************************************************
//$(function ()
//{
//	$("img.header-flag").on("click", function ()
//	{
//		debugger;
//		let _self: HTMLElement = this;
//		_self.classList.add('flag-highlighted');
//	});
//});

//$(function ()
//{
//	$("#germanFlag").on("click", function ()
//	{
//		alert("GermanFlag pressed! At this moment -- not inplemented")
//		$("#germanFlag").addClass('flag-highlighted');
//	});
//});

//$(function ()
//{
//	$("#englishFlag").on("click", function ()
//	{
//		$("#englishFlag").addClass('flag-highlighted');
//	});
//});

//$(function ()
//{
//	$("#germanFlag").on("click", function ()
//	{
//		$("#germanFlag").addClass('flag-highlighted');
//	});
//});



//$(
//	function HighlightFlagIcon(language: string): void
//	{

//	}	
//);


