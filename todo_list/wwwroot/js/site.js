// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const rows = document.querySelectorAll('tr');

rows.forEach(row => {
    const cells = row.querySelectorAll('td');
    
    cells.forEach(cell => {
        switch (cell.textContent.trim()){
            case 'Critical':
                row.classList.add('row-priority-critical');
                break;
            case 'High':
                row.classList.add('row-priority-high');
                break;
            case 'Medium':
                row.classList.add('row-priority-medium');
                break;
            case 'Low':
                row.classList.add('row-priority-low');
                break;
        }

    });
});

document.getElementById('toggle-dark-mode').addEventListener('click', function() {
    document.body.classList.toggle('dark-mode');
});