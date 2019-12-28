
var note_id = -1;
var modalNoteDetailID = "#modal_detail_body";

$(function () {
    $("#modal_detail").on('show.bs.modal', function (e) {
        var btn = $(e.relatedTarget);
        note_id = btn.data("noteid");
        $(modalNoteDetailID).load("/Notes/ShowNoteDetail/" + note_id);
    })
});