// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Event Listener to Clear the Fields elements of the CreateRequest.cshtml form.

document.addEventListener("DOMContentLoaded", function () {
    const clearButton = document.querySelector("button.btn-secondary");

    clearButton.addEventListener("click", function () {
        // Clear the select elements
        document.getElementById("RequestTypeControlSelect").selectedIndex = 0;
        document.getElementById("RequestStatusControlSelect").selectedIndex = 0;

        // Clear the text inputs
        document.getElementById("titleField").value = "";
        document.getElementById("subjectManager").value = "";
        document.getElementById("ticketDescription").value = "";
        document.getElementById("dueDate").value = "";
        document.getElementById("attachmentFiles").value = "";
        document.getElementById("revokingDate").value = "";
        document.getElementById("adminNotes").value = "";
        document.getElementById("resolutionInformation").value = "";
    });
});

