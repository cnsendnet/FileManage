using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
public partial class FilesManageList : System.Web.UI.Page
{
    CommonClass CC = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DDLBind();//绑定文件创建时间
            AllGVBind();//绑定所文件信息
            this.ddlUD.Items.Insert(0, "请选择...");   
        }
    }
    //绑定文件创建时间
    protected void DDLBind()
    {
        //打开与数据库的连接
        SqlConnection myConn = CC.GetConnection() ;
        myConn.Open();
        //查询文件创建时间
        SqlDataAdapter dapt=new SqlDataAdapter("select distinct fileUpDate from tb_files", myConn);
        DataSet ds=new DataSet();
        //填充数据集
        dapt.Fill(ds, "files");
        //绑定下拉菜单
        this.ddlUD.DataSource = ds.Tables["files"].DefaultView;
        this.ddlUD.DataTextField = ds.Tables["files"].Columns[0].ToString(); 
        this.ddlUD.DataBind();
        //释放占用的资源
        ds.Dispose();
        dapt.Dispose();
        myConn.Close();
       
    }
    //绑定所有文件信息
    protected void AllGVBind()
    {
        //打开与数据库的连接
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        //查询字符串
        SqlDataAdapter dapt = new SqlDataAdapter("select * from tb_files", myConn);
        DataSet ds = new DataSet();
        //填充数据集
        dapt.Fill(ds, "files");
        //绑定数据控件
        this.gvFiles.DataSource = ds.Tables["files"].DefaultView;
        this.gvFiles.DataKeyNames = new string[] {"fileID"};
        this.DataBind();
        //释放占用的资源
        ds.Dispose();
        dapt.Dispose();
        myConn.Close();
    }
    //绑定搜索到的文件信息
    protected void PartGVBind()
    {
        //打开与数据库的连接
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        //查询符合搜索条件的字符串
        string sqlStr = "select * from tb_files";
        if (this.txtFilesName.Text.Trim() != "" || ddlUD.SelectedIndex !=0)
        {
            sqlStr += " where ";
            if (this.txtFilesName.Text.Trim() != "" && ddlUD.SelectedIndex == 0)
            {
                sqlStr += "fileName like'%" + this.txtFilesName.Text.Trim() + "%'";
            }
            else if (this.txtFilesName.Text.Trim() == "" && ddlUD.SelectedIndex != 0)
            {
                sqlStr += "fileUpDate= '"+this.ddlUD.SelectedValue.ToString()+"'";
            }
            else
            {
                sqlStr += "fileUpDate='" + this.ddlUD.SelectedValue.ToString() + "'";
                sqlStr += "  and fileName like'%" + this.txtFilesName.Text.Trim() + "%'";
            }
        }
        SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
        DataSet ds = new DataSet();
        //填充数据集
        dapt.Fill(ds, "files");
        //绑定数据控件
        this.gvFiles.DataSource = ds.Tables["files"].DefaultView;
        this.gvFiles.DataKeyNames = new string[] { "fileID" };
        this.DataBind();
        //释放占用的资源
        ds.Dispose();
        dapt.Dispose();
        myConn.Close();
    }
    public static int IntIsSearch;//判断是否已点击了搜索按钮
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PartGVBind();
        IntIsSearch = 1;
    }
    protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    { 
        this.gvFiles.PageIndex = e.NewPageIndex;
        if (IntIsSearch == 1)
        {
            PartGVBind();
        }
        else
        {
            AllGVBind();
        }      
    }
    protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标移动到GridView控件的任意行时，该行自动变成指定颜色
            e.Row.Attributes.Add("onmouseover","this.style.backgroundColor='#BEC9F6';this.style.color='buttontext';this.style.cursor='default';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';this.style.color=''");
            //双击行打开新页
            e.Row.Attributes.Add("ondblclick", "window.open('FileInfo.aspx?id="+e.Row.Cells[0].Text+"')");
           
        }
    }
    //在文件夹Files下，删除指定的文件
    protected void DeleteTFN(string sqlStr)
    {
        //打开数据库
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
        DataSet ds = new DataSet();
        dapt.Fill(ds, "files");
        //获取指定文件的路径
        string strFilePath = Server.MapPath("Files/") + ds.Tables["files"].Rows[0][0].ToString();
        //调用File类的Delete方法，删除指定文件
        File.Delete(strFilePath);
        ds.Dispose();
        myConn.Close();
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        //获取imgbtnDelete的ImageButton对象
        ImageButton imgbtn = (ImageButton)sender;
        //引用imgbtnDelete控件的父控件上一级控件
        GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
        //获取文件真实姓名
        string sqlStr = "select fileTrueName from tb_files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
        //在文件夹Files下，删除该文件
        DeleteTFN(sqlStr);
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        //从数据库中删除该文件信息
        string sqlDelStr = "delete from tb_files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
        SqlCommand myCmd = new SqlCommand(sqlDelStr, myConn);
        myCmd.ExecuteNonQuery();
        myCmd.Dispose();
        myConn.Close();
        //重新绑定
        if (IntIsSearch == 1)
        {
            PartGVBind();
        }
        else
        {
            AllGVBind();
        }      
    }
    protected void imgbtnDF_Click(object sender, ImageClickEventArgs e)
    {
        //获取imgbtnDelete的ImageButton对象
        ImageButton imgbtn = (ImageButton)sender;
        //引用imgbtnDelete控件的父控件上一级控件
        GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
        //获取文件真实姓名
        string sqlStr = "select fileTrueName from tb_files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
        //打开数据库
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
        DataSet ds = new DataSet();
        dapt.Fill(ds, "files");
        //获取文件路径
        string strFilePath = Server.MapPath("Files//" + ds.Tables["files"].Rows[0][0].ToString());
        ds.Dispose();
        myConn.Close();
        ////下载指定的文件
        //if (File.Exists(strFilePath))
        //{
        //    Response.Clear();
        //    Response.ClearHeaders();
        //    Response.Buffer = false   ;
        //    Response.ContentType = "application/octet-stream";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFilePath, System.Text.Encoding.UTF8));
        //    Response.AppendHeader("Content-Length", strFilePath.Length.ToString());
        //    Response.WriteFile(strFilePath);
        //    Response.Flush();
        //    Response.End();
        //}
        //下载指定的文件
        if (File.Exists(strFilePath))
        {
            System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
            Response.AppendHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(file.FullName);
            Response.Flush();
            Response.End();
        }

    }

}
