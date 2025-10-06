// Archivo: wwwroot/js/site.js

// Función para inicializar los componentes de AdminLTE
function initializeAdminLTE() {
    // Verificamos que las librerías se hayan cargado.
    if (typeof adminlte !== 'undefined') {

        // Verificamos que los módulos existan antes de llamar a .init()
        // En AdminLTE 4, estos métodos pueden estar directamente bajo el objeto adminlte.
        // Usamos el código más genérico que suele funcionar:

        // adminlte.Layout.init()
        // adminlte.PushMenu.init()

        // Ejecutar la función de inicialización global de AdminLTE:
        adminlte.onDomReady(() => {
            // Este método de AdminLTE 4 busca y inicializa todos los data-widgets
            adminlte.init()
        });
    }
}

// 1. Llamar a la inicialización al cargar la página por primera vez
window.onload = initializeAdminLTE;

// 2. Definimos una función que Blazor puede llamar
// Blazor NO DEBE LLAMAR A ESTO EN LA NAVEGACIÓN, ya que AdminLTE 4 usa el DOM observer.
// Pero la mantenemos para depuración:
window.reloadAdminLTE = initializeAdminLTE;