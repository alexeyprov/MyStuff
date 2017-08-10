/* this file contains the actual definitions of */
/* the IIDs and CLSIDs */

/* link this file in with the server and any clients */


/* File created by MIDL compiler version 5.01.0164 */
/* at Mon Sep 29 17:17:17 2003
 */
/* Compiler settings for C:\Documents and Settings\zzheng\Desktop\plus\plus.idl:
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

const IID IID_ISum = {0x17137C98,0xD54C,0x4C76,{0xAD,0xF6,0x74,0x91,0xD0,0x37,0xF0,0x86}};


const IID LIBID_PLUSLib = {0xC08CD14F,0x7E94,0x4392,{0x98,0x3A,0x53,0xDC,0xEA,0x2C,0xF2,0x2B}};


const CLSID CLSID_Sum = {0xC910B122,0xA6E9,0x498B,{0xA6,0x62,0x1E,0xA7,0xD2,0x6A,0xF4,0xE2}};


#ifdef __cplusplus
}
#endif

