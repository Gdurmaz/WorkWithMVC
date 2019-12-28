$(function () {
    var noteids = [];
    $("div[data-noteid]").each(function (i, e) {
        noteids.push($(e).data("noteid"));
    });
    $.ajax({
        method: "POST",
        url: "/Notes/GetLiked",
        data: { id: noteids }
    }).done(function (data) {
        if (data.result != null && data.result.length > 0) {
            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i];
                var likenote = $("div[data-noteid=" + id + "]");
                var btn = likenote.find("button[data-liked]");
                var spn = btn.children().first();

                btn.data("liked", true);
                spn.removeClass("glyphicon-star-empty");
                spn.addClass("glyphicon-star");
            }
        }
    }).fail(function () {
        alert("Hata oluştu");
    });
    $("button[data-liked]").click(function () {
        var btn = $(this);
        var liked = btn.data("liked");
        var note_id = btn.data("noteid");
        var spnStar = btn.find("span.like-star");
        var spnCount = btn.find("span.like-count");

        $.ajax({
            method: "POST",
            url: "/Notes/SetLikeCount",
            data: { "noteid": note_id, "liked": !liked }
        }).done(function (data) {
            if (data.hasError) {
                alert(data.errorMessage)
            }
            else {
                btn.data("liked", !liked);
                spnCount.text(data.result);
                spnStar.removeClass("glyphicon-star-empty");
                spnStar.removeClass("glyphicon-star");
                if (liked) {
                    spnStar.addClass("glyphicon-star")
                }
                else {
                    spnStar.addClass("glyphicon-star-empty");
                }
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı");
        });
    });
});
