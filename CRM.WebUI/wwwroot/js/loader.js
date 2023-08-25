	var spinnerContainer = document.getElementById('spinner-section');
		var baseContent = document.getElementById('base');
		spinnerContainer.style.display = 'block';
		baseContent.style.display = 'none';

		document.addEventListener('DOMContentLoaded', function () {
			spinnerContainer.style.display = 'none';
			baseContent.style.display = 'block';

			var dynamicLinks = document.querySelectorAll('.dynamic-link');
			var buttons = document.querySelectorAll('.load');
			var inputFields = document.querySelectorAll('.form-control');

			dynamicLinks.forEach(function (link) {
				link.addEventListener('click', function (event) {
					event.preventDefault();

					spinnerContainer.style.display = 'block';
					baseContent.style.display = 'none';

					var linkUrl = link.getAttribute('href');
					window.location.href = linkUrl;
				});
			});

			buttons.forEach(function (button) {
				button.addEventListener('click', function (event) {
					var allFieldsFilled = true;

					inputFields.forEach(function (field) {
						if (field.value === "") {
							allFieldsFilled = false;
							return;
						}
					});

					if (allFieldsFilled) {

						spinnerContainer.style.display = 'block';
						baseContent.style.display = 'none';
					}
				});
			});
		});
