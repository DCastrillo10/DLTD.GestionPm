(function (window, document, undefined) {
    'use strict';
    if (!('localStorage' in window)) return;
    var nightMode = localStorage.getItem('gmtNightMode');
    if (nightMode) {
        document.documentElement.className += ' night-mode';
    }
})(window, document);


(function (window, document, undefined) {

    'use strict';

    // Feature test
    if (!('localStorage' in window)) return;

    // Get our newly insert toggle
    var nightMode = document.querySelector('#night-mode');
    if (!nightMode) return;

    // When clicked, toggle night mode on or off
    nightMode.addEventListener('click', function (event) {
        event.preventDefault();
        document.documentElement.classList.toggle('night-mode');
        if (document.documentElement.classList.contains('night-mode')) {
            localStorage.setItem('gmtNightMode', true);
            return;
        }
        localStorage.removeItem('gmtNightMode');
    }, false);

})(window, document);



function renderDonutChart() {
    const ctx = document.getElementById('donutChart').getContext('2d');
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ['Completed', 'In Progress', 'Behind'],
            datasets: [{
                data: [76, 32, 13],
                backgroundColor: ['#198754', '#ffc107', '#dc3545'],
                hoverBackgroundColor: ['#157347', '#e0a800', '#b02a37']
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true, // Mantener proporciones
            plugins: {
                legend: {
                    position: 'bottom'
                }
            }
        }
    });
}