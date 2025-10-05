#version 450
precision highp float;

// https://mcguirev10.com/2024/01/20/monkey-hi-hat-getting-started-tutorial.html#fx-post-processing-effects

in vec2 fragCoord;
uniform vec2 resolution;
uniform float time;
uniform sampler2D receivedTexture;
out vec4 fragColor;

float time_frequency = 1.0;       // change over time (hertz)
float spiral_frequency = 10.0;    // vertical ripple peaks
float displacement_amount = 0.02; // how much the spiral twists

#define fragCoord (fragCoord * resolution)

const float PI = 3.14159265359;
const float TWO_PI = 6.28318530718;

void main()
{
//    fragColor = vec4(texture(receivedTexture, fragCoord / resolution.xy));  
//}

//void disabledmain()
//{
    vec2 uv_screen = fragCoord / resolution.xy;
    vec2 uv = (fragCoord - resolution.xy * 0.5) / resolution.y;
    
    vec2 uv_spiral = sin(vec2(-TWO_PI * time * time_frequency +         //causes change over time
                              atan(uv.x, uv.y) +                        //creates the spiral
                              length(uv) * spiral_frequency * TWO_PI,   //creates the ripples
                              0.));

    fragColor = vec4(texture(receivedTexture, uv_screen + uv_spiral * displacement_amount));
}
