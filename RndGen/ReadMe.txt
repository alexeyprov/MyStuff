========================================================================
    ACTIVE TEMPLATE LIBRARY : RndGen Project Overview
========================================================================

AppWizard has created this RndGen project for you to use as the starting point for
writing your Executable (EXE).

This file contains a summary of what you will find in each of the files that
make up your project.

RndGen.vcproj
    This is the main project file for VC++ projects generated using an Application Wizard. 
    It contains information about the version of Visual C++ that generated the file, and 
    information about the platforms, configurations, and project features selected with the
    Application Wizard.

RndGen.idl
    This file contains the IDL definitions of the type library, the interfaces
    and co-classes defined in your project.
    This file will be processed by the MIDL compiler to generate:
        C++ interface definitions and GUID declarations (RndGen.h)
        GUID definitions                                (RndGen_i.c)
        A type library                                  (RndGen.tlb)
        Marshaling code                                 (RndGen_p.c and dlldata.c)

RndGen.h
    This file contains the C++ interface definitions and GUID declarations of the
    items defined in RndGen.idl. It will be regenerated by MIDL during compilation.
RndGen.cpp
    This file contains the object map and the implementation of WinMain.
RndGen.rc
    This is a listing of all of the Microsoft Windows resources that the
    program uses.


/////////////////////////////////////////////////////////////////////////////
Other standard files:

StdAfx.h, StdAfx.cpp
    These files are used to build a precompiled header (PCH) file
    named RndGen.pch and a precompiled types file named StdAfx.obj.

Resource.h
    This is the standard header file that defines resource IDs.

/////////////////////////////////////////////////////////////////////////////
Proxy/stub DLL project and module definition file:

RndGenps.vcproj
    This file is the project file for building a proxy/stub DLL if necessary.
	The IDL file in the main project must contain at least one interface and you must 
	first compile the IDL file before building the proxy/stub DLL.	This process generates
	dlldata.c, RndGen_i.c and RndGen_p.c which are required
	to build the proxy/stub DLL.

RndGenps.def
    This module definition file provides the linker with information about the exports
    required by the proxy/stub.
/////////////////////////////////////////////////////////////////////////////
