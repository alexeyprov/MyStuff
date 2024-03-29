namespace DynamicMaps {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"DynamicMaps.WorkMessage", typeof(DynamicMaps.WorkMessage))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"DynamicMaps.WorkMessageMapped", typeof(DynamicMaps.WorkMessageMapped))]
    public sealed class Map_B : Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var userCSharp"" version=""1.0"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/WorkMessage"" />
  </xsl:template>
  <xsl:template match=""/WorkMessage"">
    <xsl:variable name=""var:v1"" select=""userCSharp:StringConcat(&quot;Map B&quot;)"" />
    <WorkMessageMapped>
      <WebServiceInfo>
        <Data>
          <xsl:value-of select=""$var:v1"" />
        </Data>
        <xsl:variable name=""var:v2"" select=""userCSharp:MyConcat()"" />
        <Data>
          <xsl:value-of select=""$var:v2"" />
        </Data>
        <xsl:if test=""@MapType"">
          <OrgInfo>
            <xsl:value-of select=""@MapType"" />
          </OrgInfo>
        </xsl:if>
        <MoreData>
          <xsl:value-of select=""SomeData/SomeOtherData/text()"" />
        </MoreData>
      </WebServiceInfo>
    </WorkMessageMapped>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public string StringConcat(string param0)
{
   return param0;
}


///*Uncomment the following code for a sample Inline C# function
//that concatenates two inputs. Change the number of parameters of
//this function to be equal to the number of inputs connected to this functoid.*/

public string MyConcat()
{
throw new System.Exception(""Map Error"");
	return ""E"";
}


]]></msxsl:script>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"DynamicMaps.WorkMessage";
        
        private const string _strTrgSchemasList0 = @"DynamicMaps.WorkMessageMapped";
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"DynamicMaps.WorkMessage";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"DynamicMaps.WorkMessageMapped";
                return _TrgSchemas;
            }
        }
    }
}
