

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0361 */
/* at Thu Feb 19 15:03:27 2004
 */
/* Compiler settings for .\RndGen.idl:
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

#ifndef __RndGen_h__
#define __RndGen_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IGenerator_FWD_DEFINED__
#define __IGenerator_FWD_DEFINED__
typedef interface IGenerator IGenerator;
#endif 	/* __IGenerator_FWD_DEFINED__ */


#ifndef __Generator_FWD_DEFINED__
#define __Generator_FWD_DEFINED__

#ifdef __cplusplus
typedef class Generator Generator;
#else
typedef struct Generator Generator;
#endif /* __cplusplus */

#endif 	/* __Generator_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

#ifndef __IGenerator_INTERFACE_DEFINED__
#define __IGenerator_INTERFACE_DEFINED__

/* interface IGenerator */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IGenerator;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3CE9F3CD-E854-462B-AAD4-A131042146A0")
    IGenerator : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Seed( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Seed( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE NextRandom( 
            /* [in] */ LONG nMinValue,
            /* [in] */ LONG nMaxValue,
            /* [retval][out] */ LONG *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IGeneratorVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IGenerator * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IGenerator * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IGenerator * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IGenerator * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IGenerator * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IGenerator * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IGenerator * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Seed )( 
            IGenerator * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Seed )( 
            IGenerator * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *NextRandom )( 
            IGenerator * This,
            /* [in] */ LONG nMinValue,
            /* [in] */ LONG nMaxValue,
            /* [retval][out] */ LONG *pVal);
        
        END_INTERFACE
    } IGeneratorVtbl;

    interface IGenerator
    {
        CONST_VTBL struct IGeneratorVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IGenerator_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IGenerator_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IGenerator_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IGenerator_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IGenerator_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IGenerator_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IGenerator_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IGenerator_get_Seed(This,pVal)	\
    (This)->lpVtbl -> get_Seed(This,pVal)

#define IGenerator_put_Seed(This,newVal)	\
    (This)->lpVtbl -> put_Seed(This,newVal)

#define IGenerator_NextRandom(This,nMinValue,nMaxValue,pVal)	\
    (This)->lpVtbl -> NextRandom(This,nMinValue,nMaxValue,pVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IGenerator_get_Seed_Proxy( 
    IGenerator * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IGenerator_get_Seed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IGenerator_put_Seed_Proxy( 
    IGenerator * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IGenerator_put_Seed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IGenerator_NextRandom_Proxy( 
    IGenerator * This,
    /* [in] */ LONG nMinValue,
    /* [in] */ LONG nMaxValue,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IGenerator_NextRandom_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IGenerator_INTERFACE_DEFINED__ */



#ifndef __RndGenLib_LIBRARY_DEFINED__
#define __RndGenLib_LIBRARY_DEFINED__

/* library RndGenLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_RndGenLib;

EXTERN_C const CLSID CLSID_Generator;

#ifdef __cplusplus

class DECLSPEC_UUID("66EF5506-AB8A-4DFE-A0B2-1485844B473E")
Generator;
#endif
#endif /* __RndGenLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


