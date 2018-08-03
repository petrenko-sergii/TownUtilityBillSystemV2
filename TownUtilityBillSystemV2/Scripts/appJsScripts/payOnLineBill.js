//variables are declared in PayOnLineBill.cshtml file

var bill_Id = 0;

$(document).ready(function ()
{
	$("#findBillButton").click(function ()
	{
		$.get("/Bill/GetBillData", { billNumber: $("#Bill_Number").val() }, function (data)
		{

			customerData.style.visibility = "visible";

			if (data.Bill.Number != null)
			{
				bill_Id = data.Bill.Id;

				$("#customerData").empty();
				$("#customerData").append("<h3><strong>" + billData + ":</strong></h3>")

				if (data.CustomerModel.Customer.Surname != null)
				{
					$("#customerData").append("<h4> <strong> " + customer + ": " + data.CustomerModel.Customer.Surname + " " + data.CustomerModel.Customer.Name + "</strong></h4>")
				}
				else
				{
					$("#customerData").append("<h4> <strong>" + customer +": " + data.CustomerModel.Customer.Name + "</strong></h4>")
				}
				$("#customerData").append("<h4><strong>" + billPeriod + ": " + data.Bill.Period + "</strong></h4>")
				$("#customerData").append("<h4><strong>" + accountBalance + ": " + data.Bill.Account.Balance + " " + data.Currency.Name + "</strong></h4>")

				$("#customerData").append("<h4><strong>" + billSum + ": " + data.Bill.Sum + " " + data.Currency.Name + "</strong></h4>")


				if (data.Bill.Paid == true)
				{
					$("#customerData").append("<h4><strong><mark class='mark-yellow'>" + paidYes +"</mark></strong></h4>")
					$("#customerData").append("<br /><h4><strong><mark 'mark-yellow'>" + canNotPayWarning +"</mark></strong></h4>")
					payBillDiv.style.visibility = "hidden";
				}
				else
				{
					var totalSumToPay = data.Bill.Sum + data.Bill.Account.Balance;

					$("#customerData").append("<h4><strong>" + paidNo + "</strong></h4>")
					$("#customerData").append("<h4><strong>" + totalToPay + ': ' + "<mark class='mark-yellow'>" + totalSumToPay + "</mark> " + data.Currency.Name + "</strong></h4>")
					payBillDiv.style.visibility = "visible";
				}
			}
			else
			{
				$("#customerData").empty();
				$("#customerData").append("<h4 style=color:red><strong>" + noBillIsFoundWarning + "</strong></h4>")
			}
		});
	})
});

$(document).ready(function ()
{
	$("#payBillButton").click(function ()
	{
		$.ajax({
			type: "POST",
			url: '/Bill/CallPaymentCardForm',
			data: { bill_Id: bill_Id },
			dataType: 'json',
			crossDomain: true,
			success: function (data)
			{
				window.location.href = data + "?bill_Id=" + bill_Id;
			}
		});
	})
})