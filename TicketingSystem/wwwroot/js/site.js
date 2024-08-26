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

sendButton.addEventListener("click", function (event) {
    // Prevent form submission if validation fails
    const requestTypeControlSelect = document.getElementById("RequestTypeControlSelect").value;
    const titleField = document.getElementById("titleField").value.trim();
    const ticketDescription = document.getElementById("ticketDescription").value.trim();
    const dueDate = document.getElementById("dueDate").value.trim();

    if (!requestTypeControlSelect || !titleField || !ticketDescription || !dueDate) {
        alert("Please fill out all required fields.");
        event.preventDefault(); // Prevent form submission
    }
});

function toggleEditMode() {
    const fields = document.querySelectorAll('#viewRequestModal input, #viewRequestModal textarea');
    const editButton = document.getElementById('editButton');

    if (editButton.textContent === 'Edit') {
        // Change to edit mode
        fields.forEach(field => field.removeAttribute('readonly'));
        fields.forEach(field => field.style.backgroundColor = 'white');
        editButton.textContent = 'Save';
        editButton.classList.remove('btn-primary');
        editButton.classList.add('btn-success');
    } else {
        // Change to view mode
        fields.forEach(field => field.setAttribute('readonly', 'true'));
        fields.forEach(field => field.style.backgroundColor = '#e9ecef');
        editButton.textContent = 'Edit';
        editButton.classList.remove('btn-success');
        editButton.classList.add('btn-primary');
    }
}



