

    $(".BtnMoreInfo").click(function (event) {
        var id = "#Advantages"
        var top = $(id).offset().top;
        $('body,html').animate({ scrollTop: top }, 1500);
    });

    $('#SendOnEmail').click(function (e) {
        e.preventDefault();
        let name = $('#NameUser').val();
        let email = $('#EmailUser').val();
        let comment = $('#CommentUser').val();
        let result = true;
        $(".SuccessCon ").remove();
        $(".error ").remove();
        $("#EmailUser").removeClass("is-invalid")
        $("#NameUser").removeClass("is-invalid")
        $("#CommentUser").removeClass("is-invalid")
        if (name.length < 1) {
            result = false;
            $("#NameUser").toggleClass("is-invalid")
            $('#NameUser').before($('<label>', {
                'for': 'NameUser',
                'class': "form-label error text-danger",
                'text': 'Введите имя!'
            }));
        }
        if (comment.length < 1) {
            result = false;
            $("#CommentUser").toggleClass("is-invalid")
            $('#CommentUser').before($('<label>', {
                'for': 'CommentUser',
                'class': "form-label error text-danger",
                'text': 'Введите комментарий!'
            }));
        }
        if (email.length < 1) {
            result = false;
            $("#EmailUser").toggleClass("is-invalid")
            $('#EmailUser').before($('<label>', {
                'for': 'EmailUser',
                'class': "form-label error text-danger",
                'text': 'Введите Email!'
            }));
        }
        else {

            let regex = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            let validEmail = regex.test(email);
            if (!validEmail) {
                result = false;
                $("#EmailUser").toggleClass("is-invalid")
                $('#EmailUser').before($('<label>', {
                    'for': 'EmailUser',
                    'class': "form-label error text-danger",
                    'text': 'Неккоректный Email!'
                }));
            }
        }
        if (result) {

            let object = {
                Name: name,
                Email: email,
                Comment: comment
            };
            let obj = JSON.stringify(object)
            $.ajax({
                type: 'POST',
                url: '/Home/SendMessageEmail',
                contentType: 'application/json; charset=utf-8',
                data: obj,
                success: function (result) {
                    $('#SuccessSend').after($('<p>', {
                        'class': " text-success text-center SuccessCon",
                        'style': "font-size:18px;",
                        'text': 'Комментарий успешно отправлен!' 
                    }));

                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
        else {


        }

    });
       
 