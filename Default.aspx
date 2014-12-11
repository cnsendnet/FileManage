<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文件上传与管理</title>
    <style >
    body:{margin-top:0px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table background="images/文件上传与管理首页面.jpg" style="width: 1003px; height: 658px">
            <tr>
                <td style="width: 470px; height: 246px">
                </td>
                <td style="height: 246px">
                </td>
            </tr>
            <tr>
                <td style="width: 470px;height: 55px " valign="top">
                </td>
                <td style="height: 55px" rowspan="" valign="top">
                    <asp:ImageButton ID="ibtnFileUp" runat="server" ImageUrl="~/images/文件上传按钮.jpg"  PostBackUrl="~/FileUp.aspx" />
      </td>
            </tr>
             <tr>
                <td style="width: 470px;height: 324px">
                </td>
                <td valign="top" style ="height: 324px">
                <asp:ImageButton ID="ibtnFileManage" runat="server" ImageUrl="~/images/文件管理按钮.jpg"  PostBackUrl="~/FilesManageList.aspx" />
        </td>
            </tr>
        </table>
      <br />
        <br />
        &nbsp;<br />
        <br />
    </div>   
    </form>
</body>
</html>
