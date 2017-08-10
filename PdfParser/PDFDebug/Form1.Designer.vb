<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnOpenDocument = New System.Windows.Forms.Button
        Me.txtInflatedContents = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'btnOpenDocument
        '
        Me.btnOpenDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenDocument.Location = New System.Drawing.Point(642, 522)
        Me.btnOpenDocument.Name = "btnOpenDocument"
        Me.btnOpenDocument.Size = New System.Drawing.Size(105, 23)
        Me.btnOpenDocument.TabIndex = 0
        Me.btnOpenDocument.Text = "Open Document"
        Me.btnOpenDocument.UseVisualStyleBackColor = True
        '
        'txtInflatedContents
        '
        Me.txtInflatedContents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInflatedContents.Location = New System.Drawing.Point(12, 12)
        Me.txtInflatedContents.Name = "txtInflatedContents"
        Me.txtInflatedContents.Size = New System.Drawing.Size(735, 504)
        Me.txtInflatedContents.TabIndex = 1
        Me.txtInflatedContents.Text = ""
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 557)
        Me.Controls.Add(Me.txtInflatedContents)
        Me.Controls.Add(Me.btnOpenDocument)
        Me.Name = "Main"
        Me.Text = "Document Test Form"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOpenDocument As System.Windows.Forms.Button
    Friend WithEvents txtInflatedContents As System.Windows.Forms.RichTextBox

End Class
