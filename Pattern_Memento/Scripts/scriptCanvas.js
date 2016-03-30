$(document).ready(init);

var started = false, context, canvas;
var startX, startY;
var endX, endY;
var width, height;
var figure, color;

function init() {
    canvas = $('canvas').get(0);
    context = canvas.getContext("2d");
    
    $('canvas').on('mousemove', moveHandler);
    $('canvas').on('mousedown', downHandler);
    $('canvas').on('mouseup', upHandler);
}


// Получение координат мыши относительно элемента canvas
function getCoords(e) {
	var x = e.offsetX;
	var y = e.offsetY;

    return { x: x, y: y };
}

// Начало рисования.
function downHandler(e) {
    context.beginPath();
    startX = getCoords(e).x;
    startY = getCoords(e).y;
    context.moveTo(startX, startY);
    started = true;
}

// Прекращение рисования.
function upHandler(e) {
    started = false;
}


// обработка движения указателя по элементу canvas
function moveHandler(e) {
    endX = getCoords(e).x;
    endY = getCoords(e).y;

    if (started) {
        context.clearRect(0, 0, canvas.width, canvas.height);

        width = endX - startX;
        height = endY - startY;
        color = $('#color').val();

        if (figure == "circle") {
            paintCircle();
        }
        else if (figure == "rect") {
            paintRect();
        }
        context.stroke();
    } /*else if (endX > startX && endY > startY && endX < width + startX && endY < height + startY) {
        context.strokeStyle = "red";
        context.lineWidth = 3;
        context.strokeRect(startX, startY, width, height);
    }*/
}

//рисовать прямоугольник
function paintRect() {
    line_width = 3;
    stroke_color = color;
    artisan.drawRectangle('canvas', startX, startY, width, height, color, line_width, stroke_color);
}

//рисовать круг
function paintCircle() {
    var radius = width > height ? width : height;
    artisan.drawCircle('canvas', startX, startY, radius, color , 1, color)
}

//выбрать тип фигуры 'круг' для рисования
function selectCircle() {
    figure = "circle";
}

//выбрать тип фигуры 'прямоугольник' для рисования
function selectRect(){
    figure = "rect";
}