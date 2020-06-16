const validarEmail = function validateEmail($email) {
    // const emailReg = /^([da-z_.-]+)@([da-z.-]+).([a-z.]{2,6})$/;
    // if (!emailReg.test($email)) {
    //     return false;
    // } else {
    //     return true;
    // }
    if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test($email)) {
        return true;
    } else {
        return false;
    }
};

export { validarEmail };
