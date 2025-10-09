

window.initAdminLteSidebar = () => {
    setTimeout(() => {
        if (window.AdminLTE && window.AdminLTE.Sidebar) {
            window.AdminLTE.Sidebar._jQueryInterface.call(document.querySelectorAll('.app-sidebar'));
        }
    }, 100);
};