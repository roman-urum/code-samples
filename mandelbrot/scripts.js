var canvas  = document.getElementById('canvas');
context = canvas.getContext('2d');
canvas.addEventListener('mousedown', canvasScale);
canvas.addEventListener('contextmenu', e => e.preventDefault());
document.getElementById('left-button').addEventListener('click', canvasTranslateLeft);
document.getElementById('right-button').addEventListener('click', canvasTranslateRight);
document.getElementById('up-button').addEventListener('click', canvasTranslateUp);
document.getElementById('down-button').addEventListener('click', canvasTranslateDown);

var scale = 1;
var traslatex = 0;
var traslatey = 0;

window.onload = function onLoadHandler() {
    drawFractal();
}

function drawFractal(){
    console.log('drawFractal ');

    context.fillStyle = "rgba(255,255,255,1)";
    context.fillRect(0, 0, canvas.width, canvas.height );


    for(var i = -canvas.width; i < canvas.width; i++){
        for(var j = -canvas.height; j < canvas.height; j++){
            var it = i - traslatex;
            var jt = j - traslatey;
            var width = canvas.width;
            var height = canvas.height;

            var c = {
                re: it / width * scale,
                im: jt / height * scale
            };

            var n = 255;
            var k = 4;
            var m = amountOfMandelbrotIterations(c, n, k)
            color = m == n
                ? "rgba(0,0,0,1)"
                : `rgba(${1},${m},${1}, 1)`;

            context.fillStyle = color;
            context.fillRect( i, j, 1, 1 );
        }
    }
}

function canvasScale(e){

    console.log(e.layerX - canvas.width/2);
    console.log(e.layerY - canvas.height/2);
    //return;
    if (e.button == 0){
        scale /= getScaleStep();
        traslatex += (e.layerX - canvas.width/2) / scale;
        traslatey += (e.layerY - canvas.height/2) / scale;

    } else if (e.button == 2){
        scale *= getScaleStep();
        //traslatex += (e.layerX - canvas.width/2) * scale;
        //traslatey += (e.layerY - canvas.height/2) * scale;

    }
    drawFractal();
}

function amountOfMandelbrotIterations(c, n, k){
    var z = c;

    for(var i = 0; i < n; i++) {
        z = {
            re: z.re * z.re - z.im*z.im + c.re,
            im: 2 * z.re * z.im  + c.im
        };
        if (z.re * z.re + z.im * z.im > k) {
            return i+1;
        }
    }
    return n;
}
function canvasTranslateLeft(e){
    traslatex -= getTranslateStep();
    drawFractal();
}
function canvasTranslateRight(e){
    traslatex += getTranslateStep();
    drawFractal();
}
function canvasTranslateUp(e){
    traslatey -= getTranslateStep();
    drawFractal();
}
function canvasTranslateDown(e){
    traslatey += getTranslateStep();
    drawFractal();
}
function getTranslateStep(){
    return parseInt(document.getElementById('translate-step').value);
}
function getScaleStep(){
    return parseInt(document.getElementById('scale-step').value);
}