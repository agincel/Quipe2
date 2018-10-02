///scr_bounce(obj_ball, obj_peg)
var p1 = argument0;
var p2 = argument1;

if (p2.bounciness > 0.5 && (p1.snd == noone || !audio_is_playing(p1.snd) ||  audio_sound_get_track_position(p1.snd) > 0.01)) {
    p1.snd = audio_play_sound(asset_get_index("snd_blip_" + string(irandom_range(1, 5))), 0, false);
}
    

    
if (p2.bounciness > 0.5) {// || (p2.receivedColor != p1.drawColor && p2.colorLerp >= p2.colorLerpMax)) { //latter adds this back to the bottom
    p2.receivedColor = p1.drawColor;
    p2.colorLerp = 0;
}

var angleDeg = arctan2(p1.y - p2.y, p1.x - p2.x) * 180 / 3.1415926;

var currentForce = sqrt(sqr(p1.vX) + sqr(p1.vY));

var newX = (0.45 * p1.vX) + (1 * currentForce * p2.bounciness * dcos(angleDeg));
var newY = currentForce * p2.bounciness * dsin(angleDeg);

while(newX == 0) {
    if (p1.vX > 0)
        newX = irandom_range(1, 2);
    else if (p1.vX < 0)
        newX = irandom_range(-2, -1);
    else
        newX = irandom_range(-2, 2);
}

p1.vX = newX * 0.9;
p1.vY = newY;


