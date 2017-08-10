

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 6.00.0361 */
/* at Thu Mar 01 13:33:34 2007
 */
/* Compiler settings for .\KillMe.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#if !defined(_M_IA64) && !defined(_M_AMD64)


#pragma warning( disable: 4049 )  /* more than 64k source lines */


#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, IID_IFoo,0x7BEC3033,0xE07A,0x4C02,0xA5,0xB6,0xD7,0xF2,0x71,0x7F,0x13,0xE6);


MIDL_DEFINE_GUID(IID, IID_IFooListener,0x3050A51D,0x8FEA,0x4875,0xB8,0x2C,0x4F,0x9B,0x1B,0x42,0x70,0x34);


MIDL_DEFINE_GUID(IID, IID_IFooSpeaker,0x29EA7B6E,0xDF4E,0x4D97,0x8C,0x41,0xF7,0x11,0x05,0xF4,0x7B,0x0B);


MIDL_DEFINE_GUID(IID, LIBID_KillMeLib,0x00422B18,0xF116,0x40A9,0xB8,0xB9,0xF0,0x49,0x7D,0x81,0xD5,0x41);


MIDL_DEFINE_GUID(CLSID, CLSID_FooListener,0xAF9BAA44,0x70E4,0x43C0,0x95,0xC8,0x4E,0xA4,0xC4,0xFE,0x8F,0x67);


MIDL_DEFINE_GUID(CLSID, CLSID_FooSpeaker,0xC59F44B2,0x7B32,0x4AD7,0x95,0x40,0xEB,0xC9,0xC4,0xBE,0x19,0x74);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



#endif /* !defined(_M_IA64) && !defined(_M_AMD64)*/

