
    function AddText(item) {
        let P = $(item).prev();
        let H = $(P).prev();
        P.addClass('ForContentViews');
        H.addClass('ForHeaderViews');
        let Text_P = $(".ForContentViews").text();
        let Text_H = $(".ForHeaderViews").text();
        P.removeClass('ForContentViews');
        H.removeClass('ForHeaderViews');
        $("#staticBackdropLabel").text(Text_H);
        $("#ContentModal").text(Text_P);
    };