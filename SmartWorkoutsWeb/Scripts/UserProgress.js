
let myCircle = Circles.create({
    id: 'CircleProgress',
    radius: 90,
    value: 0,
    text: function (value) { return value + '%'; },
    width: 20,
    colors: ['#D3B6C6', '#4B253A'],
    duration: 400,
});


$.ajax({
    type: 'POST',
    url: '/User_Progress/GetProgress',
    contentType: 'application/json; charset=utf-8',
    success: function (data) {
        $("#CurrentWeight").val(data.CurrentWeight)
        $("#StartWeight").val(data.StartWeight)
        $("#DesidedWeight").val(data.DesiredWeight)
        ChangeData();
    },
    error: function (data) {
        alert(data.responseText);
    }
});

document.oninput = function () {
    let inputCurrent = document.querySelector('#CurrentWeight');
    inputCurrent.value = inputCurrent.value.replace(/[^\.\d]/g, '');
    let inputStart = document.querySelector('#StartWeight');
    inputStart.value = inputStart.value.replace(/[^\.\d]/g, '');
    let inputDesided = document.querySelector('#DesidedWeight');
    inputDesided.value = inputDesided.value.replace(/[^\.\d]/g, '');
}

function ClickSave() {
    $("#CompleteSave").remove()
    let current = $("#CurrentWeight").val()
    let start = $("#StartWeight").val()
    let desided =$("#DesidedWeight").val()
    let object = {
        StartWeight: start,
        CurrentWeight: current,
        DesiredWeight: desided
    };
    let obj = JSON.stringify(object)
    $.ajax({
        type: 'POST',
        url: '/User_Progress/UpdateCreateProgress',
        contentType: 'application/json; charset=utf-8',
        data: obj,
        success: function (data) {
            $(".RowBtn").append("<p id='CompleteSave'>Данные успешно сохранены!</p>")
            setTimeout(function () {
                $("#CompleteSave").remove()
            }, 3000);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

$("#CurrentWeight,#StartWeight,#DesidedWeight").on('input', function () {
    ChangeData();
});

function OpenProgress() {
    $('#ModalProgress').modal('show');
}

function ClickMinus(item) {
    const ValMinus = 0.1
    let Input = $(item).parent().next()
    let ValInput = parseFloat(Input.val())
    if (ValInput - ValMinus > 0) {
        Input.val((ValInput - ValMinus).toFixed(1))
        ChangeData();
    }
    else {
        Input.val(0)
        ChangeData();
    }


}

function ClickPlus(item) {
    const ValPlus = 0.1
    let Input = $(item).parent().prev()
    let ValInput = parseFloat(Input.val())
    if (ValInput + ValPlus < 500) {
        Input.val((ValInput + ValPlus).toFixed(1))
        ChangeData();
    }
    else {
        Input.val(500)
        ChangeData();
    }
}

function ChangeData() {
    let current = parseFloat($("#CurrentWeight").val())
    let start = parseFloat($("#StartWeight").val())
    let desided = parseFloat($("#DesidedWeight").val())

    if (start > desided && start >= current) {
        let substract = start - desided
        let middleCount = start - current
        let persentage = (middleCount * 100 / substract).toFixed(1)
        myCircle.update(persentage, 400);
        return
    }
 
    if (desided > start && start <= current) {

        let substract = desided - start
        let middleCount = current - start
        let persentage = (middleCount * 100 / substract).toFixed(1)
        myCircle.update(persentage, 400);

        return
    }
   
    if (start < current) {
        myCircle.update(0, 400);

        return
    }
    if (desided < current) {
        myCircle.update(100, 400);

        return
    }


}