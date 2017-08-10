namespace TestNestedSchemas.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"https://secure.prime-health.net/core/",@"RepriceStack")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"RepriceStack"})]
    public sealed class RepriceStack : Microsoft.XLANGs.BaseTypes.SchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns:tns=""https://secure.prime-health.net/core/"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" targetNamespace=""https://secure.prime-health.net/core/"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""RepriceStack"">
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""stack"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""testmode"" type=""xs:boolean"" />
              <xs:element name=""clientid"" type=""xs:string"" />
              <xs:element name=""siteid"" type=""xs:string"" />
              <xs:element name=""subclientid"" type=""xs:string"" />
              <xs:element minOccurs=""0"" name=""parentid"" type=""xs:string"" />
              <xs:element name=""clientpassword"" type=""xs:string"" />
              <xs:element name=""resultcode"" type=""xs:unsignedByte"" />
              <xs:element minOccurs=""0"" name=""result"" type=""xs:string"" />
              <xs:element name=""bills"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""bill"">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name=""formtype"" type=""xs:string"" />
                          <xs:element name=""billid"" type=""xs:string"" />
                          <xs:element name=""claimnumber"" type=""xs:string"" />
                          <xs:element minOccurs=""0"" name=""reconsiderationof"" type=""xs:string"" />
                          <xs:element name=""dateofservice"" type=""xs:dateTime"" />
                          <xs:element minOccurs=""0"" name=""providerid"" type=""xs:string"" />
                          <xs:element minOccurs=""0"" name=""providereob"" type=""xs:string"" />
                          <xs:element name=""productcode"" type=""xs:string"" />
                          <xs:element name=""patient"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name=""ssn"" type=""xs:anyType"" />
                                <xs:element name=""dob"" type=""xs:dateTime"" />
                                <xs:element name=""firstname"" type=""xs:string"" />
                                <xs:element name=""lastname"" type=""xs:string"" />
                                <xs:element name=""mi"" type=""xs:string"" />
                                <xs:element name=""address1"" type=""xs:string"" />
                                <xs:element name=""address2"" type=""xs:string"" />
                                <xs:element name=""city"" type=""xs:string"" />
                                <xs:element name=""state"" type=""xs:string"" />
                                <xs:element name=""zip"" type=""xs:string"" />
                                <xs:element minOccurs=""0"" name=""gender"" type=""xs:string"" />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                          <xs:element name=""provider"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name=""taxid"" type=""xs:unsignedInt"" />
                                <xs:element name=""facilityname"" type=""xs:string"" />
                                <xs:element name=""firstname"" type=""xs:string"" />
                                <xs:element name=""lastname"" type=""xs:string"" />
                                <xs:element name=""mi"" type=""xs:string"" />
                                <xs:element name=""degree"" type=""xs:string"" />
                                <xs:element name=""address1"" type=""xs:string"" />
                                <xs:element name=""address2"" type=""xs:string"" />
                                <xs:element name=""city"" type=""xs:string"" />
                                <xs:element name=""state"" type=""xs:string"" />
                                <xs:element name=""zip"" type=""xs:string"" />
                                <xs:element name=""specialty"" type=""xs:string"" />
                                <xs:element name=""servicezip"" type=""xs:string"" />
                                <xs:element minOccurs=""0"" name=""phone"" type=""xs:string"" />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                          <xs:element name=""lineitems"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element maxOccurs=""unbounded"" name=""lineitem"">
                                  <xs:complexType>
                                    <xs:sequence>
                                      <xs:element name=""servicefrom"" type=""xs:dateTime"" />
                                      <xs:element name=""serviceto"" type=""xs:dateTime"" />
                                      <xs:element name=""code"" type=""xs:string"" />
                                      <xs:element name=""modifier1"" type=""xs:string"" />
                                      <xs:element name=""modifier2"" type=""xs:string"" />
                                      <xs:element name=""units"" type=""xs:unsignedByte"" />
                                      <xs:element name=""pos"" type=""xs:unsignedByte"" />
                                      <xs:element name=""tos"" type=""xs:unsignedByte"" />
                                      <xs:element name=""linecharge"" type=""xs:decimal"" />
                                      <xs:element name=""statesavings"" type=""xs:decimal"" />
                                      <xs:element name=""stateallowed"" type=""xs:decimal"" />
                                      <xs:element name=""pposavings"" type=""xs:decimal"" />
                                      <xs:element name=""othersavings"" type=""xs:decimal"" />
                                      <xs:element name=""repricedlineamount"" type=""xs:decimal"" />
                                      <xs:element name=""repricedlinesavings"" type=""xs:decimal"" />
                                      <xs:element name=""resultcode"" type=""xs:unsignedByte"" />
                                      <xs:element minOccurs=""0"" name=""reasoncode"" type=""xs:string"" />
                                      <xs:element minOccurs=""0"" name=""LineSeq"" type=""xs:string"" />
                                    </xs:sequence>
                                  </xs:complexType>
                                </xs:element>
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                          <xs:element name=""diagnosiscodes"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element maxOccurs=""unbounded"" name=""string"" type=""xs:string"" />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                          <xs:element minOccurs=""0"" name=""procedurecodes"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element maxOccurs=""unbounded"" name=""string"" type=""xs:string"" />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                          <xs:element name=""totalbilledcharges"" type=""xs:decimal"" />
                          <xs:element name=""billallowance"" type=""xs:decimal"" />
                          <xs:element name=""pposavings"" type=""xs:decimal"" />
                          <xs:element name=""othersavings"" type=""xs:decimal"" />
                          <xs:element name=""stateucrsavings"" type=""xs:decimal"" />
                          <xs:element name=""totalsavings"" type=""xs:decimal"" />
                          <xs:element name=""resultCode"" type=""xs:unsignedByte"" />
                          <xs:element minOccurs=""0"" name=""result"" type=""xs:string"" />
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element name=""ignoreexhibits"" type=""xs:boolean"" />
              <xs:element name=""allowpendedbills"" type=""xs:boolean"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public RepriceStack() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "RepriceStack";
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
