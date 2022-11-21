const btnTrabajador = document.getElementById('logTrabajador');
const btnEmpleador = document.getElementById('logEmpleador');

btnEmpleador.onclick = function switchEmpleador() {
    const cuerpoLoginTrabajador = document.getElementById("LogTrabajadorForm");
    cuerpoLoginTrabajador.style.display = 'none';
    const registrarEmpleadorForm = document.getElementById('registrarEmpleadorForm');
    registrarEmpleadorForm.style.display = 'none'
    const registrarTrabajadorForm = document.getElementById('registrarTrabajadorForm');
    registrarTrabajadorForm.style.display = 'none'

    const cuerpoLogin = document.getElementById("logEmpleadorForm");
    cuerpoLogin.style.display = 'block';

    btnEmpleador.style.border = "1px solid #E5E5E5"
    btnTrabajador.style.border = "none"
}

btnTrabajador.onclick = function switchTrabajador() {
    const cuerpoLoginEmpleador = document.getElementById("logEmpleadorForm");
    cuerpoLoginEmpleador.style.display = 'none';
    const registrarEmpleadorForm = document.getElementById('registrarEmpleadorForm');
    registrarEmpleadorForm.style.display = 'none'
    const registrarTrabajadorForm = document.getElementById('registrarTrabajadorForm');
    registrarTrabajadorForm.style.display = 'none'

    const cuerpoLogin = document.getElementById("LogTrabajadorForm");
    cuerpoLogin.style.display = 'block';
    btnTrabajador.style.border = "1px solid #E5E5E5"
    btnEmpleador.style.border = "none"
}

const registrarTrabajador = document.getElementById('registrarTrabajador');
registrarTrabajador.onclick = function showRegistroTrabajador() {
    const cuerpoLoginTrabajador = document.getElementById("LogTrabajadorForm");
    cuerpoLoginTrabajador.style.display = 'none';
    const registrarTrabajadorForm = document.getElementById('registrarTrabajadorForm');
    registrarTrabajadorForm.style.display = 'block'

}

const registrarEmpleador = document.getElementById('registrarEmpleador');
registrarEmpleador.onclick = function showRegistroEmpleador() {
    const logEmpleadorForm = document.getElementById("logEmpleadorForm");
    logEmpleadorForm.style.display = 'none';
    const registrarEmpleadorForm = document.getElementById('registrarEmpleadorForm');
    registrarEmpleadorForm.style.display = 'block'
}