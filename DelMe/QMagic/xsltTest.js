var oArgs = WScript.Arguments;

if (2 != oArgs.length)
{
   WScript.Echo("Usage : cscript xsltTest.js xml xsl");
   WScript.Quit();
}

var xsl = new ActiveXObject("MSXML2.DOMDOCUMENT.4.0");
var xml = new ActiveXObject("MSXML2.DOMDocument.4.0");

xml.validateOnParse = false;
xml.async = false;
xml.load(oArgs(0));

if (xml.parseError.errorCode != 0)
{
	WScript.Echo("XML Parse Error : " + xml.parseError.reason);
	WScript.Quit();
}

xsl.async = false;
xsl.load(oArgs(1));

if (xsl.parseError.errorCode != 0)
{
	WScript.Echo("XSL Parse Error : " + xsl.parseError.reason);
	WScript.Quit();   
}

//WScript.Echo (xml.transformNode(xsl.documentElement));

try
{
	WScript.Echo(xml.transformNode(xsl.documentElement));
}
catch(err)
{
	WScript.Echo ("Transformation Error : " + err.number + "*" + err.description);
}
