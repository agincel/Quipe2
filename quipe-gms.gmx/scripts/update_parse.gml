///update_parse(string)

var ID = "";
var SCORE = "";

show_debug_message(argument0);

var i = 2; //skip first quote
while(string_char_at(argument0, i) != "|") {
    ID += string_char_at(argument0, i);
    i += 1;
}
i += 1; //skip space
while(string_char_at(argument0, i) != "|") {
    SCORE += string_char_at(argument0, i);
    i += 1;
}

var results = -1;
results[0] = ID;
results[1] = real(SCORE);
return results;
