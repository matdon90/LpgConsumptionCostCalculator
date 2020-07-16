jQuery.extend(jQuery.validator.messages, {
	required: "To pole jest wymagane.",
	remote: "Proszę naprawić pole.",
	email: "Proszę wprowadzić poprawny adres email.",
	url: "Proszę wprowadzić poprawny URL.",
	date: "Proszę wprowadzić poprawną date.",
	dateISO: "Proszę wprowadzić poprawną date (ISO).",
	number: "Proszę wprowadzić poprawny numer.",
	digits: "Proszę wprowadzić wyłącznie cyfry.",
	equalTo: "Prosze wprowadzić identyczną wartość raz jeszcze.",
	maxlength: $.validator.format("Proszę wprowadzić więcej niż {0} znaków"),
	minlength: $.validator.format("Proszę wprowadzić przynajmniej {0} znaków."),
	rangelength: $.validator.format("Proszę wprowadzić ilość znaków z zakresu od {0} do {1}."),
	range: $.validator.format("Prosze wprowadzić wartość między {0} a {1}."),
	max: $.validator.format("Proszę wprowadzić wartość mniejszą lub równą {0}."),
	min: $.validator.format("Proszę wprowadzić wartość większą lub równą {0}."),
	step: $.validator.format("Proszę wprowadzić wielokrotność {0}.")
});