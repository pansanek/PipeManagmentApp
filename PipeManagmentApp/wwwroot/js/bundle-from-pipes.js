document.addEventListener("DOMContentLoaded", function () {
    const checkboxes = document.querySelectorAll('input[name="selectedPipes"]');
    const button = document.getElementById('createPackageButton');

    function updateButtonVisibility() {
        const checkedCount = Array.from(checkboxes).filter(cb => cb.checked).length;
        button.style.display = checkedCount >= 2 ? 'inline' : 'none';
    }

    checkboxes.forEach(cb => cb.addEventListener('change', updateButtonVisibility));
    updateButtonVisibility();
});