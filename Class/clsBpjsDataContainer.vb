'------------------------------------
' public data container
'------------------------------------
Public Class clsBpjsDataContainer
    Public Property metaData As MetaData
    Public Property response As String
End Class

Public Class MetaData
    Public Property code As String
    Public Property message As String
End Class

'------------------------------------
' Container referensi diagnosa
'------------------------------------
Public Class lstDiagnosa
    Public Property diagnosa As List(Of RefDiagnosa)
End Class

Public Class RefDiagnosa
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
' container referensi poli
'------------------------------------
Public Class lstPoli
    Public Property poli As List(Of RefPoli)
End Class
Public Class RefPoli
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
'container referensi faskes
'------------------------------------
Public Class lstFaskes
    Public Property faskes As List(Of RefFaskes)
End Class
Public Class RefFaskes
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
'container referensi dokter dpjp
'------------------------------------
Public Class lstDokterDPJP
    Public Property list As List(Of refDokterDpjp)
End Class
Public Class refDokterDpjp
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
' container referensi propinsi
'------------------------------------
Public Class lstRefPropinsi
    Public Property list As List(Of RefPropinsi)
End Class
Public Class RefPropinsi
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
' container referensi kabupaten
'------------------------------------
Public Class lstKabupaten
    Public Property list As List(Of refKabupaten)
End Class
Public Class refKabupaten
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
'container referensi kecamatan
'------------------------------------
Public Class lstKecamatan
    Public Property list As List(Of refKecamatan)
End Class
Public Class refKecamatan
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
'container referensi diagnosa PRB
'------------------------------------
Public Class lstDiagnosaPRB
    Public Property list As List(Of refDiagnosaPRB)
End Class
Public Class refDiagnosaPRB
    Public Property kode As String
    Public Property nama As String
End Class

'------------------------------------
'container referensi obat generik prb
'------------------------------------
Public Class lstObatGenerikPRB
    Public Property list As List(Of refObatGenerikPRB)
End Class
Public Class refObatGenerikPRB
    Public Property kode As String
    Public Property nama As String
End Class

'-------------------------------------
'container insert SEP
'-------------------------------------
Public Class addSEP
    Public Property request As addSEPRequest
End Class

Public Class addSEPRequest
    Public Property t_sep As addtsep
End Class
Public Class addtsep
    Public Property noKartu As String
    Public Property tglSep As String
    Public Property ppkPelayanan As String
    Public Property jnsPelayanan As String
    Public Property klsRawat As addSEPKlsRawat
    Public Property noMR As String
    Public Property rujukan As addSEPRujukan
    Public Property catatan As String
    Public Property diagAwal As String
    Public Property poli As addSEPPoli
    Public Property cob As addSEPCob
    Public Property katarak As addSEPKatarak
    Public Property jaminan As addSEPJaminan
    Public Property tujuanKunj As String
    Public Property flagProcedure As String
    Public Property kdPenunjang As String
    Public Property assesmentPel As String
    Public Property skdp As addSEPSkdp
    Public Property dpjpLayan As String
    Public Property noTelp As String
    Public Property user As String
End Class
Public Class addSEPKlsRawat
    Public Property klsRawatHak As String
    Public Property klsRawatNaik As String
    Public Property pembiayaan As String
    Public Property penanggungJawab As String
End Class
Public Class addSEPRujukan
    Public Property asalRujukan As String
    Public Property tglRujukan As String
    Public Property noRujukan As String
    Public Property ppkRujukan As String
End Class
Public Class addSEPPoli
    Public Property tujuan As String
    Public Property eksekutif As String
End Class
Public Class addSEPCob
    Public Property cob As String
End Class
Public Class addSEPKatarak
    Public Property katarak As String
End Class
Public Class addSEPJaminan
    Public Property lakaLantas As String
    Public Property noLP As String
    Public Property penjamin As addSEPPenjamin
End Class
Public Class addSEPPenjamin
    Public Property tglKejadian As String
    Public Property keterangan As String
    Public Property suplesi As addSEPSuplesi
End Class
Public Class addSEPSuplesi
    Public Property suplesi As String
    Public Property noSepSuplesi As String
    Public Property lokasiLaka As addSEPLokasiLaka
End Class
Public Class addSEPLokasiLaka
    Public Property kdPropinsi As String
    Public Property kdKabupaten As String
    Public Property kdKecamatan As String
End Class
Public Class addSEPSkdp
    Public Property noSurat As String
    Public Property kodeDPJP As String
End Class

'------------------------------------
'container response insert SEP
'------------------------------------
Public Class rspSEP
    Public Property sep As SepAddRsp
End Class
Public Class SepAddRsp
    Public Property catatan As String
    Public Property diagnosa As String
    Public Property jnsPelayanan As String
    Public Property kelasRawat As String
    Public Property noSep As String
    Public Property penjamin As String
    Public Property peserta As PesertaSepAddRsp
    Public Property informasi As InformasiSepAddRsp
    Public Property poli As String
    Public Property poliEksekutif As String
    Public Property tglSep As String
End Class
Public Class PesertaSepAddRsp
    Public Property asuransi As String
    Public Property hakKelas As String
    Public Property jnsPeserta As String
    Public Property kelamin As String
    Public Property nama As String
    Public Property noKartu As String
    Public Property noMr As String
    Public Property tglLahir As String
End Class
Public Class InformasiSepAddRsp
    Public Property Dinsos As Object
    Public Property prolanisPRB As Object
    Public Property noSKTM As Object
End Class
