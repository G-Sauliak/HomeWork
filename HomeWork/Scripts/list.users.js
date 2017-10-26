var urlRefreshList = "";
var urlDetails = "";
var userID = "";
var urlPostIndex = "";

$('a[href="#"]').click(function () {
    var val = $(this).attr('UserID');
    $("#user_content").load(urlDetails, { id: val });
});

$('.nav li').on('click', function () {
    var val = $(this).attr('UserID');
    activeLi = this;
    userID = $(this).attr('UserID');
    $("#id").attr('value', val);
    $('.nav li').removeClass('active');
    $(this).addClass('active');
});


$('#EditUser').click(function (e) {
    e.preventDefault();
    $.get(urlPostIndex, { id: userID }, function (result) {
        $("#MainContainer").html(result);
    });
});

$('#DelUser').click(function (e) {
    e.preventDefault();
    $.get(urlRefreshList, { id: userID }, function (result) {
        $("#MainContainer").html(result);
    });
});



