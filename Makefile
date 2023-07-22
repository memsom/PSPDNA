CLI=dotnet
CLIFLAGS=build
CC=csc
LIBS=native/corlib.dll
CCFLAGS=-nostdlib -target:Exe 

all: tet appmenu simple

corelib: 
	${CLI} ${CLIFLAGS} corlib/corlib.csproj

tet: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/apps/tet.exe tet/Tet.cs

appmenu: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe appmenu/Program.cs

simple: corelib simpleres
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/apps/simple.exe testSimple/Program.cs

simpleapp: corelib simpleres
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe testSimple/Program.cs

simpleres:
	cp -R testSimple/res native

flappy: corelib flappyres
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/apps/flappy.exe flappy/Program.cs

flappyapp: corelib flappyres
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe flappy/Program.cs

flappyres:
	cp -R flappy/res native

tetapp: corelib
	${CC} ${CCFLAGS} -reference:${LIBS} -out:native/Dna.AppMenu.exe tet/Tet.cs

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