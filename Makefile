# This is a monlithic make file that will build all aspects of the distro

# this is where we put the built app and runtime.
BUILD_DIR = build

# dot net stuff
DOTNET = dotnet
DOTNETFLAGS = build
CSC = csc
CSCLIBS = $(BUILD_DIR)/corlib.dll
CSCFLAGS = -nostdlib -target:Exe -unsafe -langversion:7.3 -nullable:disable -deterministic -optimize- -nologo -noconfig

#psp stuff
TARGET = dna
OBJS = \
    native/dna.o native/callback.o	native/CLIFile.o native/Delegate.o native/Finalizer.o  \
    native/Generics.o native/Heap.o native/InternalCall.o native/JIT.o native/JIT_Execute.o \
    native/MetaData.o native/MetaData_Fill.o native/MetaData_Search.o native/MethodState.o \
	native/RVA.o native/Sys.o native/Thread.o native/Type.o native/System.Array.o \
	native/System.Char.o native/System.DateTime.o native/System.Diagnostics.Debugger.o \
	native/System.Environment.o native/System.Enum.o native/System.GC.o \
	native/System.IO.FileInternal.o native/System.Math.o native/System.Net.Dns.o \
	native/System.Net.Sockets.Socket.o native/System.Object.o \
	native/System.Runtime.CompilerServices.RuntimeHelpers.o native/System.RuntimeType.o  \
	native/System.String.o native/System.Type.o native/System.Threading.Interlocked.o \
	native/System.Threading.Monitor.o native/System.Threading.Thread.o native/System.ValueType.o \
	native/System.WeakReference.o native/System.Console.o native/diet-glob.o native/diet-fnmatch.o \
	native/controls.o native/Psp.Debug.o native/Psp.Controls.o  native/Psp.Display.o native/Psp.Kernel.o \
	native/graph.o native/Psp.BasicGraphics.o native/Psp.BasicGraphics2.o 

CFLAGS = -Os -I. -g -G3 -D__PSP__ #-Wall -Werror 
CXXFLAGS = $(CFLAGS) -std=c++14 -fno-rtti  #-fn0-exception
ASFLAGS = $(CFLAGS)

INCDIR   := $(INCDIR) . $(PSPSDK)/include
LIBDIR   := $(LIBDIR) . $(PSPSDK)/lib

BUILD_PRX = 1
PSP_FW_VERSION = 500 #371
PSP_LARGE_MEMORY = 1

EXTRA_TARGETS = EBOOT.PBP
PSP_EBOOT_TITLE = DotNet Anywhere
PSP_EBOOT_ICON = native/ICON0.PNG

PSPSDK=$(shell psp-config --pspsdk-path)

LIBS += -lSDL2 -lSDL2_ttf -lfreetype -lGL -lGLU -lglut -lz -lpspvfpu -lpsphprm -lpspsdk -lpspctrl -lpspumd -lpsprtc \
       -lpsppower -lpspgum -lpspgu -lpspaudiolib -lpspaudio -lpsphttp -lpspssl -lpspwlan \
	   -lpspnet_adhocmatching -lpspnet_adhoc -lpspnet_adhocctl -lm -lpspvram

# PSP boiler plate config

include pspbuild.mak

# the main build engine:

all: managed native

managed: appmenu simple tet flappy

corelib: fonts res
	${DOTNET} ${DOTNETFLAGS} corlib/corlib.csproj
	mkdir -p $(BUILD_DIR)/apps

fonts:
	cp -R native/fonts $(BUILD_DIR)

res: simpleres flappyres rockbound2res

tet: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/apps/tet.exe tet/Tet.cs

tetapp: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/Dna.AppMenu.exe tet/Tet.cs

appmenu: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/Dna.AppMenu.exe appmenu/Program.cs

simple: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/apps/simple.exe testSimple/Program.cs

simpleapp: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/Dna.AppMenu.exe testSimple/Program.cs

simpleres:
	cp -R testSimple/res $(BUILD_DIR)

flappy: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/apps/flappy.exe flappy/Program.cs

flappyapp: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/Dna.AppMenu.exe flappy/Program.cs

flappyres:
	cp -R flappy/res $(BUILD_DIR)

rockbound2app: corelib
	${CSC} ${CSCFLAGS} -reference:${CSCLIBS} -out:$(BUILD_DIR)/Dna.AppMenu.exe rockbound2/Program.cs rockbound2/ImageResources.cs

rockbound2res:
	cp -p -R rockbound2/res $(BUILD_DIR)
run:
	rm -rf $(BUILD_DIR)/log.bak
	[ ! -f $(BUILD_DIR)/log.txt ] || mv $(BUILD_DIR)/log.txt $(BUILD_DIR)/log.bak
	/Applications/PPSSPPSDL.app/Contents/MacOS/PPSSPPSDL ./$(BUILD_DIR)/EBOOT.PBP

native: $(EXTRA_TARGETS) $(FINAL_TARGET)
	mv EBOOT.PBP $(BUILD_DIR)/
	mv PARAM.SFO $(BUILD_DIR)/
	mv dna.elf $(BUILD_DIR)/
	mv dna.prx $(BUILD_DIR)/

cleandna:
	${DOTNET} clean corlib/corlib.csproj
	rm -rf ./$(BUILD_DIR)/Dna.AppMenu.*
	rm -rf ./$(BUILD_DIR)/*.*
	rm -rf ./$(BUILD_DIR)/apps/*.*
	rm -rf ./appmenu/obj
	rm -rf ./tet/obj
	rm -rf ./testSimple/obj
	rm -rf ./flappy/obj
	rm -rf EBOOT.PBP 
	rm -rf PARAM.SFO 
	rm -rf dna.elf 
	rm -rf dna.prx 
	
.PHONY: clean

clean: cleannative cleandna

rebuild: clean all