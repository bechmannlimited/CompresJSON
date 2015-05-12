
function Compress(str) {
    var rc = LZString.compress(str);
    return Base64.encode(rc);
}

function Decompress(str) {
    var rc = Base64.decode(str);
    return LZString.decompress(rc);
}

function Encrypt(str, key) {
    var encrypted = CryptoJS.AES.encrypt(str, key); 
    return encrypted.toString();
}

function Decrypt(str, key) {
    var encrypted = CryptoJS.AES.decrypt(str, key);
    return encrypted.toString(CryptoJS.enc.Utf8);
}