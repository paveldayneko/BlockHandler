;
(function() {
	$(document).ready(function() {
		contentBlockService.getBlocks(processBlocks);
	});

	var contentBlockService = (function() {

		var url = "http://localhost:2000/";

		return {
			getBlocks: getBlocks
		}

		function getBlocks(callback) {

			$.getJSON(url + "?callback=?").done(function(json) {
				console.log(json);
				if (callback) {
					callback(json);
				}
			}).fail(function() {
				alert('Error ocured during data retrieving');
			});
		}
	})();

	function processBlocks(blocks) {
		createList(blocks);
		createContainers(blocks);
	}

	function createList(blocks) {
		var rootElement = $('#block-list');
		blocks.forEach(function(block) {
			var element = "<li onclick='contentController.zoomInBlock(\"" + block.id.toString() + "\")'>" + block.id + "</li>";
			rootElement.append(element);
		});
	}

	function createContainers(blocks) {
		var rootElement = $('#image-wrapper');
		blocks.forEach(function(block) {
			var element = $('<div id="container_' + block.id + '"></div>').css({
				top: block.top + '%',
				left: block.left + '%',
				width: block.width + '%',
				height: block.height + '%'
			});
			rootElement.append(element);
		})
	}

})();

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



	function zoomInBlock(blockId) {
		clearLastActive();

		var block = $('#container_' + blockId);
		if (block) {
			block.addClass("active-block");
			currentActive = block;
		}

		var viewPort = $('.image-container');
		defaultState = block.attr('style');
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
		image.css('max-width','none');


	}

	function clearLastActive() {
		if (currentActive) {
			currentActive.removeClass("active-block");
			currentActive.attr('style', defaultState);
			$('#image-wrapper img').attr('style', '');
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
})();