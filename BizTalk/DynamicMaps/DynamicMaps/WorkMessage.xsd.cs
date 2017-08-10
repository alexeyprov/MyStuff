namespace DynamicMaps {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"",@"WorkMessage")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(DynamicMaps.MapType), XPath = @"/*[local-name()='WorkMessage' and namespace-uri()='']/@*[local-name()='MapType' and namespace-uri()='']", XsdType = @"string")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"WorkMessage"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"DynamicMaps.PropertySchema", typeof(DynamicMaps.PropertySchema))]
    public sealed class WorkMessage : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:ns0=""http://SampleMaps.PropertySchema.PropertySchema"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:imports>
        <b:namespace prefix=""ns0"" uri=""http://SampleMaps.PropertySchema.PropertySchema"" location=""DynamicMaps.PropertySchema"" />
      </b:imports>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""WorkMessage"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property name=""ns0:MapType"" xpath=""/*[local-name()='WorkMessage' and namespace-uri()='']/@*[local-name()='MapType' and namespace-uri()='']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""SomeData"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""SomeOtherData"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
      <xs:attribute name=""MapType"" type=""xs:string"" />
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public WorkMessage() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "WorkMessage";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
