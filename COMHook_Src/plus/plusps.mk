
plusps.dll: dlldata.obj plus_p.obj plus_i.obj
	link /dll /out:plusps.dll /def:plusps.def /entry:DllMain dlldata.obj plus_p.obj plus_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del plusps.dll
	@del plusps.lib
	@del plusps.exp
	@del dlldata.obj
	@del plus_p.obj
	@del plus_i.obj
