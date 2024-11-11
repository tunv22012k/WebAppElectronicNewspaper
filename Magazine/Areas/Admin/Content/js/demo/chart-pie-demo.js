// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");
var myPieChart = new Chart(ctx, {
  type: 'doughnut',
  data: {
    labels: ["Breaking news", "Thời tiết", "Tài chính", "Game", "Thể thao", "Bất động sản", "Pháp luật", "Giao thông"],
    datasets: [{
      data: [13, 11, 9, 19, 17, 12, 8,11],
        backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#f00', '#ff9400', '#ff0026', '#0b00ff','#bb00ff'],
        hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#d23e3e', '#d23e3e', '#53b76b', '#bf2f5a','#9f2fbf'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  },
  options: {
    maintainAspectRatio: false,
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
    },
    legend: {
      display: false
    },
    cutoutPercentage: 80,
  },
});
