copy /y ..\..\ModularMath\bin\Debug\netstandard1.3\ModularMath.dll .\ModularMath.dll
csc /out:ConsoleTest.exe /r:ModularMath.dll ..\Program.cs