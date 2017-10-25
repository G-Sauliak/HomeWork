var urlRefreshList = "";
var listUsers = $("#listUsers");
$("#DelUser").click(function () {
    $("#listUsers option:selected").each(function () {
        var _id = $(this).val();
        $.getJSON(urlRefreshList, { id: _id }, function (result) {
            listUsers.empty();
            $.each(result, function (i) {
                listUsers.append($("<option></option>")
                    .attr("value", result[i].Value)
                    .text(result[i].Text));
            });
            $('#listUsers option')[0].selected = true;
            $('#listUsers option')[0].click();
        });
      
    });
});
var urlDetails = "";
$("#listUsers").on("click", "option", function () {
    var val = $(this).val();
    $("#user_content").load(urlDetails, { id: val });
});
