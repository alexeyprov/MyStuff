var interpreter=new ActiveXObject("Mi.JScript.Interpreter");
if( interpreter.RunManaged() )
    managed();


function managed()
{
    try
    {

    setLog("makeout.log");
    writeln(
        "makeout (source code extract) "+
        DateTime.Now.ToLongDateString()+" "+
        DateTime.Now.ToLongTimeString() );

    showCopyFile("WebBrowserControl.sln");

    showCopyFile("Выб Браузер.doc");

    showCopyFile("WebBrowserControl.csproj");
    showCopyFile("AssemblyInfo.cs");
    showCopyFile("WebBrowserControl.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\IWebBrowser2.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\DWebBrowserEvents2.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\ReadyState.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\CommandStateChangeConstants.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\OLECMDEXECOPT.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\OLECMDF.cs");
    showCopyFile("Microsoft.InternetExplorer.Interop\\OLECMDID.cs");

    showCopyFile("GenerateInterfaceDecl\\GenerateInterfaceDecl.csproj");
    showCopyFile("GenerateInterfaceDecl\\AssemblyInfo.cs");
    showCopyFile("GenerateInterfaceDecl\\GenDecl.cs");
    showCopyFile("GenerateInterfaceDecl\\App.ico");

    
    showCopyFile("Sample1\\Sample1.csproj");
    showCopyFile("Sample1\\AssemblyInfo.cs");
    showCopyFile("Sample1\\Form1.cs");
    showCopyFile("Sample1\\Form1.resx");
    showCopyFile("Sample1\\App.ico");


    showCopyFile("CustomFileDialog\\CustomFileDialog.csproj");
    showCopyFile("CustomFileDialog\\AssemblyInfo.cs");
    showCopyFile("CustomFileDialog\\Form1.cs");
    showCopyFile("CustomFileDialog\\Form1.resx");
    showCopyFile("CustomFileDialog\\App.ico");

    showCopyFile("makeout.js");
    showCopyFile("backup.js");

    writeln(
        "source code prepared "+
        DateTime.Now.ToLongDateString()+" "+
        DateTime.Now.ToLongTimeString() );
    writeln();

    copyFile("makeout.log","out\\makeout.log");




    }
    catch( err )
    {
        writeln( err );
    }

    function showCopyFile(path)
    {
        var filename=Path.GetFileName(path);
        var relativePath;
        if( filename.ToLower()==path.ToLower() )
            relativePath="";
        else
        {
            var fullPath=Path.GetFullPath(path);
            var currPath=Path.GetFullPath(".");
            if( fullPath.ToLower().StartsWith( currPath.ToLower() ) )
            {
                relativePath=fullPath.Substring(currPath.Length);
                if( relativePath.StartsWith("\\") )
                {
                    relativePath=relativePath.Substring(1);
                }
                if( relativePath.ToLower().EndsWith(filename.ToLower()) )
                    relativePath=relativePath.Substring(
                        0,
                        relativePath.Length-filename.Length );
                if( relativePath.EndsWith("\\") )
                    relativePath=relativePath.Substring(
                        0,
                        relativePath.Length-1 );
            }
            else
            {
                relativePath=null;
            }
        }

        var targetFolder=Path.Combine("out",relativePath);
        var targetPath=Path.Combine(
            targetFolder,
            Path.GetFileName(path) );


        if( relativePath!=null
            && relativePath!=""
            && !Directory.Exists(targetFolder) )
        {
            Directory.CreateDirectory(targetFolder);
        }

        write(" "+filename+"...");
        copyFile( path, targetPath );

        var fileTime=File.GetCreationTime(path);
        if( File.GetLastWriteTime(path)>fileTime )
            fileTime=File.GetLastWriteTime(path);

        var thisStr=" "+filename+"...";
        write( "".PadLeft(40-thisStr.Length) );

        writeln(" O.K. "+fileTime.ToShortDateString()+" "+fileTime.ToLongTimeString());
    }

}

