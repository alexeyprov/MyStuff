Public Class DigitsOnly
    Inherits System.Windows.Forms.TextBox

    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = Not Char.IsDigit(e.KeyChar)
    End Sub
End Class
