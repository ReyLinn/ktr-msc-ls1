$(document).ready(function () {

    $("#btnAddOwnProfile").on('click', function () {
        $.post('Home/AddProfileToUser')
    });



});