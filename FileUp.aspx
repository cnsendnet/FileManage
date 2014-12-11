<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUp.aspx.cs" Inherits="FileUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传文件</title>
    <style >
    body:{margin-top:0px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table background="images/文件上传.jpg" style="width: 1003px; height: 658px">
            <tr>
                <td align =center valign =bottom style="height: 133px"  >
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp;
                  <asp:HyperLink ID="hpLinkDefault" runat="server" NavigateUrl="~/Default.aspx" ImageUrl="~/images/文件上传1.jpg">首页</asp:HyperLink>
                    &nbsp; &nbsp; &nbsp;&nbsp;<asp:HyperLink ID="hpLinkFM" runat="server" NavigateUrl="~/FilesManageList.aspx" ImageUrl="~/images/文件管理3.jpg">文件管理</asp:HyperLink></td>
            </tr>
        <tr>
        <td align =center valign =bottom style="height: 42px">
            上传文件</td>
        </tr>
            <tr>
                <td align="center" valign="top" rowspan="2" style="height: 483px">
                 <table id="tabFU" runat ="server" enableviewstate ="true"  cellpadding ="0"  cellspacing ="0">
                        <tr>
                             <td >
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            
                             </td>
                        </tr>
                     </table>
                   <asp:Button ID="btnUp" runat="server" Text="上传所有文件" OnClick="btnUp_Click" Width="94px"  /> 
                 <asp:Button ID="btnAddFU" runat="server" Text="增加上载文件" OnClick="btnAddFU_Click" Width="96px" />  
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </form>
</body>
</html>
