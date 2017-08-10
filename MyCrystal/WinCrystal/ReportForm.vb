Public Class ReportFrom
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cvwMain As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cbsMain As WinCrystal.CustomerBasic
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents scnNorthwind As System.Data.SqlClient.SqlConnection
    Friend WithEvents sdaCustomers As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents ndsMain As WinCrystal.dsNorthwind
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cvwMain = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.cbsMain = New WinCrystal.CustomerBasic
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.scnNorthwind = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.sdaCustomers = New System.Data.SqlClient.SqlDataAdapter
        Me.ndsMain = New WinCrystal.dsNorthwind
        CType(Me.ndsMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cvwMain
        '
        Me.cvwMain.ActiveViewIndex = -1
        Me.cvwMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvwMain.Location = New System.Drawing.Point(0, 0)
        Me.cvwMain.Name = "cvwMain"
        Me.cvwMain.ReportSource = Nothing
        Me.cvwMain.Size = New System.Drawing.Size(292, 273)
        Me.cvwMain.TabIndex = 0
        '
        'cbsMain
        '
        Me.cbsMain.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.DefaultPaperOrientation
        Me.cbsMain.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        Me.cbsMain.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Upper
        Me.cbsMain.PrintOptions.PrinterDuplex = CrystalDecisions.Shared.PrinterDuplex.Default
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region," & _
        " PostalCode, Country, Phone, Fax FROM Customers"
        Me.SqlSelectCommand1.Connection = Me.scnNorthwind
        '
        'scnNorthwind
        '
        Me.scnNorthwind.ConnectionString = "workstation id=NIKOLAYZL2K;packet size=4096;integrated security=SSPI;data source=" & _
        "NIKOLAYZL2K;persist security info=True;initial catalog=Northwind"
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "INSERT INTO Customers(CustomerID, CompanyName, ContactName, ContactTitle, Address" & _
        ", City, Region, PostalCode, Country, Phone, Fax) VALUES (@CustomerID, @CompanyNa" & _
        "me, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country" & _
        ", @Phone, @Fax); SELECT CustomerID, CompanyName, ContactName, ContactTitle, Addr" & _
        "ess, City, Region, PostalCode, Country, Phone, Fax FROM Customers WHERE (Custome" & _
        "rID = @CustomerID)"
        Me.SqlInsertCommand1.Connection = Me.scnNorthwind
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar, 5, "CustomerID"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 40, "CompanyName"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactName", System.Data.SqlDbType.NVarChar, 30, "ContactName"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactTitle", System.Data.SqlDbType.NVarChar, 30, "ContactTitle"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 60, "Address"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 15, "City"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.NVarChar, 15, "Region"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar, 10, "PostalCode"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 15, "Country"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.NVarChar, 24, "Phone"))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.NVarChar, 24, "Fax"))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "UPDATE Customers SET CustomerID = @CustomerID, CompanyName = @CompanyName, Contac" & _
        "tName = @ContactName, ContactTitle = @ContactTitle, Address = @Address, City = @" & _
        "City, Region = @Region, PostalCode = @PostalCode, Country = @Country, Phone = @P" & _
        "hone, Fax = @Fax WHERE (CustomerID = @Original_CustomerID) AND (Address = @Origi" & _
        "nal_Address OR @Original_Address IS NULL AND Address IS NULL) AND (City = @Origi" & _
        "nal_City OR @Original_City IS NULL AND City IS NULL) AND (CompanyName = @Origina" & _
        "l_CompanyName) AND (ContactName = @Original_ContactName OR @Original_ContactName" & _
        " IS NULL AND ContactName IS NULL) AND (ContactTitle = @Original_ContactTitle OR " & _
        "@Original_ContactTitle IS NULL AND ContactTitle IS NULL) AND (Country = @Origina" & _
        "l_Country OR @Original_Country IS NULL AND Country IS NULL) AND (Fax = @Original" & _
        "_Fax OR @Original_Fax IS NULL AND Fax IS NULL) AND (Phone = @Original_Phone OR @" & _
        "Original_Phone IS NULL AND Phone IS NULL) AND (PostalCode = @Original_PostalCode" & _
        " OR @Original_PostalCode IS NULL AND PostalCode IS NULL) AND (Region = @Original" & _
        "_Region OR @Original_Region IS NULL AND Region IS NULL); SELECT CustomerID, Comp" & _
        "anyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, " & _
        "Phone, Fax FROM Customers WHERE (CustomerID = @CustomerID)"
        Me.SqlUpdateCommand1.Connection = Me.scnNorthwind
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar, 5, "CustomerID"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 40, "CompanyName"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactName", System.Data.SqlDbType.NVarChar, 30, "ContactName"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactTitle", System.Data.SqlDbType.NVarChar, 30, "ContactTitle"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 60, "Address"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 15, "City"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.NVarChar, 15, "Region"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar, 10, "PostalCode"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 15, "Country"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.NVarChar, 24, "Phone"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.NVarChar, 24, "Fax"))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerID", System.Data.SqlDbType.NVarChar, 5, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CustomerID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address", System.Data.SqlDbType.NVarChar, 60, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Address", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_City", System.Data.SqlDbType.NVarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "City", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 40, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CompanyName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ContactName", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ContactName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ContactTitle", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ContactTitle", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Country", System.Data.SqlDbType.NVarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Country", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Fax", System.Data.SqlDbType.NVarChar, 24, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Fax", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Phone", System.Data.SqlDbType.NVarChar, 24, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Phone", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PostalCode", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PostalCode", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Region", System.Data.SqlDbType.NVarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Region", System.Data.DataRowVersion.Original, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM Customers WHERE (CustomerID = @Original_CustomerID) AND (Address = @O" & _
        "riginal_Address OR @Original_Address IS NULL AND Address IS NULL) AND (City = @O" & _
        "riginal_City OR @Original_City IS NULL AND City IS NULL) AND (CompanyName = @Ori" & _
        "ginal_CompanyName) AND (ContactName = @Original_ContactName OR @Original_Contact" & _
        "Name IS NULL AND ContactName IS NULL) AND (ContactTitle = @Original_ContactTitle" & _
        " OR @Original_ContactTitle IS NULL AND ContactTitle IS NULL) AND (Country = @Ori" & _
        "ginal_Country OR @Original_Country IS NULL AND Country IS NULL) AND (Fax = @Orig" & _
        "inal_Fax OR @Original_Fax IS NULL AND Fax IS NULL) AND (Phone = @Original_Phone " & _
        "OR @Original_Phone IS NULL AND Phone IS NULL) AND (PostalCode = @Original_Postal" & _
        "Code OR @Original_PostalCode IS NULL AND PostalCode IS NULL) AND (Region = @Orig" & _
        "inal_Region OR @Original_Region IS NULL AND Region IS NULL)"
        Me.SqlDeleteCommand1.Connection = Me.scnNorthwind
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerID", System.Data.SqlDbType.NVarChar, 5, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CustomerID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address", System.Data.SqlDbType.NVarChar, 60, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Address", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_City", System.Data.SqlDbType.NVarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "City", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 40, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CompanyName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ContactName", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ContactName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ContactTitle", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ContactTitle", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Country", System.Data.SqlDbType.NVarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Country", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Fax", System.Data.SqlDbType.NVarChar, 24, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Fax", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Phone", System.Data.SqlDbType.NVarChar, 24, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Phone", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PostalCode", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PostalCode", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Region", System.Data.SqlDbType.NVarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Region", System.Data.DataRowVersion.Original, Nothing))
        '
        'sdaCustomers
        '
        Me.sdaCustomers.DeleteCommand = Me.SqlDeleteCommand1
        Me.sdaCustomers.InsertCommand = Me.SqlInsertCommand1
        Me.sdaCustomers.SelectCommand = Me.SqlSelectCommand1
        Me.sdaCustomers.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Customers", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("ContactTitle", "ContactTitle"), New System.Data.Common.DataColumnMapping("Address", "Address"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("Region", "Region"), New System.Data.Common.DataColumnMapping("PostalCode", "PostalCode"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("Phone", "Phone"), New System.Data.Common.DataColumnMapping("Fax", "Fax")})})
        Me.sdaCustomers.UpdateCommand = Me.SqlUpdateCommand1
        '
        'ndsMain
        '
        Me.ndsMain.DataSetName = "dsNorthwind"
        Me.ndsMain.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'ReportFrom
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.cvwMain)
        Me.Name = "ReportFrom"
        Me.Text = "Report WinForms Sample"
        CType(Me.ndsMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ReportFrom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '      sdaCustomers.Fill(ndsMain)
        '      cbsMain.SetDataSource(ndsMain)
        '      cvwMain.ReportSource = cbsMain
        Dim scmSPCustomers As New SqlClient.SqlCommand("usp_SelectCustomers", scnNorthwind)
        scmSPCustomers.CommandType = CommandType.StoredProcedure
        scmSPCustomers.Parameters.Add("@sCompany", "A")

        Dim sdaSPCustomers As New SqlClient.SqlDataAdapter(scmSPCustomers)
        Dim dsReport As New DataSet

        sdaSPCustomers.Fill(dsReport, "Customers")
        cbsMain.SetDataSource(dsReport)
        cvwMain.ReportSource = cbsMain

    End Sub

End Class
