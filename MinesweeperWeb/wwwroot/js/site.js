// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// call the function generateBoard to get initial board

// click a cell and reveal the closed cell along with it data?
    // click
var gridItems = document.querySelectorAll(".grid-item");
console.log(gridItems);
    // check if grid item is a closed cell, which can only be clicked on
gridItems.forEach((gridItem, index) => {
    gridItem.addEventListener("click", function () {
        if (gridItem.classList.contains("closed")) {
            // clickable
            console.log(gridItem);
            gridItem.classList.add("opened");
            gridItem.classList.remove("closed");
        }
    })
}) 


