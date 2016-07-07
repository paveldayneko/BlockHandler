
var contentController = (function() {
	var currentActive = null;
	var defaultState = null;
	var imageMaxWidth = 0;
	var imageMaxHeight = 0;

	getImagetNaturalSize();

	return {
		zoomInBlock: zoomInBlock,
		zoomOut: clearLastActive
	}


	function scaleContent(block, viewPort) {
		var height = viewPort.height();
		var width = viewPort.width();


		var image = $('#image-wrapper img');
		var imageHeight = image.height();
		var imageWidth = image.width();

		var widthRatio = width / block.width();
		var heightRatio = height / block.height();

		if (image.height() * heightRatio > imageMaxHeight) {
			heightRatio = imageMaxHeight / imageHeight;
		}
		if (image.width() * widthRatio > imageMaxWidth) {
			widthRatio = imageMaxWidth / imageWidth;
		}

		block.height(block.height() * heightRatio);
		block.width(block.width() * widthRatio);

		image.height(imageHeight * heightRatio);
		image.width(imageWidth * widthRatio);

		image.css('max-width', 'none');

	}

	function adjustPosition(block, viewPort) {

		var blockOffset = block.offset();

		var contYCenter = viewPort.offset().top + viewPort.height() / 2;
		var blockYCenter = block.offset().top + block.height() / 2;

		var scrollYValue = blockYCenter - contYCenter;
		var maxYscroll = viewPort[0].scrollHeight - viewPort.outerHeight();
		if (scrollYValue < 0) {
			viewPort.css('padding-top', Math.abs(scrollYValue));
		}
		if (scrollYValue > maxYscroll) {
			viewPort.css('padding-bottom', scrollYValue - maxYscroll);
		}
		viewPort.scrollTop(scrollYValue);


		var contXCenter = viewPort.offset().left + viewPort.width() / 2;
		var blockXCenter = block.offset().left + block.width() / 2;

		var scrollXValue = blockXCenter - contXCenter;
		var maxXscroll = viewPort[0].scrollWidth - viewPort.outerWidth();
		if (scrollXValue < 0) {
			viewPort.css('padding-left', Math.abs(scrollXValue));
		}
		if (scrollXValue > maxXscroll) {
			viewPort.css('padding-right', (scrollXValue - maxXscroll) * 2);
		}
		viewPort.scrollLeft(scrollXValue);
	}

	function clearLastActive() {
		if (currentActive) {
			currentActive.removeClass("active-block");
			currentActive.attr('style', defaultState);
			$('#image-wrapper img').attr('style', '');
			$('.image-container').attr('style', '');
		}
	}

	function getImagetNaturalSize() {
		var image = new Image();
		image.src = $('#image-wrapper img').attr("src");
		image.onload = function() {
			imageMaxWidth = this.width;
			imageMaxHeight = this.height;
		};

	}

	function zoomInBlock(blockId) {
		clearLastActive();

		var block = $('#container_' + blockId);
		var viewPort = $('.image-container');

		if (!block) {
			return;
		}

		block.addClass("active-block");

		currentActive = block;
		defaultState = block.attr('style');		

		scaleContent(block, viewPort)

		adjustPosition(block, viewPort);

	}


})();