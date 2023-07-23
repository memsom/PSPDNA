# PSP DNA - a DotNet runtime for Sony PSP

## What is PSPDNA

This is the Dot Net Anywhere interpreter ported to the PSP as well as a few basic demos I got running. 

## What is DNA?

[Dotnet Anywhere](https://github.com/chrisdunelm/DotNetAnywhere) is a DotNet runtime that was written by [Chris Bacon](https://github.com/chrisdunelm). It was used as a basis of the initial version of Blazor. This source base is based on my changes [here](https://github.com/memsom/DNA), which attempted to consolidate the Blazor changes along with some other changes made elsewhere on GitHub forks of Chris's original repo. The code is reasonably complete, but is by no means a full blooded DotNet runtime. This version will use DotNet Core compiled assemblies at least, and as such can be used with fairly modern compilers.

## What is does...

You can compile fairly non trivial C# code and run it with quite a lot of success on the PSP (mine is a 3000.) 

To compile the C source, I use a copy of the [Minimalist PSP SDK](https://github.com/pmlopes/minpsp) for Windows, and under MacOS I have a custom built GCC tool chain [based on this repo.](https://github.com/pspdev/psptoolchain) (although it does not entirely cleanly compile under Catalina.)

I'm not going to claim it was a lot of work - it wasn't really too hard. But it was the first decent bit of PSP programming I've done, so it has been pretty rewarding to see it function.

I'd be happy to get contributions. It is what it is, and so it is not intended to be "release quality".

I've been using VS2019 Mac to develop, and VSCode to compile the C/C++. I have a template that sets up all the code completion for VSCode for both Windows and Mac, so that might help some of you guys. The code will compile under Windows too, but I use Windows all day at work and so wanted to do something a bit different at home.

The `corelib` is custom. There is a PSP root namespace for PSP specific stuff. You can shell out to other assemblies, but as per PSP rules, you'd need to build all native code in to the runtime. I currently include SDL2 (NB. it is wrapped by the API, SDL2 is not available directly) as it was easier to get the graphics working on. I have a standard "square moving around on a background" demo. I also ported "tet" from the TinyC games repo as it was pretty simple and quite easy to port.

The graphics primitives are under PSP namespace and are all pretty simple at the moment. I had a go at making them use pure PSP SDK API calls, but ended up using SDL2 as the performance was pretty bad. I'll revisit that at some point.

## I give no warranty!! Use at your own risk.
This is still experimental and I haven't had much time to look at it since late 2020/early 2021, so it is still at an early stage of development.

# Issues with modern DotNet tooling

Dotnet has moved on since I got this originally working. As such the process of building has slightly changed. The tooling changes completely broke the master branch. I have managed to get it to the point where I can build it again - that much works. But we don't seem to be able to use the old "net40" target anymore (I believe it was removed fromt he compilers) and so we are stuck with making more modern assemblies. Unfortunately this then breaks Visual Studio completely (at least under macOS). It really can't handle building the code anymore - it basically can't seem to actually handle the options I had to add to make it more or less work from the command line. The issues mostly revolve around modern tooling not honouring the `<NoStdLib>` tag in the _.csproj_ files. I did kind of get that working under VS Mac 2022, but the comiler then refused to link to the `corlib.dll`. I might have fixed this later on by throwing more settings at the projects, but VS Mac just became too upredictable and VSCode is now perfectly adequate.

So - rather than struggle - I am now using VSCode for all aspects of the project (as the C was always being done in VSCode with a Makefile.) I have written a Makefile that will build the C# projects - this makes it simple to use VSCode (as we have no debug anyway, so it will never be a "thing" till the VM supports soft debugging somehow.) Just run "make all" to buld all of the DotNet code, the Makefile for the native is still separate and lives in the same place. If you are under Windows, you might get VS2022 to work - I guess. But given we don;t use anything from VS2022 to make this code, it probably is overkill anyway.

We did have other build issues - as it turned out the PSPSDK I moved from my old laptop was actually the wrong one. I have now got a working SDK (that I built for macOS 64bit, works with Catalina, also tested under Ventura.) I will probably upload that SDK as an asset, but you should probably use the latest version fromt he main PSPSDK repo (build the compiler if there isn't a prebuilt one.) The version that didn't work had a non working SDL and so the Tet app ran, but to a black screen, and the menu was glitchy. If you see this, your issue will be the same as mine.

One note - I had to install the last version of the dotnet SDK's for core 3.1 and version 6 to make the compiler gods happy - you might also want to do this. It might just work. I think 6.0.X should work.

When I get a version that works fully again I will do a release.
