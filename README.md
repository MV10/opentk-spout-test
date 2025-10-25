# opentk-spout-test

Wires up the basics of [Spout2](https://github.com/leadedge/Spout2) send / receive of OpenGL textures.

The project accepts one of three command-line arguments:
* `sender` copies program output to one or more receivers
* `receiver` copies another program's output using a shared texture
* `alloc` is a receiver mode using an internally-allocated texture

> NOTE: The `receiver` mode is not currently working. The `alloc` mode for reciving content works correctly.

An optional second argument is a Spout sender name (enclosed in quotes if the name includes spaces). For sender mode, the name identifies the sender. The default sender name is "test" if none is specified. For receiver modes, the name identifies the sender to receive from. If it is not specified, the receiver attaches to the first sender it finds.

Sendr mode runs a simple plasma-color shader. Both receiver modes apply a ripple effect shader to the incoming texture. Use the Spout2 sample programs for testing (version info and link below).

This uses the [Spout.NETCore](https://github.com/AWAS666/Spout.NETCore) bindings and Spout v2.007.017. Get the Spout2 sample programs from the SDK binaries archive on the [.017 release page](https://github.com/leadedge/Spout2/releases/tag/2.007.017) and/or the [.014 release page](https://github.com/leadedge/Spout2/releases/tag/2.007.014) (the sample senders for each have different content but are compatible).

Note that my [eyecandy](https://github.com/MV10/eyecandy) library is used as a convenience. It provides some basic functionality from the [OpenTK](https://github.com/opentk/opentk) windowing support, shader compilation, and uniform handling, but it's entirely incidental to the Spout processing. OpenTK itself is a thin wrapper around OpenGL and GLFW windowing APIs.

<img width="991" height="1063" alt="image" src="https://github.com/user-attachments/assets/1facb76d-5115-4b76-a307-80bd6e70f2cc" />

