
TARGET = dna
OBJS = CLIFile.o Delegate.o Finalizer.o Generics.o Heap.o InternalCall.o \
       JIT.o JIT_Execute.o MetaData.o MetaData_Fill.o MetaData_Search.o MethodState.o \
	   RVA.o Sys.o Thread.o Type.o System.Array.o System.Char.o \
	   System.DateTime.o System.Diagnostics.Debugger.o System.Environment.o System.Enum.o \
	   System.GC.o System.IO.FileInternal.o System.Math.o System.Net.Dns.o \
	   System.Net.Sockets.Socket.o System.Object.o System.Runtime.CompilerServices.RuntimeHelpers.o \
	   System.RuntimeType.o System.String.o System.Type.o System.Threading.Interlocked.o \
	   System.Threading.Monitor.o System.Threading.Thread.o System.ValueType.o System.WeakReference.o \
	   System.Console.o controls.o Psp.Debug.o Psp.Controls.o \
	   Psp.Display.o Psp.Kernel.o graph_sdl.o Psp.BasicGraphics.o Psp.BasicGraphics2.o \
	   callback.o dna.o 

CFLAGS = -Os -I. -g -O0 #-Wall -Werror 
CXXFLAGS = $(CFLAGS) -std=c++14 -fno-rtti  #-fn0-exception
ASFLAGS = $(CFLAGS)

LIBS += -lSDL2 -lSDL2_ttf -lfreetype 
#-lGL -lGLU -lglut -lz -lpspvfpu -lpsphprm -lpspsdk -lpspctrl -lpspumd -lpsprtc \
#       -lpsppower -lpspgum -lpspgu -lpspaudiolib -lpspaudio -lpsphttp -lpspssl -lpspwlan \
#	   -lpspnet_adhocmatching -lpspnet_adhoc -lpspnet_adhocctl -lm -lpspvram

CC = gcc
DEPS =

BUILD_DIR = ../host
APPS_DIR = $(BUILD_DIR)/apps
RES_DIR = $(BUILD_DIR)/res
FONTS_DIR = $(BUILD_DIR)/fonts
PSPBUILD_DIR = ../build

all: setup dna

setup: managed exe resources

managed: 
	mkdir -p $(BUILD_DIR)/
	cp -R $(PSPBUILD_DIR)/*.dll $(BUILD_DIR)
	cp -R $(PSPBUILD_DIR)/*.exe $(BUILD_DIR)

exe:
	mkdir -p $(APPS_DIR)/
	cp -R $(PSPBUILD_DIR)/apps/*.exe $(APPS_DIR)/ 

resources: 
	mkdir -p $(RES_DIR)/
	cp -R ../flappy/res/*.* $(RES_DIR)
	cp -R ../testSimple/res/*.* $(RES_DIR)
	cp -R ../rockbound2/res/*.* $(RES_DIR)
	mkdir -p $(FONTS_DIR)/
	cp -R ../native/fonts/*.* $(FONTS_DIR)/

%.o: %.c 
	$(CC) -c -o $@ $< $(CFLAGS) 

dna: $(OBJS)
	$(CC) -o $(BUILD_DIR)/$@ $^ $(CFLAGS) $(LIBS)

.PHONY: clean

clean:
	rm -f *.o
	rm -rf $(BUILD_DIR)

