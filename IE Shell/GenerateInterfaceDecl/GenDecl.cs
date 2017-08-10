using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;


namespace GenerateInterfaceDecl
{
	class GenDecl
	{
        const string tlb="shdocvw.dll";
        const string intf=
            //"DWebBrowserEvents2";
            "IWebBrowser2";

        static readonly DateTime start=DateTime.Now;

        static object typelib;
        static string tlbFilePath;
        static Assembly asm;
        static Type intfType;
        static StringWriter dumpWriter;

		[STAThread]
		static void Main(string[] args)
		{
            try
            {
                Console.WriteLine("Generating C# interfaces for "+intf);
                Console.WriteLine("v"+Assembly.GetExecutingAssembly().GetName().Version+" by O.Mihailik");
                Console.WriteLine("");

                Console.Write("Opening "+tlb+"...");
                OpenTlb();
                Console.WriteLine(" O.K. "+tlbFilePath);

                Console.Write("Extracting type information...");
                CreateAssembly();
                Console.WriteLine(" O.K.");

                Console.Write("Looking for "+intf+"...");
                FindInterface();
                Console.WriteLine(" O.K. "+intfType.GUID.ToString("B"));
            
                Console.Write("Dumping "+intf+"...");
                DumpInterface();
                Console.WriteLine(" O.K.");

                Console.Write("Storing C# code...");
                StoreCode();
                Console.WriteLine(" O.K.");

                Console.WriteLine("");
                TimeSpan totalTime=DateTime.Now-start;
                Console.WriteLine("Total time: "+totalTime.TotalSeconds.ToString("0.000")+" sec");
            }
            catch( Exception err )
            {
                Console.WriteLine("");
                Console.WriteLine(err);
            }
		}

        static void OpenTlb()
        {
            tlbFilePath=Path.GetFullPath(tlb);
            if( !File.Exists(tlbFilePath) )
                tlbFilePath=Path.Combine(System.Environment.SystemDirectory,tlb);
            if( !File.Exists(tlbFilePath) )
                tlbFilePath=Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System),tlb);
            if( !File.Exists(tlbFilePath) )
                throw new Exception("Cannot find Type Library "+tlb+".");

            LoadTypeLibEx(
                tlbFilePath,
                RegKind.RegKind_None,
                out typelib );

