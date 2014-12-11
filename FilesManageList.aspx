<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FilesManageList.aspx.cs" Inherits="FilesManageList"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文件管理</title>
    <style >
    body:{margin-top:0px}
        .style1
        {
            height: 78px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table  width =100%  align =center background="images/文件上传.jpg" style="width: 1003px; height: 658px"  >
      <tr>
       <td style="height: 105px">
           &nbsp;</td>
      </tr>
          <tr>
              <td align="center" style="height: 45px" valign="top">
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp;&nbsp; &nbsp;<asp:HyperLink ID="hpLinkDefault" runat="server" NavigateUrl="~/Default.aspx" ImageUrl="~/images/文件上传1.jpg">首页</asp:HyperLink>
                  &nbsp; &nbsp;
                  <asp:HyperLink ID="hpLinkUpF" runat="server" NavigateUrl="~/FileUp.aspx" ImageUrl="~/images/文件上传2.jpg">上传文件</asp:HyperLink></td>
          </tr>
            <tr>
                <td align =center style="height: 35px"  valign =top  >
                    文件管理</td>
            </tr>
            <tr>
                <td valign =top class="style1"  >
                <table  width =100% style ="font-size:9pt" cellpadding ="0px" cellspacing ="0px">
                <tr>
                <td align=right style="width: 455px" >
                    文件名称关键字：</td>
                <td align =left>
                    <asp:TextBox ID="txtFilesName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td align =right style=" width: 455px;" >
                    创建时间：</td>
                <td align =left >
                    <asp:DropDownList ID="ddlUD" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
                </tr>
                    <tr>
                        <td style="width: 455px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 455px; ">
                        </td>
                         <td align="left" >
                             <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
                             </td>
                    </tr>
                </table>
                </td>
               
            </tr>
            <tr>
                <td width =100% align =center style="font-size :9pt; height: 395px;"  valign =top >
        <asp:GridView ID="gvFiles" runat="server" Width =700px AllowPaging="True" AutoGenerateColumns="False" PageSize="5" OnPageIndexChanging="gvFiles_PageIndexChanging" OnRowDataBound="gvFiles_RowDataBound"  DataKeyNames ="filesID" Height="155px">
            <Columns>
                <asp:BoundField DataField="fileID" HeaderText="文件编号">
                    <ControlStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="fileName" HeaderText="文件名称">
                    <ControlStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="fileUpDate" HeaderText="创建时间">
                    <ControlStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="永久删除">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/删除.gif" OnClick="imgbtnDelete_Click"/>
                </ItemTemplate>
                    <ControlStyle Font-Size="Small" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="文件下载">
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl ="~/images/文件下载.gif" OnClick="imgbtnDF_Click"/>   
                </ItemTemplate>
                    <ControlStyle Font-Size="Small" />
                </asp:TemplateField>
              
            </Columns>
        </asp:GridView>
                </td>
            </tr>
         
        </table>
    
    </div>
    </form>
</body>
</html>
