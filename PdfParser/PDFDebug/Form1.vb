Public Class Main

    Private Sub btnOpenDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDocument.Click
        Dim ofd As New OpenFileDialog()
        Dim ow() As PDFParser.ObjectWrapper
        Dim sb As New System.Text.StringBuilder()

        ofd.Filter = "PDF|*.pdf"
        ofd.InitialDirectory = System.Environment.GetEnvironmentVariable("%USERPROFILE%") + "\Desktop"

        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ow = PDFParser.Objects.GetAllObjectBlobs( _
                                                    New System.IO.MemoryStream( _
                                                    System.IO.File.ReadAllBytes(ofd.FileName)))
            For Each wrapper As PDFParser.ObjectWrapper In ow
                sb.Append("********************" + wrapper.header + "**************************" + vbCrLf)
                If wrapper.header.Contains("FlateDecode") AndAlso Not wrapper.header.Contains("DecodeParms") Then
                    Try
                        sb.Append(PDFParser.Inflator.FlateDecodeToASCII(New System.IO.MemoryStream(wrapper.bytes)))
                    Catch ex As Exception
                        sb.Append("EXCEPTION: " + ex.Message)
                    End Try
                End If
                sb.Append(vbCrLf)
                sb.Append("************************************************************************" + vbCrLf)
            Next
            txtInflatedContents.Text = sb.ToString()
        End If
    End Sub
End Class
