var messageContainer = document.getElementById('messageContainer');
if (messageContainer) {
	setTimeout(function () {
		messageContainer.style.opacity = 0;
		messageContainer.style.transform = 'translateY(-45px)';
		messageContainer.addEventListener('transitionend', function () {
			messageContainer.style.display = 'none';
		});
	}, 2500);
}