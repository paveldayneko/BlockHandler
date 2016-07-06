var http = require('http'),
    fileSystem = require('fs'),
    path = require('path');
    var url = require('url');

http.createServer(function(request, response) {
        var filePath = path.join(__dirname, 'Pg001.json');
        var stat = fileSystem.statSync(filePath);
        var reqObj = url.parse(request.url, true);

  console.log("---------------------------------");
        console.log(reqObj.query);

        response.writeHead(200, {
            'Content-Type': 'application/javascript'           
        });

        fileSystem.readFile(filePath, 'utf8', function(err, data) {
            if (err) {
                return console.log(err);
            }           
            response.end(reqObj.query.callback + '(' + data + ')');
        });

    
    })
    .listen(2000);