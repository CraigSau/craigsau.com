var i = 0;
var text = "Hi, I'm Craig"
var speed = 100;

document.addEventListener("DOMContentLoaded", function typeWriter() {
    if (i < text.length) {
        document.getElementById("typewriter").innerHTML += text.charAt(i);
        i++;
        setTimeout(typeWriter, speed);
    }
})