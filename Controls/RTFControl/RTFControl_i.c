/* this file contains the actual definitions of */
/* the IIDs and CLSIDs */

/* link this file in with the server and any clients */


/* File created by MIDL compiler version 5.01.0164 */
/* at Wed Dec 29 11:00:07 2004
 */
/* Compiler settings for D:\Projects\MyStuff\RTFControl\RTFControl.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )
#ifdef __cplusplus
extern "C"{
#endif 


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

const IID IID_ITextControl = {0x438CF549,0x7F67,0x45FB,{0x8A,0x9C,0x91,0xEF,0xFA,0x7F,0x5F,0x13}};


const IID LIBID_RTFCONTROLLib = {0xB1B3D823,0x3BFC,0x494F,{0x81,0x87,0x52,0xC0,0x36,0x30,0x3B,0x82}};


const CLSID CLSID_TextControl = {0x462C59B7,0xF7B1,0x4999,{0xAD,0x0F,0x9F,0x4B,0x14,0x55,0xE0,0x9A}};


#ifdef __cplusplus
}
#endif

