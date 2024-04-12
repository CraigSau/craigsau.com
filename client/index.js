var i = 0;
var j = 0;
var text = "Hi, I'm Craig"
var speed = 100;
var text2 = "My passions include "
// I have never been so disgusted at myself for something I have done before...
var passions = [
    "Fitness     ",
    "Travel     ",
    "Gaming     ",
    "Anime     ",
    "Manga     ",
    "Programming     ",
    "Self-Improvement     "
];
var word = 0;
var letter = 0;
var passionsCount = passions.length;
var wordComplete = false;


document.addEventListener("DOMContentLoaded", typeWriter());

function typeWriter() {
    if (i < text.length) {
        document.querySelector("#tw1").classList.add('cursor');
        document.querySelector("#intro").innerHTML += text.charAt(i);
        i++;
        setTimeout(typeWriter, speed);
    }
    else if (i >= text.length && j < text2.length){
        setTimeout(function() {
            document.querySelector("#tw1").classList.remove('cursor');
            document.querySelector("#tw2").classList.add('cursor');
            typeWriterPassion();
        }, 1000);
    }
    else { 
        setTimeout(function() {
            typeWriterRecurringType();
        }, 1000);
    }
}


function typeWriterPassion() {
    if (j < text2.length) {
        document.querySelector("#passions").innerHTML += text2.charAt(j);
        j++;
        setTimeout(typeWriterPassion, speed);  
    }
    else {
        typeWriterRecurringType();
    }
};

function typeWriterRecurringType() {
    if (letter < passions[word].length && wordComplete == false) {
        document.querySelector('#passions').innerHTML += passions[word].charAt(letter);
        letter++;
        if (letter >= passions[word].length) { wordComplete = true; }
    }
    else if (wordComplete == true) {
        let string = document.querySelector("#passions").innerHTML;
        string = string.slice(0, -passions[word].length);
        document.querySelector("#passions").innerHTML = string;
        letter = 0;
        if (letter == 0) {
            wordComplete = false; 
            word++;
        }
    }
    if (word == passions.length) {word = 0;} 
    setTimeout(typeWriterRecurringType, speed)
}


// THIS SHIT SHOULD HAVE WORKED, BUT SOME DUMB ASS BULL SHIT IS GOING ON AND IDK WHAT SO 
// I GAVE UP AND HOBBLED TOGETHER THIS DUMB SHIT ABOVE AND LEAVING THIS HERE TO GET MAD 
// ABOUT IN THE FUTURE AS WELL
// Basically I suck, skill diff
function typeWriterPassionRecurring() {
    for (let wordListInd = 0; wordListInd < passions.length; wordListInd++) {
        let word = ""; 
        console.log("BIG LOOP");
        for (let wordInd = 0; wordInd < passions[wordListInd].length; wordInd++) {
            word = passions[wordListInd].charAt(wordInd);
            document.querySelector("#passions").innerHTML += word;
            setTimeout(typeWriterPassionRecurring, speed);
            pause(3000);
        }

        for (let wordInd = passions[wordListInd].length; wordInd > 0; wordInd--) {
            let string = document.querySelector("#passions").innerHTML;
            string = string.slice(0, -1);
            document.querySelector("#passions").innerHTML = string;
            setTimeout(typeWriterPassionRecurring, speed);
            pause(3000);
        }
    }
}
