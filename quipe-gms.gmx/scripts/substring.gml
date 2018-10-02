///substring(string, start, length)
var str = argument0;
var start = argument1 + 1; //I'm gonna index by 0, dammit
var result = "";

for (var i = 0; i < argument2; i++) {
    result += string_char_at(str, start + i);
}   

return result;
