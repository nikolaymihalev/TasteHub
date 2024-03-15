document.getElementById("searchLink").addEventListener("click", function () {
    var searchTerm = document.getElementById("searchInput").value;
    var url = "/Recipe/Search?title=" + encodeURIComponent(searchTerm);
    window.location.href = url; // Redirect to the search URL
});