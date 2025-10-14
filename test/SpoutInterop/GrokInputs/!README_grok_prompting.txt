
Create Spout.dll.exports.txt by running this in the top source directory
where Spout.dll is:

  dumpbin /exports Spout.dll > Spout.dll.exports.txt

Copy all of the latest Spout2 repo headers from the SpoutLibrary and
SpoutGL directories, then click !concat_header_files.cmd to produce
headers.txt.

The docs are just View Frame copy-paste of the HTML from the Spout
docs pages for the Spout and SpoutUtils classes.  

Monitor the generated code for comments indicating the user should
follow Grok's example. If seen, interrupt the output and clarify
that the goal is for Grok to generate all code with no placeholders
or omissions, then tell it to try again.

Prompting is a multi-step process to account for Grok "losing focus"
with more complex requirements.

Grok has a 10 file attachment limit. Attach these files:
  Spout.dll.exports.txt
  SpoutDocs1.html.txt
  SpoutDocs2.html.txt
  headers.txt


Prompt #1:
-------------------------------------------------------------
Spout.dll is a native Windows library called Spout2 written in C and C++. "Spout.dll.exports.txt" is the output of `dumpbin /exports` for the current build of Spout.dll. The header files are concatenated in "headers.txt" with separator lines indicating the original header filename (such as Spout.h), and they correspond to that build of Spout.dll. The headers should be used to estimate sizeof for C++ objects (add a safe fudge-factor to account for any uncertainties). If Grok needs other parts of the original native Spout2 project, please ask, do not search for them. The user has a complete, current, local copy of the Spout2 repository available. The SpoutLibrary header is provided but there is no SpoutLibrary C-compatible DLL available at this time. Focus on Spout.DLL and the provided exports.

SpoutDocs1.html.txt and SpoutDocs2.html.txt are the HTML from the official Spout documentation listing publicly-exposed classes, functions, and related features like enums.

The first goal is for Grok to produce a C# .NET 8 P/Invoke interop file called `SpoutNative.cs` exposing a public class named `SpoutNative`. The interop declarations in `SpoutNative` should correspond to the HTML documentation for the native Spout, SpoutSender, SpoutReceiver, and SpoutUtils classes, functions, and any inputs such as enums, structs, constants, etc. 

DO NOT generate wrapper classes at this time. ONLY produce the `SpoutNative.cs` interop declarations class. Use `SpoutInterop` as the class namespace. Enums, structs and similar entities should be declared within that namespace, in the `SpoutNative.cs` file but NOT part of the SpoutNative class itself. 

The following is of MAXIMUM importance: Grok will produce full, complete C# source code files. DO NOT provide partial examples. DO NOT assume the user will complete anything. DO NOT create any TODO items. The entire purpose of this request is for Grok to write ALL of the code. The C# file must match 100% of the documented functions of the native DLL for Spout, SpoutSender, SpoutReceiver, and SpoutUtils, and should not contain anything that is not necessary to invoking those specific functions.


Prompt #2:
-------------------------------------------------------------
Using the `SpoutNative.cs` declarations you just generated, and also referencing the native DLL's public functions described in SpoutDocs1.html.txt and SpoutDocs2.html.txt provided earlier, generate C# .NET 8 wrapper classes `SpoutSender.cs`, `SpoutReceiver.cs` and `SpoutUtils.cs` matching the native DLL classes of the same names. Use `SpoutInterop` as the class namespace.

The following is of MAXIMUM importance: Grok will produce full, complete C# source code files. DO NOT provide partial examples. DO NOT assume the user will complete anything. DO NOT create any TODO items. The entire purpose of this request is for Grok to write ALL of the code. The C# wrapper files must match 100% of the documented functions of the native DLL for SpoutSender, SpoutReceiver, and SpoutUtils, and should not contain anything that is not necessary to invoking those specific functions.

DO NOT reference external sites. Only use the materials generated or provided previously. Existing implementations on the web are out of date.


Prompt #3:
-------------------------------------------------------------
(So far, there is no third prompt... put everything into VS and see what breaks.)
