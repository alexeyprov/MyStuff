

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


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

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
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

#ifndef __KillMe_h__
#define __KillMe_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IFoo_FWD_DEFINED__
#define __IFoo_FWD_DEFINED__
typedef interface IFoo IFoo;
#endif 	/* __IFoo_FWD_DEFINED__ */


#ifndef __IFooListener_FWD_DEFINED__
#define __IFooListener_FWD_DEFINED__
typedef interface IFooListener IFooListener;
#endif 	/* __IFooListener_FWD_DEFINED__ */


#ifndef __IFooSpeaker_FWD_DEFINED__
#define __IFooSpeaker_FWD_DEFINED__
typedef interface IFooSpeaker IFooSpeaker;
#endif 	/* __IFooSpeaker_FWD_DEFINED__ */


#ifndef __FooListener_FWD_DEFINED__
#define __FooListener_FWD_DEFINED__

#ifdef __cplusplus
typedef class FooListener FooListener;
#else
typedef struct FooListener FooListener;
#endif /* __cplusplus */

#endif 	/* __FooListener_FWD_DEFINED__ */


#ifndef __FooSpeaker_FWD_DEFINED__
#define __FooSpeaker_FWD_DEFINED__

#ifdef __cplusplus
typedef class FooSpeaker FooSpeaker;
#else
typedef struct FooSpeaker FooSpeaker;
#endif /* __cplusplus */

#endif 	/* __FooSpeaker_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

#ifndef __IFoo_INTERFACE_DEFINED__
#define __IFoo_INTERFACE_DEFINED__

/* interface IFoo */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IFoo;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("7BEC3033-E07A-4C02-A5B6-D7F2717F13E6")
    IFoo : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IFooVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFoo * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFoo * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFoo * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFoo * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFoo * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFoo * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFoo * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Name )( 
            IFoo * This,
            /* [retval][out] */ BSTR *pVal);
        
        END_INTERFACE
    } IFooVtbl;

    interface IFoo
    {
        CONST_VTBL struct IFooVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFoo_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IFoo_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IFoo_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IFoo_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IFoo_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IFoo_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IFoo_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IFoo_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IFoo_get_Name_Proxy( 
    IFoo * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IFoo_get_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IFoo_INTERFACE_DEFINED__ */


#ifndef __IFooListener_INTERFACE_DEFINED__
#define __IFooListener_INTERFACE_DEFINED__

/* interface IFooListener */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IFooListener;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3050A51D-8FEA-4875-B82C-4F9B1B427034")
    IFooListener : public IFoo
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Listen( 
            /* [in] */ BSTR data) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IFooListenerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFooListener * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFooListener * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFooListener * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFooListener * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFooListener * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFooListener * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFooListener * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Name )( 
            IFooListener * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Listen )( 
            IFooListener * This,
            /* [in] */ BSTR data);
        
        END_INTERFACE
    } IFooListenerVtbl;

    interface IFooListener
    {
        CONST_VTBL struct IFooListenerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFooListener_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IFooListener_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IFooListener_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IFooListener_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IFooListener_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IFooListener_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IFooListener_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IFooListener_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)


#define IFooListener_Listen(This,data)	\
    (This)->lpVtbl -> Listen(This,data)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IFooListener_Listen_Proxy( 
    IFooListener * This,
    /* [in] */ BSTR data);


void __RPC_STUB IFooListener_Listen_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IFooListener_INTERFACE_DEFINED__ */


#ifndef __IFooSpeaker_INTERFACE_DEFINED__
#define __IFooSpeaker_INTERFACE_DEFINED__

/* interface IFooSpeaker */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IFooSpeaker;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("29EA7B6E-DF4E-4D97-8C41-F71105F47B0B")
    IFooSpeaker : public IFoo
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Speak( 
            /* [retval][out] */ BSTR *ret) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IFooSpeakerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFooSpeaker * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFooSpeaker * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFooSpeaker * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFooSpeaker * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFooSpeaker * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFooSpeaker * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFooSpeaker * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Name )( 
            IFooSpeaker * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Speak )( 
            IFooSpeaker * This,
            /* [retval][out] */ BSTR *ret);
        
        END_INTERFACE
    } IFooSpeakerVtbl;

    interface IFooSpeaker
    {
        CONST_VTBL struct IFooSpeakerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFooSpeaker_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IFooSpeaker_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IFooSpeaker_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IFooSpeaker_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IFooSpeaker_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IFooSpeaker_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IFooSpeaker_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IFooSpeaker_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)


#define IFooSpeaker_Speak(This,ret)	\
    (This)->lpVtbl -> Speak(This,ret)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IFooSpeaker_Speak_Proxy( 
    IFooSpeaker * This,
    /* [retval][out] */ BSTR *ret);


void __RPC_STUB IFooSpeaker_Speak_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IFooSpeaker_INTERFACE_DEFINED__ */



#ifndef __KillMeLib_LIBRARY_DEFINED__
#define __KillMeLib_LIBRARY_DEFINED__

/* library KillMeLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_KillMeLib;

EXTERN_C const CLSID CLSID_FooListener;

#ifdef __cplusplus

class DECLSPEC_UUID("AF9BAA44-70E4-43C0-95C8-4EA4C4FE8F67")
FooListener;
#endif

EXTERN_C const CLSID CLSID_FooSpeaker;

#ifdef __cplusplus

class DECLSPEC_UUID("C59F44B2-7B32-4AD7-9540-EBC9C4BE1974")
FooSpeaker;
#endif
#endif /* __KillMeLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


