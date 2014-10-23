<%@ Page Language="VB" AutoEventWireup="false" Inherits="Songs.Songs_Frameset" CodeBehind="Frameset.aspx.vb" %>

<html>
<head>
    <title>Gestion des chants</title>
</head>
<body style="border: 0; margin: 0; overflow: hidden;">
    <iframe style="width: 100%; height: 100%;" src="UpdateSong.aspx?<%=Request.QueryString%>"></iframe>
</body>
</html>
