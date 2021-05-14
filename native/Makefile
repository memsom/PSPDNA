
TARGET = dna
OBJS = dna.o callback.o	CLIFile.o Delegate.o Finalizer.o Generics.o Heap.o InternalCall.o \
       JIT.o JIT_Execute.o MetaData.o MetaData_Fill.o MetaData_Search.o MethodState.o \
	   RVA.o Sys.o Thread.o Type.o System.Array.o System.Char.o \
	   System.DateTime.o System.Diagnostics.Debugger.o System.Environment.o System.Enum.o \
	   System.GC.o System.IO.FileInternal.o System.Math.o System.Net.Dns.o \
	   System.Net.Sockets.Socket.o System.Object.o System.Runtime.CompilerServices.RuntimeHelpers.o \
	   System.RuntimeType.o System.String.o System.Type.o System.Threading.Interlocked.o \
	   System.Threading.Monitor.o System.Threading.Thread.o System.ValueType.o System.WeakReference.o \
	   System.Console.o diet-glob.o diet-fnmatch.o controls.o Psp.Debug.o Psp.Controls.o \
	   Psp.Display.o Psp.Kernel.o graph.o Psp.BasicGraphics.o Psp.BasicGraphics2.o

# Pinvoke.o 

# #CFLAGS= -O0 -g -I. -Wall -Werror 
# #-mips2 -mcpu=r3900
# #CFLAGS= -Os -g -I. -Wall -Werror -mips2 -mcpu=r3900
# CFLAGS= -Os -I. -Wall -Werror

# LDFLAGS+=--no-undefined
# LDADD+=-s
# LDADD+=-lm

CFLAGS = -Os -I. -g -G3 #-Wall -Werror 
CXXFLAGS = $(CFLAGS) -std=c++14 -fno-rtti  #-fn0-exception
ASFLAGS = $(CFLAGS)

#PSP specific flags
BUILD_PRX = 1
PSP_FW_VERSION = 500 #371
PSP_LARGE_MEMORY = 1

EXTRA_TARGETS = EBOOT.PBP
PSP_EBOOT_TITLE = DotNet Anywhere
PSP_EBOOT_ICON = ICON0.PNG

PSPSDK=$(shell psp-config --pspsdk-path)

LIBS += -lSDL2 -lGL -lGLU -lglut -lz -lpspvfpu -lpsphprm -lpspsdk -lpspctrl -lpspumd -lpsprtc \
       -lpsppower -lpspgum -lpspgu -lpspaudiolib -lpspaudio -lpsphttp -lpspssl -lpspwlan \
	   -lpspnet_adhocmatching -lpspnet_adhoc -lpspnet_adhocctl -lm -lpspvram

include $(PSPSDK)/lib/build.mak


