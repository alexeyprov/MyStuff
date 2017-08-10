namespace TestNestedSchemas.Pipelines
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class CustomReceivePipeline : Microsoft.BizTalk.PipelineOM.ReceivePipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>f66b9f5e-43ff-4f5f-ba46-885348ae1b4e</CategoryId>  <FriendlyName>Receive</FriendlyName>"+
"  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Decode\" minOccurs=\""+
"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4103-4cce-4536-83fa-4a5040674ad6\" />      <Component"+
"s />    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"2\" Name=\"Disassemble\" "+
"minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"FirstMatch\" stageId=\"9d0e4105-4cce-4536-83fa-4a5040674ad6\" "+
"/>      <Components>        <Component>          <Name>Microsoft.BizTalk.Component.XmlDasmComp,Micro"+
"soft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35<"+
"/Name>          <ComponentName>XML disassembler</ComponentName>          <Description>Streaming XML "+
"disassembler</Description>          <Version>1.0</Version>          <Properties>            <Propert"+
"y Name=\"EnvelopeSpecNames\">              <Value xsi:type=\"xsd:string\">BTS.soap_envelope_1__1+Envelop"+
"e, Microsoft.BizTalk.GlobalPropertySchemas, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf385"+
"6ad364e35</Value>            </Property>            <Property Name=\"EnvelopeSpecTargetNamespaces\">  "+
"            <Value xsi:type=\"xsd:string\">http://schemas.xmlsoap.org/soap/envelope/</Value>          "+
"  </Property>            <Property Name=\"DocumentSpecNames\">              <Value xsi:type=\"xsd:strin"+
"g\">TestNestedSchemas.Schemas.RepriceStack</Value>            </Property>            <Property Name=\""+
"DocumentSpecTargetNamespaces\">              <Value xsi:type=\"xsd:string\">https://secure.prime-health"+
".net/core/</Value>            </Property>            <Property Name=\"AllowUnrecognizedMessage\">     "+
"         <Value xsi:type=\"xsd:boolean\">false</Value>            </Property>            <Property Nam"+
"e=\"ValidateDocument\">              <Value xsi:type=\"xsd:boolean\">false</Value>            </Property"+
">            <Property Name=\"RecoverableInterchangeProcessing\">              <Value xsi:type=\"xsd:bo"+
"olean\">false</Value>            </Property>            <Property Name=\"HiddenProperties\">           "+
"   <Value xsi:type=\"xsd:string\">EnvelopeSpecTargetNamespaces,DocumentSpecTargetNamespaces</Value>   "+
"         </Property>          </Properties>          <CachedDisplayName>XML disassembler</CachedDisp"+
"layName>          <CachedIsManaged>true</CachedIsManaged>        </Component>      </Components>    "+
"</Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"3\" Name=\"Validate\" minOccurs=\""+
"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e410d-4cce-4536-83fa-4a5040674ad6\" />      <Component"+
"s>        <Component>          <Name>Microsoft.BizTalk.Component.XmlValidator,Microsoft.BizTalk.Pipe"+
"line.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</Name>          <"+
"ComponentName>XML validator</ComponentName>          <Description>XML validator component.</Descript"+
"ion>          <Version>1.0</Version>          <Properties>            <Property Name=\"DocumentSpecNa"+
"me\">              <Value xsi:type=\"xsd:string\">TestNestedSchemas.Schemas.RepriceStack</Value>       "+
"     </Property>            <Property Name=\"DocumentSpecTargetNamespaces\">              <Value xsi:t"+
"ype=\"xsd:string\">https://secure.prime-health.net/core/</Value>            </Property>            <Pr"+
"operty Name=\"HiddenProperties\">              <Value xsi:type=\"xsd:string\">DocumentSpecTargetNamespac"+
"es</Value>            </Property>            <Property Name=\"RecoverableInterchangeProcessing\">     "+
"         <Value xsi:type=\"xsd:boolean\">false</Value>            </Property>          </Properties>  "+
"        <CachedDisplayName>XML validator</CachedDisplayName>          <CachedIsManaged>true</CachedI"+
"sManaged>        </Component>      </Components>    </Stage>    <Stage>      <PolicyFileStage _locAt"+
"trData=\"Name\" _locID=\"4\" Name=\"ResolveParty\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\""+
"9d0e410e-4cce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>  </Stages></Document>";
        
        private const string _versionDependentGuid = "55e63ba0-4872-4957-84f9-68916724b03f";
        
        public CustomReceivePipeline()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4105-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.firstRecognized);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Microsoft.BizTalk.Component.XmlDasmComp,Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"EnvelopeSpecNam"+
"es\">      <Value xsi:type=\"xsd:string\">BTS.soap_envelope_1__1+Envelope, Microsoft.BizTalk.GlobalProp"+
"ertySchemas, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</Value>    </Property"+
">    <Property Name=\"EnvelopeSpecTargetNamespaces\">      <Value xsi:type=\"xsd:string\">http://schemas"+
".xmlsoap.org/soap/envelope/</Value>    </Property>    <Property Name=\"DocumentSpecNames\">      <Valu"+
"e xsi:type=\"xsd:string\">TestNestedSchemas.Schemas.RepriceStack</Value>    </Property>    <Property N"+
"ame=\"DocumentSpecTargetNamespaces\">      <Value xsi:type=\"xsd:string\">https://secure.prime-health.ne"+
"t/core/</Value>    </Property>    <Property Name=\"AllowUnrecognizedMessage\">      <Value xsi:type=\"x"+
"sd:boolean\">false</Value>    </Property>    <Property Name=\"ValidateDocument\">      <Value xsi:type="+
"\"xsd:boolean\">false</Value>    </Property>    <Property Name=\"RecoverableInterchangeProcessing\">    "+
"  <Value xsi:type=\"xsd:boolean\">false</Value>    </Property>    <Property Name=\"HiddenProperties\">  "+
"    <Value xsi:type=\"xsd:string\">EnvelopeSpecTargetNamespaces,DocumentSpecTargetNamespaces</Value>  "+
"  </Property>  </Properties></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
            stage = this.AddStage(new System.Guid("9d0e410d-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp1 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("Microsoft.BizTalk.Component.XmlValidator,Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");;
            if (comp1 is IPersistPropertyBag)
            {
                string comp1XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties>    <Property Name=\"DocumentSpecNam"+
"e\">      <Value xsi:type=\"xsd:string\">TestNestedSchemas.Schemas.RepriceStack</Value>    </Property> "+
"   <Property Name=\"DocumentSpecTargetNamespaces\">      <Value xsi:type=\"xsd:string\">https://secure.p"+
"rime-health.net/core/</Value>    </Property>    <Property Name=\"HiddenProperties\">      <Value xsi:t"+
"ype=\"xsd:string\">DocumentSpecTargetNamespaces</Value>    </Property>    <Property Name=\"RecoverableI"+
"nterchangeProcessing\">      <Value xsi:type=\"xsd:boolean\">false</Value>    </Property>  </Properties"+
"></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp1XmlProperties);;
                ((IPersistPropertyBag)(comp1)).Load(pb, 0);
            }
            this.AddComponent(stage, comp1);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}
