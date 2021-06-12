function BuySubscription(item) {
    let id = item.id
    let object = {
       id:id
    };
    let obj = JSON.stringify(object)
    $.ajax({
        type: 'POST',
        url: '/Premium_Works/BuySubscription',
        contentType: 'application/json; charset=utf-8',
        data: obj,
        success: function (result) {
            window.location.href = result.ask;

        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}