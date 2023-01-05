Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class clsCreateSEP
 private function SaveSep()
            'binding data into data constructor class
            'untuk kontrol silahkan disesuaikan dengan control
            'yang ada pada form desain
            'ini hanya contoh untuk melakukan request ke api bpjs
            Dim addsep As addSEP = New addSEP With
            {
                .request = New addSEPRequest With {
                    .t_sep = New addtsep With {
                        .noKartu = txtNomorKartu.Text.Trim,
                        .tglSep = cTglSep.Trim,
                        .ppkPelayanan = txtKodePPKPelayanan.Text.Trim,
                        .jnsPelayanan = txtKodeJenisPelayanan.Text.Trim,
                            .klsRawat = New addSEPKlsRawat With {
                                .klsRawatHak = txtKodeHakKelas.Text.Trim,
                                .klsRawatNaik = txtKodeNaikKelas.Text.Trim,
                                .pembiayaan = txtKodePembiayaan.Text.Trim,
                                .penanggungJawab = txtPenanggungJawab.Text.Trim
                            },
                        .noMR = txtNoRM.Text.Trim,
                        .rujukan = New addSEPRujukan With {
                            .asalRujukan = txtAsalRujukan.Text.Trim,
                            .tglRujukan = txtTglRujukan.Text.Trim,
                            .noRujukan = txtNomorRujukan.Text.Trim,
                            .ppkRujukan = txtKodePPKRujukan.Text.Trim
                        },
                        .catatan = txtCatatan.Text.Trim,
                        .diagAwal = txtKodeDiagnosa.Text.Trim,
                        .poli = New addSEPPoli With {
                            .tujuan = txtKodePoliTujuan.Text.Trim,
                            .eksekutif = cboPoliEksekutif.Text.Trim
                            },
                        .cob = New addSEPCob With {
                            .cob = cboCOB.Text.Trim
                            },
                        .katarak = New addSEPKatarak With {
                            .katarak = cboKatarak.Text.Trim
                            },
                        .jaminan = New addSEPJaminan With {
                            .lakaLantas = txtLakaLantas.Text.Trim,
                            .noLP = txtNoLp.Text.Trim,
                            .penjamin = New addSEPPenjamin With {
                                .tglKejadian = txtTglKejadian.Text.Trim,
                                .keterangan = txtKeteranganKL.Text.Trim,
                                .suplesi = New addSEPSuplesi With {
                                    .suplesi = cboSuplesi.Text.Trim,
                                    .noSepSuplesi = txtNoSuplesi.Text.Trim,
                                    .lokasiLaka = New addSEPLokasiLaka With {
                                        .kdPropinsi = txtKodePropinsi.Text.Trim,
                                        .kdKabupaten = txtKodeKabupaten.Text.Trim,
                                        .kdKecamatan = txtKodeKecamatan.Text.Trim
                                    }
                                }
                             }
                        },
                        .tujuanKunj = txtTujuanKunjungan.Text.Trim,
                        .flagProcedure = txtFlagProcedure.Text.Trim,
                        .kdPenunjang = txtPenunjang.Text.Trim,
                        .assesmentPel = txtAssementPelayanan.Text.Trim,
                        .skdp = New addSEPSkdp With {
                            .noSurat = txtNomorSuratKontrol.Text.Trim,
                            .kodeDPJP = txtKodeDokterDpjp.Text.Trim
                            },
                        .dpjpLayan = txtDpjpLayanan.Text.Trim,
                        .noTelp = txtNoTelponDpjp.Text.Trim,
                        .user = frmLogin.txtUserId.Text.Trim
                    }
                }
            }

            'deklarasikan variabel untuk request ke server
            Dim cUrl As String = My.Settings.bpjs_baseurl.Trim & "/SEP/2.0/insert"
            Dim cTimeStamp As String = clsBpjsHeader.TimeStamp.Trim
            Dim cData As String = My.Settings.bpjs_consid.Trim & "&" & cTimeStamp.Trim
            Dim result As String = ""
            Dim cRequest As String = JsonConvert.SerializeObject(addsep).Trim
            Dim cResponse As String = ""

            txtResponse.ForeColor = Color.Green
            txtResponse.Text = JToken.Parse(cRequest.Trim).ToString(Formatting.Indented)

            objHttp.DefaultRequestHeaders.Clear()
            objHttp.DefaultRequestHeaders.Add("X-cons-id", My.Settings.bpjs_consid.Trim)
            objHttp.DefaultRequestHeaders.Add("X-Timestamp", cTimeStamp.Trim)
            objHttp.DefaultRequestHeaders.Add("X-Signature", clsBpjsHeader.CreateSignature(cData.Trim))
            objHttp.DefaultRequestHeaders.Add("user_key", My.Settings.bpjs_userkey.Trim)

            '-----------------------------------------------------
            'post dengan sendasync dan httprequestmessage
            '-----------------------------------------------------
            Dim request As HttpRequestMessage = New HttpRequestMessage With {
                    .Content = New StringContent(cRequest.Trim, Encoding.UTF8, "application/x-www-form-urlencoded"),
                    .Method = HttpMethod.Post,
                    .RequestUri = New Uri(cUrl.Trim)
                }

            Dim response As HttpResponseMessage = Await objHttp.SendAsync(request)
            response.EnsureSuccessStatusCode()

            If response.IsSuccessStatusCode Then
                result = Await response.Content.ReadAsStringAsync
            End If

            'read the response as string result 
            If result.Trim <> "" Then
                'deserialize json into data container class
                Dim jsonResponse = JsonConvert.DeserializeObject(Of clsBpjsDataContainer)(result.Trim)

                'if success code = 200
                If jsonResponse.metaData.code.Trim = "200" Then

                    'defince key for descrypt
                    Dim cKey As String = My.Settings.bpjs_consid.Trim &
                                         My.Settings.bpjs_secretkey.Trim &
                                         cTimeStamp.Trim
                    'decrypt respon
                    Dim cDecResp = clsBpjsSecurity.Decrypt(cKey.Trim, jsonResponse.response.Trim)
                    cResponse = clsBpjsLZString.DecompressFromEncodedUriComponent(cDecResp).ToString

                    'simpan sep yang berhasil dibuat kedalam database
                    Dim dtSEP = JsonConvert.DeserializeObject(Of rspSEP)(cResponse.Trim)
                    txtNomorSep.Text = dtSEP.sep.noSep

                    'tampilkan status decrypt ke txtresponse
                    txtResponse.ForeColor = Color.Green
                    txtResponse.Text = JToken.Parse(cResponse.Trim).ToString(Formatting.Indented)

                    If cboTujuanKunjungan.Text.Trim = "0. Normal" Then
                        cJenisKunjungan = "Konsultasi dokter (pertama)"
                    ElseIf cboTujuanKunjungan.Text.Trim = "1. Prosedur" Then
                        cJenisKunjungan = "Procedure"
                    ElseIf cboTujuanKunjungan.Text.Trim = "2. Konsul Dokter" Then
                        cJenisKunjungan = "Konsul dokter lanjutan"
                    End If

                    'save sep kesimrs
                    Dim Conn As SqlConnection = DBConnect.KoneksiSQL
                    Conn.Open()
                    Using Cmd As New SqlCommand()
                        With Cmd
                            .Connection = Conn
                            .CommandText = "add_bpjs_sep"
                            .CommandType = CommandType.StoredProcedure
                            .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                            .Parameters(0).SqlDbType = SqlDbType.NChar
                            .Parameters(0).Direction = ParameterDirection.Output

                            .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                            .Parameters.Add(New SqlParameter("@register_kunjungan", SqlDbType.Char, 17)).Value = cRegisterKunjungan.Trim
                            .Parameters.Add(New SqlParameter("@nomor_rekam_medik", SqlDbType.Char, 17)).Value = txtNoRM.Text.Trim
                            .Parameters.Add(New SqlParameter("@nosep", SqlDbType.Char, 19)).Value = dtSEP.sep.noSep.ToString.Trim
                            .Parameters.Add(New SqlParameter("@tglsep", SqlDbType.Char, 10)).Value = dtSEP.sep.tglSep.ToString.Trim
                            .Parameters.Add(New SqlParameter("@jnspelayanan", SqlDbType.Char, 10)).Value = dtSEP.sep.jnsPelayanan.ToString.Trim
                            .Parameters.Add(New SqlParameter("@kelasrawat", SqlDbType.Char, 10)).Value = dtSEP.sep.kelasRawat.ToString.Trim
                            .Parameters.Add(New SqlParameter("@diagnosa", SqlDbType.Char, 100)).Value = dtSEP.sep.diagnosa.ToString.Trim
                            .Parameters.Add(New SqlParameter("@norujukan", SqlDbType.Char, 19)).Value = txtNomorRujukan.Text.Trim
                            .Parameters.Add(New SqlParameter("@poli", SqlDbType.Char, 50)).Value = dtSEP.sep.poli.ToString.Trim
                            .Parameters.Add(New SqlParameter("@polieksekutif", SqlDbType.Char, 5)).Value = dtSEP.sep.poliEksekutif.ToString.Trim
                            .Parameters.Add(New SqlParameter("@catatan", SqlDbType.Char, 100)).Value = dtSEP.sep.catatan.ToString.Trim
                            .Parameters.Add(New SqlParameter("@penjamin", SqlDbType.Char, 50)).Value = dtSEP.sep.penjamin.ToString.Trim
                            .Parameters.Add(New SqlParameter("@nokartu", SqlDbType.Char, 15)).Value = dtSEP.sep.peserta.noKartu.ToString.Trim
                            .Parameters.Add(New SqlParameter("@nama", SqlDbType.Char, 50)).Value = dtSEP.sep.peserta.nama.ToString.Trim
                            .Parameters.Add(New SqlParameter("@tgllahir", SqlDbType.Char, 10)).Value = dtSEP.sep.peserta.tglLahir.ToString.Trim
                            .Parameters.Add(New SqlParameter("@kelamin", SqlDbType.Char, 10)).Value = dtSEP.sep.peserta.kelamin.ToString.Trim
                            .Parameters.Add(New SqlParameter("@jnspeserta", SqlDbType.Char, 50)).Value = dtSEP.sep.peserta.jnsPeserta.ToString.Trim
                            .Parameters.Add(New SqlParameter("@hakkelas", SqlDbType.Char, 10)).Value = dtSEP.sep.peserta.hakKelas.ToString.Trim
                            .Parameters.Add(New SqlParameter("@asuransi", SqlDbType.Char, 30)).Value = dtSEP.sep.peserta.asuransi.ToString.Trim
                            .Parameters.Add(New SqlParameter("@notelpon", SqlDbType.Char, 15)).Value = txtNoTelponDpjp.Text.Trim
                            .Parameters.Add(New SqlParameter("@dokter", SqlDbType.Char, 50)).Value = txtDokterDpjp.Text.Trim
                            .Parameters.Add(New SqlParameter("@faskesperujuk", SqlDbType.Char, 50)).Value = txtNamaPPKRujukan.Text.Trim
                            .Parameters.Add(New SqlParameter("@jeniskunjungan", SqlDbType.Char, 50)).Value = cJenisKunjungan.Trim
                            .Parameters.Add(New SqlParameter("@adduser", SqlDbType.Char, 15)).Value = frmLogin.txtUserId.Text.Trim
                            .ExecuteNonQuery()

                            If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                                DisableText()
                            Else
                                MsgBox(.Parameters("@mERROR_MESSAGE").Value.ToString.Trim)
                                Exit Sub
                            End If
                        End With
                    End Using
                    Conn.Close()
                    MsgBox("SEP berhasil dibuat", MsgBoxStyle.Information, "Sukses")
                    btnBatal.Enabled = False
                    btnSimpan.Enabled = False
                    btnCariRujukan.Enabled = False
                    btnCreateSuratKontrol.Enabled = False
                    btnBaru.Enabled = False
                    DisableText()
                Else
                    'show message error and bind into text response
                    MsgBox(jsonResponse.metaData.message.Trim, MsgBoxStyle.Critical, "Error " & jsonResponse.metaData.code.Trim)
                    txtResponse.Text = JToken.Parse(result.Trim).ToString(Formatting.Indented)
                    txtResponse.ForeColor = Color.Red
                End If
            Else
                MsgBox(result.ToString.Trim, MsgBoxStyle.Critical, "Error")
                txtResponse.Text = result.ToString.Trim
                txtResponse.ForeColor = Color.Red
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        Return True
 End function
End Class