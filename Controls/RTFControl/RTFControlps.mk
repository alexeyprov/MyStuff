
RTFControlps.dll: dlldata.obj RTFControl_p.obj RTFControl_i.obj
	link /dll /out:RTFControlps.dll /def:RTFControlps.def /entry:DllMain dlldata.obj RTFControl_p.obj RTFControl_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del RTFControlps.dll
	@del RTFControlps.lib
	@del RTFControlps.exp
	@del dlldata.obj
	@del RTFControl_p.obj
	@del RTFControl_i.obj
