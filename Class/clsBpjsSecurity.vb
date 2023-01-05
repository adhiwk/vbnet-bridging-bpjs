Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Public Class clsBpjsSecurity
    Shared Function Decrypt(ByVal key As String, ByVal data As String) As String
        Dim decData As String = Nothing
        Dim keys As Byte()() = GetHashKeys(key)

        Try
            decData = DecryptStringFromBytes_Aes(data, keys(0), keys(1))
        Catch __unusedCryptographicException1__ As CryptographicException
        Catch __unusedArgumentNullException2__ As ArgumentNullException
        End Try

        Return decData
    End Function

    Shared Function DecryptStringFromBytes_Aes(ByVal cipherTextString As String, ByVal Key As Byte(), ByVal IV As Byte()) As String
        Dim cipherText = Convert.FromBase64String(cipherTextString)
        If cipherText Is Nothing OrElse cipherText.Length <= 0 Then Throw New ArgumentNullException("cipherText")
        If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
        If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")
        Dim plaintext As String = Nothing

        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = Key
            aesAlg.IV = IV
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)

            Using msDecrypt As MemoryStream = New MemoryStream(cipherText)

                Using csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                    Using srDecrypt As StreamReader = New StreamReader(csDecrypt)
                        plaintext = srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using

        Return plaintext
    End Function

    Shared Function GetHashKeys(ByVal key As String) As Byte()()
        Dim result = New Byte(1)() {}
        Dim enc = Encoding.UTF8
        Dim sha2 As SHA256 = New SHA256CryptoServiceProvider()
        Dim rawKey = enc.GetBytes(key)
        Dim rawIV = enc.GetBytes(key)
        Dim hashKey As Byte() = sha2.ComputeHash(rawKey)
        Dim hashIV As Byte() = sha2.ComputeHash(rawIV)
        Array.Resize(hashIV, 16)
        result(0) = hashKey
        result(1) = hashIV
        Return result
    End Function
End Class
