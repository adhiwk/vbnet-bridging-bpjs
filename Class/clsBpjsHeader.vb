Imports System.Text
Imports System.Security.Cryptography
Public Class clsBpjsHeader
    Shared Function TimeToUnix(ByVal dteDate As Date) As String
        If dteDate.IsDaylightSavingTime = True Then
            dteDate = DateAdd(DateInterval.Hour, -1, dteDate)
        End If
        TimeToUnix = DateDiff(DateInterval.Second, #1/1/1970#, dteDate)
    End Function

    Shared Function TimeStamp() As String
        Dim xTimeStamp As String
        xTimeStamp = TimeToUnix(DateTime.UtcNow)
        Return xTimeStamp
    End Function

    Shared Function CreateSignature(ByVal cData As String) As String
        Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(My.Settings.bpjs_secretkey.Trim))
        Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(cData.Trim))
        Dim xSignature = Convert.ToBase64String(signature)
        Return xSignature.Trim
    End Function
End Class
