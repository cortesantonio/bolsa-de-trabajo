const btnTrabajador = document.getElementById('logTrabajador');
const btnEmpleador = document.getElementById('logEmpleador');


btnEmpleador.onclick = function switchEmpleador() {
    const cuerpoLoginTrabajador = document.getElementById("LogTrabajadorForm");
    cuerpoLoginTrabajador.style.display = 'none';

    const cuerpoLogin = document.getElementById("logEmpleadorForm");
    cuerpoLogin.style.display = 'block';

    btnEmpleador.style.backgroundColor = "gray"
    btnTrabajador.style.backgroundColor = "transparent"
}

btnTrabajador.onclick = function switchTrabajador() {
    const cuerpoLoginEmpleador = document.getElementById("logEmpleadorForm");
    cuerpoLoginEmpleador.style.display = 'none';

    const cuerpoLogin = document.getElementById("LogTrabajadorForm");
    cuerpoLogin.style.display = 'block';
    btnTrabajador.style.backgroundColor = "gray"
    btnEmpleador.style.backgroundColor = "transparent"
}

const registrarTrabajador = document.getElementById('registrarTrabajador');
registrarTrabajador.onclick = function showRegistroTrabajador() {
    const cuerpoLoginTrabajador = document.getElementById("LogTrabajadorForm");
    cuerpoLoginTrabajador.style.display = 'none'; 
    const registrarTrabajadorForm = document.getElementById('registrarTrabajadorForm');
    registrarTrabajadorForm.style.display = 'block'

}