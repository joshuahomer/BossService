<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BossService.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <strong>
                Boss Service by Joshua Homer
            </strong>
        </div>
    </form>
    <p>
        Is today a Holiday? Use the <strong>/api/IsItHoliday</strong> to find out!
    </p>
    <p>
        Want to pull a random sentence from one of the Lord of the Rings books? Use the <strong>/api/RandomSentences</strong> to get some!
    </p>
    <p>
        <font size="-1">To get back to this page just go to <strong>/Home.aspx</strong></font>
    </p>
</body>
</html>
