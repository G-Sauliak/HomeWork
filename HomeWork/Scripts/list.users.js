var urlDeleteUser = "";
var urlDetails = "";
var userID = "";
var urlEditPost = "";

$('#UserListBox').on('click', 'a[href = "#"]', function () {
    var val = $(this).attr('UserID');
    $("#user_content").load(urlDetails, { id: val });
});

$('#UserListBox').on('click', '.list-group-item', function () {
    $("#select-validation").empty();
    userID = $(this).children('span').attr('UserID');
    $('.nav li').removeClass('active');
    $(this).addClass('active');
});


$('#EditUser').click(function () {
    if (!userID == "") {
        $.get(urlEditGet, { id: userID }, function (result) {
            $("#MainContainer").html(result);
        });
    }
    else {
        $("#select-validation").text("Please select user!")
    }
});

$('#DelUser').click(function (e) {
    var indexPage = $("#current-page").text();
    var counter = $("#counter").text();
    counter -= $("#delete-user").text();
    $.get(urlDeleteUser, { id: userID, indexPage: indexPage, counter: counter }, function (result) {
        $("#UserListBox").html(result);
    });

    e.preventDefault();
});


$('#UserListBox').on('click', '.pagination li', function () {
    var nextPage = $(this).children('a').attr('indexPage');
    var counter = $("#counter").text();

    if (nextPage < 0) return false
    if ($("#total-count").text() < counter) {
        if ($("#current-page").text() < nextPage) {
            return false;
        }
        counter -= $("#revert").text();
    } else if ($("#current-page").text() > nextPage) {
        counter -= $("#revert").text();
    }

    $.get('/User/RefreshList', { indexPage: nextPage, counter: counter }, function (result) {
        $("#UserListBox").html(result);
    });
});





