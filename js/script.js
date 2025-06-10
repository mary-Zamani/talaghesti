function hideSidebar() {
    const sidebar = document.querySelector('.sidebar-list-css')
    sidebar.style.display = 'none'
}
$(document).ready(function (){
    $('.sidebar-open-btn-js').click(function (){
        $('.sidebar-list-css').css('right', '0');
    });
    $('.sidebar-close-btn-js').click(function (){
        $('.sidebar-list-css').css('right', '-300px');
    });
    $('.open-close-side-sub-js').click(function () {
        $(this).next('.sidebar-sub-menu-css').slideToggle();
        $(this).find('i.dropdown-css').toggleClass('rotate');
        
    });
})