
Imports System.Net.Http
Imports System.Text

Public Class clsBpjsWebRequest
    Shared Async Function GetDataAsync(ByVal url As String,
                                       ByVal _httpclient As HttpClient) As Task(Of String)
        Dim result As String = ""
        Try
            Dim response As HttpResponseMessage = Await _httpclient.GetAsync(url)
            response.EnsureSuccessStatusCode()
            If response.IsSuccessStatusCode Then
                result = Await response.Content.ReadAsStringAsync
            End If
        Catch ex As HttpRequestException
            result = "{" & vbCrLf &
                         Chr(34) & "metaData" & Chr(34) & ":{" & vbCrLf &
                         Chr(34) & "code" & Chr(34) & ":" & Chr(34) & "400" & Chr(34) & "," & vbCrLf &
                         Chr(34) & "message" & Chr(34) & ":" & Chr(34) & ex.Message.Trim & Chr(34) & vbCrLf &
                         "}," & vbCrLf &
                         Chr(34) & "response" & Chr(34) & ":" & "null" & "}"

        End Try
        Return result.Trim
    End Function

    Shared Async Function CreateAsync(ByVal _httpclient As HttpClient,
                                      ByVal cUrl As String,
                                      ByVal cJson As String) As Task(Of String)
        Dim result As String = ""
        Try

            Dim request As HttpRequestMessage = New HttpRequestMessage With {
                .Content = New StringContent(cJson.Trim, Encoding.UTF8, "application/x-www-form-urlencoded"),
                .Method = HttpMethod.Post,
                .RequestUri = New Uri(cUrl.Trim)
         }
            Dim response As HttpResponseMessage = Await _httpclient.SendAsync(request)
            response.EnsureSuccessStatusCode()
            If response.IsSuccessStatusCode Then
                result = Await response.Content.ReadAsStringAsync
            End If
        Catch ex As HttpRequestException
            result = "{" & vbCrLf &
                     Chr(34) & "metaData" & Chr(34) & ":{" & vbCrLf &
                     Chr(34) & "code" & Chr(34) & ":" & Chr(34) & "400" & Chr(34) & "," & vbCrLf &
                     Chr(34) & "message" & Chr(34) & ":" & Chr(34) & ex.Message.Trim & Chr(34) & vbCrLf &
                     "}," & vbCrLf &
                     Chr(34) & "response" & Chr(34) & ":" & "null" & "}"
        End Try
        Return result.Trim
    End Function

    Shared Async Function CreateRawAsync(ByVal _httpclient As HttpClient,
                                      ByVal cUrl As String,
                                      ByVal objData As String) As Task(Of String)
        Dim resultJson As String = ""
        Try

            Dim postData As StringContent = New StringContent(objData.Trim, Encoding.UTF8, "application/x-www-form-urlencoded")
            Using result As HttpResponseMessage = Await _httpclient.PostAsync(cUrl.Trim, postData)
                resultJson = Await result.Content.ReadAsStringAsync
            End Using
        Catch ex As HttpRequestException
            resultJson = "{" & vbCrLf &
                     Chr(34) & "metaData" & Chr(34) & ":{" & vbCrLf &
                     Chr(34) & "code" & Chr(34) & ":" & Chr(34) & "400" & Chr(34) & "," & vbCrLf &
                     Chr(34) & "message" & Chr(34) & ":" & Chr(34) & ex.Message.Trim & Chr(34) & vbCrLf &
                     "}," & vbCrLf &
                     Chr(34) & "response" & Chr(34) & ":" & "null" & "}"
        End Try
        Return resultJson.Trim
    End Function

    Shared Async Function CreateKeyAsync(ByVal curl As String,
                                         ByVal _httpclient As HttpClient) As Task(Of String)
        Dim result As String = ""

        Dim sep = New List(Of KeyValuePair(Of String, String))()
        sep.Add(New KeyValuePair(Of String, String)("user", "slackware"))
        sep.Add(New KeyValuePair(Of String, String)("password", "rahasia123"))

        Dim req = New HttpRequestMessage(HttpMethod.Post, curl.Trim) With {
            .Content = New FormUrlEncodedContent(sep)
        }

        Dim resp As HttpResponseMessage = Await _httpclient.SendAsync(req)
        resp.EnsureSuccessStatusCode()
        If resp.IsSuccessStatusCode Then
            result = Await resp.Content.ReadAsStringAsync
        End If
        Return result.Trim
    End Function
    Shared Async Function UpdateAsync(ByVal cJson As String,
                                      ByVal cUrl As String,
                                      ByVal _httpclient As HttpClient) As Task(Of String)
        Dim result As String = ""
        Try

            Dim request As HttpRequestMessage = New HttpRequestMessage With {
                .Content = New StringContent(cJson.Trim, Encoding.UTF8, "application/x-www-form-urlencoded"),
                .Method = HttpMethod.Put,
                .RequestUri = New Uri(cUrl.Trim)
         }
            Dim response As HttpResponseMessage = Await _httpclient.SendAsync(request)
            response.EnsureSuccessStatusCode()
            If response.IsSuccessStatusCode Then
                result = Await response.Content.ReadAsStringAsync
            End If
        Catch ex As HttpRequestException
            result = "{" & vbCrLf &
                     Chr(34) & "metaData" & Chr(34) & ":{" & vbCrLf &
                     Chr(34) & "code" & Chr(34) & ":" & Chr(34) & "400" & Chr(34) & "," & vbCrLf &
                     Chr(34) & "message" & Chr(34) & ":" & Chr(34) & ex.Message.Trim & Chr(34) & vbCrLf &
                     "}," & vbCrLf &
                     Chr(34) & "response" & Chr(34) & ":" & "null" & "}"
        End Try
        Return result.Trim
    End Function

    Shared Async Function UpdateRawAsync(ByVal _httpclient As HttpClient,
                                      ByVal cUrl As String,
                                      ByVal objData As String) As Task(Of String)
        Dim resultJson As String = ""
        Try
            Dim postData As StringContent = New StringContent(objData.Trim, Encoding.UTF8, "application/x-www-form-urlencoded")
            Using result As HttpResponseMessage = Await _httpclient.PutAsync(cUrl.Trim, postData)
                resultJson = Await result.Content.ReadAsStringAsync
            End Using
        Catch ex As HttpRequestException
            resultJson = "{" & vbCrLf &
                     Chr(34) & "metaData" & Chr(34) & ":{" & vbCrLf &
                     Chr(34) & "code" & Chr(34) & ":" & Chr(34) & "400" & Chr(34) & "," & vbCrLf &
                     Chr(34) & "message" & Chr(34) & ":" & Chr(34) & ex.Message.Trim & Chr(34) & vbCrLf &
                     "}," & vbCrLf &
                     Chr(34) & "response" & Chr(34) & ":" & "null" & "}"
        End Try
        Return resultJson.Trim
    End Function

    Shared Async Function DeleteAsync(ByVal cJson As String,
                                      ByVal cUrl As String,
                                      ByVal _httpclient As HttpClient) As Task(Of String)
        Dim result As String = ""
        Try

            Dim request As HttpRequestMessage = New HttpRequestMessage With {
                .Content = New StringContent(cJson.Trim, Encoding.UTF8, "application/x-www-form-urlencoded"),
                .Method = HttpMethod.Delete,
                .RequestUri = New Uri(cUrl.Trim)
            }

            Dim response As HttpResponseMessage = Await _httpclient.SendAsync(request)
            response.EnsureSuccessStatusCode()
            If response.IsSuccessStatusCode Then
                result = Await response.Content.ReadAsStringAsync
            End If

        Catch ex As HttpRequestException
            result = "{" & vbCrLf &
                     Chr(34) & "metaData" & Chr(34) & ":{" & vbCrLf &
                     Chr(34) & "code" & Chr(34) & ":" & Chr(34) & "400" & Chr(34) & "," & vbCrLf &
                     Chr(34) & "message" & Chr(34) & ":" & Chr(34) & ex.Message.Trim & Chr(34) & vbCrLf &
                     "}," & vbCrLf &
                     Chr(34) & "response" & Chr(34) & ":" & "null" & "}"
        End Try
        Return result.Trim
    End Function
End Class

