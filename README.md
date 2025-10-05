# opentk-spout-test

Wires up the basics of [Spout2](https://github.com/leadedge/Spout2) send / receive of OpenGL textures.

The project accepts a `sender` or `receiver` command line argument to run either mode. An optional second argument specifies the Spout name. If not provided, the default name is "test". If your name contains spaces, wrap it in quotes. There is also an `alloc` argument to create a receiver that uses an internally-allocated texture instead of the shared texture.

> NOTE: The non-allocating receiver is not working; the Spout2 maintainer indicated there is a bug with that call. The allocating receiver works fine, and the sender works.

Send mode runs a simple plasma-color shader. Receive mode applies a ripple effect shader to the incoming texture. Use the Spout2 sample programs for testing (version info and link below).

Currently using [Spout.NETCore](https://github.com/AWAS666/Spout.NETCore) bindings. The package has bindings copied from the .NET Framework-based [Spout.Net](https://github.com/Ruminoid/Spout.NET) which were probably targeting Spout v2.006 based on the announcement date in the Spout2 issues. However, the bindings seem to work fine with newer versions and the package ships with v2.007.015. Get the Spout2 sample programs from the SDK binaries archive on that version's [release page](https://github.com/leadedge/Spout2/releases/tag/2.007.015). The v2.007.016 sample send/recieve programs also work as expected (but the library seems to randomly crash on init, hence the use of 015 instead of 016).

Note that my [eyecandy](https://github.com/MV10/eyecandy) library is used as a convenience. It provides some basic functionality from the [OpenTK](https://github.com/opentk/opentk) windowing support, shader compilation, and uniform handling, but it's entirely incidental to the Spout processing. OpenTK itself is a thin wrapper around OpenGL and GLFW windowing APIs.

As of this test, Spout2 is at a years-newer version, but currently the CppSharp package used to generate the bindings is years out of date and the available package is incompatible with VS2022 / Clang19. What is shown here seems to work, so hopefully this will carry my real project along until CppSharp is finally updated again.

<img width="991" height="1063" alt="image" src="https://github.com/user-attachments/assets/1facb76d-5115-4b76-a307-80bd6e70f2cc" />

