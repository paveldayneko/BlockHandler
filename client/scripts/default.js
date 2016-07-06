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

	}

	function clearLastActive() {
		if (currentActive)
			currentActive.removeClass("active-block");
	}
})();