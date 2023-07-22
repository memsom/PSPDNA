CLI=dotnet
CLIFLAGS=build
CC=csc
LIBS=native/corlib.dll
CCFLAGS=-nostdlib -target:Exe -unsafe -langversion:8.0 -nullable:disable -deterministic -optimize- -nologo -noconfig

all: tet appmenu simple

corelib: fonts res
	${CLI} ${CLIFLAGS} corlib/corlib.csproj
	cp -R native/corlib.dll native/apps/

fonts:
	cp -R native/fonts native/apps/

res: simpleres flappyres 

tet: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/apps/tet.exe tet/Tet.cs

tetapp: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe tet/Tet.cs

appmenu: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe appmenu/Program.cs

simple: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/apps/simple.exe testSimple/Program.cs

simpleapp: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe testSimple/Program.cs

simpleres:
	cp -R testSimple/res native

flappy: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/apps/flappy.exe flappy/Program.cs

flappyapp: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe flappy/Program.cs

flappyres:
	cp -R flappy/res native

run:
	rm -rf ./native/log.bak
	mv ./native/log.txt ./native/log.bak
	/Applications/PPSSPPSDL.app/Contents/MacOS/PPSSPPSDL ./native/EBOOT.PBP

native:
	make -C native/
	
.PHONY: clean

clean:
	${CLI} clean corlib/corlib.csproj
	rm -rf ./native/Dna.AppMenu.*
	rm -rf ./native/corlib.*
	rm -rf ./native/apps/*.*
	rm -rf ./appmenu/obj
	rm -rf ./tet/obj
	rm -rf ./testSimple/obj
	cd native 
	make -C native/ clean