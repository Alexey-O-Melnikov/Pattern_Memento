function HistiryCanvas() {
    this.indexState = 0;
    this.canvases = [];
    this.saveState = function (can) {
        if(this.indexState < this.canvases.length) {
            this.canvases.splice(this.indexState, this.canvases.length - this.indexState);
        }
        var clonCan = {};
        Object.assign(clonCan, can);
        this.canvases.push(clonCan);
        this.indexState++;
    }
    this.loadState = function (go) {
        if (go === "cancel" && this.indexState > 1) {
            this.indexState--;
        }
        else if (go === "restore" && this.indexState < this.canvases.length) {
            this.indexState++;
        }

        return this.canvases[this.indexState - 1];
    }
}

function Canvas() {
    this.figures = [];
    this.addFigure = function (figure) {
        this.figures.push(figure);
    }
    this.getCoords = function (e) {
        var x = e.offsetX;
        var y = e.offsetY;

        return { x: x, y: y };
    }
    this.paintFigures = function () {
        context.clearRect(0, 0, canvasElement.width, canvasElement.height);
        if (this.figures.length > 0) {
            for (var i = 0; i < this.figures.length; i++) {
                this.figures[i].paint();
            }
        }
    }
    this.paintActivFirure = function (x, y) {
        this.paintFigures();
        for (var i = this.figures.length - 1; i >= 0; i--) {
            if (this.figures[i].insideMe(x, y)) {
                this.figures[i].paintContour();
                break;
            }
        }

    }
}

function Circle(x, y, r, color) {
    this.type = "circle";
    this.x = x;
    this.y = y;
    this.r = r;
    this.color = color;
    this.paint = function () {
        artisan.drawCircle('canvas', this.x, this.y, this.r, this.color, 3, this.color)
    }
    this.paintContour = function () {
        var fillStyle = "rgba(100,150,185,0)";
        var strokeStyle = "red";
        artisan.drawCircle('canvas', this.x, this.y, this.r, fillStyle, 3, strokeStyle)
    }
    this.insideMe = function (x, y) {
        if(Math.pow(x - this.x, 2) + Math.pow(y - this.y, 2) < Math.pow(this.r, 2)){
            return true;
        }
        return false;
    }
}

function Rectangle(x, y, width, height, color) {
    this.type = "rectangle";
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.color = color;
    this.paint = function () {
        artisan.drawRectangle('canvas', this.x, this.y, this.width, this.height, this.color, 3, this.color);
    }
    this.paintContour = function () {
        var fillStyle = "rgba(100,150,185,0)";
        var strokeStyle = "red";
        artisan.drawRectangle('canvas', this.x, this.y, this.width, this.height, fillStyle, 3, strokeStyle);
    }
    this.insideMe = function (x, y) {
        if (x > this.x && y > this.y && x < this.x + this.width && y < this.y + this.height) {
            return true;
        }
        return false;
    }
}

$().ready(init);

var started = false, context, canvasElement;
var startX, startY;
var figure, color;
var canvas;
var historyCanvas;


function init() {
    canvasElement = $('canvas').get(0);
    context = canvasElement.getContext("2d");
    
    $('canvas').on('mousemove', moveHandler);
    $('canvas').on('mousedown', downHandler);
    $('canvas').on('mouseup', upHandler);

    historyCanvas = new HistiryCanvas();
    canvas = new Canvas();
}

function selectCircle() {
    figure = new Circle();
}

function selectRect() {
    figure = new Rectangle();
}

function cancel() {
    canvas = historyCanvas.loadState("cancel");
    for (var i = 0; i < canvas.figures.length; i++) {
        canvas.figures[i].paint();
    }
}

function restore() {
    canvas = historyCanvas.loadState("restore");
    for (var i = 0; i < canvas.figures.length; i++) {
        canvas.figures[i].paint();
    }
}

// Начало рисования.
function downHandler(e) {
    context.beginPath();
    startX = canvas.getCoords(e).x;
    startY = canvas.getCoords(e).y;
    context.moveTo(startX, startY);
    color = $('#color').val();
    started = true;
}

// Прекращение рисования.
function upHandler(e) {
    started = false;
    if (figure) {
        canvas.addFigure(figure);
        historyCanvas.saveState(canvas);
    }
}

// обработка движения указателя по элементу canvas
function moveHandler(e) {
    var endX = canvas.getCoords(e).x;
    var endY = canvas.getCoords(e).y;
    if (started) {
        var width = endX - startX;
        var height = endY - startY;

        if (!figure) {
            return;
        }
        else if (figure.type === "circle") {
            var r = width > height ? width : height;
            figure = new Circle(startX, startY, r, color);
        }
        else if (figure.type === "rectangle") {
            figure = new Rectangle(startX, startY, width, height, color);
        }
        canvas.paintFigures();
        figure.paint();
    } else if (canvas.figures.length > 0) {
        canvas.paintActivFirure(endX, endY);
    }
}

