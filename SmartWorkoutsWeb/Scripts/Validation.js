
let DateMax = new Date();
DateMax.setFullYear(DateMax.getFullYear() - 16);
$('#InpDate').datepicker({ maxDate: DateMax })


function OpenАuthorization() {
    $('#ModalLogin').modal('show');
}
function OpenRegestration() {
    $('#ModalLogin').modal('hide');
    $('#ModalRegestration').modal('show');
    $("body").addClass("modal-open");
}



function formatDate(date) {

    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;

    var mm = date.getMonth() + 1;
    if (mm < 10) mm = '0' + mm;

    var yy = date.getFullYear();

    return dd + '.' + mm + '.' + yy;
}
function Regestration(item) {
    let first_name = $('#InpName').val();
    let last_name = $('#InpSurname').val();
    let middle_name = $('#InpMiddleName').val();
    let email = $('#InpEmail').val();
    if ($('#InpDate').val() == '') {
        let NowDate = new Date();
        $('#InpDate').val(formatDate(NowDate));
    }
    let dateBirth = $('#InpDate').val();
    let login = $('#InpLogin').val();
    let password = $('#InpPassword').val();
    let repeatPass = $('#RepeatPass').val();
    let result = true;
    $(".error").remove();
    $("#InpName").removeClass("is-invalid")
    $("#InpSurname").removeClass("is-invalid")
    $("#InpEmail").removeClass("is-invalid")
    $("#InpDate").removeClass("is-invalid")
    $("#InpLogin").removeClass("is-invalid")
    $("#RepeatPass").removeClass("is-invalid")
    $("#InpPassword").removeClass("is-invalid")
   
    if (first_name.length < 1) {
        result = false;
        $("#InpName").toggleClass("is-invalid")
        $('#InpName').before($('<label>', {
            'for': 'InpName',
            'class': "form-label error text-danger",
            'text': 'Введите имя!'
        }));
    }

    if (last_name.length < 1) {
        result = false;
        $("#InpSurname").toggleClass("is-invalid")
        $('#InpSurname').before($('<label>', {
            'for': 'InpSurname',
            'class': "form-label error text-danger",
            'text': 'Введите фамилию!'
        }));
    }


    if (email.length < 1) {
        result = false;
        $("#InpEmail").toggleClass("is-invalid")
        $('#InpEmail').before($('<label>', {
            'for': 'InpEmail',
            'class': "form-label error text-danger",
            'text': 'Введите email!'
        }));
    }
    else {
        let regex = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        let validEmail = regex.test(email);
        if (!validEmail) {
            result = false;
            $("#InpEmail").toggleClass("is-invalid")
            $('#InpEmail').before($('<label>', {
                'for': 'InpEmail',
                'class': "form-label error text-danger",
                'text': 'Введите корретный email!'
            }));
        }
    }

    if (dateBirth.length > 1) {
        let NowDate = new Date();
        let MinDate = new Date(1900, 01, 01);
        let day = dateBirth.split(".")[0], month = dateBirth.split(".")[1], year = dateBirth.split(".")[2];
        let InpDate = new Date(year, month - 1, day);
        if (InpDate >= NowDate) {
            result = false;
        }
        else {
            if (InpDate <= MinDate) {
                result = false;
            }
        }
    }
    else {
        result = false;
    }


    if (password.length < 6) {
        result = false;
        $("#InpPassword").toggleClass("is-invalid")
        $('#InpPassword').before($('<label>', {
            'for': 'InpPassword',
            'class': "form-label error text-danger",
            'text': 'Длина должна быть >= 6 знаков!'
        }));
    }

    if (repeatPass != password) {
        result = false;
        $("#RepeatPass").toggleClass("is-invalid")
        $('#RepeatPass').before($('<label>', {
            'for': 'RepeatPass',
            'class': "form-label error text-danger",
            'text': 'Пароли не совпадают!'
        }));
    }

    if (login.length < 1) {
        result = false;
        $("#InpLogin").toggleClass("is-invalid")
        $('#InpLogin').before($('<label>', {
            'for': 'InpLogin',
            'class': "form-label error text-danger",
            'text': 'Введите логин!'
        }));
    }
    
    if (result) {
        let object = {
            UserLogin: login
        };
        let obj = JSON.stringify(object)
        $.ajax({
            type: 'POST',
            url: '/Users/CheckRepeatLogin',
            contentType: 'application/json; charset=utf-8',
            data: obj,
            success: function (data) {
                if (!data.msg) {
                    let object = {
                        UserName: first_name,
                        UserSurname: last_name,
                        UserEmail: email,
                        UserDateBirth: dateBirth,
                        UserLogin: login,
                        UserPassword: password
                    };
                    let obj = JSON.stringify(object)
                    $.ajax({
                        type: 'POST',
                        url: '/Users/Create',
                        contentType: 'application/json; charset=utf-8',
                        data: obj,
                        success: function (result) {
                            $('.CompleteReg').after($('<p>', {
                                'class': " text-center RegComplete mt-4 mb-md-0",
                                'style': "font-size:18px;",
                                'text': 'Вы успешно зарегистрировались!'
                            }));
                           
                                $('#ModalRegestration').modal('hide');
                                $('#ModalLogin').modal('show');
                           
                          
                        },
                        error: function (data) {
                            alert(data.responseText);
                        }
                    });
                }
                else {
                    $("#InpLogin").toggleClass("is-invalid")
                    $('#InpLogin').before($('<label>', {
                        'for': 'InpLogin',
                        'class': "form-label error text-danger",
                        'text': 'Такой логин уже существует!'
                    }));
                   
                }
            },
            error: function (data) {
                alert(data.responseText);

            }
        });
    }

    }

function Autorization(e) {
    let login = $('#LoginUser').val();
    let password = $('#PasswordUser').val();
    let result = true;
    $(".error").remove();
    $("#LoginUser").removeClass("is-invalid")
    $("#PasswordUser").removeClass("is-invalid")
    if (login.length < 1) {
        result = false;
        $("#LoginUser").toggleClass("is-invalid")
        $('#LoginUser').before($('<label>', {
            'for': 'LoginUser',
            'class': "form-label error text-danger",
            'text': 'Неверный логин!'
        }));
    }
    if (password.length < 1) {
        $("#PasswordUser").toggleClass("is-invalid")
        $('#PasswordUser').before($('<label>', {
            'for': 'PasswordUser',
            'class': "form-label error text-danger",
            'text': 'Неверный пароль!'
        }));
    }
    else {
        if (result) {
           
               
            let object = {
                Login: login,
                Password: password,
            };
            let obj = JSON.stringify(object)
            $.ajax({
                type: 'POST',
                url: '/Home/Login',
                contentType: 'application/json; charset=utf-8',
                data: obj,
                success: function (result) {
                    if (result.AllResult) {
                        $('#ModalLogin').modal('hide');
                        location.reload();
                    }
                    else {
                        if (!result.LoginCheck) {
                            $("#LoginUser").toggleClass("is-invalid")
                            $('#LoginUser').before($('<label>', {
                                'for': 'LoginUser',
                                'class': "form-label error text-danger",
                                'text': 'Неверный логин!'
                            }));
                        }
                        if (!result.PasswordCheck) {
                                    
                            $("#PasswordUser").toggleClass("is-invalid")
                            $('#PasswordUser').before($('<label>', {
                                'for': 'PasswordUser',
                                'class': "form-label error text-danger",
                                'text': 'Неверный пароль!'
                            }));
                        }
                    }
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
             
        }

    }
}