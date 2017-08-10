namespace TestNestedSchemas.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://schemas.xmlsoap.org/soap/envelope/",@"Envelope")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"Envelope"})]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"TestNestedSchemas.Schemas.RepriceStack", typeof(TestNestedSchemas.Schemas.RepriceStack))]
    public sealed class InputData : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:ns0=""https://secure.prime-health.net/core/"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" targetNamespace=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:import schemaLocation=""TestNestedSchemas.Schemas.RepriceStack"" namespace=""https://secure.prime-health.net/core/"" />
  <xs:annotation>
    <xs:appinfo>
      <b:references>
        <b:reference targetNamespace=""https://secure.prime-health.net/core/"" />
      </b:references>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""Envelope"">
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""Body"">
          <xs:complexType>
            <xs:sequence>
              <xs:element ref=""ns0:RepriceStack"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public InputData() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "Envelope";
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
