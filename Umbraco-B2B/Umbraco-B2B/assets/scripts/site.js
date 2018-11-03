$(document).ready(function() {
    $('#logout').click(function(e) {
        e.preventDefault();
        $.ajax({
            url: '/umbraco/surface/ProfileSurface/HandleLogout',
            type: 'POST',
            success: function(data) {
                $(location).attr('href', '/');
            }
        });
    });
});