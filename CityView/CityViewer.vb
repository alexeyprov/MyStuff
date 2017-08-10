Imports System.Web
Imports CityView.TS.TerraService
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class CityViewer
    Implements IHttpHandler

    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        Dim strCity As String, strState As String, strScale As String
        strCity = context.Request.Item("City")
        strState = context.Request.Item("State")
        strScale = context.Request.Item("Scale")
        If strCity <> vbNullString And strState <> vbNullString Then
            Dim res As Scale
            res = Scale.Scale8m
            If strScale <> vbNullString Then
                Select Case strScale
                    Case "1"
                        res = Scale.Scale1m
                    Case "2"
                        res = Scale.Scale2m
                    Case "4"
                        res = Scale.Scale4m
                    Case "8"
                        res = Scale.Scale8m
                    Case "16"
                        res = Scale.Scale16m
                    Case "32"
                        res = Scale.Scale32m
                End Select
            End If

            'Generate image
            Dim strType As String
            Dim fmt As ImageFormat

            strType = "image/jpeg"
            fmt = ImageFormat.Jpeg
            Dim bmp As Bitmap
            bmp = GetTiledImage(strCity, strState, res, 600, 400)

            If bmp Is Nothing Then
                bmp = GetErrorImage("Image not available")
                strType = "image/gif"
                fmt = ImageFormat.Gif
            End If

            context.Response.ContentType = strType
            bmp.Save(context.Response.OutputStream, fmt)
            bmp.Dispose()
        End If

    End Sub

    Protected Function GetTiledImage(ByVal strCity As String, ByVal strState As String, _
                                     ByVal scale As TS.TerraService.Scale, _
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
            p.Country = "US"

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
                    tile = Image.FromStream(stm)

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

    Protected Function GetErrorImage(ByVal strError As String) As Bitmap
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
        Dim brWhite As New SolidBrush(Color.White)

        g.FillRectangle(brWhite, 0, 0, cx, cy)
        g.DrawString(strError, fnt, brRed, 0, 0)

        brWhite.Dispose()
        brRed.Dispose()
        fnt.Dispose()
        g.Dispose()

        'Return bitmap
        Return bmp
    End Function

End Class
