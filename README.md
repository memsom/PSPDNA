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
