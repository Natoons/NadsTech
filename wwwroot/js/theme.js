// Theme initialization
(function() {
    // Check for saved theme preference or default to 'light'
    const theme = localStorage.getItem('theme') || 'light';
    
    // Apply the theme
    if (theme === 'dark') {
        document.documentElement.classList.add('dark');
    } else {
        document.documentElement.classList.remove('dark');
    }
    
    // Prevent flash of unstyled content
    document.documentElement.style.display = 'block';
})(); 