//История изменения поля
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

//Поле для рисования
function Canvas() {
    this.figures = [];
    this.selectedFigure = new Figure();
}
Canvas.prototype.addFigure = function (figure) {
    for (var i = 0; i < this.figures.length; i++) {
        if (this.figures[i] == figure) {
            return;
        }
    }
    this.figures.push(figure);
}
Canvas.prototype.getCoords = function (e) {
    var x = e.offsetX;
    var y = e.offsetY;

    return { x: x, y: y };
}
Canvas.prototype.paintFigures = function () {
    context.clearRect(0, 0, canvasElement.width, canvasElement.height);
    if (this.figures.length > 0) {
        for (var i = 0; i < this.figures.length; i++) {
            this.figures[i].paint();
        }
    }
}
Canvas.prototype.activFirureIndex = function (x, y) {
    for (var i = this.figures.length - 1; i >= 0; i--) {
        if (this.figures[i].insideMe(x, y)) {
            return i;
        }
    }
}
Canvas.prototype.activFirure = function (x, y) {
    return this.figures[this.activFirureIndex(x, y)];
}
Canvas.prototype.deleteFigure = function (x, y) {
    this.figures.splice(this.activFirureIndex(x, y), 1);
}
Canvas.prototype.deleteLastFigure = function () {
    this.figures.splice(this.figures.length - 1, 1);
}
Canvas.prototype.highlightedFigure = function (x, y) {
    this.selectedFigure = this.activFirure(x, y);
    if (this.selectedFigure) {
        this.deleteFigure(x, y);
        this.addFigure(this.selectedFigure);
    }
    this.paintFigures();
    this.activFirure(x, y).paintContour();
}

//Абстрактная фигура
function Figure() {
    this.type;
    this.color;
    this.x;
    this.y;
}
Figure.prototype.paint = function () { };
Figure.prototype.paintContour = function () { };
Figure.prototype.insideMe = function (x, y) { };

//Круг
function Circle(x, y, r, color) {
    this.type = "circle";
    this.x = x;
    this.y = y;
    this.r = r;
    this.color = color;
}
Circle.prototype = new Figure();
Circle.prototype.paint = function () {
    artisan.drawCircle('canvas', this.x, this.y, this.r, this.color, 3, this.color)
}
Circle.prototype.paintContour = function () {
    var fillStyle = "rgba(100,150,185,0)";
    var strokeStyle = "red";
    artisan.drawCircle('canvas', this.x, this.y, this.r, fillStyle, 3, strokeStyle)
}
Circle.prototype.insideMe = function (x, y) {
    if (Math.pow(x - this.x, 2) + Math.pow(y - this.y, 2) <= Math.pow(this.r, 2)) {
        return true;
    }
    return false;
}

//Прямоугольник
function Rectangle(x, y, width, height, color) {
    this.type = "rectangle";
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.color = color;
}
Rectangle.prototype = new Figure();
Rectangle.prototype.paint = function () {
    artisan.drawRectangle('canvas', this.x, this.y, this.width, this.height, this.color, 3, this.color);
}
Rectangle.prototype.paintContour = function () {
    var fillStyle = "rgba(0,0,0,0)";
    var strokeStyle = "red";
    artisan.drawRectangle('canvas', this.x, this.y, this.width, this.height, fillStyle, 3, strokeStyle);
}
Rectangle.prototype.insideMe = function (x, y) {
    if (x >= this.x && y >= this.y && x < this.x + this.width && y < this.y + this.height) {
        return true;
    }
    return false;
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////

$().ready(init);

var started = false, took = false;
var context, canvasElement;
var startX, startY;
var dx, dy;
var figure = new Figure();
var color;
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

//рисуем круг
function selectCircle() {
    figure = new Circle();
}

//рисуем прямугольник
function selectRect() {
    figure = new Rectangle();
}

//отмена действия
function cancel() {
    canvas = historyCanvas.loadState("cancel");
    for (var i = 0; i < canvas.figures.length; i++) {
        canvas.figures[i].paint();
    }
}

//восстановление действия
function restore() {
    canvas = historyCanvas.loadState("restore");
    for (var i = 0; i < canvas.figures.length; i++) {
        canvas.figures[i].paint();
    }
}

//удаление фигуры
function delet() {
    canvas.deleteLastFigure();
    canvas.paintFigures();
}

//изменяем цвет
function changeColor(){
    figure = canvas.selectedFigure;
    canvas.deleteLastFigure();
    color = $('#color').val();
    if (figure.type === "circle") {
        figure = new Circle(figure.x, figure.y, figure.r, color);
    }
    if (figure.type === "rectangle") {
        figure = new Rectangle(figure.x, figure.y, figure.width, figure.height, color);
    }
    if (figure) {
        canvas.addFigure(figure);
        //historyCanvas.saveState(canvas);
    }
    canvas.paintFigures();
    figure.paint();
    figure.paintContour();
}

// Начало рисования.
function downHandler(e) {
    startX = canvas.getCoords(e).x;
    startY = canvas.getCoords(e).y;
    if (canvas.activFirureIndex(startX, startY) >= 0) {
        canvas.highlightedFigure(startX, startY);

        figure = canvas.selectedFigure;
        canvas.deleteLastFigure();
        dx = startX - figure.x;
        dy = startY - figure.y;
        took = true;
    } else {
        context.beginPath();
        context.moveTo(startX, startY);
        color = $('#color').val();
        started = true;
    }
}

// Прекращение рисования.
function upHandler(e) {
    started = false;
    took = false;
    canvas.selectedFigure = figure;
    if (figure) {
        canvas.addFigure(figure);
        //historyCanvas.saveState(canvas);
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
        figure.paintContour();
    }
    else if (took) {

        if (figure.type === "circle") {
            figure = new Circle(endX - dx, endY - dy, figure.r, figure.color);
        }
        if (figure.type === "rectangle") {
            figure = new Rectangle(endX - dx, endY - dy, figure.width, figure.height, figure.color);
        }

        canvas.paintFigures();
        figure.paint();
        figure.paintContour();
    }
    //else if (canvas.figures.length > 0 && canvas.activFirure(endX, endY)) {
    //    //canvas.highlightedFigure(endX, endY);
    //    context.clearRect(0, 0, canvasElement.width, canvasElement.height);
    //    canvas.activFirure(endX, endY).paintContour();
    //}
}