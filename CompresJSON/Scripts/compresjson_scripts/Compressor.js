
function Compress(str) {
    var rc = LZString.compress(str);
    //console.log(rc);
    return Base64.encode(rc);
}

function Decompress(str) {
    var rc = Base64.decode(str);
    //console.log(rc)
    return LZString.decompress(rc);
}