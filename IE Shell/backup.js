var interpreter=new ActiveXObject("JScript.Interpreter");
if( interpreter.RunManaged() )
    managed();


function managed()
{

    setLog("backup.log");
    writeln(
        "backup "+
        DateTime.Now.ToLongDateString()+" "+
        DateTime.Now.ToLongTimeString() );
    var arxibName=createArxibName();

    write("RAR "+arxibName+"...");
    runNotWait(
        "rar32.exe a "+arxibName+
        " *.cs *.vb *.csproj *.xml "+
        " *.resx *.resource *.sln *.log "+
        " *.txt *.jpg *.htm *.html "+
        " *.css *.js *.vbs "+
        " *.xsd *.bmp *.ico *.gif *.png "+
        " -rr -r -mdg -m5" );
    writeln(" O.K.");
    writeln("");

    function createArxibName()
    {
        var arxib=DateTime.Now.ToString("yyyy-MM-dd");
        if( !File.Exists(arxib+".rar") )
            return arxib+".rar";
        for( var i=1;i<100;i++ )
        {
            var arxib1=arxib+"+"+i.ToString().PadLeft(2,'0')+".rar";
            if( !File.Exists( arxib1 ) )
                return arxib1;
        }
        throw new Exception("File already exists: "+arxib+".rar.");
    }


}