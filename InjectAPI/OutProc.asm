;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;
;		Copyright (C) 1999, CAT Computing Center (R).   
;
;		Project:		Ijector
;		Description:	Ijectored proc
;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
		.386p
		.model	flat
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
public	_GetOutAddress
public	_GetProcLen
public	_GetOutAddress2
public	_GetProcLen2
public	_GetOutAddress3
public	_GetProcLen3
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
		.code    
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
_GetOutAddress	proc
		mov		eax,OutProc
		ret
_GetOutAddress	endp
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
_GetProcLen	proc
		mov		eax,ProcLen
		ret
_GetProcLen	endp
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
_GetOutAddress2	proc
		mov		eax,OutProc2
		ret
_GetOutAddress2	endp
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
_GetProcLen2	proc
		mov		eax,ProcLen2
		ret
_GetProcLen2	endp
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
_GetOutAddress3	proc
		mov		eax,OutProc3
		ret
_GetOutAddress3	endp
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
_GetProcLen3	proc
		mov		eax,ProcLen3
		ret
_GetProcLen3	endp
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
OutProc:
		pop		eax
		push	eax
		mov		edx,eax
		add		edx,10h				;INJDATA::szDLLName
		push	edx
		call	dword ptr [eax]		;LoadLibrary
		test	eax,eax
		jz		@Fail

		pop		edx
		push	edx
		mov		ecx,edx
		add		ecx,08h				;INJDATA::bUnload
		mov		ebx,[ecx]
		test	ebx,[ecx]
		jz		@OK
		
		add		edx,04h				;INJDATA::dwFreeProc
		push	eax
		call	dword ptr [edx]		;FreeLibrary

@OK:
		mov		eax,1
		jmp		@Exit
@Fail:
		mov		eax,0ffffffffh
		jmp		@Exit
@Exit:
		pop		edx
		add		edx,0ch				;INJDATA::dwCompleted
		mov		dword ptr [edx],eax
@Loop:	jmp		@Loop

		int		3h
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
ProcLen	= $ - OutProc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
OutProc2:
		push	ebp
		mov		ebp,esp
		mov		eax,[ebp+8]
		mov		edx,eax
		add		edx,10h				;INJDATA::szDLLName
		push	edx
		call	dword ptr [eax]		;LoadLibrary
		test	eax,eax
		jz		@Fail2

		mov		edx,[ebp+8]
		mov		ecx,edx
		add		ecx,08h				;INJDATA::bUnload
		mov		ebx,[ecx]
		test	ebx,[ecx]
		jz		@OK2
		
		add		edx,04h				;INJDATA::dwFreeProc
		push	eax
		call	dword ptr [edx]		;FreeLibrary

@OK2:
		mov		eax,1
		jmp		@Exit2
@Fail2:
		mov		eax,0ffffffffh
		jmp		@Exit2
@Exit2:
		mov		esp,ebp
		pop		ebp
		ret		4

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
ProcLen2	= $ - OutProc2
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
OutProc3:
		push	ebp
		mov		ebp,esp
		mov		eax,[ebp+8]
		mov		edx,eax
		add		edx,10h				;INJDATA::szDLLName
		push	edx
		call	dword ptr [eax]		;GetModuleHandle
		test	eax,eax
		jz		@Fail3

		mov		edx,[ebp+8]
		add		edx,04h				;INJDATA::dwFreeProc
		push	eax
		call	dword ptr [edx]		;FreeLibrary
		test	eax,eax
		jz		@Fail3

		mov		eax,1
		jmp		@Exit3
@Fail3:
		mov		eax,0ffffffffh
		jmp		@Exit3
@Exit3:
		mov		esp,ebp
		pop		ebp
		ret		4

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
ProcLen3	= $ - OutProc3
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
		end
