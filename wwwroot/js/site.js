// reference: https://thewebdev.info/2021/05/02/how-to-check-if-a-mouse-button-is-kept-down-after-we-press-it-with-javascript/
boxes = [];
coords1 = [];
var mouseDown = 0;  // Keep track of whether the mouse if currently clicked (a drag is in progress)
var canvasWidth;
var canvasHeight;
coords2 = [];
var width;
var length;
var box;
snips = [];
var image = new Image();
var chosenImage = document.querySelector("#surveyImage");
const dataToSend = {
    xstart: null,
    ystart: null,
    width: null,
    height: null
}
//global variables that are used later in the program
//the last variable dataToSend, declared with const means this is a JS object

function doc_loaded() {
    //these methods are called as soon as document loads
    loadImage();
    canvasResize();
    setUpNode();
}

function mouseDownHandler(e) {
    mouseDown++;// add one to mouse down value - this tracks whether mouse is up or down where 1 = down and 0 = up
    beginBox(e);
}
//what happens when the mouse is down

function mouseUpHandler(e) {
    mouseDown--;
    drawBox(e);
}
//what happens when mouse is up

function mouseMoveHandler(e) {
    if (mouseDown == 1) {
        drawBoxOnDrag(e);
    }
    //if mouse is down, draw while dragging
}

function canvasResize() {
    canvasWidth = image.naturalWidth;
    canvasHeight = image.naturalHeight;
    //change canvas dimensions to fit image
}



function loadImage() {
    var canvas = document.getElementById("imageCanvas");
    //way of referencing canvas element

    document.querySelector("#surveyImage").addEventListener("change", () => {
        canvasResize();
        loadImage();
        //as soon as surveyImage changes, call these two functions

        image.src = URL.createObjectURL(chosenImage.files[0]);
        //creates URL object of only the first of files if many have been uploaded and stores this url as the image source
    })

    canvas.width = image.naturalWidth;
    canvas.height = image.naturalHeight;
    //resizes canvas to fit image

    context = canvas.getContext('2d');
    context.drawImage(image, 0, 0);
    //draws image starting at top  left

}

function drawBoxOnDrag(e) {

    var canvas = document.getElementById("imageCanvas");

    // Show the survey background image
    var ctx = canvas.getContext("2d");
    ctx.drawImage(image, 0, 0);

    ctx.strokeStyle = "#FF0000";//red

    // Draw all existing boxes
    for (i = 0; i < boxes.length; i++) {
        ctx.strokeRect(boxes[i][0], boxes[i][1], boxes[i][2], boxes[i][3]); //nested array where loop is iterating through x, y, width & height values to draw boxes

    }

    //Get the finish coordinates of the box
    coords2 = getEndMouseCoordinates(e);
    width = coords2[0] - coords1[0];
    length = coords2[1] - coords1[1];

    // Draw the new box
    var box = [coords1[0], coords1[1], width, length];

    ctx.strokeRect(box[0], box[1], box[2], box[3]);

}

function showMouseCoordinates(e) {

    var outputLabel = document.getElementById("lblMouseCoordinates");
    var coords = getStartMouseCoordinates(e);

    outputLabel.innerText = `(${coords[0]},${coords[1]})`;
    return coords;
}

function getStartMouseCoordinates(e) {
    var canvas = document.getElementById("imageCanvas");
    var rect = canvas.getBoundingClientRect();
    var x = e.clientX - rect.left;
    var y = e.clientY - rect.top;
    //finds distance between mouse and top left to figure out coords
    return [x, y];
}

function getEndMouseCoordinates(e) {
    var canvas = document.getElementById("imageCanvas");
    var rect = canvas.getBoundingClientRect();
    var x = e.clientX - rect.left;
    var y = e.clientY - rect.top;
    return [x, y];
}

function beginBox(e) {

    var canvas = document.getElementById("imageCanvas");

    var ctx = canvas.getContext("2d");

    coords1 = getStartMouseCoordinates(e);
}

function drawBox(e) {
    var canvas = document.getElementById("imageCanvas");
    var ctx = canvas.getContext("2d");
    coords2 = getEndMouseCoordinates(e);
    width = coords2[0] - coords1[0];
    length = coords2[1] - coords1[1];

    box = [coords1[0], coords1[1], width, length];

    ctx.strokeStyle = "#FF0000";
    ctx.strokeRect(box[0], box[1], box[2], box[3]);

    boxes.push(box);
}

function setUpNode() {
    const express = require('express');
    const app = express();
    const router = express.Router();


    // Parse URL-encoded bodies (as sent by HTML forms)
    app.use(express.urlencoded());

    // Parse JSON bodies (as sent by API clients)
    app.use(express.json());

    app.get('/', function (request, response) {
        response.sendFile('index.html', { root: __dirname });
    });


    app.post("/data", (req, res) => {
        console.log(req.body);
        //res.send("About this wiki");
    });

    app.listen(3000, () => {
        console.log('server started');
    });


    module.exports = router;
    //sets up node.js to send over data to server
}

function sendData() {
    // Create an XMLHttpRequest object to act as HTTP client
    const xhttp = new XMLHttpRequest();


    var jsonifiedData = "";
    for (var i = 0; i < boxes.length; i++) {
        dataToSend.xstart = boxes[i][0];
        dataToSend.ystart = boxes[i][1];
        dataToSend.width = boxes[i][2];
        dataToSend.length = boxes[i][3];
        //assigns value of box properties to properties of JS object
        xhttp.onload = function () {
            document.getElementById("responseMessage").innerHTML = "<p>Data sent.</p>";
        }
        xhttp.open("POST", "/data", true);
        //data gets sent
        xhttp.setRequestHeader('Content-Type', 'application/json')
        // Send a request
        var jsonifiedData = jsonifiedData + JSON.stringify(dataToSend);
    }
    xhttp.send(jsonifiedData);
}