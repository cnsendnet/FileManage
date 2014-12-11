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
            DDLBind();//���ļ�����ʱ��
            AllGVBind();//�����ļ���Ϣ
            this.ddlUD.Items.Insert(0, "��ѡ��...");   
        }
    }
    //���ļ�����ʱ��
    protected void DDLBind()
    {
        //�������ݿ������
        SqlConnection myConn = CC.GetConnection() ;
        myConn.Open();
        //��ѯ�ļ�����ʱ��
        SqlDataAdapter dapt=new SqlDataAdapter("select distinct fileUpDate from tb_files", myConn);
        DataSet ds=new DataSet();
        //������ݼ�
        dapt.Fill(ds, "files");
        //�������˵�
        this.ddlUD.DataSource = ds.Tables["files"].DefaultView;
        this.ddlUD.DataTextField = ds.Tables["files"].Columns[0].ToString(); 
        this.ddlUD.DataBind();
        //�ͷ�ռ�õ���Դ
        ds.Dispose();
        dapt.Dispose();
        myConn.Close();
       
    }
    //�������ļ���Ϣ
    protected void AllGVBind()
    {
        //�������ݿ������
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        //��ѯ�ַ���
        SqlDataAdapter dapt = new SqlDataAdapter("select * from tb_files", myConn);
        DataSet ds = new DataSet();
        //������ݼ�
        dapt.Fill(ds, "files");
        //�����ݿؼ�
        this.gvFiles.DataSource = ds.Tables["files"].DefaultView;
        this.gvFiles.DataKeyNames = new string[] {"fileID"};
        this.DataBind();
        //�ͷ�ռ�õ���Դ
        ds.Dispose();
        dapt.Dispose();
        myConn.Close();
    }
    //�����������ļ���Ϣ
    protected void PartGVBind()
    {
        //�������ݿ������
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        //��ѯ���������������ַ���
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
        //������ݼ�
        dapt.Fill(ds, "files");
        //�����ݿؼ�
        this.gvFiles.DataSource = ds.Tables["files"].DefaultView;
        this.gvFiles.DataKeyNames = new string[] { "fileID" };
        this.DataBind();
        //�ͷ�ռ�õ���Դ
        ds.Dispose();
        dapt.Dispose();
        myConn.Close();
    }
    public static int IntIsSearch;//�ж��Ƿ��ѵ����������ť
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
            //����ƶ���GridView�ؼ���������ʱ�������Զ����ָ����ɫ
            e.Row.Attributes.Add("onmouseover","this.style.backgroundColor='#BEC9F6';this.style.color='buttontext';this.style.cursor='default';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';this.style.color=''");
            //˫���д���ҳ
            e.Row.Attributes.Add("ondblclick", "window.open('FileInfo.aspx?id="+e.Row.Cells[0].Text+"')");
           
        }
    }
    //���ļ���Files�£�ɾ��ָ�����ļ�
    protected void DeleteTFN(string sqlStr)
    {
        //�����ݿ�
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
        DataSet ds = new DataSet();
        dapt.Fill(ds, "files");
        //��ȡָ���ļ���·��
        string strFilePath = Server.MapPath("Files/") + ds.Tables["files"].Rows[0][0].ToString();
        //����File���Delete������ɾ��ָ���ļ�
        File.Delete(strFilePath);
        ds.Dispose();
        myConn.Close();
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        //��ȡimgbtnDelete��ImageButton����
        ImageButton imgbtn = (ImageButton)sender;
        //����imgbtnDelete�ؼ��ĸ��ؼ���һ���ؼ�
        GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
        //��ȡ�ļ���ʵ����
        string sqlStr = "select fileTrueName from tb_files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
        //���ļ���Files�£�ɾ�����ļ�
        DeleteTFN(sqlStr);
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        //�����ݿ���ɾ�����ļ���Ϣ
        string sqlDelStr = "delete from tb_files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
        SqlCommand myCmd = new SqlCommand(sqlDelStr, myConn);
        myCmd.ExecuteNonQuery();
        myCmd.Dispose();
        myConn.Close();
        //���°�
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
        //��ȡimgbtnDelete��ImageButton����
        ImageButton imgbtn = (ImageButton)sender;
        //����imgbtnDelete�ؼ��ĸ��ؼ���һ���ؼ�
        GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
        //��ȡ�ļ���ʵ����
        string sqlStr = "select fileTrueName from tb_files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
        //�����ݿ�
        SqlConnection myConn = CC.GetConnection();
        myConn.Open();
        SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
        DataSet ds = new DataSet();
        dapt.Fill(ds, "files");
        //��ȡ�ļ�·��
        string strFilePath = Server.MapPath("Files//" + ds.Tables["files"].Rows[0][0].ToString());
        ds.Dispose();
        myConn.Close();
        ////����ָ�����ļ�
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
        //����ָ�����ļ�
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
