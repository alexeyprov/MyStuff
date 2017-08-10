Imports System.Drawing.Imaging
Imports System.IO
Imports WinCityView.net.terraservice

Friend Class CityViewer

    ' Construction/Destruction
    Protected Overrides Sub Finalize()
        If Not (m_bmp Is Nothing) Then
            m_bmp.Dispose()
        End If
    End Sub

    ' Attributes
    Public ReadOnly Property Image() As Image
        Get
            Return m_bmp
        End Get
    End Property

    ' Operations
    Public Function GetCityImage(ByVal strCity As String, ByVal strState As String, ByVal nScale As Integer, _
                                 ByVal cx As Integer, ByVal cy As Integer) As Image
        If Not (m_bmp Is Nothing) Then
            m_bmp.Dispose()
        End If

        m_bmp = GetTiledImage(strCity, strState, CType(CInt(Math.Log(nScale, 2)) + CInt(Scale.Scale1m), Scale), _
                 cx, cy)

        If m_bmp Is Nothing Then
            m_bmp = GetErrorImage("Image not available")
        End If

        Return m_bmp
    End Function

    ' Implementation
    Protected Shared Function GetTiledImage(ByVal strCity As String, ByVal strState As String, _
                                     ByVal scale As Scale, _
                                     ByVal cx As Integer, ByVal cy As Integer) As Bitmap
        Dim bmp As Bitmap
        Dim g As Graphics
        Dim x, y As Integer
        Dim x1, x2, y1, y2 As Integer

        bmp = Nothing
        g = Nothing
        Try
            Dim ts As New TerraService

            'Get the place longitude and lattitude
            Dim p As New Place
            p.City = strCity
            p.State = strState
            p.Country = "USA"

            Dim pt As LonLatPt
            pt = ts.ConvertPlaceToLonLatPt(p)

            'Compute parameters of bounding box
            Dim abb As AreaBoundingBox
            abb = ts.GetAreaFromPt(pt, Theme.Photo, scale, cx, cy)

            bmp = New Bitmap(cx, cy, PixelFormat.Format32bppRgb)
            g = Graphics.FromImage(bmp)

            x1 = abb.NorthWest.TileMeta.Id.X
            x2 = abb.SouthEast.TileMeta.Id.X
            y1 = abb.NorthWest.TileMeta.Id.Y
            y2 = abb.SouthEast.TileMeta.Id.Y

            For x = x1 To x2
                For y = y1 To y2 Step -1
                    Dim tid As TileId
                    Dim stm As MemoryStream
                    tid = abb.NorthWest.TileMeta.Id
                    tid.X = x
                    tid.Y = y

                    stm = New MemoryStream(ts.GetTile(tid))
                    Dim tile As Image
                    tile = System.Drawing.Image.FromStream(stm)

                    g.DrawImage(tile, _
                        (x - x1) * tile.Width - abb.NorthWest.Offset.XOffset, _
                        (y1 - y) * tile.Height - abb.NorthWest.Offset.YOffset, _
                        tile.Width, tile.Height)

                    tile.Dispose()
                Next y
            Next x

            Return bmp
        Catch ex As Exception
            If Not (bmp Is Nothing) Then
                bmp.Dispose()
            End If
            Return Nothing
        Finally
            If Not (g Is Nothing) Then
                g.Dispose()
            End If

        End Try

    End Function

    Protected Shared Function GetErrorImage(ByVal strError As String) As Bitmap
        'Determine witdth and height of bitmap
        Dim bmp As New Bitmap(1, 1, PixelFormat.Format32bppRgb)
        Dim g As Graphics
        Dim cx, cy As Integer

        g = Graphics.FromImage(bmp)
        Dim fnt As New Font("Verdana", 10)
        Dim size As SizeF
        size = g.MeasureString(strError, fnt)

        cx = size.Width
        cy = size.Height

        g.Dispose()
        bmp.Dispose()

        'Draw bitmap
        bmp = New Bitmap(cx, cy, PixelFormat.Format32bppRgb)
        g = Graphics.FromImage(bmp)
        Dim brRed As New SolidBrush(Color.Red)
        Dim brGreen As New SolidBrush(Color.Green)

        g.FillRectangle(brGreen, 0, 0, cx, cy)
        g.DrawString(strError, fnt, brRed, 0, 0)

        brGreen.Dispose()
        brRed.Dispose()
        fnt.Dispose()
        g.Dispose()

        'Return bitmap
        Return bmp
    End Function

    ' Data Members
    Private m_bmp As Bitmap
End Class
