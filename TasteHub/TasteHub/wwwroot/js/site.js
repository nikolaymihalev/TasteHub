// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("searchLink").addEventListener("click", function () {
    var searchTerm = document.getElementById("searchInput").value;
    var url = "/Recipe/Search?title=" + encodeURIComponent(searchTerm);
    window.location.href = url; // Redirect to the search URL
});
