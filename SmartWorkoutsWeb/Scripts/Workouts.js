	$('.slider').slick({
		arrows: true,
		dots: true,
		slidesToShow: 3,
		slidesToScroll:1,
		autoplay: true,
		speed: 1000,
		autoplaySpeed: 2000,
		responsive: [
			{
				breakpoint: 900,
				settings: {
					slidesToShow: 2
				}
			},
			{
				breakpoint: 550,
				settings: {
					slidesToShow: 1
				}
			}
		]
	});
$(function ()
{
	let id = "Press"
	let object = {
		Type: id
	};
	let obj = JSON.stringify(object)
	$.ajax({
		type: 'POST',
		url: '/Workouts/LoadWorkouts',
		contentType: 'application/json; charset=utf-8',
		data: obj,
		success: function (data) {
			$('#ElementsWorkouts').html(data);
		},
		error: function (data) {
			alert(data.responseText);
		}
	});
});
function LoadWorkouts(item) {
	let id = item.id;
	let object = {
		Type: id
	};
	let obj = JSON.stringify(object)
	$.ajax({
		type: 'POST',
		url: '/Workouts/LoadWorkouts',
		contentType: 'application/json; charset=utf-8',
		data: obj,
		success: function (data) {
			$('#ElementsWorkouts').html(data);
		},
		error: function(data) {
			alert(data.responseText);
		}
	});
}

function test(item) {
	if ($(item).find('i').hasClass('fa-minus')) {
		$(item).find('i').toggleClass('fa-minus')
		$(item).find('i').toggleClass('fa-plus')
	}
	else {
		$("#accordion").find('i').removeClass('fa-minus')
		$("#accordion").find('i').addClass('fa-plus')
		$(item).find('i').removeClass('fa-plus')
		$(item).find('i').addClass('fa-minus')
    }



}

function OpenModelWorkout(item) {
	let P = $(item).prev();
	let H = $(P).prev();
	P.addClass('ForContentWorkout');
	H.addClass('ForHeaderWorkout');
	let Text_P = $(".ForContentWorkout").text();
	let Text_H = $(".ForHeaderWorkout").text();
	P.removeClass('ForContentWorkout');
	H.removeClass('ForHeaderWorkout');
	$("#HeaderModelWorkout").text(Text_H);
	$("#ContentModalWorkout").text(Text_P);
	$('#ModalWorkout').modal('show');
	let IdWorkout = item.id;
	let object = {
		Id: IdWorkout
	};
	let obj = JSON.stringify(object)
	$.ajax({
		type: 'POST',
		url: '/Workouts/LoadExercisesInWorkouts',
		contentType: 'application/json; charset=utf-8',
		data: obj,
		success: function (data) {
			$('#ExercisesWorkout').html(data);
		},
		error: function (data) {
			alert(data.responseText);
		}
	});
}
