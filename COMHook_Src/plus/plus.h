/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Mon Sep 29 17:17:17 2003
 */
/* Compiler settings for C:\Documents and Settings\zzheng\Desktop\plus\plus.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __plus_h__
#define __plus_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ISum_FWD_DEFINED__
#define __ISum_FWD_DEFINED__
typedef interface ISum ISum;
#endif 	/* __ISum_FWD_DEFINED__ */


#ifndef __Sum_FWD_DEFINED__
#define __Sum_FWD_DEFINED__

#ifdef __cplusplus
typedef class Sum Sum;
#else
typedef struct Sum Sum;
#endif /* __cplusplus */

#endif 	/* __Sum_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ISum_INTERFACE_DEFINED__
#define __ISum_INTERFACE_DEFINED__

/* interface ISum */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISum;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("17137C98-D54C-4C76-ADF6-7491D037F086")
    ISum : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE method1( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE method2( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE woo( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISumVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ISum __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ISum __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ISum __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *method1 )( 
            ISum __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *method2 )( 
            ISum __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *woo )( 
            ISum __RPC_FAR * This);
        
        END_INTERFACE
    } ISumVtbl;

    interface ISum
    {
        CONST_VTBL struct ISumVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISum_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISum_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISum_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISum_method1(This)	\
    (This)->lpVtbl -> method1(This)

#define ISum_method2(This)	\
    (This)->lpVtbl -> method2(This)

#define ISum_woo(This)	\
    (This)->lpVtbl -> woo(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE ISum_method1_Proxy( 
    ISum __RPC_FAR * This);


void __RPC_STUB ISum_method1_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE ISum_method2_Proxy( 
    ISum __RPC_FAR * This);


void __RPC_STUB ISum_method2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE ISum_woo_Proxy( 
    ISum __RPC_FAR * This);


void __RPC_STUB ISum_woo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISum_INTERFACE_DEFINED__ */



#ifndef __PLUSLib_LIBRARY_DEFINED__
#define __PLUSLib_LIBRARY_DEFINED__

/* library PLUSLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_PLUSLib;

EXTERN_C const CLSID CLSID_Sum;

#ifdef __cplusplus

class DECLSPEC_UUID("C910B122-A6E9-498B-A662-1EA7D26AF4E2")
Sum;
#endif
#endif /* __PLUSLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