            if( typelib==null )
                throw new Exception("Type Library loading failed for \""+tlbFilePath+"\".");
        }

        static void CreateAssembly()
        {
            asm=new TypeLibConverter().ConvertTypeLibToAssembly(
                typelib,
                tlb,
                0,
                new NotifySink(),
                null,
                null,
                false );
        }

        class NotifySink : ITypeLibImporterNotifySink
        {
            public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
            {
                if( eventKind==ImporterEventKind.ERROR_REFTOINVALIDTYPELIB )
                    throw new Exception("Reference to invalid TypeLib, "+eventMsg);
            }

            public Assembly ResolveRef(object typeLib)
            {
                return null;
            }
        }

        static void FindInterface()
        {
            foreach( Type t in asm.GetTypes() )
            {
                if( t.Name==intf )
                {
                    intfType=t;
                    return;
                }
            }
        }

        static void DumpInterface()
        {
            dumpWriter=new StringWriter();
            
            writeUsing("System");
            writeUsing("System.ComponentModel");
            writeUsing("System.Collections");
            writeUsing("System.Diagnostics");
            writeUsing("System.Reflection");
            writeUsing("System.Runtime.InteropServices");
            writeUsing("System.Text");

            writeln();

            writeTypeStart(intfType);

            ArrayList allMembers=new ArrayList(
                intfType.GetMembers( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ) );

            ArrayList methods=new ArrayList();

            foreach( MemberInfo member in allMembers.ToArray() )
            {
                if( member is PropertyInfo )
                {
                    PropertyInfo prop=member as PropertyInfo;
                    MethodInfo[] accessors=prop.GetAccessors();
                    if( accessors.Length==1 )
                    {
                        allMembers.Remove(prop);
                        allMembers.Insert( allMembers.IndexOf(accessors[0]), prop );
                        allMembers.Remove(accessors[0]);
                    }
                    else if( allMembers.IndexOf(accessors[1])-allMembers.IndexOf(accessors[0])==1
                        && accessors[0].Name.StartsWith("set") )
                    {
                        allMembers.Remove(prop);
                        allMembers.Insert( allMembers.IndexOf(accessors[0]), prop );
                        allMembers.Remove(accessors[0]);
                        allMembers.Remove(accessors[1]);
                    }
                }
            }

            allMembers.Reverse();

            foreach( MemberInfo member in allMembers )
            {
                writeMember(member);
            }

            writeTypeEnd();
        }

        const int indentStep=4;
        static string indentStr;
        static bool newLine=true;

        static void writeln()
        {
            writeln("");
        }

        static void writeln(string str)
        {
            if( newLine )
                dumpWriter.Write(indentStr);
            dumpWriter.WriteLine(str);
            newLine=true;
        }

        static void write(string str)
        {
            if( newLine )
                dumpWriter.Write(indentStr);
            dumpWriter.Write(str);
            newLine=false;
        }

        static void indent()
        {
            indentStr+=new string(' ',indentStep);
        }

        static void unindent()
        {
            indentStr=indentStr.Remove(0,indentStep);
        }

        static void writeUsing(string ns)
        {
            dumpWriter.WriteLine("using "+ns+";");
        }

        static void writeAttribute(string name)
        {
            writeAttribute(name,null as string[]);
        }

        static void writeAttribute(string name, string prefix)
        {
            writeAttribute(name,null as string[]);
        }


        static void writeAttribute(string name, string param1, params string[] parameters)
        {
            ArrayList pList=new ArrayList();
            pList.Add(param1);
            pList.AddRange(parameters);
            writeAttribute(
                name,
                pList.ToArray(typeof(string)) as string[] );
        }

        static void writeAttribute(string name, string[] parameters)
        {
            if( parameters==null
                || parameters.Length==0 )
                writeln("["+name+"]");
            else
            {
                write("["+name+"(");
                for( int i=0; i<parameters.Length; i++ )
                {
                    if( i>0 )
                        write(",");
                    write(parameters[i]);;
                }
                write(")]");
            }
            writeln();
        }

        static void writeTypeStart(Type t)
        {
            writeAttributes(t);
            if( t.IsInterface )
                writeln("public interface "+t.Name);
            else
                writeln("public class "+t.Name);
            writeln("{");
            indent();
        }

        static void writeTypeEnd()
        {
            unindent();
            writeln("}");
        }

        static void writeMember(MemberInfo m)
        {
            writeAttributes(m);
            if( m is MethodInfo )
                writeMethodDecl(m as MethodInfo);
            else if( m is PropertyInfo )
                writePropertyDecl(m as PropertyInfo);
            else
                writeln(m.ToString());

            writeln();
        }

        static void writeTypeName(Type t)
        {
            if( t.IsByRef )
            {
                write("ref ");
                writeTypeName(t.GetElementType());
                return;
            }

            if( t==typeof(object) )
                write("object");
            else if( t==typeof(int) )
                write("int");
            else if( t==typeof(uint) )
                write("uint");
            else if( t==typeof(long) )
                write("long");
            else if( t==typeof(ulong) )
                write("ulong");
            else if( t==typeof(short) )
                write("short");
            else if( t==typeof(ushort) )
                write("ushort");
            else if( t==typeof(string) )
                write("string");
            else if( t==typeof(byte) )
                write("byte");
            else if( t==typeof(bool) )
                write("bool");
            else if( t==typeof(void) )
                write("void");

//            else if( t.IsEnum )
//                write("int");

            else if( t.Namespace==intfType.Namespace
                || t.Namespace=="System"
                || t.Namespace=="System.Text"
                || t.Namespace=="System.Reflection"
                || t.Namespace=="System.Runtime.InteropServices" )
                write(t.Name);
            else
                write(t.FullName);
        }

        static void writeMethodDecl(MethodInfo m)
        {
            writeTypeName(m.ReturnType);
            write(" ");
            write(m.Name);
            write("(");
            ParameterInfo[] args=m.GetParameters();
            if( args!=null && args.Length>0 )
            {
                writeln();
                indent();
                for( int i=0; i<args.Length; i++ )
                {
                    writeAttributes(args[i].GetCustomAttributes(false),null);
                    if( args[i].IsIn && args[i].IsOut )
                        write("[In,Out] ");
                    else if( args[i].IsIn )
                        write("[In] ");
                    else if( args[i].IsOut )
                        write("[Out] ");
                    writeTypeName(args[i].ParameterType);
                    write(" ");
                    write(args[i].Name);
                    if( i<args.Length-1 )
                        write(",");
                    else
                        write(" );");
                    writeln();
                }
                unindent();
            }
            else
            {
                writeln(");");
            }            
        }

        static void writePropertyDecl(PropertyInfo m)
        {
            writeTypeName(m.PropertyType);
            write(" ");
            write(m.Name);
            write(" {");
            if( m.GetGetMethod()!=null )
                write(" get;");
            if( m.GetSetMethod()!=null )
                write(" set;");
            writeln(" }");            
        }

        static void writeAttributes(object[] attrs, string prefix)
        {
            if( attrs==null || attrs.Length==0 )
                return;

            foreach( Attribute attr in attrs )
            {
                ArrayList pList=new ArrayList();
                foreach( PropertyInfo prop in attr.GetType().GetProperties() )
                {
                    if( prop.DeclaringType!=attr.GetType() )
                        continue;
                    object propValue=prop.GetValue(attr, new object[]{});
                    string propValueStr;
                    
                    if( propValue==null )
                        continue;
                    
                    if( propValue is string )
                        propValueStr="\""+propValue+"\"";
                    else
                        propValueStr=propValue+"";
                    
                    if( prop.Name=="Value" )
                        pList.Insert(0,propValueStr);
                    else
                        pList.Add( prop.Name+"="+propValueStr );
                }
                string attrName=attr.GetType().Name;
                if( attrName.EndsWith("Attribute") )
                    attrName=attrName.Substring(0,attrName.Length-"Attribute".Length);
                writeAttribute(makeAttributePrefix(prefix,attrName),pList.ToArray(typeof(string)) as string[]);
            }
        }

        static string makeAttributePrefix(string prefix, string attrName)
        {
            if( prefix!=null && prefix!="" )
                return prefix+":"+attrName;
            else
                return attrName;
        }

        static void writeAttributes(MemberInfo m)
        {
            writeAttributes(m.GetCustomAttributes(false),null);
        }



        static void StoreCode()
        {
            using( StreamWriter store=new StreamWriter(intf+".cs",false,Encoding.Default) )
            {
                store.WriteLine( dumpWriter.ToString() );
            }
        }

        enum RegKind
        {
            RegKind_Default = 0,
            RegKind_Register = 1,
            RegKind_None = 2
        }

        [DllImport("oleaut32.dll",CharSet=CharSet.Unicode,PreserveSig=false)]
        private static extern void LoadTypeLibEx(
            string strTypeLibName,
            RegKind regKind,
            [MarshalAs(UnmanagedType.Interface)] out object typeLib );
	}
}
