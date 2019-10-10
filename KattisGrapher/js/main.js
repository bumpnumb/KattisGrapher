window.onload = function () {

    var w = window.innerWidth - 200;
    console.log(w);
    var x = document.getElementById("chart");
    x.style.maxWidth = w + "px";

    //var chart = new ApexCharts(document.querySelector("#timeline-chart"), options);

    //chart.render();
};


var searchBar = document.getElementsByClassName("searchTerm")[0];
var searchButton = document.getElementsByClassName("searchButton")[0];
// Execute a function when the user releases a key on the keyboard
searchBar.addEventListener("keyup", function (event) {
    // Number 13 is the "Enter" key on the keyboard
    if (event.keyCode === 13 && searchBar.value !== "") {
        // Cancel the default action, if needed
        event.preventDefault();
        // Trigger the button element with a click
        searchButton.click();
    }
});

searchButton.addEventListener("click", function (event) {
    document.getElementsByClassName("search_wrap")[0].classList.add("top");
    localStorage.setItem('Username', searchBar.value);
    sendTextMessage("Track\r\n" + searchBar.value);
});


