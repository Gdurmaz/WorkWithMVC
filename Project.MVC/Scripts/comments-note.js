
//Bu js dosyası yorum için yazılmıştır. Bootstrap Modal App.Code içindedir.
//this js file is written for comment. Bootstrap's Modal in  App.Code.

var note_id = -1;
var modalCommentID = "#modal_comment_body";

$(function () {
    $('#modal_comment').on('show.bs.modal', function (e) {
        var btn = $(e.relatedTarget);
        note_id = btn.data("noteid");
        $(modalCommentID).load("/Comments/ShowComment/" + note_id);
    });
});

function doComment(btn, mode, commentid, spanid) {
    var button = $(btn);
    var btnMode = button.data("ModeEdit");
    if (mode == "edit_click") {
        if (!btnMode) {
            button.data("ModeEdit", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var btnspan = button.find("span");
            btnspan.removeClass("glyphicon-edit");
            btnspan.addClass("glyphicon-ok");
            $(spanid).addClass("editable");
            $(spanid).attr("contenteditable", true);
            $(spanid).focus();
        }
        else {
            button.data("ModeEdit", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success");
            var btnspan = button.find("span");
            btnspan.addClass("glyphicon-edit");
            btnspan.removeClass("glyphicon-ok");
            $(spanid).removeClass("editable");
            $(spanid).attr("contenteditable", false);
            var txt = $(spanid).text();
            $.ajax({
                method: "POST",
                url: "/Comments/EditComment/" + commentid,
                data: { text: txt }
            }).done(function (data) {
                if (data.result) {
                    $(modalCommentID).load("/Comments/ShowComment/" + note_id);
                }
                else {
                    alert("Yorum güncellenemedi.");
                }
            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            });
        }
    }
    else if (mode == "delete_click") {
        var _confirm = confirm("Yorum silinsin mi?");
        if (!_confirm) { return false };
        $.ajax({
            method: "GET",
            url: "/Comments/DeleteComment/" + commentid
        }).done(function (data) {
            if (data.result) {
                $(modalCommentID).load("/Comments/ShowComment/" + note_id);
            }
            else {
                alert("Yorum silinemedi");
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }
    else if (mode == "new_click") {
        var txt = $("#new_comment_text").val();
        $.ajax({
            method:"POST",
            url: "/Comments/InsertComment",
            data: { "note_id": note_id, "text": txt }
        }).done(function (data) {
            if (data.result) {
                $(modalCommentID).load("/Comments/ShowComment/" + note_id);
            }
            else {
                alert("Yorum eklenmedi.");
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }
}
