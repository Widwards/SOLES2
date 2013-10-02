# CoffeeScript
$(document).ready =>
	place = $("#solition")

	$("#btn-solve").click ->
		console.log "Click!"
		$.ajax
			url: "/"
			data: $("#ExpressionForm").serialize()
			type: 'POST'
			success: (response) ->
				console.log response
				result = $.parseJSON response
				clearInfo()
				console.log result
				if result.IsSuccess
					s = result.Solution
					console.log s
				else
					s = "<div class=\"error-message alert alert-dismissable alert-danger\">"
					s += result.ErrorMessage
					s += "</div>"
				$("#solution").append s



	clearInfo = ->
		$("#solution").empty()